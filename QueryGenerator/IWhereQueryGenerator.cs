using QueryGenerator.Enums;
using QueryGenerator.Models;

namespace QueryGenerator
{
	public interface IWhereQueryGenerator
	{
		public DatabaseType DatabaseType { get; init; }
		public string TableName { get; init; }
		string GenerateQuery(List<WhereClauseModel> whereClauseModels);
	}
}
