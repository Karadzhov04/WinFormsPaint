using Draw.src.Model;
using Draw.src.Model.Actions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Draw
{
    /// <summary>
    /// Класът, който ще бъде използван при управляване на диалога.
    /// </summary>
    [Serializable]
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

        public bool IsMarqueeSelecting { get; set; } = false;
        public Point StartSelectionPoint { get; set; }
        public Rectangle SelectionRectangle { get; set; }


        public Stack<IAction> undoStack = new Stack<IAction>();
        public Stack<IAction> redoStack = new Stack<IAction>();
        public List<Shape> ClipboardShapes { get; set; } = new List<Shape>();


        public event EventHandler ModelChanged;

        public List<string> AllNames = new List<string>();
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

            ModelChanged?.Invoke(this, EventArgs.Empty);
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

            ModelChanged?.Invoke(this, EventArgs.Empty);
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

            ModelChanged?.Invoke(this, EventArgs.Empty);
        }


        public void AddRandomLine(string name)
        {
            Random rnd = new Random();
			float x = rnd.Next(100, 1000);
			float y = rnd.Next(100, 600);
			float x2 = rnd.Next(100, 1000);
			float y2 = rnd.Next(100, 600);

			float rectX = Math.Min(x, x2);
			float rectY = Math.Min(y, y2);
			float width = Math.Abs(x2 - x);
			float height = Math.Abs(y2 - y);

			LineShape line = new LineShape(new RectangleF(rectX, rectY, width, height));
			line.Name = name;

			ShapeList.Add(line);

            ModelChanged?.Invoke(this, EventArgs.Empty);
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

            ModelChanged?.Invoke(this, EventArgs.Empty);
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
					for (int j = group.SubShapes.Count - 1; j >= 0; j--)
					{
						Shape sub = group.SubShapes[j];

						if (sub.Contains(point))
						{
							return sub; 
						}
					}

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

            float dx = newLocation.X - LastLocation.X;
            float dy = newLocation.Y - LastLocation.Y;

            List<MoveAction> actions = new List<MoveAction>();

            foreach (var shape in Selection)
            {
                PointF oldLocation = shape.Location;
                PointF[] delta = new PointF[] { new PointF(dx, dy) };

                Matrix inverseRotation = shape.Rotation.Clone();
                inverseRotation.Invert();
                inverseRotation.TransformVectors(delta);

                PointF center = new PointF(
                    shape.Location.X + shape.Width / 2,
                    shape.Location.Y + shape.Height / 2
                );

                center = new PointF(center.X + delta[0].X, center.Y + delta[0].Y);

                shape.Location = new PointF(
                    center.X - shape.Width / 2,
                    center.Y - shape.Height / 2
                );

                actions.Add(new MoveAction(shape, oldLocation, shape.Location));
            }

            foreach (var action in actions)
                undoStack.Push(action);

            redoStack.Clear();
            LastLocation = newLocation;

            OnModelChanged();
        }


        public void TranslateGroupTo(GroupShape group, PointF newLocation)
		{
			float dx = newLocation.X - LastLocation.X;
			float dy = newLocation.Y - LastLocation.Y;

			foreach (var shape in group.SubShapes)
			{
				Matrix inverseRotation = shape.Rotation.Clone();
				inverseRotation.Invert();

				PointF[] delta = new PointF[] { new PointF(dx, dy) };
				inverseRotation.TransformVectors(delta);

				shape.Location = new PointF(
					shape.Location.X + delta[0].X,
					shape.Location.Y + delta[0].Y
				);
			}

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

                foreach (var shape in Selection)
                {
                    group.SubShapes.Add(shape);
                }

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

                ShapeList.Remove(group);

                Selection.Clear();
                Selection.AddRange(group.SubShapes);
            }
        }
        public override void Draw(Graphics grfx)
        {
            base.Draw(grfx);
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
				group.SubShapes.Remove(shapeToRemove); 
				ShapeList.Add(shapeToRemove); 

				if (group.SubShapes.Count == 0)
				{
					ShapeList.Remove(group);
				}
			}
		}
        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                var action = undoStack.Pop();
                action.Undo();
                redoStack.Push(action);
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                var action = redoStack.Pop();
                action.Redo();
                undoStack.Push(action);
            }
        }

        public Shape GetPrimitiveAtPoint(Point point)
        {
			var shapeList = ShapeList;
            for (int i = shapeList.Count - 1; i >= 0; i--)
            {
                if (shapeList[i].Contains(point))
                {
                    return shapeList[i];
                }
            }
            return null;
        }

        public void TranslateSelection(float dx, float dy)
        {
            foreach (var shape in Selection)
            {
                PointF[] delta = new PointF[] { new PointF(dx, dy) };

                Matrix inverseRotation = shape.Rotation.Clone();
                inverseRotation.Invert();
                inverseRotation.TransformVectors(delta);

                PointF center = new PointF(
                    shape.Location.X + shape.Width / 2,
                    shape.Location.Y + shape.Height / 2
                );

                center = new PointF(center.X + delta[0].X, center.Y + delta[0].Y);
                shape.Location = new PointF(
                    center.X - shape.Width / 2,
                    center.Y - shape.Height / 2
                );
            }
        }


        public void CopySelection()
        {
            ClipboardShapes.Clear();

            foreach (Shape shape in Selection)
            {
                Shape copiedShape = (Shape)shape.Clone();

                string baseName = shape.Name;
                string newName = GenerateUniqueCopyName(baseName);

                copiedShape.Name = newName;

                ClipboardShapes.Add(copiedShape);
            }
        }

        private string GenerateUniqueCopyName(string originalName)
        {
            int copyIndex = 1;
            string newName;

            do
            {
                newName = $"{originalName}_copy{copyIndex}";
                copyIndex++;
            } while (AllNames.Contains(newName));

            return newName;
        }

        public void PasteClipboard()
        {
            if (ClipboardShapes.Count == 0) return;

            List<Shape> newShapes = new List<Shape>();

            foreach (Shape shape in ClipboardShapes)
            {
                Shape newShape = (Shape)shape.Clone();

                PointF[] delta = new PointF[] { new PointF(20, 20) };

                Matrix inverseRotation = newShape.Rotation.Clone();
                inverseRotation.Invert();
                inverseRotation.TransformVectors(delta);

                newShape.Location = new PointF(
                    newShape.Location.X + delta[0].X,
                    newShape.Location.Y + delta[0].Y
                );

                ShapeList.Add(newShape);
                AllNames.Add(newShape.Name); 

                newShapes.Add(newShape);
            }

            Selection.Clear();
            Selection.AddRange(newShapes);
        }

        public Shape FindDeepestShapeAt(PointF point)
        {
            return FindDeepestShapeRecursive(ShapeList, point);
        }

        private Shape FindDeepestShapeRecursive(List<Shape> shapes, PointF point)
        {
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                var shape = shapes[i];

                if (shape is GroupShape group)
                {
                    var inner = FindDeepestShapeRecursive(group.SubShapes, point);
                    if (inner != null)
                    {
                        return group; 
                    }


                    if (group.Rectangle.Contains(point))
                    {
                        return group;
                    }
                }
                else
                {
                    if (shape.Contains(point))
                    {
                        return shape;
                    }
                }
            }

            return null;
        }

        public void SaveToJson(string filePath)
        {
            var shapes = ShapeList;

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All 
            };

            string json = JsonConvert.SerializeObject(shapes, Formatting.Indented, settings);
            File.WriteAllText(filePath, json);
        }

        public void LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            string json = File.ReadAllText(filePath);
            var loadedShapes = JsonConvert.DeserializeObject<List<Shape>>(json, settings);

            if (loadedShapes != null)
            {
                ShapeList.Clear();

                ShapeList = loadedShapes;
            }
        }
        public void SaveToBinary(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, this.ShapeList);
            }
        }
        public void LoadFromBinary(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                this.ShapeList = (List<Shape>)formatter.Deserialize(fs);
            }
           // OnModelChanged(); // извикай събитието за презареждане на изгледа
        }

        public void SelectShapesInRectangle(RectangleF rect, List<Shape> shapeList = null)
        {
            if (shapeList == null)
                shapeList = ShapeList;

            foreach (var shape in shapeList)
            {
                if (shape is GroupShape group)
                {
                    SelectShapesInRectangle(rect, group.SubShapes);
                }
                else
                {
                    if (rect.Contains(Rectangle.Round(shape.Rectangle)))
                    {
                        Selection.Add(shape);
                    }
                }
            }
        }

        public void TranslateSelection(Point newLocation)
        {
            PointF delta = new PointF(newLocation.X - LastLocation.X, newLocation.Y - LastLocation.Y);

            foreach (var shape in Selection)
            {
                shape.Move(delta); 
            }

            LastLocation = newLocation;
        }

        public void OnModelChanged()
        {
            ModelChanged?.Invoke(this, EventArgs.Empty);
        }
        public void SaveViewPortAsImage(DoubleBufferedPanel viewPort)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PNG Image|*.png";
                saveDialog.Title = "Запази изображение";
                saveDialog.FileName = "Изглед.png";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Създаване на Bitmap със същите размери като панела
                    Bitmap bmp = new Bitmap(viewPort.Width, viewPort.Height);
                    viewPort.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                    // Запазване на изображението
                    bmp.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);

                    MessageBox.Show("Изображението е успешно запазено!");
                }
            }
        }

        public void AddRandomFigure(string name)
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            NewFigureShape circle = new NewFigureShape(new Rectangle(x, y, 200, 200));
            circle.FillColor = Color.White;
            circle.Name = name;

            ShapeList.Add(circle);

            ModelChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
