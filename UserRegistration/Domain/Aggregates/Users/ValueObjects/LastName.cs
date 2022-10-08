namespace Domain.Aggregates.Users.ValueObjects;

public class LastName : SeedWorks.ValueObject
{

	private const int MaxLength = 100;

	public static FluentResults.Result<LastName> Create(string value)
	{
		var result =
				new Result<LastName>();

		value = value.Fix();

		if (value is null)
		{
			string errorMessage = string.Format
					(Resources.Messages.Validations.Required, DataDictionary.LastName);

			result.WithError(errorMessage: errorMessage);

			return result;
		}

		if (value.Length > MaxLength)
		{
			string errorMessage = string.Format
				(Resources.Messages.Validations.MaxLength, DataDictionary.LastName, MaxLength);

			result.WithError(errorMessage: errorMessage);

			return result;
		}

		var returnValue =
				new LastName(value);

		result.WithValue(value: returnValue);

		return result;
	}


	private LastName()
	{
	}

	private LastName(string value)
	{
		Value = value;
	}

	public string Value { get; set; }
	protected override IEnumerable<object> GetEqualityComponents()
	{
		throw new NotImplementedException();
	}

}