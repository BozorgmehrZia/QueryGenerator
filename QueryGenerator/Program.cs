using QueryGenerator;
using QueryGenerator.Constraints;
using QueryGenerator.Enums;
using QueryGenerator.Models;
using SqlKata;

class Program
{
    public static void Main(string[] args)
    {
        var query = new Query("Users")
            .Where(q => q.Where("Location", "=", "Bhuj")
                .Where(qq => qq.Where("Age", ">", 20)
                    .OrWhere("Age", "<", 27)))
            .OrWhere(q => q.Where("Location", "=", "Mumbai")
                .Where("Age", ">", 25));
        var sql = new WhereQueryGenerator().GenerateQuery(CreateTestConstraint(), new Query("Users"));
    }

    private static void TestCompilers()
    {
        var table = "\"STAR_20.0.0.0_Dashboard\".ObjectFramework.BaseObject";
        var sqlQuery = new SqlKata.Query(table)
            .Select("Id", "ObjectTypeId")
            .SelectJsonData("JsonContent", "ConnectionSource", "ConnectionSource")
            .Where("ObjectTypeId", "=", "d2e6acdc-3cc2-435d-a8dd-7f881e21792c")
            .WhereJsonData("JsonContent", "ConnectionSource", "=", 1);

        var chQuery = new SqlKata.Query("users")
            .Select("username")
            .Where("userId", "=", 2);


        var postgresQuery = new MyPostgresCompiler().Compile(sqlQuery);
        var sqlServerQuery = new MySqlServerCompiler().Compile(sqlQuery);
        var clickHouse = new ClickHouseCompiler().Compile(chQuery);
    }
    
    private static LogicalConstraint CreateTestConstraint()
    {
        return new LogicalConstraint()
        {
            LogicalOperationType = LogicalOperationType.Or,
            LogicalConstraints = new()
            {
                new LogicalConstraint()
                {
                    LogicalOperationType = LogicalOperationType.And,
                    FieldConstraints = new()
                    {

                        new FieldConstraint()
                        {
                            FirstOperandColumnName = "Location",
                            OperatorType = OperatorType.Equal,
                            SecondOperand = new()
                            {
                                SecondOperandType = SecondOperandType.Value,
                                Value = "Bhuj"
                            }
                        },
                    },
                    LogicalConstraints = new()
                    {
                        new LogicalConstraint()
                        {
                            LogicalOperationType = LogicalOperationType.Or,
                            FieldConstraints = new()
                            {
                                new FieldConstraint()
                                {
                                    FirstOperandColumnName = "Age",
                                    OperatorType = OperatorType.LessThan,
                                    SecondOperand = new()
                                    {
                                        SecondOperandType = SecondOperandType.Value,
                                        Value = 27
                                    }
                                },
                                new FieldConstraint()
                                {
                                    FirstOperandColumnName = "Age",
                                    OperatorType = OperatorType.GreaterThan,
                                    SecondOperand = new()
                                    {
                                        SecondOperandType = SecondOperandType.Value,
                                        Value = 20
                                    }
                                }
                            }
                        }
                    }
                },
                new LogicalConstraint()
                {
                    LogicalOperationType = LogicalOperationType.And,
                    FieldConstraints = new()
                    {
                        new FieldConstraint()
                        {
                            FirstOperandColumnName = "Location",
                            OperatorType = OperatorType.Equal,
                            SecondOperand = new()
                            {
                                SecondOperandType = SecondOperandType.Value,
                                Value = "Mumbi"
                            }
                        },
                        new FieldConstraint()
                        {
                            FirstOperandColumnName = "Age",
                            OperatorType = OperatorType.GreaterThan,
                            SecondOperand = new()
                            {
                                SecondOperandType = SecondOperandType.Value,
                                Value = 25
                            }
                        }

                    }
                }
            }
        };
    }
}