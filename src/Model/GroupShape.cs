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
    [Serializable]
    public class GroupShape : Shape
    {
        public GroupShape() { }
        public List<Shape> SubShapes = new List<Shape>();

		public bool IsRotated { get; set; } = false;
		public GroupShape(RectangleF rect) : base(rect)
        {
        }
        public GroupShape(GroupShape rectangle) : base(rectangle)
        {
        }
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            // Рисуваме всички подфигури
            foreach (var shape in SubShapes)
            {
                shape.DrawSelf(grfx);
            }

            // ВИНАГИ рисуваме bounding box около групата
            List<PointF> allTransformedPoints = new List<PointF>();

            foreach (var shape in SubShapes)
            {
                PointF[] corners = new PointF[]
                {
            shape.Location,
            new PointF(shape.Location.X + shape.Width, shape.Location.Y),
            new PointF(shape.Location.X + shape.Width, shape.Location.Y + shape.Height),
            new PointF(shape.Location.X, shape.Location.Y + shape.Height)
                };

                shape.Rotation.TransformPoints(corners);
                allTransformedPoints.AddRange(corners);
            }

            float minX = allTransformedPoints.Min(p => p.X);
            float minY = allTransformedPoints.Min(p => p.Y);
            float maxX = allTransformedPoints.Max(p => p.X);
            float maxY = allTransformedPoints.Max(p => p.Y);

            int padding = 40;

            grfx.DrawRectangle(
                Pens.Red,
                minX - padding,
                minY - padding,
                (maxX - minX) + 2 * padding,
                (maxY - minY) + 2 * padding
            );
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

        public override Shape Clone()
        {
            return new GroupShape(this);
        }
    }

}
