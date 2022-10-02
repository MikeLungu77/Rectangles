using RectanglesDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectanglesDataAccess.Repositories.Interfaces
{
    public interface IRectanglesRepository
    {
        void CreateGrid(Size size);
        Grid? GetGrid();
        void CreateRectangle(Rectangle rectangle);
        List<Rectangle> GetRectangles();
        Rectangle GetRectangle(Position position);
        void SaveRectangles(List<Rectangle> rectangles);
    }
}
