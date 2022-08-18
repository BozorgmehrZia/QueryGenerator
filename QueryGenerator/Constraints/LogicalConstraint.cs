using QueryGenerator.Enums;
using QueryGenerator.Models;

namespace QueryGenerator.Constraints
{
    public class LogicalConstraint : Constraint
    {
        public List<Constraint> InputConstraints { get; set; }
        public LogicalOperationType LogicalOperationType { get; set; }
    }
}