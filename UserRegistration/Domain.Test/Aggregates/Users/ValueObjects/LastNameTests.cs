using Framework;

namespace Domain.Test.Aggregates.Users.ValueObjects;

public class LastNameTests
{

	[Fact]
	public void ShouldReturnErrorWhenLastNameIsNull()
	{
		string value = null;

		var lastName =
			Domain.Aggregates.Users.ValueObjects.LastName.Create(value);

		lastName.IsFailed.Should().BeTrue();
		lastName.IsSuccess.Should().BeFalse();
		lastName.Errors.Count().Should().BeGreaterThanOrEqualTo(1);

		string errorMessage = string.Format
						(Resources.Messages.Validations.Required, DataDictionary.LastName);
		lastName.Errors.FirstOrDefault(x => x.Message == errorMessage).Should().NotBeNull();
	}

	[Fact]
	public void ShouldReturnErrorWhenLastNameIsMoreThenMaxLength()
	{
		var value =
			"aGRfoSYvnnWp6aGRfoSYvnnWp6dVb30aGRfoSYvnnWp6dVb30aGRfoSYvnnW" +
			"p6dVb30dVb30aGRfoSYvnnWp6dVb30aGRfoSYvnnWp6dVb30";

		var lastName =
			Domain.Aggregates.Users.ValueObjects.LastName.Create(value);

		lastName.IsFailed.Should().BeTrue();
		lastName.IsSuccess.Should().BeFalse();
		lastName.Errors.Count().Should().BeGreaterThanOrEqualTo(1);

		string errorMessage = string.Format
			(Resources.Messages.Validations.MaxLength, DataDictionary.LastName, 100);
		lastName.Errors.FirstOrDefault(x => x.Message == errorMessage).Should().NotBeNull();

	}

	[Fact]
	public void ShouldTrimUnnecessarySpaces()
	{
		var value =
				"   BEYGI     ";

		var lastName =
			Domain.Aggregates.Users.ValueObjects.LastName.Create(value);

		lastName.IsFailed.Should().BeFalse();
		lastName.IsSuccess.Should().BeTrue();
		lastName.Errors.Count().Should().Be(0);

		lastName.Value.Value.Should().Be(value.Fix());

	}
}
