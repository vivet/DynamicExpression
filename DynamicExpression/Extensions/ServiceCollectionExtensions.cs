using System;
using DynamicExpression.Interfaces;
using DynamicExpression.ModelBinders;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicExpression.Extensions;

/// <summary>
/// Service Collection Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds <see cref="IQuery"/> and <see cref="IQueryCriteria"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddQueryModelBinders(this IServiceCollection services)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        services
            .AddMvc(x =>
            {
                var queryModelBinderProvider = new QueryModelBinderProvider();

                x.ModelBinderProviders
                    .Add(queryModelBinderProvider);
            });

        return services;
    }
}