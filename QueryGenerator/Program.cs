using QueryGenerator;
using QueryGenerator.Enums;
using QueryGenerator.Models;

class Program
{
    public static void Main(string[] args)
    {
        var whereQueryGenerator = new WhereQueryGenerator
        {
            DatabaseType = DatabaseType.Oracle,
            TableName = "BaseObjects"
        };
        var whereClauseModels = new List<WhereClauseModel>
        {
            new()
            {
                FirstOperandColumnName = "Column1",
                SecondOperand = new()
                {
                    SecondOperandType = SecondOperandType.Value,
                    Value = "salam"
                },
                OperatorType = OperatorType.GreaterThan
            }
        };
        

        var table = "\"STAR_20.0.0.0_Dashboard\".ObjectFramework.BaseObject";

        //var query = whereQueryGenerator.GenerateQuery(whereClauseModels);

        var sqlQuery = new SqlKata.Query(table)
            .Select("Id", "ObjectTypeId")
            .SelectJsonData("JsonContent", "ConnectionSource", "ConnectionSource")
            .Where("ObjectTypeId", "=", "d2e6acdc-3cc2-435d-a8dd-7f881e21792c")
            .WhereJsonData("JsonContent", "ConnectionSource", "=", 1);

        var postgresQuery = new MyPostgresCompiler().Compile(sqlQuery);
        var sqlServerQuery = new MySqlServerCompiler().Compile(sqlQuery);

    }
}