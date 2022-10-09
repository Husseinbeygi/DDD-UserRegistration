using Domain.Aggregates.Users.ValueObjects;

namespace Domain.Aggregates.Users;

public class User : SeedWorks.AggregateRoot
{
	public static FluentResults.Result<User>
		Create(string firstName, string lastName, string email)
	{
		var result =
			new FluentResults.Result<User>();

		var _firstname=
				ValueObjects.FirstName.Create(value: firstName);
		result.WithErrors(errors: _firstname.Errors);

		var _lastname =
			ValueObjects.LastName.Create(value: lastName);
		result.WithErrors(errors: _lastname.Errors);

		var _email =
			ValueObjects.Email.Create(value: email);
		result.WithErrors(errors: _email.Errors);


		if (result.IsFailed)
		{
			return result;
		}

		var returnValue =
			new User(firstName: _firstname.Value,
			lastName:_lastname.Value,email:_email.Value);

		string successMessage = string.Format
			(Resources.Messages.Successes.SuccessCreate,
			DataDictionary.User);

		result.WithSuccess(successMessage);


		result.WithValue(value: returnValue);

		return result;

	}
	private User()
	{
	}

	private User(FirstName firstName, LastName lastName, Email email)
	{
		FirstName = firstName;
		LastName = lastName;
		Email = email;
	}

	public ValueObjects.FirstName FirstName { get; set; }
	public ValueObjects.LastName LastName { get; set; }
	public ValueObjects.Email Email { get; set; }

}
