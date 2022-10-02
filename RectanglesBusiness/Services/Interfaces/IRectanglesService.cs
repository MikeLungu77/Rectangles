using RectanglesDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectanglesBusiness.Interfaces
{
    public interface IRectanglesService
    {
        void CreateGrid(Size size);
        string CreateRectangle(Size size, Position position);
        Rectangle? FindRectangle(Position position);
        void RemoveRectangle(Position position);
    }
}
