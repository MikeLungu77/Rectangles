using FluentValidation.TestHelper;
using RectanglesBusiness.ValidationAttributes;
using RectanglesDataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace RectanglesUnitTests
{
    [TestClass]
    public class SizeValidatorTests
    {
        private int LargeWidth = 26;
        private int LargeHeight = 26;

        private SizeValidator validator = new SizeValidator();

        private string LargeWidthError = "Width must be less than 25";
        private string LargeHeightError = "Height must be less than 25";

        private string SmallWidthError = "Width must be at least 1";
        private string SmallHeightError = "Height must be at least 1";

        [TestMethod]
        public void Validate_When_Width_IsLarge_And_Height_IsLarge_Return_Errors()
        {
            var size = new Size(LargeWidth, LargeHeight);

            var result = validator.TestValidate(size);

            result.ShouldHaveValidationErrorFor(size => size.Width)
                  .WithErrorMessage(LargeWidthError);
            result.ShouldHaveValidationErrorFor(schedule => schedule.Height)
                  .WithErrorMessage(LargeHeightError);
        }

        [TestMethod]
        public void Validate_When_Negative_Values_Return_Errors()
        {
            var size = new Size(-1, -1);

            var result = validator.TestValidate(size);

            result.ShouldHaveValidationErrorFor(size => size.Width)
                  .WithErrorMessage(SmallWidthError);
            result.ShouldHaveValidationErrorFor(schedule => schedule.Height)
                  .WithErrorMessage(SmallHeightError);
        }

        [TestMethod]
        public void Validate_When_Width_IsLarge_Return_Error()
        {
            var size = new Size(LargeWidth, 20);

            var result = validator.TestValidate(size);

            result.ShouldHaveValidationErrorFor(size => size.Width)
                  .WithErrorMessage(LargeWidthError);
            result.ShouldNotHaveValidationErrorFor(schedule => schedule.Height);
        }

        [TestMethod]
        public void Validate_When_Height_IsLarge_Return_Error()
        {
            var size = new Size(20, LargeHeight);

            var result = validator.TestValidate(size);

            result.ShouldNotHaveValidationErrorFor(size => size.Width);
            result.ShouldHaveValidationErrorFor(schedule => schedule.Height)
                  .WithErrorMessage(LargeHeightError);
        }

        [TestMethod]
        public void Validate_When_Height_And_Width_InRange_Returns_NoError()
        {
            var size = new Size(20, 20);

            var result = validator.TestValidate(size);

            result.ShouldNotHaveValidationErrorFor(size => size.Width);
            result.ShouldNotHaveValidationErrorFor(schedule => schedule.Height);
        }
    }
}
