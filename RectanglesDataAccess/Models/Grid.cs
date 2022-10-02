using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectanglesDataAccess.Models
{
    public class Grid
    {
        public Size Size { get; private set; }

        public Grid(Size size)
        {
            Size = size;
        }
    }
}
