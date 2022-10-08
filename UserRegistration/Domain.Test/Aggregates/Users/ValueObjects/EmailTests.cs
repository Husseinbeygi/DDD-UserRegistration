namespace Domain.Test.Aggregates.Users.ValueObjects;

public class EmailTests 
{
	[Fact]
	public void ShouldReturnErrorWhenEmailIsNull()
	{
		string value = null;

		var email =
			Domain.Aggregates.Users.ValueObjects.Email.Create(value);

		email.IsFailed.Should().BeTrue();
		email.IsSuccess.Should().BeFalse();
		email.Errors.Count().Should().BeGreaterThanOrEqualTo(1);

		string errorMessage = string.Format
						(Resources.Messages.Validations.Required, DataDictionary.EmailAddress);
		email.Errors.FirstOrDefault(x => x.Message == errorMessage).Should().NotBeNull();

	}

	[Theory]
	[InlineData("hussein")]
	[InlineData("hussein@")]
	[InlineData("hussein#")]
	[InlineData("hussein@gamil.")]
	[InlineData("hussein@gamil")]
	[InlineData("hussein    beygi   @gamil")]
	[InlineData("''hussein    beygi   ''@gamil''.com")]

	public void ShouldReturnErrorWhenEmailIsNotValid(string value)
	{
		var email =
			Domain.Aggregates.Users.ValueObjects.Email.Create(value);

		email.IsFailed.Should().BeTrue();
		email.IsSuccess.Should().BeFalse();
		email.Errors.Count().Should().BeGreaterThanOrEqualTo(1);

		string errorMessage = string.Format
						(Resources.Messages.Validations.RegularExpression,
						DataDictionary.EmailAddress);
		email.Errors.FirstOrDefault(x => x.Message == errorMessage).Should().NotBeNull();

	}
}
