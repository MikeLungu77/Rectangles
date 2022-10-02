using FluentValidation;
using RectanglesDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectanglesBusiness.ValidationAttributes
{
    public sealed class SizeValidator : AbstractValidator<Size>
    {
        public SizeValidator()
        {
            RuleFor(i => i.Width)
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .WithMessage("Width must be at least 1")
                .LessThanOrEqualTo(25)
                .WithMessage("Width must be less than 25");
            RuleFor(i => i.Height)
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .WithMessage("Height must be at least 1")
                .LessThanOrEqualTo(25)
                .WithMessage("Height must be less than 25");
        }
    }
}
