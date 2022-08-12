using System.Text.Json.Nodes;
using QueryGenerator.Enums;
using QueryGenerator.Models;
using QueryGenerator.Enums;
using SqlKata.Compilers;

namespace QueryGenerator
{
	public class WhereQueryGenerator : IWhereQueryGenerator
	{
		public DatabaseType DatabaseType { get; init; }
		public string TableName { get; init; }
		public string GenerateQuery(List<WhereClauseModel> whereClauseModels)
		{
			var query = new SqlKata.Query(TableName);

			// foreach (var whereClauseModel in whereClauseModels)
			// {
			// 	var secondOperand = whereClauseModel.SecondOperand;
			// 	query = secondOperand.SecondOperandType switch
			// 	{
			// 		SecondOperandType.Value => query.Where(whereClauseModel.FirstOperandColumnName,
			// 			whereClauseModel.OperatorType.ToOperatorString(),
			// 			secondOperand.Value),
			// 		SecondOperandType.Column => query.WhereColumns(whereClauseModel.FirstOperandColumnName,
			// 			whereClauseModel.OperatorType.ToOperatorString(),
			// 			secondOperand.ColumnName),
			// 		SecondOperandType.ValuesList => query.WhereIn(whereClauseModel.FirstOperandColumnName,
			// 			secondOperand.ValuesList),
			// 		_ => query
			// 	};
			// }

			query = query.Where(JsonValue.Create("Attributes")?.ToString(), "$.name");
			return GetCompiler().Compile(query).ToString();
		}

		private Compiler GetCompiler()
		{
			return DatabaseType switch
			{
				DatabaseType.SqlServer => new SqlServerCompiler(),
				DatabaseType.Postgres => new PostgresCompiler(),
				DatabaseType.Oracle => new OracleCompiler(),
				DatabaseType.Sqlite => new SqliteCompiler(),
				DatabaseType.MySql => new MySqlCompiler(),
				_ => throw new Exception()

			};
		}
	}
}
