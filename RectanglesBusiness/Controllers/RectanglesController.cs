using FluentValidation;
using RectanglesBusiness.Interfaces;
using RectanglesDataAccess.Models;
using RectanglesBusiness.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectanglesBusiness.Controllers
{
    public class RectanglesController
    {
        private readonly IRectanglesService _rectanglesService;

        public RectanglesController(IRectanglesService rectanglesService)
        {
            _rectanglesService = rectanglesService; 
        }

        public string? CreateGrid(Size size)
        {
            var sizeValidator = new SizeValidator();
            var validRes = sizeValidator.Validate(size);
            if (validRes.IsValid)
                _rectanglesService.CreateGrid(size);
            return validRes?.Errors?.FirstOrDefault()?.ErrorMessage;
        }
        public string? CreateRectangle(Size size, Position position)
        {
            var sizeValidator = new SizeValidator();
            var validSize = sizeValidator.Validate(size);
            var positionValidator = new PositionValidator();
            var validPos = positionValidator.Validate(position);
            if (validSize.IsValid && validPos.IsValid)
            {
               return _rectanglesService.CreateRectangle(size, position);
            }
            else
                return $"Size error{validSize?.Errors?.FirstOrDefault()?.ErrorMessage} : Position error: {validPos?.Errors?.FirstOrDefault()?.ErrorMessage}";
        }
        public Rectangle FindRectangle(Position position)
        {
            return _rectanglesService.FindRectangle(position);
        }
        public void RemoveRectangle(Position position)
        {
            _rectanglesService.RemoveRectangle(position);
        }

    }
}
