# Dynamic Expression
[![Build Status](https://travis-ci.org/vivet/DynamicExpression.svg?branch=master)](https://travis-ci.org/vivet/DynamicExpression)
[![NuGet](https://img.shields.io/nuget/dt/DynamicExpression.svg)](https://www.nuget.org/packages/DynamicExpression/)
[![NuGet](https://img.shields.io/nuget/v/DynamicExpression.svg)](https://www.nuget.org/packages/DynamicExpression/)

Easyly, construct lambda expressions dynamically, and turn criteria models into Linq queries. Additionally the library implements a query object model, defining the criteria (optional) and properties for pagination and ordering, to use directly in querying methods.  

Feel free to contribute, throw questions and report issues. I usually respond fast (24-48 hours).  

##### Table of Contents:
* [Query](#query)
* [Query Criteria](#query-criteria)
* [Criteria Expression Operations](#criteria-expression-operations)
* [Linq Extensions](#linq-extensions)

#### Query
The query object model has a generic and a non-generic implementations.  
The ```Query``` is used when no filtering is required, but pagination and ordering is still needed, while the ```Query<TCriteria>``` is used when custom filter expressions should be applied.  

#### Query Criteria
The query criteria derives from the interface ```IQueryCriteria```, and implements a single method ```GetExpression<TModel>()```, where ```TModel``` defines the model type of the linq statement the critera expression is converted into. Additionally the query critiera implementation contains properties for each wanted criteria. In the method body of ```GetExpression<TModel>()``` build the ```CriteriaExpression```, defining logical operations and mapping model and criteria properties.  

Simple example.
```csharp
public class MyModel
{
    public string MyProperty { get; set; }
}

public class MyQueryCriteria : IQueryCriteria
{
    public string MyCriteria { get; set; }
    
    public virtual CriteriaExpression GetExpression<TModel>() 
        where TEntity : class
    {
        var expression = new CriteriaExpression();
        
        expression
            .Equal("MyProperty", this.MyCriteria);

        return expression;
    }
}
```
  
Another example, combining two criteria properties to one model property.
```csharp
public class MyModel
{
    public DateTime CreatedAt { get; set; }
}

public class MyQueryCriteria : IQueryCriteria
{
    public DateTimeOffset? AfterAt { get; set; }
    public DateTimeOffset? BeforeAt { get; set; }

    public override CriteriaExpression GetExpression() 
        where TEntity : class
    {
        var expression = base.GetExpression();

        if (this.BeforeAt.HasValue)
        {
            expression.LessThanOrEqual("CreatedAt", this.BeforeAt);
        }
        
        if (this.AfterAt.HasValue)
        {    
            expression.GreaterThanOrEqual("CreatedAt", this.AfterAt);
        }
        
        return expression;
    }
}
```

#### Criteria Expression Operations
When constructing the ```CriteriaExpression``` the following methods are available.  
Note, that not all operations are valid on all data types, but it should be apparent.
* Equal
* NotEqual
* Contains
* Not Contains
* StartsWith
* EndsWith
* GreaterThan
* GreaterThanOrEqual
* LessThan
* LessThanOrEqual
* Between
* IsNull
* IsNotNull
* IsNullOrWhiteSpace
* IsNotNullOrWhiteSpace
* IsEmpty
* IsNotEmpty

#### Linq Extensions
The library comes with a few ```IQueryable<T>``` extension methods. They serve to convert the ```CriteriaExpression``` returned by the ```GetExpression<TModel>()```, into a valid linq expression.  

The list below shows the extension methods
* ```IQueryable<T> Order<T>(IOrdering)```
* ```IQueryable<T> Limit<T>(IPagination)```
* ```IQueryable<T> Where<T>(IQueryCriteria)```
  
Using the ```IQueryCriteria``` implementation from above, applying the criteria expression would look like this.
```csharp
var criteria = new MyQueryCriteria();

var result = myQueryable
    .Where(criteria) 
```
The other extensions methods applies pagination and ordering to the query, ```IQueryable<T>```.
