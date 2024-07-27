using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DynamicExpression.ModelBinders.Const;

/// <summary>
/// Constants
/// </summary>
internal static class Constants 
{
    /// <summary>
    /// Json Serializer Settings.
    /// </summary>
    internal static readonly JsonSerializerSettings jsonSerializerSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        PreserveReferencesHandling = PreserveReferencesHandling.None,
        Converters =
        {
            new StringEnumConverter()
        }
    };
}