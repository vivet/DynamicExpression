## Dynamic Expression v1.0.1
Construct lambda expressions dynamically, and turn criteria models into Linq queries.  
Feel free to contribute, throw questions and report issues. I usually respond fast (24-48 hours).  
#### Nuget: https://www.nuget.org/packages/GoogleApi (netstandard1.1, net4.5, portable-net45+win8+wpa81).
  
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
