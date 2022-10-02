using FluentValidation;
using RectanglesDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectanglesBusiness.ValidationAttributes
{
    public sealed class PositionValidator : AbstractValidator<Position>
    {
        public PositionValidator()
        {
            RuleFor(i => i.X)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("X must be at least 0")
                .LessThanOrEqualTo(25)
                .WithMessage("X must be less than 25");
            RuleFor(i => i.Y)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Y must be at least 0")
                .LessThanOrEqualTo(25)
                .WithMessage("Y must be less than 25");
        }
    }
}
