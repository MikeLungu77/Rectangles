using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RectanglesDataAccess.Models
{
    public class Rectangle 
    {
        public Size Size { get; private set; }
        public Position Position { get; private set; }

        public Rectangle(Size size, Position position)
        {
            Size = size;
            Position = position;
        }

        public override string ToString()
        {
            return $"Rectangle: size: {this.Size.Width}x{this.Size.Height} position: {this.Position.X}:{this.Position.Y}";
        }
    }
}
