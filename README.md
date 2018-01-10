# Dynamic Expression v1.0.4
[![Build Status](https://travis-ci.org/vivet/DynamicExpression.svg?branch=master)](https://travis-ci.org/vivet/DynamicExpression)
[![NuGet](https://img.shields.io/nuget/dt/DynamicExpression.svg)](https://www.nuget.org/packages/DynamicExpression/)

Construct lambda expressions dynamically, and turn criteria models into Linq queries.  
Feel free to contribute, throw questions and report issues. I usually respond fast (24-48 hours).  
  
### Introduction
Simply implement the interface: ```IQueryCriteria```, and fluently build the expression, relating properties of the ```IQueryCriteria``` implementation to a model or entity. Just follow the two steps in the example below.

##### 1. Implementing Query Criteria model.
```csharp
public class MyQueryCriteria : IQueryCriteria
{
    public string MyCriteria { get; set; }
    
    public virtual CriteriaExpression GetExpression<T>() where TEntity : class
    {
        var expression = new CriteriaExpression();
        expression.Equal("MyProperty", this.MyCriteria);

        return expression;
    }
}
```
  
##### 2. Convert to Linq Expression.
Convert the instance of 'MyQueryCriteria' to a valid Linq Expression, by invoking the method 'GetExpression<T>().
```csharp
var criteria = new MyQueryCriteria();
var result = myQueryable.Where(criteria) 
```
