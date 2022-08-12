using System.Text.RegularExpressions;

namespace QueryGenerator;

public class WrapperTest
{
    public static string Identifier { get; set; } = "\"";
    public static string ColumnAsKeyword { get; set; } = "AS ";
    public static string MyWrap(string value)
    {
        if (value.ToLowerInvariant().Contains(" as "))
        {
            var index = value.ToLowerInvariant().IndexOf(" as ");
            var before = value.Substring(0, index);
            var after = value.Substring(index + 4);

            return Wrap(before) + " AS " + WrapValue(after);
        }
        
        if (value.Contains("."))
        {
            return string.Join(".", Regex.Split(value, "\\.(?=([^\"]*\"[^\"]*\")*[^\"]*$)").Select((x, index) => WrapValue(x.Trim('\"'))));
        }

        // If we reach here then the value does not contain an "AS" alias
        // nor dot "." expression, so wrap it as regular value.
        return WrapValue(value);
    }
    
    public static string Wrap(string value)
    {
        if (value.ToLowerInvariant().Contains(" as "))
        {
            var index = value.ToLowerInvariant().IndexOf(" as ");
            var before = value.Substring(0, index);
            var after = value.Substring(index + 4);

            return Wrap(before) + " AS " + WrapValue(after);
        }
        
        if (value.Contains("."))
        {
            return string.Join(".", value.Split(".").Select((x, index) => WrapValue(x)));
        }

        // If we reach here then the value does not contain an "AS" alias
        // nor dot "." expression, so wrap it as regular value.
        return WrapValue(value);
    }
    public static string WrapValue(string value)
    {
        if (value == "*") return value;

        var opening = "\"";
        var closing = "\"";

        return opening + value.Replace(closing, closing + closing) + closing;
    }
}