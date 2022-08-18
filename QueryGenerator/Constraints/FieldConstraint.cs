using QueryGenerator.Enums;
using QueryGenerator.Models;

namespace QueryGenerator.Constraints
{
    public class FieldConstraint : Constraint
    {
        public string FirstOperandColumnName { get; set; }
        public OperatorType OperatorType { get; set; }
        public SecondOperandModel SecondOperand { get; set; }
    }
}