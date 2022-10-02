using FluentValidation.TestHelper;
using RectanglesBusiness.ValidationAttributes;
using RectanglesDataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace RectanglesUnitTests
{
    [TestClass]
    public class PositionValidatorTests
    {
        private int LargeX = 26;
        private int LargeY = 26;

        private PositionValidator validator = new PositionValidator();

        private string LargeXError = "X must be less than 25";
        private string LargeYError = "Y must be less than 25";

        private string SmallXError = "X must be at least 0";
        private string SmallYError = "Y must be at least 0";

        [TestMethod]
        public void Validate_When_Y_IsLarge_And_Y_IsLarge_Return_Errors()
        {
            var position = new Position(LargeX, LargeY);

            var result = validator.TestValidate(position);

            result.ShouldHaveValidationErrorFor(position => position.X)
                  .WithErrorMessage(LargeXError);
            result.ShouldHaveValidationErrorFor(position => position.Y)
                  .WithErrorMessage(LargeYError);
        }

        [TestMethod]
        public void Validate_When_Negative_Values_Return_Errors()
        {
            var position = new Position(-1, -1);

            var result = validator.TestValidate(position);

            result.ShouldHaveValidationErrorFor(position => position.X)
                  .WithErrorMessage(SmallXError);
            result.ShouldHaveValidationErrorFor(position => position.Y)
                  .WithErrorMessage(SmallYError);
        }

        [TestMethod]
        public void Validate_When_X_IsLarge_Return_Error()
        {
            var position = new Position(LargeX, 20);

            var result = validator.TestValidate(position);

            result.ShouldHaveValidationErrorFor(position => position.X)
                  .WithErrorMessage(LargeXError);
            result.ShouldNotHaveValidationErrorFor(position => position.Y);
        }

        [TestMethod]
        public void Validate_When_Y_IsLarge_Return_Error()
        {
            var position = new Position(20, LargeY);

            var result = validator.TestValidate(position);

            result.ShouldNotHaveValidationErrorFor(position => position.X);
            result.ShouldHaveValidationErrorFor(position => position.Y)
                  .WithErrorMessage(LargeYError);
        }

        [TestMethod]
        public void Validate_When_X_And_Y_InRange_Returns_NoError()
        {
            var position = new Position(20, 20);

            var result = validator.TestValidate(position);

            result.ShouldNotHaveValidationErrorFor(position => position.X);
            result.ShouldNotHaveValidationErrorFor(position => position.Y);
        }
    }
}
