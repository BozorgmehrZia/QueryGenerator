using QueryGenerator.Constraints;
using QueryGenerator.Enums;
using SqlKata;
using SqlKata.Compilers;

namespace QueryGenerator
{
    public class WhereQueryGenerator : IWhereQueryGenerator
    {
        public DatabaseType DatabaseType { get; init; }
        public string TableName { get; init; }

        public Query GenerateQuery(Constraint constraint)
        {
            var finalQuery = new Query(TableName);
            return finalQuery.Where(WhereConstraint(finalQuery, constraint));
        }

        private Func<Query, Query> WhereConstraint(Query query, Constraint constraint)
        {
            if (constraint is FieldConstraint fieldConstraint)
            {
                return GetQueryFunctionFromFieldConstraint(fieldConstraint);
            }

            if (constraint is LogicalConstraint logicalConstraint)
            {
                if (logicalConstraint.LogicalOperationType == LogicalOperationType.And)
                {
                    return q =>
                    {
                        foreach (var inputConstraint in logicalConstraint.InputConstraints)
                        {
                            q.Where(WhereConstraint(q, inputConstraint));
                        }

                        return q;
                    };
                }

                return q =>
                {
                    foreach (var inputConstraint in logicalConstraint.InputConstraints)
                    {
                        q.OrWhere(WhereConstraint(q, inputConstraint));
                    }

                    return q;
                };
            }

            return q => q;
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