using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
	/// </summary>
	public class RectangleShape : Shape
	{
		#region Constructor
		
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
			if (base.Contains(point))
				// Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
				// В случая на правоъгълник - директно връщаме true
				return true;
			else
				// Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
				return false;
		}
		
		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
            base.DrawSelf(grfx);

			ChangeSize(Scale);
            RectangleF rect = new RectangleF(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            Color color = Color.FromArgb(255- Transparency, FillColor);
			Pen pen = new Pen(StrokeColor, Stroke);
			LinearGradientBrush brush = new LinearGradientBrush(rect, Color1Gradient, Color2Gradient, LinearGradientMode.Horizontal);

            Matrix oldTransform = grfx.Transform;

            // Прилагаме ротация само на тази фигура
            grfx.Transform = Rotation;

            grfx.FillRectangle(brush, rect);
            grfx.DrawRectangle(pen, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

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
