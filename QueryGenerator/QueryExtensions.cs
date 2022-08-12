using SqlKata.Extensions;

namespace QueryGenerator;

public static class QueryExtensions
{
    public static SqlKata.Query WhereJsonData(this SqlKata.Query query, string source, string path, string operation, object value)
    {
        var type = ConvertSystemDataTypeToPostgresType(value.GetType());
        return query.ForPostgreSql(q => q.WhereRaw($"(\"{source}\"::json ->> '{path}') :: {type} {operation} {value}"))
            .ForSqlServer(q => q.WhereRaw($"JSON_VALUE({source},'$.{path}') {operation} {value}"));
    }
    public static SqlKata.Query SelectJsonData(this SqlKata.Query query, string source, string path, string? alias = null)
    {
        alias = alias is null ? string.Empty : $" as {alias}";
        return query.ForPostgreSql(q => q.SelectRaw($"\"{source}\"::json ->> '{path}'{alias}"))
            .ForSqlServer(q => q.SelectRaw($"JSON_VALUE({source},'$.{path}'){alias}"));
    }

    private static string ConvertSystemDataTypeToPostgresType(Type type)
    {
        if (type == typeof(bool))
        {
            return "boolean";
        }
        if (type == typeof(short))
        {
            return "smallint";
        }
        if (type == typeof(int))
        {
            return "integer";
        }
        if (type == typeof(long))
        {
            return "bigint";
        }
        if (type == typeof(float))
        {
            return "real";
        }
        if (type == typeof(double))
        {
            return "double precision";
        }
        if (type == typeof(decimal))
        {
            return "numeric";
        }
        if (type == typeof(string))
        {
            return "text";
        }
        if (type == typeof(Guid))
        {
            return "uuid";
        }
        if (type == typeof(byte[]))
        {
            return "bytea";
        }
        if (type == typeof(DateTime))
        {
            return "timestamp without time zone";
        }
        if (type == typeof(TimeSpan))
        {
            return "time without time zone";
        }

        return type.ToString();
    }
}