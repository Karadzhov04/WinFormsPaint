using Draw.src.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
    /// <summary>
    /// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
    /// </summary>
    [Serializable]
    public class RectangleShape : Shape
	{
        #region Constructor
        public RectangleShape() { }
        public RectangleShape(RectangleF rect) : base(rect)
		{
		}
		
		public RectangleShape(RectangleShape rectangle) : base(rectangle)
		{
		}

		#endregion

		/// <summary>
		/// Проверка за принадлежност на точка point към правоъгълника.
		/// В случая на правоъгълник този метод може да не бъде пренаписван, защото
		/// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
		/// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
		/// елемента в този случай).
		/// </summary>
		public override bool Contains(PointF point)
		{
			PointF local = GeometryUtils.ToLocal(point, this);
			Console.WriteLine($"Глобална: {point}, Локална: {local}, RotationAngle: {RotateDegree}");
			Console.WriteLine($"BoundingBox: (0,0,{Width},{Height})");

			return new RectangleF(0, 0, Width, Height).Contains(local);
		}
        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            Matrix oldTransform = grfx.Transform;

            grfx.TranslateTransform(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2);
            grfx.RotateTransform(RotateDegree);
            ChangeSize(Scale);
            //grfx.ScaleTransform(Scale, Scale);
            grfx.TranslateTransform(-Rectangle.Width / 2, -Rectangle.Height / 2);

            RectangleF localRect = new RectangleF(0, 0, Rectangle.Width, Rectangle.Height);
            Pen pen = new Pen(StrokeColor, Stroke);
            Color color = Color.FromArgb(255 - Transparency, FillColor);

            Brush brush;
            if (Color1Gradient != Color.Empty && Color2Gradient != Color.Empty)
            {
                brush = new LinearGradientBrush(localRect, Color1Gradient, Color2Gradient, LinearGradientMode.Horizontal);
            }
            else
            {
                brush = new SolidBrush(color);
            }

            grfx.FillRectangle(brush, localRect);
            grfx.DrawRectangle(pen, 0, 0, Rectangle.Width, Rectangle.Height);

            grfx.Transform = oldTransform;
        }

        public override void ChangeSize(float scale)
        {
            Width = OriginalWidth + scale;
            Height = OriginalHeight + scale;
        }

        public override Shape Clone()
        {
            return new RectangleShape(this);
        }
    }
}

