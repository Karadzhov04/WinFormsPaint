using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model.Helpers
{
	public static class GeometryUtils
	{
		public static PointF RotatePoint(PointF point, PointF center, float angle)
		{
			double radians = angle * (Math.PI / 180);
			float cos = (float)Math.Cos(radians);
			float sin = (float)Math.Sin(radians);

			float dx = point.X - center.X;
			float dy = point.Y - center.Y;

			return new PointF(
				center.X + (dx * cos - dy * sin),
				center.Y + (dx * sin + dy * cos)
			);
		}

		public static PointF ToLocal(PointF point, Shape shape)
		{
			Matrix transform = shape.Rotation.Clone();
			transform.Translate(shape.Location.X, shape.Location.Y, MatrixOrder.Append); // Първо завърти, после премести
			transform.Invert(); // Инвертираме за връщане към локални координати

			PointF[] points = new PointF[] { point };
			transform.TransformPoints(points);
			return points[0];
		}
	}
}
