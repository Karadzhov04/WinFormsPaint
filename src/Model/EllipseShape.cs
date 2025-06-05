using Draw.src.Model.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    [Serializable]
    public class EllipseShape : Shape
    {
        #region Constructor
        public EllipseShape() { }
        public EllipseShape(RectangleF rect) : base(rect)
        {
        }

        public EllipseShape(EllipseShape rect) : base(rect)
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

            Matrix oldTransform = grfx.Transform;

            grfx.TranslateTransform(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2);
            grfx.RotateTransform(RotateDegree);
            //grfx.ScaleTransform(Scale, Scale);
            ChangeSize(Scale);
            grfx.TranslateTransform(-Rectangle.Width / 2, -Rectangle.Height / 2);

            RectangleF ellipse = new RectangleF(0, 0, Rectangle.Width, Rectangle.Height);
            Pen pen = new Pen(StrokeColor, Stroke);
            Color color = Color.FromArgb(255 - Transparency, FillColor);

            Brush brush;
            if (Color1Gradient != Color.Empty && Color2Gradient != Color.Empty)
            {
                brush = new LinearGradientBrush(ellipse, Color1Gradient, Color2Gradient, LinearGradientMode.Horizontal);
            }
            else
            {
                brush = new SolidBrush(color);
            }

            grfx.FillEllipse(brush, ellipse);
            grfx.DrawEllipse(pen, ellipse);

            grfx.Transform = oldTransform;
        }

        public override void ChangeSize(float scale)
        {
            Width = OriginalWidth + scale;
            Height = OriginalHeight + scale;
        }

        public override Shape Clone()
        {
            return new EllipseShape(this);
        }
    }
}
