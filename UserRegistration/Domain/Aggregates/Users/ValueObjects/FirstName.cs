namespace Domain.Aggregates.Users.ValueObjects;

public class FirstName : SeedWorks.ValueObject
{
	private const int MaxLength = 50;
	public static Result<FirstName> Create(string value)
	{
		var result =
			new Result<FirstName>();

		value = value.Fix();

		if (value is null)
		{
			string errorMessage = string.Format
					(Resources.Messages.Validations.Required, DataDictionary.FirstName);

			result.WithError(errorMessage: errorMessage);

			return result;
		}

		if (value.Length > MaxLength)
		{
			string errorMessage = string.Format
				(Resources.Messages.Validations.MaxLength, DataDictionary.FirstName, MaxLength);

			result.WithError(errorMessage: errorMessage);

			return result;
		}

		var returnValue =
				new FirstName(value);

		result.WithValue(value: returnValue);

		return result;
	}

	private FirstName(string value)
	{
		Value = value;
	}

	public string Value { get; }

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}

	public override string ToString()
	{
		if (string.IsNullOrWhiteSpace(Value))
		{
			return "---";
		}
		else
		{
			return Value;
		}
	}

}
