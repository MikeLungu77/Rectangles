using RectanglesBusiness.Interfaces;
using RectanglesDataAccess.Models;
using RectanglesDataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectanglesBusiness.Services
{
    public class RectanglesService : IRectanglesService
    {
        private readonly IRectanglesRepository _rectanglesRepository;

        public RectanglesService(IRectanglesRepository rectanglesRepository)
        {
            _rectanglesRepository = rectanglesRepository;
        }

        public void CreateGrid(Size size)
        {
            _rectanglesRepository.CreateGrid(size);
        }

        public string CreateRectangle(Size size, Position position)
        {
            var validationResult = ValidateCreateRectangle(size, position);
            if (string.IsNullOrEmpty(validationResult))
            {
                var rectangle = new Rectangle(size, position);
                _rectanglesRepository.CreateRectangle(rectangle);
                return "";
            }
            return validationResult;
        }

        public Rectangle? FindRectangle(Position position)
        {
            var rectangles = _rectanglesRepository.GetRectangles();
            if (rectangles.Count == 0) 
                return null;
            else
            {
                return rectangles.FirstOrDefault(x =>
                   (position.X > x.Position.X && position.X < (x.Position.X + x.Size.Width)) &&
                   (position.Y > x.Position.Y && position.Y < (x.Position.Y + x.Size.Height)) 
                );
            }
        }

        public void RemoveRectangle(Position position)
        {
            var rectangles = _rectanglesRepository.GetRectangles();
            var rectangle = FindRectangle(position);
            if (rectangle is not null)
            {
                rectangles.Remove(rectangle);
                _rectanglesRepository.SaveRectangles(rectangles);
            }
        }

        private string ValidateCreateRectangle(Size size, Position position)
        {
            var grid = _rectanglesRepository.GetGrid();
            var rectangles = _rectanglesRepository.GetRectangles();
            if (grid is null) return "Create grid first";
            if (size.Width + position.X > grid!.Size.Width 
                || size.Height + position.Y > grid!.Size.Height)
                return "Rectangle would extend beyond grid boundaries";
            if (rectangles.Count() == 0) return string.Empty;
            if (Intersects(new Rectangle(size, position), rectangles))
                return "This rectangle would overlap an existing one";

            return string.Empty;
        }

        private bool Intersects(Rectangle newRectangle, List<Rectangle> rectangles)
        {
            bool xintersects = false;
            bool yintersects = false;
            foreach (Rectangle rectangle in rectangles)
            {
                if (newRectangle.Position.X < rectangle.Position.X + rectangle.Size.Width
                    && newRectangle.Position.X + newRectangle.Size.Width > rectangle.Position.X)
                    xintersects = true;
                if (newRectangle.Position.Y < rectangle.Position.Y + rectangle.Size.Height
                    && newRectangle.Position.Y + newRectangle.Size.Height > rectangle.Position.Y)
                    yintersects = true;
                if (xintersects && yintersects) return true;
            }
            return false;
        }
    }
}
