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

		private List<Shape> allShapes = new List<Shape>();
		public List<Shape> AllShapes
		{
			get { return allShapes; }
			set { allShapes = value; }
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
		public void AddRandomRectangle(string name)
		{
			Random rnd = new Random();
			int x = rnd.Next(100,1000);
			int y = rnd.Next(100,600);
			
			RectangleShape rect = new RectangleShape(new Rectangle(x,y,100,200));
			rect.FillColor = Color.White;
			rect.Name = name;

			ShapeList.Add(rect);
		}

        public void AddRandomStar(string name)
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            StarShape star = new StarShape(new Rectangle(x, y, 100, 200));
            star.FillColor = Color.White;
			star.Name = name;

            ShapeList.Add(star);
        }

        public void AddRandomEllipse(string name)
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, 100, 200));
            ellipse.FillColor = Color.White;
			ellipse.Name = name;

            ShapeList.Add(ellipse);
        }


        public void AddRandomLine(string name)
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
			line.Name = name;

			ShapeList.Add(line);
        }

        public void AddRandomPoint(string name)
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            float size = 5; // Размер на точката
            PointShape point = new PointShape(new RectangleF(x - size / 2, y - size / 2, size, size));

			point.Name = name;
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
				Shape shape = ShapeList[i];

				if (shape is GroupShape group)
				{
					// Първо минаваме през подфигурите
					for (int j = group.SubShapes.Count - 1; j >= 0; j--)
					{
						Shape sub = group.SubShapes[j];

						if (sub.Contains(point))
						{
							return sub; // Връщаме вътрешната фигура, ако я уцелим
						}
					}

					// Ако не е в подфигурите, проверяваме цялата група (по избор)
					if (group.Contains(point))
					{
						return group;
					}
				}
				else if (shape.Contains(point))
				{
					return shape;
				}
			}

			return null;
		}
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
                inverseRotation.TransformVectors(delta);//Ако фигурата не беше завъртяна, в каква посока щеше да е движението?"

				// Актуализираме позицията на фигурата
				shape.Location = new PointF(shape.Location.X + delta[0].X, shape.Location.Y + delta[0].Y);
				/*
				 първо взимаме това колко градуса е завъртяна, после я връщаме в първоначалният вид,
				 където чрез dx и DY и прилагаме правилна посока на векторите в правилната координатна система и даваме тези нови 
				 координати на фигурата
				*/
			}

			// Обновяваме последната позиция
			LastLocation = newLocation;
        }

		public void TranslateGroupTo(GroupShape group, PointF newLocation)
		{
			// Изчислиме правилната разлика спрямо предишната мишка
			float dx = newLocation.X - LastLocation.X;
			float dy = newLocation.Y - LastLocation.Y;

			foreach (var shape in group.SubShapes)
			{
				// Вземи обратната ротация на всяка фигура
				Matrix inverseRotation = shape.Rotation.Clone();
				inverseRotation.Invert();

				// Преобразувай delta в локалната координатна система на фигурата
				PointF[] delta = new PointF[] { new PointF(dx, dy) };
				inverseRotation.TransformVectors(delta);

				shape.Location = new PointF(
					shape.Location.X + delta[0].X,
					shape.Location.Y + delta[0].Y
				);
			}
			// Обнови последната позиция на мишката
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
					if (group.IsRotated)
					{
						// Взимаме въртяните ъгли
						List<PointF> allTransformedPoints = new List<PointF>();

						foreach (var shape in group.SubShapes)
						{
							PointF[] corners = new PointF[]
							{
				                shape.Location,
				                new PointF(shape.Location.X + shape.Width, shape.Location.Y),
				                new PointF(shape.Location.X + shape.Width, shape.Location.Y + shape.Height),
				                new PointF(shape.Location.X, shape.Location.Y + shape.Height)
							};

							shape.Rotation.TransformPoints(corners);
							allTransformedPoints.AddRange(corners);
						}

						float minX = allTransformedPoints.Min(p => p.X);
						float minY = allTransformedPoints.Min(p => p.Y);
						float maxX = allTransformedPoints.Max(p => p.X);
						float maxY = allTransformedPoints.Max(p => p.Y);

						grfx.DrawRectangle(
							Pens.Red,
							minX - 5,
							minY - 5,
							(maxX - minX) + 10,
							(maxY - minY) + 10
						);
					}
					else
					{
						// Класическия bounding box
						float minX = float.PositiveInfinity;
						float minY = float.PositiveInfinity;
						float maxX = float.NegativeInfinity;
						float maxY = float.NegativeInfinity;

						foreach (var shape in group.SubShapes)
						{
							if (shape.Location.X < minX) minX = shape.Location.X;
							if (shape.Location.Y < minY) minY = shape.Location.Y;
							if (shape.Location.X + shape.Width > maxX) maxX = shape.Location.X + shape.Width;
							if (shape.Location.Y + shape.Height > maxY) maxY = shape.Location.Y + shape.Height;
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
		public GroupShape GetParentGroupOfShape(Shape shape)
		{
			foreach (var item in ShapeList)
			{
				if (item is GroupShape group && group.SubShapes.Contains(shape))
				{
					return group;
				}
			}
			return null;
		}

		public void UngroupShapeFromGroup(GroupShape group, Shape shapeToRemove)
		{
			if (group.SubShapes.Contains(shapeToRemove))
			{
				group.SubShapes.Remove(shapeToRemove); // Махаме фигурата от групата
				ShapeList.Add(shapeToRemove); // Добавяме я обратно в основния списък

				// Ако групата остане празна, махаме и нея
				if (group.SubShapes.Count == 0)
				{
					ShapeList.Remove(group);
				}
			}
		}


	}
}
