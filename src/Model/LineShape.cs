using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    internal class LineShape : Shape
    {
        #region Constructor

        public LineShape(RectangleF rect) : base(rect) { }

		#endregion

		public override bool Contains(PointF point)
		{
			// Взимаме началната и крайната точка на линията
			PointF start = Rectangle.Location;
			PointF end = new PointF(Rectangle.Right, Rectangle.Bottom);

			const float tolerance = 3f; // Допустимо разстояние (в пиксели)

			// Изчисляваме разстоянието от точката до линията
			float distance = DistanceFromPointToLine(point, start, end);

			return distance <= tolerance;
		}

		private float DistanceFromPointToLine(PointF p, PointF a, PointF b)
		{
			float dx = b.X - a.X;
			float dy = b.Y - a.Y;

			if (dx == 0 && dy == 0)
			{
				// a и b са една и съща точка
				dx = p.X - a.X;
				dy = p.Y - a.Y;
				return (float)Math.Sqrt(dx * dx + dy * dy);
			}

			// Проекция t на точката върху отсечката (0 <= t <= 1)
			float t = ((p.X - a.X) * dx + (p.Y - a.Y) * dy) / (dx * dx + dy * dy);
			t = Math.Max(0, Math.Min(1, t)); // Ограничаваме t в интервала [0, 1]

			// Изчисляваме проектираната точка върху отсечката
			float projX = a.X + t * dx;
			float projY = a.Y + t * dy;

			dx = p.X - projX;
			dy = p.Y - projY;

			return (float)Math.Sqrt(dx * dx + dy * dy);
		}
		public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            ChangeSize(Scale);

            Matrix oldTransform = grfx.Transform;
            // Прилагаме ротация само на тази фигура
            grfx.Transform = Rotation;

            Pen pen = new Pen(StrokeColor, Stroke);
            grfx.DrawLine(pen, Rectangle.Location,
                new PointF(Rectangle.Right, Rectangle.Top));

            // Връщаме оригиналната трансформация, за да не влияе на другите фигури
            grfx.Transform = oldTransform;
        }

        public override void ChangeSize(float scale)
        {
            Width = OriginalWidth + scale;
            Height = OriginalHeight + scale;
        }
    }
}
