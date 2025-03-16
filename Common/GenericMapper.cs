using System.Collections;

namespace Common;
using System.Reflection;

public static class GenericMapper
{

    public static IList<TDestination>? MapList<TCollection, TSourceUnderlying, TDestination>(TCollection? source) where TDestination : class, new () {
        Type? underlyingType = source?.GetType().GetGenericArguments()[0];
        Type? genericType = source?.GetType();
        if (source != null
            && genericType?.GetInterfaces().Contains(typeof(IList)) == true
            && underlyingType == typeof(TSourceUnderlying)) {
            if (source is IList<TSourceUnderlying> mapData) {
                IList<TDestination> mappedList = new List<TDestination>(mapData.Select(Map<TSourceUnderlying, TDestination>).ToList()!);
                return mappedList;
            }
        }
        return null;
    }
    public static TDestination? Map<TSource, TDestination>(TSource? source) where TDestination : class, new()
    {
        if (source == null)
        {
            return null;
        }

        TDestination destination = new();

        PropertyInfo[] sourceProperties = typeof(TSource).GetProperties();
        PropertyInfo[] destinationProperties = typeof(TDestination).GetProperties();

        foreach (PropertyInfo sourceProperty in sourceProperties)
        {
            foreach (PropertyInfo destinationProperty in destinationProperties)
            {
                if (sourceProperty.Name == destinationProperty.Name &&
                    sourceProperty.PropertyType == destinationProperty.PropertyType &&
                    destinationProperty.CanWrite)
                {
                    object? value = sourceProperty.GetValue(source);
                    destinationProperty.GetSetMethod()?.Invoke(destination, new object[] { value });
                    break;
                }
            }
        }
        return destination;
    }
}