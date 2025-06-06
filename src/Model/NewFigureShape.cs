using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace Draw.src.Model
{
    internal class NewFigureShape : Shape
    {
        #region Constructor
        public NewFigureShape() { }
        public NewFigureShape(RectangleF rect) : base(rect)
        {
        }

        public NewFigureShape(NewFigureShape rect) : base(rect)
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

            RectangleF circle = new RectangleF(0, 0, Rectangle.Width, Rectangle.Height);
            Pen pen = new Pen(StrokeColor, Stroke);
            Color color = Color.FromArgb(255 - Transparency, FillColor);

            Brush brush;
            if (Color1Gradient != Color.Empty && Color2Gradient != Color.Empty)
            {
                brush = new LinearGradientBrush(circle, Color1Gradient, Color2Gradient, LinearGradientMode.Horizontal);
            }
            else
            {
                brush = new SolidBrush(color);
            }
            PointF point1 = new PointF(circle.X + circle.Width /2 , circle.Y);
            PointF point2 = new PointF(circle.X + circle.Width /2 , circle.Y + circle.Height);
            PointF point3 = new PointF(circle.X + circle.Width * 0.15f , circle.Y + circle.Height*0.15f);
            PointF point4 = new PointF(circle.X + circle.Width * 0.85f , circle.Y + circle.Height * 0.85f);
            PointF point5 = new PointF(circle.X + circle.Width * 0.15f , circle.Y + circle.Height * 0.85f);
            PointF point6 = new PointF(circle.X + circle.Width * 0.85f , circle.Y + circle.Height * 0.15f);

            grfx.FillEllipse(brush, circle);
            grfx.DrawEllipse(pen, circle);
            grfx.DrawLine(pen, point1, point2);
            grfx.DrawLine(pen, point3, point4);
            grfx.DrawLine(pen, point5, point6);


            grfx.Transform = oldTransform;
        }

        public override void ChangeSize(float scale)
        {
            Width = OriginalWidth + scale;
            Height = OriginalHeight + scale;
        }

        public override Shape Clone()
        {
            return new NewFigureShape(this);
        }
    }
}
