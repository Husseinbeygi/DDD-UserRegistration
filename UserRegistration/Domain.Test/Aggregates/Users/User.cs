namespace Domain.Test.Aggregates.Users;

public class UserTest
{
	[Theory]
	[InlineData("", "", "")]
	[InlineData(null, null, null)]
	[InlineData("", "beygi", "hb@g.com")]
	[InlineData(null, "beygi", "hb@g.com")]
	[InlineData("hussein", "", "hb@g.com")]
	[InlineData("hussein", null, "hb@g.com")]
	[InlineData("hussein", "beygi", "")]
	[InlineData("hussein", "beygi", null)]
	[InlineData("hussein", "beygi", "hb@g")]
	public void ShouldReturnErrorWhenInputDataIsInvalid(string fname, string lname, string email)
	{
		var result =
				Domain.Aggregates.Users.User.Create(fname, lname, email);

		result.Should().NotBeNull();
		result.IsFailed.Should().BeTrue();
		result.IsSuccess.Should().BeFalse();

		result.Successes.Should().HaveCount(0);
		result.Errors.Should().HaveCountGreaterThan(0);

	}

	[Fact]
	public void ShouldCreateUserWhenInputDataIsValid()
	{
		var result =
		Domain.Aggregates.Users.User.Create("Hussein", "Beygi", "husseinbeygi.dev@gmail.com");

		result.Should().NotBeNull();
		result.IsFailed.Should().BeFalse();
		result.IsSuccess.Should().BeTrue();

		result.Successes.Should().HaveCountGreaterThan(0);
		result.Errors.Should().HaveCount(0);

	}
}
