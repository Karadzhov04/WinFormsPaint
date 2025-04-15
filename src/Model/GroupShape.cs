using Draw.src.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    public class GroupShape : Shape
    {
        public List<Shape> SubShapes = new List<Shape>();

		public bool IsRotated { get; set; } = false;
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
		public void RotateGroup(float angle)
		{
			PointF center = GetGroupCenter();

			foreach (var shape in SubShapes)
			{
				shape.Location = GeometryUtils.RotatePoint(shape.Location, center, angle);
				shape.Rotation.RotateAt(angle, center);
			}

			IsRotated = true;

		}
		public PointF GetGroupCenter()
		{
			float minX = SubShapes.Min(s => s.Location.X);
			float minY = SubShapes.Min(s => s.Location.Y);
			float maxX = SubShapes.Max(s => s.Location.X + s.Width);
			float maxY = SubShapes.Max(s => s.Location.Y + s.Height);

			return new PointF((minX + maxX) / 2, (minY + maxY) / 2);
		}


	}

}
