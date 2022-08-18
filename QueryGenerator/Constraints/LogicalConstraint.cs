using QueryGenerator.Enums;
using QueryGenerator.Models;

namespace QueryGenerator.Constraints
{
    public class LogicalConstraint
    {
        public LogicalConstraint(List<FieldConstraint> fieldConstraints, List<LogicalConstraint> logicalConstraints, LogicalOperationType logicalOperationType)
        {
            FieldConstraints = fieldConstraints ?? new List<FieldConstraint>();
            LogicalConstraints = logicalConstraints ?? new List<LogicalConstraint>();
            LogicalOperationType = logicalOperationType;
        }

        public LogicalConstraint() : this(new List<FieldConstraint>(), new List<LogicalConstraint>(), LogicalOperationType.And)
        {

        }

        public List<FieldConstraint> FieldConstraints { get; set; }
        public List<LogicalConstraint> LogicalConstraints { get; set; }
        public LogicalOperationType LogicalOperationType { get; set; }
    }
}