using QueryGenerator.Constraints;
using QueryGenerator.Enums;
using SqlKata;
using SqlKata.Compilers;

namespace QueryGenerator
{
    public class WhereQueryGenerator : IWhereQueryGenerator
    {
        public Query GenerateQuery(LogicalConstraint constraint, Query query)
        {
            return query.Where(LogicalConstraint(constraint));
        }

        private Func<Query, Query> LogicalConstraint(LogicalConstraint logicalConstraint)
        {
            return logicalConstraint.LogicalOperationType switch
            {
                LogicalOperationType.And => q => AndConstraint(q, logicalConstraint),
                LogicalOperationType.Or => q => OrConstraint(q, logicalConstraint),
                _ => throw new NotSupportedException()
            };
        }

        private Query OrConstraint(Query q, LogicalConstraint logicalConstraint)
        {
            foreach (var inputConstraint in logicalConstraint.FieldConstraints)
            {
                q.OrWhere(GetQueryFunctionFromFieldConstraint(inputConstraint));
            }
            foreach (var inputConstraint in logicalConstraint.LogicalConstraints)
            {
                q.OrWhere(LogicalConstraint(inputConstraint));
            }

            return q;
        }

        private Query AndConstraint(Query q, LogicalConstraint logicalConstraint)
        {
            foreach (var inputConstraint in logicalConstraint.FieldConstraints)
            {
                q.Where(GetQueryFunctionFromFieldConstraint(inputConstraint));
            }
            foreach (var inputConstraint in logicalConstraint.LogicalConstraints)
            {
                q.Where(LogicalConstraint(inputConstraint));
            }

            return q;
        }

        private Func<Query, Query> GetQueryFunctionFromFieldConstraint(FieldConstraint fieldConstraint)
        {
            var secondOperand = fieldConstraint.SecondOperand;
            return secondOperand.SecondOperandType switch
            {
                SecondOperandType.Value => q => q.Where(fieldConstraint.FirstOperandColumnName,
                    fieldConstraint.OperatorType.ToOperatorString(),
                    secondOperand.Value),
                SecondOperandType.Column => q => q.WhereColumns(fieldConstraint.FirstOperandColumnName,
                    fieldConstraint.OperatorType.ToOperatorString(),
                    secondOperand.ColumnName),
                SecondOperandType.ValuesList => q => q.WhereIn(fieldConstraint.FirstOperandColumnName,
                    secondOperand.ValuesList),
                _ => q => q
            };
        }
    }
}