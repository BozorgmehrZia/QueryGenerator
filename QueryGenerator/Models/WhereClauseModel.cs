using QueryGenerator.Enums;

namespace QueryGenerator.Models
{
	public class WhereClauseModel
	{
		public string FirstOperandColumnName { get; set; }
		public OperatorType OperatorType { get; set; }
		public SecondOperandModel SecondOperand { get; set; }
	}
}
