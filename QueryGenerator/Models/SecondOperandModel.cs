using QueryGenerator.Enums;

namespace QueryGenerator.Models
{
	public class SecondOperandModel
	{
		public string ColumnName { get; set; }
		public object Value { get; set; }
		public List<object> ValuesList { get; set; }
		public SecondOperandType SecondOperandType { get; set; }
	}
}
