using Draw.src.Model.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    internal class EllipseShape : Shape
    {
        #region Constructor

        public EllipseShape(RectangleF rect) : base(rect)
        {
        }

		#endregion

		public override bool Contains(PointF point)
		{
			PointF local = ToLocal(point);

			float a = Width / 2;
			float b = Height / 2;

			float dx = local.X - a;
			float dy = local.Y - b;

			return (dx * dx) / (a * a) + (dy * dy) / (b * b) <= 1;
		}


		public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            ChangeSize(Scale);

            Pen pen = new Pen(StrokeColor, Stroke);
            Color color = Color.FromArgb(255 - Transparency, FillColor);
            RectangleF ellipse = new RectangleF(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

            //Rectangle rect = new Rectangle(50, 50, 100, 100);
            Brush brush;
            if (Color1Gradient != Color.Empty && Color2Gradient != Color.Empty)
            {
                brush = new LinearGradientBrush(ellipse, Color1Gradient, Color2Gradient, LinearGradientMode.Horizontal);
            }
            else
            {
                brush = new SolidBrush(color);
            }

            Matrix oldTransform = grfx.Transform;

            // Прилагаме ротация само на тази фигура
            grfx.Transform = Rotation;

            grfx.FillEllipse(brush, ellipse);
            grfx.DrawEllipse(pen, ellipse);

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
