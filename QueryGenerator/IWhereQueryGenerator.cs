using QueryGenerator.Constraints;
using QueryGenerator.Enums;
using SqlKata;

namespace QueryGenerator
{
	public interface IWhereQueryGenerator
	{
		Query GenerateQuery(LogicalConstraint constraint, Query query);
	}
}
