using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
    /// <summary>
    /// Базовия клас на примитивите, който съдържа общите характеристики на примитивите.
    /// </summary>
    [Serializable]
    public abstract class Shape
	{
		#region Constructors
		
		public Shape()
		{
		}
		
		public Shape(RectangleF rect)
		{
			rectangle = rect;
            OriginalWidth = rect.Width;
            OriginalHeight = rect.Height;
        }
		
		public Shape(Shape shape)
		{
            this.Rectangle = shape.Rectangle;
            this.FillColor = shape.FillColor;
            this.Stroke = shape.Stroke;
            this.StrokeColor = shape.StrokeColor;
            this.Transparency = shape.Transparency;
            this.Color1Gradient = shape.Color1Gradient;
            this.Color2Gradient = shape.Color2Gradient;
            this.Rotation = shape.Rotation.Clone(); // ВАЖНО: Matrix е референтен тип
            this.RotateDegree = shape.RotateDegree;
            this.OriginalWidth = shape.OriginalWidth;
            this.OriginalHeight = shape.OriginalHeight;
            this.Scale = shape.Scale;
            this.Name = shape.Name;
        }
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Обхващащ правоъгълник на елемента.
		/// </summary>
		private RectangleF rectangle;		
		public virtual RectangleF Rectangle {
			get { return rectangle; }
			set { rectangle = value; }
		}
		
		/// <summary>
		/// Широчина на елемента.
		/// </summary>
		public virtual float Width {
			get { return Rectangle.Width; }
			set { rectangle.Width = value; }
		}
		
		/// <summary>
		/// Височина на елемента.
		/// </summary>
		public virtual float Height {
			get { return Rectangle.Height; }
			set { rectangle.Height = value; }
		}
		
		/// <summary>
		/// Горен ляв ъгъл на елемента.
		/// </summary>
		public virtual PointF Location {
			get { return Rectangle.Location; }
			set { rectangle.Location = value; }
		}
		
		/// <summary>
		/// Цвят на елемента.
		/// </summary>
		private Color fillColor;		
		public virtual Color FillColor {
			get { return fillColor; }
			set { fillColor = value; }
		}

        private int stroke;
        public virtual int Stroke
        {
            get { return stroke; }
            set { stroke = value; }
        }

        private Color strokeColor = Color.Black;
        public virtual Color StrokeColor
        {
            get { return strokeColor; }
            set { strokeColor = value; }
        }

        private int transparency = 0;
        public virtual int Transparency
        {
            get { return transparency; }
            set { transparency = value; }
        }

        private Color color1Gradient = Color.Empty;
        public virtual Color Color1Gradient
        {
            get { return color1Gradient; }
            set { color1Gradient = value; }
        }

        private Color color2Gradient = Color.Empty;
        public virtual Color Color2Gradient
        {
            get { return color2Gradient; }
            set { color2Gradient = value; }
        }

        [NonSerialized]
        private Matrix rotation;

        public Matrix Rotation
        {
            get { return (rotation ?? new Matrix()).Clone(); }
            set { rotation = value; }
        }

        private float rotateDegree = 0f;
        public virtual float RotateDegree
        {
            get { return rotateDegree; }
            set
            {
                rotateDegree = value;

                // Изчисляваме центъра на фигурата
                PointF center = new PointF(
                    Location.X + Width / 2,
                    Location.Y + Height / 2
                );

                // Обновяваме ротационната матрица
                rotation = new Matrix();
                rotation.RotateAt(rotateDegree, center);
            }
        }
        public float OriginalWidth { get; set; }
        public float OriginalHeight { get; set; }

        private float scale = 1f;
        public virtual float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

		private string name;
		public virtual string Name
		{
			get { return name; }
			set { name = value; }
		}
        #endregion


        /// <summary>
        /// Проверка дали точка point принадлежи на елемента.
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>Връща true, ако точката принадлежи на елемента и
        /// false, ако не пренадлежи</returns>

        public virtual bool Contains(PointF point)
        {
            if (RotateDegree != 0)
            {
                // Център на ротация – обикновено центърът на фигурата
                PointF center = new PointF(Rectangle.X + Rectangle.Width / 2f, Rectangle.Y + Rectangle.Height / 2f);

                // Обратна ротация на точката спрямо центъра
                PointF transformedPoint = RotatePoint(point, center, -RotateDegree);

                // Проверка спрямо оригиналния правоъгълник
                return Rectangle.Contains(transformedPoint.X, transformedPoint.Y);
            }

            return Rectangle.Contains(point.X, point.Y);
        }

        private PointF RotatePoint(PointF point, PointF pivot, float angle)
        {
            double radians = angle * Math.PI / 180.0;
            float dx = point.X - pivot.X;
            float dy = point.Y - pivot.Y;

            float rotatedX = (float)(dx * Math.Cos(radians) - dy * Math.Sin(radians)) + pivot.X;
            float rotatedY = (float)(dx * Math.Sin(radians) + dy * Math.Cos(radians)) + pivot.Y;

            return new PointF(rotatedX, rotatedY);
        }

        public PointF ToLocal(PointF point)
		{
			Matrix transform = this.Rotation.Clone();
			transform.Translate(this.Location.X, this.Location.Y, MatrixOrder.Append);
			transform.Invert();

			PointF[] points = new PointF[] { point };
			transform.TransformPoints(points);
			return points[0];
		}


		/// <summary>
		/// Визуализира елемента.
		/// </summary>
		/// <param name="grfx">Къде да бъде визуализиран елемента.</param>
		public virtual void DrawSelf(Graphics grfx)
		{
			// shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
		}

        public virtual void ChangeSize(float scale)
        {
            
        }
		public abstract Shape Clone();

        public virtual void Move(PointF delta)
        {
            Location = new PointF(Location.X + delta.X, Location.Y + delta.Y);
        }

    }
}
