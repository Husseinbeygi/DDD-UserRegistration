using System.Text.RegularExpressions;

namespace Domain.Aggregates.Users.ValueObjects;

public class Email : SeedWorks.ValueObject
{
	private const int MaxLength = 200;
	public static FluentResults.Result<Email> Create(string value)
	{
		var result =
			new FluentResults.Result<Email>();

		value =
			value.Fix();

		if (value is null)
		{
			string errorMessage = string.Format
					(Resources.Messages.Validations.Required,
					DataDictionary.EmailAddress);

			result.WithError(errorMessage: errorMessage);

			return result;
		}

		if (value.Length > MaxLength)
		{
			string errorMessage = string.Format
				(Resources.Messages.Validations.MaxLength,
				DataDictionary.EmailAddress, MaxLength);

			result.WithError(errorMessage: errorMessage);

			return result;
		}

		if (!IsValidEmail(email: value))
		{
			string errorMessage = string.Format
				(Resources.Messages.Validations.RegularExpression,
				DataDictionary.EmailAddress);

			result.WithError(errorMessage: errorMessage);

			return result;
		}

		var returnValue =
				new Email(value);

		result.WithValue(value: returnValue);

		return result;

	}

	public Email()
	{
	}

	public Email(string value)
	{
		Value = value;
	}

	public string Value { get; set; }
	protected override IEnumerable<object> GetEqualityComponents()
	{
		throw new NotImplementedException();
	}

	private static bool IsValidEmail(string email)
	{
		if (email.EndsWith("."))
		{
			return false;
		}
		try
		{
			Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			Match match = regex.Match(email);
			if (match.Success)
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}
}
