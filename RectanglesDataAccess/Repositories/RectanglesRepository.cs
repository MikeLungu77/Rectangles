using RectanglesDataAccess.Models;
using RectanglesDataAccess.Repositories.Interfaces;

namespace RectanglesBusiness.Repositories
{
    public class RectanglesRepository : IRectanglesRepository
    {
        private Grid? _grid;
        private List<Rectangle> _rectangles = new List<Rectangle>();

        public void CreateGrid(Size size)
        {
            _grid = new Grid(size);
        }

        public void CreateRectangle(Rectangle rectangle)
        {
            _rectangles.Add(rectangle);
        }

        public Grid? GetGrid()
        {
            return _grid;
        }

        public Rectangle GetRectangle(Position position)
        {
            throw new NotImplementedException();
        }

        public List<Rectangle> GetRectangles()
        {
            return _rectangles;
        }

        public void SaveRectangles(List<Rectangle> rectangles)
        {
            _rectangles = rectangles;
        }
    }
}
