using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    internal class GroupShape : Shape
    {
        public List<Shape> SubShapes = new List<Shape>();

        public GroupShape(RectangleF rect) : base(rect)
        {
        }
        public GroupShape(GroupShape rectangle) : base(rectangle)
        {
        }
        public override void DrawSelf(Graphics graphics)
        {
            foreach (var shape in SubShapes)
            {
                shape.DrawSelf(graphics);
            }
        }

        public override bool Contains(PointF point)
        {
            return SubShapes.Any(shape => shape.Contains(point));
        }

        public override PointF Location
        {
            get => base.Location;
            set
            {
                foreach (Shape shape in SubShapes)
                {
                    shape.Location = new PointF(
                            shape.Location.X - Location.X + value.X,
                            shape.Location.Y - Location.Y + value.Y
                        );
                }
                base.Location = value;
            }
        }
    }

}
