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
        var dictionary = new Dictionary<Type, string>
        {
            { typeof(bool), "boolean" },
            { typeof(short), "smallint" },
            { typeof(int), "integer" },
            { typeof(long), "bigint" },
            { typeof(float), "real" },
            { typeof(double), "double precision" },
            { typeof(decimal), "numeric" },
            { typeof(string), "text" },
            { typeof(Guid), "uuid" },
            { typeof(byte[]), "bytea" },
            { typeof(DateTime), "timestamp without time zone" },
            { typeof(TimeSpan), "time without time zone" }
        };

        return dictionary.TryGetValue(type, out var typeString) ? typeString : type.ToString();
    }
}