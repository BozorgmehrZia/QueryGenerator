using System.Text.RegularExpressions;
using SqlKata.Compilers;

namespace QueryGenerator;

public class ClickHouseCompiler : Compiler
{
    public override string Wrap(string value)
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
        
        return WrapValue(value);
    }
    
}