using Framework;

namespace Domain.Test.Aggregates.Users.ValueObjects;

public class FirstNameTests
{
    [Fact]
    public void ShouldReturnErrorWhenFirstNameIsNull()
    {
        string value = null;

		var firstName =
            Domain.Aggregates.Users.ValueObjects.FirstName.Create(value);

        firstName.IsFailed.Should().BeTrue();
        firstName.IsSuccess.Should().BeFalse();
        firstName.Errors.Count().Should().BeGreaterThanOrEqualTo(1);

		string errorMessage = string.Format
	                	(Resources.Messages.Validations.Required, DataDictionary.FirstName);
        firstName.Errors.FirstOrDefault(x => x.Message == errorMessage).Should().NotBeNull();
    }

    [Fact]
    public void ShouldReturnErrorWhenFirstNameIsMoreThenMaxLength()
    {
        var value =
			"aGRfoSYvnnWp6dVb30aGRfoSYvnnWp6dVb30aGRfoSYvnnWp6dVb30";

		var firstName =
			Domain.Aggregates.Users.ValueObjects.FirstName.Create(value);

		firstName.IsFailed.Should().BeTrue();
		firstName.IsSuccess.Should().BeFalse();
		firstName.Errors.Count().Should().BeGreaterThanOrEqualTo(1);

		string errorMessage = string.Format
			(Resources.Messages.Validations.MaxLength, DataDictionary.FirstName, 50);
		firstName.Errors.FirstOrDefault(x => x.Message == errorMessage).Should().NotBeNull();

	}

	[Fact]
	public void ShouldFormatUnnecessarySpaces()
	{
		var value =
			"   HUSSEIN     ";

		var firstName =
			Domain.Aggregates.Users.ValueObjects.FirstName.Create(value);

		firstName.IsFailed.Should().BeFalse();
		firstName.IsSuccess.Should().BeTrue();
		firstName.Errors.Count().Should().Be(0);

		firstName.Value.Value.Should().Be(value.Fix());
	}

}