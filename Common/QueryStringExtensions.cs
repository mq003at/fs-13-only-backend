using System.Reflection;

public static class QueryStringExtensions
{
    public static T ParseParams<T>(this IQueryCollection query)
        where T : class, new()
    {
        var filter = new T();

        foreach (var param in query)
        {
            var property = typeof(T).GetProperty(
                param.Key,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
            );

            if (property == null)
            {
                continue;
            }

            var value = Convert.ChangeType(param.Value.First(), property.PropertyType);
            property.SetValue(filter, value);
        }

        return filter;
    }
}
