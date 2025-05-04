using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Базовия клас на примитивите, който съдържа общите характеристики на примитивите.
	/// </summary>
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
			this.Height = shape.Height;
			this.Width = shape.Width;
			this.Location = shape.Location;
			this.rectangle = shape.rectangle;
			
			this.FillColor =  shape.FillColor;
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

        private int transparency;
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

        private Matrix rotation = new Matrix();
        public virtual Matrix Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        private float rotateDegree;
        public virtual float RotateDegree
        {
            get { return rotateDegree; }
            set { rotateDegree = value; }
        }
        public float OriginalWidth { get; set; }
        public float OriginalHeight { get; set; }

        private float scale = 0;
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
			return Rectangle.Contains(point.X, point.Y);
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
    }
}
