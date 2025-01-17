using HoTeach.API.Common;

namespace HoTeach.API.UnitTests
{
    public class TestCoverage
    {
        [Fact]
        public void Result_SuccessWithData_ShouldReturnCorrectData()
        {
            // Arrange
            var expectedData = "Test Data";

            // Act
            Result<string> result = Result<string>.SuccessWith(expectedData);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal(expectedData, result.Data);
        }
    }
}