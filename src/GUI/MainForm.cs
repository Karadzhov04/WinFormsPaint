using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using Draw.src.Model.Helpers;
using System.Linq;
using System.Drawing.Drawing2D;
using Draw.src.GUI;
using Draw.src.Model.Actions;


namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();
		
		public MainForm()
		{
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;

            this.MouseClick += Form1_MouseClick;
            foreach (Control c in this.Controls)
            {
                c.MouseClick += Form1_MouseClick;
            }
            this.KeyPreview = true;

            this.KeyDown += MainForm_KeyDown;

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        /// <summary>
        /// Изход от програмата. Затваря главната форма, а с това и програмата.
        /// </summary>
        void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
            if (dialogProcessor.IsMarqueeSelecting && dialogProcessor.SelectionRectangle.Width > 0 && dialogProcessor.SelectionRectangle.Height > 0)
            {
                using (Pen pen = new Pen(Color.Blue) { DashStyle = DashStyle.Dash })
                {
                    e.Graphics.DrawRectangle(pen, dialogProcessor.SelectionRectangle);
                }
            }

            dialogProcessor.ReDraw(sender, e);
		}

        /// <summary>
        /// Бутон, който поставя на произволно място правоъгълник със зададените размери.
        /// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
        /// </summary>
        void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
            string newName;
            bool isUnique = false;

            while (!isUnique)
            {
                newName = Microsoft.VisualBasic.Interaction.InputBox("Въведете име:");

                if (string.IsNullOrWhiteSpace(newName))
                {
                    // Прекъсваме ако потребителят не е въвел нищо (или е натиснал Cancel)
                    return;
                }

                if (!dialogProcessor.AllNames.Contains(newName))
                {
                    isUnique = true;

                    dialogProcessor.AllNames.Add(newName);
                    dialogProcessor.AddRandomRectangle(newName);

                    statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";
                    UpdateShapeComboBox();
                    dialogProcessor.OnModelChanged();
                    viewPort.Invalidate();
                }
                else
                {
                    MessageBox.Show("Името е заето! Моля, опитайте с друго име.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
        /// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
        /// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
        /// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
        /// </summary>
        void ViewPortMouseDown(object sender, MouseEventArgs e)
        {
            if (pickUpSpeedButton.Checked)
            {
                Shape clickedShape = dialogProcessor.FindDeepestShapeAt(e.Location);

                if (clickedShape != null)
                {
                    // Цъкнато върху фигура
                    if (Control.ModifierKeys == Keys.Control)
                    {
                        if (dialogProcessor.Selection.Contains(clickedShape))
                            dialogProcessor.Selection.Remove(clickedShape);
                        else
                            dialogProcessor.Selection.Add(clickedShape);
                    }
                    else
                    {
                        dialogProcessor.Selection.Clear();
                        dialogProcessor.Selection.Add(clickedShape);
                    }

                    dialogProcessor.IsDragging = true;
                    dialogProcessor.LastLocation = e.Location;

                    if (dialogProcessor.Selection.Count > 0)
                    {
                        statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
                        LoadShapeProperties(dialogProcessor.Selection[0]);
                    }
                }
                else
                {
                    // Няма фигура под курсора
                    if (dialogProcessor.Selection.Count > 0)
                    {
                        // Проверка дали кликът е вътре в общия bounding box на селекцията
                        RectangleF selectionBounds = GetSelectionBounds(dialogProcessor.Selection);
                        if (selectionBounds.Contains(e.Location))
                        {
                            // Просто искаме да влачим селекцията
                            dialogProcessor.IsDragging = true;
                            dialogProcessor.LastLocation = e.Location;
                            return;
                        }
                    }

                    // Ако не е върху селекцията → започваме нова селекция с правоъгълник
                    dialogProcessor.Selection.Clear();
                    dialogProcessor.StartSelectionPoint = e.Location;
                    dialogProcessor.IsMarqueeSelecting = true;
                }

                viewPort.Invalidate();
                return;
            }

            // Ако не сме в режим pickUp
            dialogProcessor.Selection.Clear();
            dialogProcessor.StartSelectionPoint = e.Location;
            dialogProcessor.IsMarqueeSelecting = true;

            dialogProcessor.OnModelChanged();
            viewPort.Invalidate();
        }
        private RectangleF GetSelectionBounds(List<Shape> selection)
        {
            if (selection == null || selection.Count == 0)
                return RectangleF.Empty;

            float minX = float.MaxValue, minY = float.MaxValue;
            float maxX = float.MinValue, maxY = float.MinValue;

            foreach (var shape in selection)
            {
                RectangleF r = shape.Rectangle;
                if (r.Left < minX) minX = r.Left;
                if (r.Top < minY) minY = r.Top;
                if (r.Right > maxX) maxX = r.Right;
                if (r.Bottom > maxY) maxY = r.Bottom;
            }

            float padding = 5;
            return new RectangleF(minX - padding, minY - padding,
                                  (maxX - minX) + 2 * padding, (maxY - minY) + 2 * padding);
        }
        private void LoadShapeProperties(Shape shape)
        {
            if (shape == null) return;

            txtX.Text = shape.Location.X.ToString();
            txtY.Text = shape.Location.Y.ToString();
            txtStrokeColor.Text = ColorTranslator.ToHtml(shape.StrokeColor);
            txtFillColor.Text = ColorTranslator.ToHtml(shape.FillColor);
        }

        private void ClearShapeProperties()
        {
            txtX.Text = "";
            txtY.Text = "";
            txtStrokeColor.Text = "";
            txtFillColor.Text = "";
        }

        /// <summary>
        /// Прихващане на преместването на мишката.
        /// Ако сме в режм на "влачене", то избрания елемент се транслира.
        /// </summary>
        void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dialogProcessor.IsDragging && dialogProcessor.Selection.Count > 0)
            {
                statusBar.Items[0].Text = "Последно действие: Влачене";
                dialogProcessor.TranslateSelection(e.Location);
                dialogProcessor.OnModelChanged();
                viewPort.Invalidate();
            }

            if (dialogProcessor.IsMarqueeSelecting)
            {
                Point currentPoint = e.Location;
                int x = Math.Min(dialogProcessor.StartSelectionPoint.X, currentPoint.X);
                int y = Math.Min(dialogProcessor.StartSelectionPoint.Y, currentPoint.Y);
                int width = Math.Abs(dialogProcessor.StartSelectionPoint.X - currentPoint.X);
                int height = Math.Abs(dialogProcessor.StartSelectionPoint.Y - currentPoint.Y);

                dialogProcessor.SelectionRectangle = new Rectangle(x, y, width, height);
                dialogProcessor.OnModelChanged();
                viewPort.Invalidate(); 
            }

        }
        /// <summary>
        /// Прихващане на отпускането на бутона на мишката.
        /// Излизаме от режим "влачене".
        /// </summary>
        void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            if (dialogProcessor.IsMarqueeSelecting)
            {
                dialogProcessor.Selection.Clear();

                dialogProcessor.SelectShapesInRectangle(dialogProcessor.SelectionRectangle);

                if (dialogProcessor.Selection.Count == 1)
                {
                    LoadShapeProperties(dialogProcessor.Selection[0]);
                }
                else 
                {
                    ClearShapeProperties();
                }

                dialogProcessor.IsMarqueeSelecting = false;
                pickUpSpeedButton.Checked = true;
                dialogProcessor.OnModelChanged();
                viewPort.Invalidate();
            }

            dialogProcessor.IsDragging = false;

            if (e.Button == MouseButtons.Right)
            {
                PointF mouseLocation = new PointF(e.Location.X, e.Location.Y);
                Shape chosenShape = dialogProcessor.ContainsPoint(mouseLocation);

                if (chosenShape != null)
                {
                    dialogProcessor.Selection.Clear();                   
                    dialogProcessor.Selection.Add(chosenShape);    
                    
                    contextMenuStrip1.Show(viewPort, e.Location);
                }
            }
        }

        private void trackBarStroke_ValueChanged(object sender, EventArgs e)
        {
			if (dialogProcessor.Selection.Count > 0) 
			{
				if (dialogProcessor.Selection[0] is GroupShape group)
				{
					foreach (Shape shape in group.SubShapes)
					{
						shape.Stroke = trackBarStroke.Value;
					}
				}
				else
				{
					foreach (Shape shape in dialogProcessor.Selection)
					{
						shape.Stroke = trackBarStroke.Value;
					}
				}
                dialogProcessor.OnModelChanged();
                viewPort.Invalidate();
            }
        }
        private void ColorPickerButtonClicked(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK && dialogProcessor.Selection.Count > 0)
            {
                Color newColor = colorDialog1.Color;
                List<IAction> actions = new List<IAction>();

                if (dialogProcessor.Selection[0] is GroupShape group)
                {
                    foreach (Shape shape in group.SubShapes)
                    {
                        Color oldColor = shape.FillColor;
                        shape.FillColor = newColor;
                        actions.Add(new ColorChangeAction(shape, oldColor, newColor));
                    }
                }
                else
                {
                    foreach (Shape shape in dialogProcessor.Selection)
                    {
                        Color oldColor = shape.FillColor;
                        shape.FillColor = newColor;
                        actions.Add(new ColorChangeAction(shape, oldColor, newColor));
                    }
                }

                // Добавяне към Undo стека
                foreach (var action in actions)
                    dialogProcessor.undoStack.Push(action);

                dialogProcessor.redoStack.Clear();

                dialogProcessor.OnModelChanged();
                viewPort.Invalidate();
            }
        }


        private void TransparencyButtonClicked(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection.Count > 0)
			{
				if (dialogProcessor.Selection[0] is GroupShape group)
				{
					foreach (Shape shape in group.SubShapes)
					{
						shape.Transparency = trackBarTransparency.Value;
					}
				}
				else
				{
					foreach (Shape shape in dialogProcessor.Selection)
					{
						shape.Transparency = trackBarTransparency.Value;
					}
				}
                dialogProcessor.OnModelChanged();
                viewPort.Invalidate();
			}
        }

        private void StrokeColorPickerClicked(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK && dialogProcessor.Selection.Count > 0)
            {
                ApplyColorToSelection(colorDialog1.Color, isStroke: true);
            }
        }

        private void drawElipseSpeedButtonClick(object sender, EventArgs e)
        {
            string newName;
            bool isUnique = false;

            while (!isUnique)
            {
                newName = Microsoft.VisualBasic.Interaction.InputBox("Въведете име:");

                if (string.IsNullOrWhiteSpace(newName))
                {
                    return;
                }

                if (!dialogProcessor.AllNames.Contains(newName))
                {
                    isUnique = true;

                    dialogProcessor.AllNames.Add(newName);
                    dialogProcessor.AddRandomEllipse(newName);

                    statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";
                    UpdateShapeComboBox();
                    dialogProcessor.OnModelChanged();
                    viewPort.Invalidate();
                }
                else
                {
                    MessageBox.Show("Името е заето! Моля, опитайте с друго име.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void drawStarSpeedButtonClick(object sender, EventArgs e)
        {
            string newName;
            bool isUnique = false;

            while (!isUnique)
            {
                newName = Microsoft.VisualBasic.Interaction.InputBox("Въведете име:");

                if (string.IsNullOrWhiteSpace(newName))
                {
                    return;
                }

                if (!dialogProcessor.AllNames.Contains(newName))
                {
                    isUnique = true;

                    dialogProcessor.AllNames.Add(newName);
                    dialogProcessor.AddRandomStar(newName);

                    statusBar.Items[0].Text = "Последно действие: Рисуване на звезда";
                    UpdateShapeComboBox();
                    dialogProcessor.OnModelChanged();
                    viewPort.Invalidate();
                }
                else
                {
                    MessageBox.Show("Името е заето! Моля, опитайте с друго име.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void GradientColor1PickerClicked(object sender, EventArgs e)
        {
            if (colorDialog3.ShowDialog() == DialogResult.OK && dialogProcessor.Selection.Count > 0)
            {
				if (dialogProcessor.Selection[0] is GroupShape group)
				{
					foreach (Shape shape in group.SubShapes)
					{
						shape.Color1Gradient = colorDialog3.Color;
					}
				}
				else
				{
					foreach (Shape shape in dialogProcessor.Selection)
					{
						shape.Color1Gradient = colorDialog3.Color;
					}
				}
                dialogProcessor.OnModelChanged();
                viewPort.Invalidate();
            }
        }

        private void GradientColor2PickerClicked(object sender, EventArgs e)
        {
            if (colorDialog3.ShowDialog() == DialogResult.OK && dialogProcessor.Selection.Count > 0)
            {
				if (dialogProcessor.Selection[0] is GroupShape group)
				{
					foreach (Shape shape in group.SubShapes)
					{
						shape.Color2Gradient = colorDialog3.Color;
					}
				}
				else
				{
					foreach (Shape shape in dialogProcessor.Selection)
					{
						shape.Color2Gradient = colorDialog3.Color;
					}
				}
                dialogProcessor.OnModelChanged();
                viewPort.Invalidate();
            }
        }

        private void drawLineSpeedButtonClick(object sender, EventArgs e)
        {
            string newName;
            bool isUnique = false;

            while (!isUnique)
            {
                newName = Microsoft.VisualBasic.Interaction.InputBox("Въведете име:");

                if (string.IsNullOrWhiteSpace(newName))
                {
                    return;
                }

                if (!dialogProcessor.AllNames.Contains(newName))
                {
                    isUnique = true;

                    dialogProcessor.AllNames.Add(newName);
                    dialogProcessor.AddRandomLine(newName);

                    statusBar.Items[0].Text = "Последно действие: Рисуване на линия";
                    UpdateShapeComboBox();
                    dialogProcessor.OnModelChanged();
                    viewPort.Invalidate();
                }
                else
                {
                    MessageBox.Show("Името е заето! Моля, опитайте с друго име.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void drawPointSpeedButtonClick(object sender, EventArgs e)
        {
            string newName;
            bool isUnique = false;

            while (!isUnique)
            {
                newName = Microsoft.VisualBasic.Interaction.InputBox("Въведете име:");

                if (string.IsNullOrWhiteSpace(newName))
                {
                    return;
                }

                if (!dialogProcessor.AllNames.Contains(newName))
                {
                    isUnique = true;

                    dialogProcessor.AllNames.Add(newName);
                    dialogProcessor.AddRandomPoint(newName);

                    statusBar.Items[0].Text = "Последно действие: Рисуване на точка";
                    UpdateShapeComboBox();
                    dialogProcessor.OnModelChanged();
                    viewPort.Invalidate();
                }
                else
                {
                    MessageBox.Show("Името е заето! Моля, опитайте с друго име.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private bool isWaitingForClick = false;

        private void CheckPointInPolygonClick(object sender, EventArgs e)
        {
            isWaitingForClick = true;
            MessageBox.Show("Цъкнете някъде в прозореца, за да проверите дали точката е във фигура.");
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isWaitingForClick)
            {
                isWaitingForClick = false;

                PointF mousePoint = e.Location; 

                bool insidePoint = false;
                if (dialogProcessor.Selection.Count == 0)
                {
                    MessageBox.Show("Няма избрана фигура!");
                    return;
                }
                foreach (Shape shape in dialogProcessor.Selection)
                {
                   if (shape.Contains(mousePoint))
                    {
                        MessageBox.Show("Точката е във фигура!");
                        insidePoint = true;
                    }
                }

                if(!insidePoint)
                MessageBox.Show("Точката не е в нито една фигура.");

            }
        }
        private void rotatePrimitiveSpeedButton_Click(object sender, EventArgs e)
        {
            float angle = float.Parse(Microsoft.VisualBasic.Interaction.InputBox("Въведете стойност:"));

            List<IAction> actions = new List<IAction>();

            if (dialogProcessor.Selection.Count == 1 && dialogProcessor.Selection[0] is GroupShape group)
            {
                group.RotateGroup(angle);
            }
            else if (dialogProcessor.Selection.Count > 0)
            {
                foreach (Shape shape in dialogProcessor.Selection)
                {
                    float oldAngle = shape.RotateDegree;

                    shape.RotateDegree += angle;

                    PointF center = new PointF(
                        shape.Location.X + shape.Width / 2,
                        shape.Location.Y + shape.Height / 2
                    );

                    shape.Rotation = new Matrix();
                    shape.Rotation.RotateAt(shape.RotateDegree, center);

                    float newAngle = shape.RotateDegree;

                    actions.Add(new RotateAction(shape, oldAngle, newAngle));
                }
            }
            foreach (var action in actions)
                dialogProcessor.undoStack.Push(action);

            dialogProcessor.redoStack.Clear();

            dialogProcessor.OnModelChanged();
            viewPort.Invalidate();
        }


        private void numericUpDownScaleChange(object sender, EventArgs e)
        {
            float scale = (float)numericUpDownScale.Value;

            if (dialogProcessor.Selection.Count > 0) 
            {
                if (dialogProcessor.Selection.Count == 1 && dialogProcessor.Selection[0] is GroupShape group)
                {
                    foreach (Shape shape in group.SubShapes)
                    {
                        shape.Scale = scale;
                    }
                }
                foreach (Shape shape in dialogProcessor.Selection)
                {
                    shape.Scale = scale;
                }            
            }
            dialogProcessor.OnModelChanged();
            viewPort.Invalidate();
        }
        private void RemoveGradientsButton_Click(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection.Count > 0)
            {
				if (dialogProcessor.Selection[0] is GroupShape group)
				{
					foreach (Shape shape in group.SubShapes)
					{
						shape.Color1Gradient = Color.Empty;
						shape.Color2Gradient = Color.Empty;
					}
				}
				else
				{
					foreach (Shape shape in dialogProcessor.Selection)
					{
						shape.Color1Gradient = Color.Empty;
						shape.Color2Gradient = Color.Empty;
					}
				}
                dialogProcessor.OnModelChanged();
                viewPort.Invalidate();
            }
        }

        private void GroupingButton_Click(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection.Count == 1 && dialogProcessor.Selection[0] is GroupShape)
            {
                dialogProcessor.UngroupSelectedShape();
            }
            else
            {

                dialogProcessor.GroupSelectedShapes();
            }
            //UpdateShapeComboBox();
            dialogProcessor.OnModelChanged();
            viewPort.Invalidate();
        }

		private void SearchPrimitive(object sender, EventArgs e)
		{
			string name = textBoxNameObject.Text.Trim();
			Shape searchedShape = dialogProcessor.ShapeList.FirstOrDefault(s => s.Name == name);
			if (searchedShape != null)
			{
				dialogProcessor.Selection.Clear();
				dialogProcessor.Selection.Add(searchedShape);

				if (dialogProcessor.Selection.Count > 0)
				{
					pickUpSpeedButton.Checked = true;
					statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
					dialogProcessor.IsDragging = true;
					dialogProcessor.LastLocation = searchedShape.Location;
                    dialogProcessor.OnModelChanged();
                    viewPort.Invalidate();
				}
			}	
		}

        private void giveColor(object sender, EventArgs e)
        {
            string colorName = textBoxNameObject.Text.Trim();
            Color newColor = Color.FromName(colorName);

            if (!newColor.IsKnownColor)
            {
                MessageBox.Show("Invalid color name!");
                return;
            }

            if (dialogProcessor.Selection.Count > 0)
            {
                ApplyColorToSelection(newColor, isStroke: false);
            }
        }


        private void GiveColorViaRGBA(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Въведете RGBA стойности (пример: 255,0,0,128)", "Цвят");
            var parts = input.Split(',').Select(p => p.Trim()).ToArray();

            if (parts.Length == 4 &&
                byte.TryParse(parts[0], out byte r) &&
                byte.TryParse(parts[1], out byte g) &&
                byte.TryParse(parts[2], out byte b) &&
                byte.TryParse(parts[3], out byte a))
            {
                Color color = Color.FromArgb(a, r, g, b);

                if (dialogProcessor.Selection.Count > 0)
                {
                    ApplyColorToSelection(color, isStroke: false);
                }
            }
            else
            {
                MessageBox.Show("Невалиден формат. Моля, въведете RGBA стойности коректно.");
            }
        }
        private void ApplyColorToSelection(Color newColor, bool isStroke)
        {
            List<IAction> actions = new List<IAction>();

            if (dialogProcessor.Selection.Count > 0)
            {
                var shapes = new List<Shape>();

                if (dialogProcessor.Selection[0] is GroupShape group)
                    shapes.AddRange(group.SubShapes);
                else
                    shapes.AddRange(dialogProcessor.Selection);

                foreach (var shape in shapes)
                {
                    Color oldColor = isStroke ? shape.StrokeColor : shape.FillColor;

                    if (isStroke)
                        shape.StrokeColor = newColor;
                    else
                        shape.FillColor = newColor;

                    actions.Add(new ColorChangeAction(shape, oldColor, newColor));
                }
            }

            foreach (var action in actions)
                dialogProcessor.undoStack.Push(action);

            dialogProcessor.redoStack.Clear();

            dialogProcessor.OnModelChanged();
            viewPort.Invalidate();
        }

        private void TranslateViaCoords(object sender, EventArgs e)
        {
            string dxInput = Microsoft.VisualBasic.Interaction.InputBox("Въведете отместване по X (ΔX)", "Транслация");
            string dyInput = Microsoft.VisualBasic.Interaction.InputBox("Въведете отместване по Y (ΔY)", "Транслация");

            if (float.TryParse(dxInput, out float dx) && float.TryParse(dyInput, out float dy))
            {
				PointF location = new PointF(dx, dy);
                dialogProcessor.TranslateTo(location);
                dialogProcessor.OnModelChanged();
                viewPort.Invalidate();
            }
            else
            {
                MessageBox.Show("Невалидно число за ΔX или ΔY!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeletePrimitive(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
					"Сигурни ли сте, че искате да изтриете избрания(те) обект(и)?",
					"Потвърждение за изтриване",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Warning
				);

            if (result != DialogResult.Yes)
            {
                return;
            }
            var selectedShapes = dialogProcessor.Selection.ToList();

            foreach (Shape shape in selectedShapes)
            {
                dialogProcessor.ShapeList.Remove(shape);
            }

            dialogProcessor.Selection.Clear();
            UpdateShapeComboBox();
            dialogProcessor.OnModelChanged();
            dialogProcessor.AllNames.Clear();
            viewPort.Invalidate();

            ClearShapeProperties();
        }

        private void UndoClicked(object sender, EventArgs e)
        {
            dialogProcessor.Undo();
            dialogProcessor.OnModelChanged();
            viewPort.Invalidate();
        }

        private void RedoClicked(object sender, EventArgs e)
        {
            dialogProcessor.Redo();
            dialogProcessor.OnModelChanged();
            viewPort.Invalidate();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                dialogProcessor.Undo();
                e.Handled = true;
            }

            else if (e.KeyCode == Keys.Delete)
            {
                DeletePrimitive(sender, e);
                e.Handled = true;
            }

            else if (e.Control && e.KeyCode == Keys.A)
            {
                dialogProcessor.Selection.Clear();
                foreach (var shape in dialogProcessor.ShapeList)
                {
                    dialogProcessor.Selection.Add(shape);
                }
                e.Handled = true;
            }

            else if (e.KeyCode == Keys.Escape)
            {
                dialogProcessor.Selection.Clear();
                e.Handled = true;
            }

            int moveStep = 5;
            if (dialogProcessor.Selection.Count > 0)
            {
                if (e.KeyCode == Keys.Left)
                {
                    dialogProcessor.TranslateSelection(-moveStep, 0);
                }
                else if (e.KeyCode == Keys.Right)
                {
                    dialogProcessor.TranslateSelection(moveStep, 0);
                }
                else if (e.KeyCode == Keys.Up)
                {
                    dialogProcessor.TranslateSelection(0, -moveStep);
                }
                else if (e.KeyCode == Keys.Down)
                {
                    dialogProcessor.TranslateSelection(0, moveStep);
                }
                else if (e.Control && e.KeyCode == Keys.C)
                {
                    dialogProcessor.CopySelection();
                }
                else if (e.Control && e.KeyCode == Keys.V)
                {
                    dialogProcessor.PasteClipboard();
                }

                e.Handled = true;
            }
            dialogProcessor.OnModelChanged();
            viewPort.Invalidate();
        }

        private void CopyObj(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection.Count > 0)
            {
                dialogProcessor.CopySelection();
                dialogProcessor.OnModelChanged();
                viewPort.Invalidate();
            }

        }

        private void PasteObj(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection.Count > 0)
            {
                dialogProcessor.PasteClipboard();
                UpdateShapeComboBox();
                dialogProcessor.OnModelChanged();
                viewPort.Invalidate();
            }
        }

        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection.Count == 0) return;

            Shape selectedShape = dialogProcessor.Selection[0];

            try
            {
                float x = float.Parse(txtX.Text);
                float y = float.Parse(txtY.Text);
                Color stroke = ColorTranslator.FromHtml(txtStrokeColor.Text);
                Color fill = ColorTranslator.FromHtml(txtFillColor.Text);

                selectedShape.Location = new PointF(x, y);
                selectedShape.StrokeColor = stroke;
                selectedShape.FillColor = fill;

                dialogProcessor.OnModelChanged();
                viewPort.Invalidate(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при прилагане: " + ex.Message);
            }
        }

        private void openNewViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageViewForm viewForm = new ImageViewForm(this.dialogProcessor); 
                                                                              
            viewForm.StartPosition = FormStartPosition.Manual;
            viewForm.Location = new Point(this.Location.X + 100, this.Location.Y + 100);
            viewForm.Size = new Size(600, 500);
            viewForm.Owner = this;

            viewForm.Show();

        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON файлове (*.json)|*.json";
            saveFileDialog.Title = "Запази файла";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                dialogProcessor.SaveToJson(saveFileDialog.FileName);
                MessageBox.Show("Файлът е записан успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON файлове (*.json)|*.json";
            ofd.Title = "Зареди файл";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dialogProcessor.LoadFromJson(ofd.FileName);
                    dialogProcessor.OnModelChanged();
                    viewPort.Invalidate(); 
                    MessageBox.Show("Файлът е зареден успешно.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Грешка при зареждане: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnSaveBinary_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Binary файлове (*.bin)|*.bin";
            sfd.Title = "Запази като двоичен файл";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dialogProcessor.SaveToBinary(sfd.FileName);
                    MessageBox.Show("Файлът е запазен успешно.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Грешка при запис: " + ex.Message);
                }
            }
        }
        private void btnLoadBinary_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Binary файлове (*.bin)|*.bin";
            ofd.Title = "Зареди двоичен файл";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dialogProcessor.LoadFromBinary(ofd.FileName);

                    dialogProcessor.OnModelChanged();
                    viewPort.Invalidate();
                    MessageBox.Show("Файлът е зареден успешно.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Грешка при зареждане: " + ex.Message);
                }
            }
        }

        private void comboBoxShapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedName = comboBoxShapes.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedName)) return;

            Shape selectedShape = dialogProcessor.ShapeList
                .FirstOrDefault(shape => shape.Name == selectedName);

            if (selectedShape != null)
            {
                dialogProcessor.Selection.Clear();
                dialogProcessor.Selection.Add(selectedShape);

                dialogProcessor.OnModelChanged();
                viewPort.Invalidate(); 
                statusBar.Items[0].Text = $"Селектирана фигура: {selectedShape.Name}";
            }
        }

        private void UpdateShapeComboBox()
        {
            comboBoxShapes.Items.Clear();

            foreach (Shape shape in dialogProcessor.ShapeList)
            {
                AddShapeToComboBoxRecursive(shape);
            }

            if (comboBoxShapes.Items.Count > 0)
            {
                comboBoxShapes.SelectedIndex = 0;
            }
        }
        private void AddShapeToComboBoxRecursive(Shape shape)
        {
            if (shape is GroupShape group)
            {
                foreach (Shape inner in group.SubShapes)
                {
                    AddShapeToComboBoxRecursive(inner);
                }
            }
            else
            {
                comboBoxShapes.Items.Add(shape.Name);
            }
        }
        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.SaveViewPortAsImage(this.viewPort);
        }

        private void drawNewShapeButton_Clicked(object sender, EventArgs e)
        {
            string newName;
            bool isUnique = false;

            while (!isUnique)
            {
                newName = Microsoft.VisualBasic.Interaction.InputBox("Въведете име:");

                if (string.IsNullOrWhiteSpace(newName))
                {
                    return;
                }

                if (!dialogProcessor.AllNames.Contains(newName))
                {
                    isUnique = true;

                    dialogProcessor.AllNames.Add(newName);
                    dialogProcessor.AddRandomFigure(newName);

                    statusBar.Items[0].Text = "Последно действие: Рисуване на нова фигура";
                    UpdateShapeComboBox();
                    dialogProcessor.OnModelChanged();
                    viewPort.Invalidate();
                }
                else
                {
                    MessageBox.Show("Името е заето! Моля, опитайте с друго име.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}