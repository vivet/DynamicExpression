using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DynamicExpression.Entities;
using DynamicExpression.Enums;
using DynamicExpression.Interfaces;
using DynamicExpression.ModelBinders.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DynamicExpression.ModelBinders;

/// <inheritdoc />
public class QueryModelBinder : IModelBinder
{
    /// <summary>
    /// Json Serializer Settings.
    /// </summary>
    protected static readonly JsonSerializerSettings jsonSerializerSettings = new()
    {
        MaxDepth = 128,
        Culture = CultureInfo.CurrentCulture,
        NullValueHandling = NullValueHandling.Ignore,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        PreserveReferencesHandling = PreserveReferencesHandling.None,
        Converters =
        {
            new StringEnumConverter()
        }
    }; 
   
    /// <inheritdoc />
    public virtual async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        var request = bindingContext.ActionContext.HttpContext.Request;

        var requestBody = await request.Body
            .ReadAllAsync();

        var model = string.IsNullOrEmpty(requestBody) 
            ? new Query()
            : JsonConvert.DeserializeObject<Query>(requestBody, QueryModelBinder.jsonSerializerSettings);

        var pagingCount = this.GetPaginationCount(request);
        var pagingNumber = this.GetPaginationNumber(request);
        var pagingSkip = this.GetPaginationSkip(request);
        var orderingBy = this.GetOrderingBy(request);
        var orderingDirection = this.GetOrderingDirection(request);

        model.Paging.Count = pagingCount ?? model.Paging.Count;
        model.Paging.Number = pagingNumber ?? model.Paging.Number;
        model.Paging.Skip = pagingSkip ?? model.Paging.Skip;
        model.Order.By = orderingBy ?? model.Order.By ?? "Id";
        model.Order.Direction = orderingDirection ?? model.Order.Direction;

        bindingContext.Result = ModelBindingResult.Success(model);
    }

    /// <summary>
    /// Get Ordering By.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequest"/></param>
    /// <returns>The ordering by.</returns>
    protected string GetOrderingBy(HttpRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var orderBy = request.Query["Order.By"].FirstOrDefault();

        return orderBy;
    }

    /// <summary>
    /// Get Ordering Direction.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequest"/></param>
    /// <returns>The ordering direction.</returns>
    protected OrderingDirection? GetOrderingDirection(HttpRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var success = Enum.TryParse<OrderingDirection>(request.Query["Order.Direction"].FirstOrDefault(), true, out var direction);
        if (!success)
        {
            return null;
        }

        return direction;
    }

    /// <summary>
    /// Get Pagination Count.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequest"/></param>
    /// <returns>The pagination count</returns>
    protected int? GetPaginationCount(HttpRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var success = int.TryParse(request.Query["Paging.Count"].FirstOrDefault(), out var count);
        if (!success)
        {
            return null;
        }

        return count;
    }

    /// <summary>
    /// Get Pagination Number.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequest"/></param>
    /// <returns>The pagination number.</returns>
    protected int? GetPaginationNumber(HttpRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var success = int.TryParse(request.Query["Paging.Number"].FirstOrDefault(), out var number);
        if (!success)
        {
            return null;
        }

        return number;
    }

    /// <summary>
    /// Get Pagination Skip.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequest"/></param>
    /// <returns>The pagination skip.</returns>
    protected int? GetPaginationSkip(HttpRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var success = int.TryParse(request.Query["Paging.Skip"].FirstOrDefault(), out var skip);
        if (!success)
        {
            return null;
        }

        return skip;
    }
}

/// <inheritdoc />
public class QueryModelBinder<TCriteria> : QueryModelBinder
    where TCriteria : class, IQueryCriteria, new()
{
    /// <inheritdoc />
    public override async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        var request = bindingContext.ActionContext.HttpContext.Request;

        var requestBody = await request.Body
            .ReadAllAsync();

        var model = string.IsNullOrEmpty(requestBody)
            ? new Query<TCriteria>()
            : JsonConvert.DeserializeObject<Query<TCriteria>>(requestBody, QueryModelBinder.jsonSerializerSettings);

        var pagingCount = this.GetPaginationCount(request);
        var pagingNumber = this.GetPaginationNumber(request);
        var pagingSkip = this.GetPaginationSkip(request);
        var orderingBy = this.GetOrderingBy(request);
        var orderingDirection = this.GetOrderingDirection(request);

        model.Paging.Count = pagingCount ?? model.Paging.Count;
        model.Paging.Number = pagingNumber ?? model.Paging.Number;
        model.Paging.Skip = pagingSkip ?? model.Paging.Skip;
        model.Order.By = orderingBy ?? model.Order.By ?? "Id";
        model.Order.Direction = orderingDirection ?? model.Order.Direction;

        model.Criteria = this.GetCriteria(request) ?? model.Criteria;

        bindingContext.Result = ModelBindingResult.Success(model);
    }

    /// <summary>
    /// Returns the Criteria of type <typeparamref name="TCriteria"/>, from the <see cref="HttpRequest.Query"/>.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequest"/>.</param>
    /// <returns>The Criteria of type <typeparamref name="TCriteria"/>.</returns>
    protected virtual TCriteria GetCriteria(HttpRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var criteria = new TCriteria();

        typeof(TCriteria)
            .GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance)
            .ToList()
            .ForEach(x =>
            {
                var success = request.Query.TryGetValue(x.Name, out var values);
                if (!success)
                {
                    return;
                }

                var value = values.FirstOrDefault();
                if (value == null)
                {
                    return;
                }

                if (x.PropertyType == typeof(TimeSpan) || x.PropertyType == typeof(TimeSpan?))
                {
                    x.SetValue(criteria, TimeSpan.Parse(value));
                }
                else if (x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?))
                {
                    x.SetValue(criteria, DateTime.Parse(value));
                }
                else if (x.PropertyType == typeof(DateTimeOffset) || x.PropertyType == typeof(DateTimeOffset?))
                {
                    x.SetValue(criteria, DateTimeOffset.Parse(value));
                }
                else if (x.PropertyType == typeof(Guid) || x.PropertyType == typeof(Guid?))
                {
                    x.SetValue(criteria, Guid.Parse(value));
                }
                else
                {
                    x.SetValue(criteria, value);
                }
            });

        return criteria;
    }
}