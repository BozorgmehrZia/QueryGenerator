namespace QueryGenerator.Enums
{
	public enum OperatorType
	{
		Equal ,
		GreaterThan,
		LessThan,
		GreaterThanOrEqual,
		LessThanOrEqual,
		NotEqual,
		Between,
		Like,
		In
	}

	public static class OperatorTypeExtensions
	{
		public static string ToOperatorString(this OperatorType operatorType)
		{
			return operatorType switch
			{
				OperatorType.Equal => "=",
				OperatorType.GreaterThan => ">",
				OperatorType.LessThan => "<",
				OperatorType.GreaterThanOrEqual => ">=",
				OperatorType.LessThanOrEqual => "<=",
				OperatorType.NotEqual => "<>",
				OperatorType.Between => "BETWEEN",
				OperatorType.Like => "LIKE",
				OperatorType.In => "IN",
				_ => throw new ArgumentException(nameof(operatorType))
			};

		}
	}
}
