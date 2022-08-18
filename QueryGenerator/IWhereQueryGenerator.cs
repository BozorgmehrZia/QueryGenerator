using QueryGenerator.Constraints;
using QueryGenerator.Enums;
using SqlKata;

namespace QueryGenerator
{
	public interface IWhereQueryGenerator
	{
		public DatabaseType DatabaseType { get; init; }
		public string TableName { get; init; }
		Query GenerateQuery(Constraint constraint);
	}
}
