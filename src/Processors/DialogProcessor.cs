using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor
		
		public DialogProcessor()
		{
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Избран елемент.
		/// </summary>
		private List<Shape> selection = new List<Shape>();
		public List<Shape> Selection {
			get { return selection; }
			set { selection = value; }
		}
		
		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
		
		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation {
			get { return lastLocation; }
			set { lastLocation = value; }
		}
		
		#endregion
		
		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>
		public void AddRandomRectangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100,1000);
			int y = rnd.Next(100,600);
			
			RectangleShape rect = new RectangleShape(new Rectangle(x,y,100,200));
			rect.FillColor = Color.White;

			ShapeList.Add(rect);
		}

        public void AddRandomStar()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            StarShape star = new StarShape(new Rectangle(x, y, 100, 200));
            star.FillColor = Color.White;

            ShapeList.Add(star);
        }

        public void AddRandomEllipse()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, 100, 200));
            ellipse.FillColor = Color.White;

            ShapeList.Add(ellipse);
        }


        public void AddRandomLine()
        {
            Random rnd = new Random();
			float x = rnd.Next(100, 1000);
			float y = rnd.Next(100, 600);
			float x2 = rnd.Next(100, 1000);
			float y2 = rnd.Next(100, 600);

			// Гарантираме, че RectangleF ще има правилна позиция и размери
			float rectX = Math.Min(x, x2);
			float rectY = Math.Min(y, y2);
			float width = Math.Abs(x2 - x);
			float height = Math.Abs(y2 - y);

			LineShape line = new LineShape(new RectangleF(rectX, rectY, width, height));
			ShapeList.Add(line);
        }

        public void AddRandomPoint()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            float size = 5; // Размер на точката
            PointShape point = new PointShape(new RectangleF(x - size / 2, y - size / 2, size, size));
            ShapeList.Add(point);
        }
        /// <summary>
        /// Проверява дали дадена точка е в елемента.
        /// Обхожда в ред обратен на визуализацията с цел намиране на
        /// "най-горния" елемент т.е. този който виждаме под мишката.
        /// </summary>
        /// <param name="point">Указана точка</param>
        /// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
        public Shape ContainsPoint(PointF point)
        {
            for (int i = ShapeList.Count - 1; i >= 0; i--)
            {
                if (ShapeList[i].Contains(point))
                {
                    //ShapeList[i].FillColor = Color.Red;

                    return ShapeList[i];
                }
            }
            return null;
        }
        //public List<Shape> ContainsPoint(PointF point)
        //{
        //    List<Shape> selectedShapes = new List<Shape>();

        //    for (int i = ShapeList.Count - 1; i >= 0; i--) // Започваме отгоре надолу
        //    {
        //        if (ShapeList[i].Contains(point))
        //        {
        //            selectedShapes.Add(ShapeList[i]); // Добавяме всички фигури, които съдържат точката
        //        }
        //    }

        //    return selectedShapes; // Връщаме списък с всички попаднали фигури
        //}

        /// <summary>
        /// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
        /// </summary>
        /// <param name="p">Вектор на транслация.</param>
        //      public void TranslateTo(PointF p)
        //{
        //	if (selection.Count > 0) {
        //		foreach (Shape shape in selection)
        //		{
        //                  shape.Location = new PointF(shape.Location.X + p.X - lastLocation.X, shape.Location.Y + p.Y - lastLocation.Y);	
        //		}

        //              lastLocation = p;
        //          }
        public void TranslateTo(PointF newLocation)
        {
            if (Selection.Count == 0) return;

            // Изчисляваме разликата спрямо последната позиция
            float dx = newLocation.X - LastLocation.X;
            float dy = newLocation.Y - LastLocation.Y;

            foreach (var shape in Selection)
            {
                    // Взимаме центъра на текущата фигура
                PointF center = new PointF(
                    shape.Location.X + shape.Width / 2,
                    shape.Location.Y + shape.Height / 2
                );

                // Създаваме обратната трансформация за всяка фигура
                Matrix inverseRotation = shape.Rotation.Clone();
                inverseRotation.Invert();

                // Преобразуваме вектора на изместване спрямо ротацията
                PointF[] delta = new PointF[] { new PointF(dx, dy) };
                inverseRotation.TransformVectors(delta);

                // Актуализираме позицията на фигурата
                shape.Location = new PointF(shape.Location.X + delta[0].X, shape.Location.Y + delta[0].Y);

            }

            // Обновяваме последната позиция
            LastLocation = newLocation;
        }
        public void GroupSelectedShapes()
        {
            if (Selection.Count > 1)
            {
                float minX = float.PositiveInfinity;
                float minY = float.PositiveInfinity;
                float maxX = float.NegativeInfinity;
                float maxY = float.NegativeInfinity;

                foreach (Shape shape in Selection)
                { 
                    if(shape.Location.X < minX) minX = shape.Location.X;    
                    if(shape.Location.Y < minY) minY = shape.Location.Y;
                    
                    if( shape.Location.X + shape.Width > maxX) maxX = shape.Location.X + shape.Width;

                    if( shape.Location.Y + shape.Height > maxY) maxY = shape.Location.Y + shape.Height;
                }

                RectangleF groupBounds = new RectangleF(minX, minY, maxX - minX, maxY - minY);

                GroupShape group = new GroupShape(groupBounds);

                // Добавяме фигурите към групата
                foreach (var shape in Selection)
                {
                    group.SubShapes.Add(shape);
                }

                // Обновяваме селекцията и ShapeList
                Selection.Clear();
                Selection.Add(group);
                ShapeList.Add(group);
                ShapeList.RemoveAll(shape => group.SubShapes.Contains(shape));
            }
        }

        public void UngroupSelectedShape()
        {
            if (Selection.Count == 1 && Selection[0] is GroupShape group)
            {
                ShapeList.AddRange(group.SubShapes);

                // Премахваме групата от ShapeList
                ShapeList.Remove(group);

                // Обновяваме селекцията
                Selection.Clear();
                Selection.AddRange(group.SubShapes);
            }
        }
        public override void Draw(Graphics grfx)
        {
            base.Draw(grfx);

            if (Selection.Count > 0)
            {
                if (Selection.Count == 1 && Selection[0] is GroupShape group)
                {
                    float minX = float.PositiveInfinity;
                    float minY = float.PositiveInfinity;
                    float maxX = float.NegativeInfinity;
                    float maxY = float.NegativeInfinity;

                    foreach (Shape shape in Selection)
                    {
                        if (shape.Location.X < minX) minX = shape.Location.X;
                        if (shape.Location.Y < minY) minY = shape.Location.Y;
                        if (shape.Location.X + shape.Width > maxX)
                            maxX = shape.Location.X + shape.Width;
                        if (shape.Location.Y + shape.Height > maxY)
                            maxY = shape.Location.Y + shape.Height;
                    }

                    grfx.DrawRectangle(
                        Pens.Red,
                        minX - 5,
                        minY - 5,
                        (maxX - minX) + 10,
                        (maxY - minY) + 10
                    );
                }
            }
        }
    }
}
