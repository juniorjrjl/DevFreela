namespace DevFreela.UnitTests.Utils;

public static class ObjectUtils
{
    

    public static void SetValueOnPrivateProperty<T, P>(this T target, string propertyName, P Value) where T : class
    {
        var propInfo = typeof(T).GetProperty(propertyName);
        ArgumentNullException.ThrowIfNull(propInfo);
        propInfo.SetValue(target, Value);
    }

}