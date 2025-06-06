namespace Draw
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jSONFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ungroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.strokeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ellipseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.starToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.currentStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.speedMenu = new System.Windows.Forms.ToolStrip();
            this.drawRectangleSpeedButton = new System.Windows.Forms.ToolStripButton();
            this.pickUpSpeedButton = new System.Windows.Forms.ToolStripButton();
            this.drawStarSpeedButton = new System.Windows.Forms.ToolStripButton();
            this.ColorPickerButton = new System.Windows.Forms.ToolStripButton();
            this.drawElipseSpeedButton = new System.Windows.Forms.ToolStripButton();
            this.drawLineSpeedButton = new System.Windows.Forms.ToolStripButton();
            this.drawPointSpeedButton = new System.Windows.Forms.ToolStripButton();
            this.CheckPointInPolygonButton = new System.Windows.Forms.ToolStripButton();
            this.rotatePrimitiveSpeedButton = new System.Windows.Forms.ToolStripButton();
            this.GroupingButton = new System.Windows.Forms.ToolStripButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.trackBarStroke = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarTransparency = new System.Windows.Forms.TrackBar();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.colorDialog3 = new System.Windows.Forms.ColorDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.numericUpDownScale = new System.Windows.Forms.NumericUpDown();
            this.RemoveGradientsButton = new System.Windows.Forms.Button();
            this.textBoxNameObject = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillColorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.strokeColorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.transformToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button9 = new System.Windows.Forms.Button();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStrokeColor = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtFillColor = new System.Windows.Forms.TextBox();
            this.comboBoxShapes = new System.Windows.Forms.ComboBox();
            this.viewPort = new Draw.DoubleBufferedPanel();
            this.button10 = new System.Windows.Forms.Button();
            this.mainMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.speedMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStroke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTransparency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScale)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1328, 28);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.addToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.uploadFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newViewToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.addToolStripMenuItem.Text = "Add ";
            // 
            // newViewToolStripMenuItem
            // 
            this.newViewToolStripMenuItem.Name = "newViewToolStripMenuItem";
            this.newViewToolStripMenuItem.Size = new System.Drawing.Size(155, 26);
            this.newViewToolStripMenuItem.Text = "new View";
            this.newViewToolStripMenuItem.Click += new System.EventHandler(this.openNewViewToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jSONFileToolStripMenuItem,
            this.xMLFileToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // jSONFileToolStripMenuItem
            // 
            this.jSONFileToolStripMenuItem.Name = "jSONFileToolStripMenuItem";
            this.jSONFileToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.jSONFileToolStripMenuItem.Text = "JSON file";
            this.jSONFileToolStripMenuItem.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // xMLFileToolStripMenuItem
            // 
            this.xMLFileToolStripMenuItem.Name = "xMLFileToolStripMenuItem";
            this.xMLFileToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.xMLFileToolStripMenuItem.Text = "Binary file";
            this.xMLFileToolStripMenuItem.Click += new System.EventHandler(this.btnSaveBinary_Click);
            // 
            // uploadFileToolStripMenuItem
            // 
            this.uploadFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jSONToolStripMenuItem,
            this.xMLToolStripMenuItem});
            this.uploadFileToolStripMenuItem.Name = "uploadFileToolStripMenuItem";
            this.uploadFileToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.uploadFileToolStripMenuItem.Text = "Upload file";
            // 
            // jSONToolStripMenuItem
            // 
            this.jSONToolStripMenuItem.Name = "jSONToolStripMenuItem";
            this.jSONToolStripMenuItem.Size = new System.Drawing.Size(133, 26);
            this.jSONToolStripMenuItem.Text = "JSON";
            this.jSONToolStripMenuItem.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // xMLToolStripMenuItem
            // 
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.Size = new System.Drawing.Size(133, 26);
            this.xMLToolStripMenuItem.Text = "Binary";
            this.xMLToolStripMenuItem.Click += new System.EventHandler(this.btnLoadBinary_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transformToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.groupToolStripMenuItem,
            this.ungroupToolStripMenuItem,
            this.setColorToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.addToolStripMenuItem1});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // transformToolStripMenuItem
            // 
            this.transformToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotateToolStripMenuItem});
            this.transformToolStripMenuItem.Name = "transformToolStripMenuItem";
            this.transformToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.transformToolStripMenuItem.Text = "Transform";
            // 
            // rotateToolStripMenuItem
            // 
            this.rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.rotateToolStripMenuItem.Text = "Rotate";
            this.rotateToolStripMenuItem.Click += new System.EventHandler(this.rotatePrimitiveSpeedButton_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeletePrimitive);
            // 
            // groupToolStripMenuItem
            // 
            this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            this.groupToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.groupToolStripMenuItem.Text = "Group";
            this.groupToolStripMenuItem.Click += new System.EventHandler(this.GroupingButton_Click);
            // 
            // ungroupToolStripMenuItem
            // 
            this.ungroupToolStripMenuItem.Name = "ungroupToolStripMenuItem";
            this.ungroupToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.ungroupToolStripMenuItem.Text = "Ungroup";
            this.ungroupToolStripMenuItem.Click += new System.EventHandler(this.GroupingButton_Click);
            // 
            // setColorToolStripMenuItem
            // 
            this.setColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fillColorToolStripMenuItem,
            this.strokeColorToolStripMenuItem});
            this.setColorToolStripMenuItem.Name = "setColorToolStripMenuItem";
            this.setColorToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.setColorToolStripMenuItem.Text = "Set color";
            // 
            // fillColorToolStripMenuItem
            // 
            this.fillColorToolStripMenuItem.Name = "fillColorToolStripMenuItem";
            this.fillColorToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.fillColorToolStripMenuItem.Text = "Fill color";
            this.fillColorToolStripMenuItem.Click += new System.EventHandler(this.ColorPickerButtonClicked);
            // 
            // strokeColorToolStripMenuItem
            // 
            this.strokeColorToolStripMenuItem.Name = "strokeColorToolStripMenuItem";
            this.strokeColorToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.strokeColorToolStripMenuItem.Text = "Stroke color";
            this.strokeColorToolStripMenuItem.Click += new System.EventHandler(this.StrokeColorPickerClicked);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.UndoClicked);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.RedoClicked);
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rectangleToolStripMenuItem,
            this.ellipseToolStripMenuItem,
            this.starToolStripMenuItem,
            this.lineToolStripMenuItem,
            this.pointToolStripMenuItem});
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(165, 26);
            this.addToolStripMenuItem1.Text = "Add Shape";
            // 
            // rectangleToolStripMenuItem
            // 
            this.rectangleToolStripMenuItem.Name = "rectangleToolStripMenuItem";
            this.rectangleToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.rectangleToolStripMenuItem.Text = "Rectangle";
            this.rectangleToolStripMenuItem.Click += new System.EventHandler(this.DrawRectangleSpeedButtonClick);
            // 
            // ellipseToolStripMenuItem
            // 
            this.ellipseToolStripMenuItem.Name = "ellipseToolStripMenuItem";
            this.ellipseToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.ellipseToolStripMenuItem.Text = "Ellipse";
            this.ellipseToolStripMenuItem.Click += new System.EventHandler(this.drawElipseSpeedButtonClick);
            // 
            // starToolStripMenuItem
            // 
            this.starToolStripMenuItem.Name = "starToolStripMenuItem";
            this.starToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.starToolStripMenuItem.Text = "Star";
            this.starToolStripMenuItem.Click += new System.EventHandler(this.drawStarSpeedButtonClick);
            // 
            // lineToolStripMenuItem
            // 
            this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            this.lineToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.lineToolStripMenuItem.Text = "Line";
            this.lineToolStripMenuItem.Click += new System.EventHandler(this.drawLineSpeedButtonClick);
            // 
            // pointToolStripMenuItem
            // 
            this.pointToolStripMenuItem.Name = "pointToolStripMenuItem";
            this.pointToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.pointToolStripMenuItem.Text = "Point";
            this.pointToolStripMenuItem.Click += new System.EventHandler(this.drawPointSpeedButtonClick);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentStatusLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 673);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusBar.Size = new System.Drawing.Size(1328, 22);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "statusStrip1";
            // 
            // currentStatusLabel
            // 
            this.currentStatusLabel.Name = "currentStatusLabel";
            this.currentStatusLabel.Size = new System.Drawing.Size(0, 16);
            // 
            // speedMenu
            // 
            this.speedMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.speedMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawRectangleSpeedButton,
            this.pickUpSpeedButton,
            this.drawStarSpeedButton,
            this.ColorPickerButton,
            this.drawElipseSpeedButton,
            this.drawLineSpeedButton,
            this.drawPointSpeedButton,
            this.CheckPointInPolygonButton,
            this.rotatePrimitiveSpeedButton,
            this.GroupingButton});
            this.speedMenu.Location = new System.Drawing.Point(0, 28);
            this.speedMenu.Name = "speedMenu";
            this.speedMenu.Size = new System.Drawing.Size(1328, 27);
            this.speedMenu.TabIndex = 3;
            this.speedMenu.Text = "toolStrip1";
            // 
            // drawRectangleSpeedButton
            // 
            this.drawRectangleSpeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawRectangleSpeedButton.Image = ((System.Drawing.Image)(resources.GetObject("drawRectangleSpeedButton.Image")));
            this.drawRectangleSpeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawRectangleSpeedButton.Name = "drawRectangleSpeedButton";
            this.drawRectangleSpeedButton.Size = new System.Drawing.Size(29, 24);
            this.drawRectangleSpeedButton.Text = "DrawRectangleButton";
            this.drawRectangleSpeedButton.Click += new System.EventHandler(this.DrawRectangleSpeedButtonClick);
            // 
            // pickUpSpeedButton
            // 
            this.pickUpSpeedButton.CheckOnClick = true;
            this.pickUpSpeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pickUpSpeedButton.Image = ((System.Drawing.Image)(resources.GetObject("pickUpSpeedButton.Image")));
            this.pickUpSpeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pickUpSpeedButton.Name = "pickUpSpeedButton";
            this.pickUpSpeedButton.Size = new System.Drawing.Size(29, 24);
            this.pickUpSpeedButton.Text = "toolStripButton1";
            // 
            // drawStarSpeedButton
            // 
            this.drawStarSpeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawStarSpeedButton.Image = ((System.Drawing.Image)(resources.GetObject("drawStarSpeedButton.Image")));
            this.drawStarSpeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawStarSpeedButton.Name = "drawStarSpeedButton";
            this.drawStarSpeedButton.Size = new System.Drawing.Size(29, 24);
            this.drawStarSpeedButton.Text = "toolStripButton1";
            this.drawStarSpeedButton.Click += new System.EventHandler(this.drawStarSpeedButtonClick);
            // 
            // ColorPickerButton
            // 
            this.ColorPickerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ColorPickerButton.Image = ((System.Drawing.Image)(resources.GetObject("ColorPickerButton.Image")));
            this.ColorPickerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ColorPickerButton.Name = "ColorPickerButton";
            this.ColorPickerButton.Size = new System.Drawing.Size(29, 24);
            this.ColorPickerButton.Text = "toolStripButton1";
            this.ColorPickerButton.Click += new System.EventHandler(this.ColorPickerButtonClicked);
            // 
            // drawElipseSpeedButton
            // 
            this.drawElipseSpeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawElipseSpeedButton.Image = ((System.Drawing.Image)(resources.GetObject("drawElipseSpeedButton.Image")));
            this.drawElipseSpeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawElipseSpeedButton.Name = "drawElipseSpeedButton";
            this.drawElipseSpeedButton.Size = new System.Drawing.Size(29, 24);
            this.drawElipseSpeedButton.Text = "toolStripButton1";
            this.drawElipseSpeedButton.Click += new System.EventHandler(this.drawElipseSpeedButtonClick);
            // 
            // drawLineSpeedButton
            // 
            this.drawLineSpeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawLineSpeedButton.Image = ((System.Drawing.Image)(resources.GetObject("drawLineSpeedButton.Image")));
            this.drawLineSpeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawLineSpeedButton.Name = "drawLineSpeedButton";
            this.drawLineSpeedButton.Size = new System.Drawing.Size(29, 24);
            this.drawLineSpeedButton.Text = "toolStripButton1";
            this.drawLineSpeedButton.Click += new System.EventHandler(this.drawLineSpeedButtonClick);
            // 
            // drawPointSpeedButton
            // 
            this.drawPointSpeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawPointSpeedButton.Image = ((System.Drawing.Image)(resources.GetObject("drawPointSpeedButton.Image")));
            this.drawPointSpeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawPointSpeedButton.Name = "drawPointSpeedButton";
            this.drawPointSpeedButton.Size = new System.Drawing.Size(29, 24);
            this.drawPointSpeedButton.Text = "toolStripButton1";
            this.drawPointSpeedButton.Click += new System.EventHandler(this.drawPointSpeedButtonClick);
            // 
            // CheckPointInPolygonButton
            // 
            this.CheckPointInPolygonButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CheckPointInPolygonButton.Image = ((System.Drawing.Image)(resources.GetObject("CheckPointInPolygonButton.Image")));
            this.CheckPointInPolygonButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CheckPointInPolygonButton.Name = "CheckPointInPolygonButton";
            this.CheckPointInPolygonButton.Size = new System.Drawing.Size(29, 24);
            this.CheckPointInPolygonButton.Text = "CheckPoint";
            this.CheckPointInPolygonButton.Click += new System.EventHandler(this.CheckPointInPolygonClick);
            // 
            // rotatePrimitiveSpeedButton
            // 
            this.rotatePrimitiveSpeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rotatePrimitiveSpeedButton.Image = ((System.Drawing.Image)(resources.GetObject("rotatePrimitiveSpeedButton.Image")));
            this.rotatePrimitiveSpeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotatePrimitiveSpeedButton.Name = "rotatePrimitiveSpeedButton";
            this.rotatePrimitiveSpeedButton.Size = new System.Drawing.Size(29, 24);
            this.rotatePrimitiveSpeedButton.Text = "toolStripButton1";
            this.rotatePrimitiveSpeedButton.Click += new System.EventHandler(this.rotatePrimitiveSpeedButton_Click);
            // 
            // GroupingButton
            // 
            this.GroupingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GroupingButton.Image = ((System.Drawing.Image)(resources.GetObject("GroupingButton.Image")));
            this.GroupingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GroupingButton.Name = "GroupingButton";
            this.GroupingButton.Size = new System.Drawing.Size(29, 24);
            this.GroupingButton.Text = "toolStripButton1";
            this.GroupingButton.Click += new System.EventHandler(this.GroupingButton_Click);
            // 
            // trackBarStroke
            // 
            this.trackBarStroke.Location = new System.Drawing.Point(366, 20);
            this.trackBarStroke.Name = "trackBarStroke";
            this.trackBarStroke.Size = new System.Drawing.Size(89, 56);
            this.trackBarStroke.TabIndex = 5;
            this.trackBarStroke.ValueChanged += new System.EventHandler(this.trackBarStroke_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(388, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Stroke";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(493, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Transparency";
            // 
            // trackBarTransparency
            // 
            this.trackBarTransparency.Location = new System.Drawing.Point(496, 19);
            this.trackBarTransparency.Maximum = 255;
            this.trackBarTransparency.Name = "trackBarTransparency";
            this.trackBarTransparency.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarTransparency.Size = new System.Drawing.Size(88, 56);
            this.trackBarTransparency.TabIndex = 9;
            this.trackBarTransparency.ValueChanged += new System.EventHandler(this.TransparencyButtonClicked);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(337, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Choose stroke color";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.StrokeColorPickerClicked);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(613, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 56);
            this.button2.TabIndex = 11;
            this.button2.Text = "Choose GradientColor1";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.GradientColor1PickerClicked);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(613, 74);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 60);
            this.button3.TabIndex = 12;
            this.button3.Text = "Choose GradientColor2";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.GradientColor2PickerClicked);
            // 
            // numericUpDownScale
            // 
            this.numericUpDownScale.Location = new System.Drawing.Point(749, 20);
            this.numericUpDownScale.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownScale.Name = "numericUpDownScale";
            this.numericUpDownScale.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownScale.TabIndex = 13;
            this.numericUpDownScale.ValueChanged += new System.EventHandler(this.numericUpDownScaleChange);
            // 
            // RemoveGradientsButton
            // 
            this.RemoveGradientsButton.Location = new System.Drawing.Point(613, 140);
            this.RemoveGradientsButton.Name = "RemoveGradientsButton";
            this.RemoveGradientsButton.Size = new System.Drawing.Size(110, 52);
            this.RemoveGradientsButton.TabIndex = 14;
            this.RemoveGradientsButton.Text = "Remove Gradients";
            this.RemoveGradientsButton.UseVisualStyleBackColor = true;
            this.RemoveGradientsButton.Click += new System.EventHandler(this.RemoveGradientsButton_Click);
            // 
            // textBoxNameObject
            // 
            this.textBoxNameObject.Location = new System.Drawing.Point(12, 93);
            this.textBoxNameObject.Name = "textBoxNameObject";
            this.textBoxNameObject.Size = new System.Drawing.Size(100, 22);
            this.textBoxNameObject.TabIndex = 15;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(118, 65);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(106, 47);
            this.button4.TabIndex = 16;
            this.button4.Text = "Потърси обект";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.SearchPrimitive);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(115, 118);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(106, 32);
            this.button5.TabIndex = 17;
            this.button5.Text = "задай цвят";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.giveColor);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(898, 19);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(209, 23);
            this.button6.TabIndex = 18;
            this.button6.Text = "Задай цвят чрез  RGBA стойности";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.GiveColorViaRGBA);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1118, 19);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(198, 23);
            this.button7.TabIndex = 19;
            this.button7.Text = "X, Y to move";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.TranslateViaCoords);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(1210, 65);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(94, 24);
            this.button8.TabIndex = 20;
            this.button8.Text = "Изтрий";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.DeletePrimitive);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeColorToolStripMenuItem,
            this.deleteToolStripMenuItem1,
            this.transformToolStripMenuItem1,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(167, 124);
            // 
            // changeColorToolStripMenuItem
            // 
            this.changeColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fillColorToolStripMenuItem1,
            this.strokeColorToolStripMenuItem1});
            this.changeColorToolStripMenuItem.Name = "changeColorToolStripMenuItem";
            this.changeColorToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.changeColorToolStripMenuItem.Text = "Change color";
            // 
            // fillColorToolStripMenuItem1
            // 
            this.fillColorToolStripMenuItem1.Name = "fillColorToolStripMenuItem1";
            this.fillColorToolStripMenuItem1.Size = new System.Drawing.Size(172, 26);
            this.fillColorToolStripMenuItem1.Text = "Fill color";
            this.fillColorToolStripMenuItem1.Click += new System.EventHandler(this.ColorPickerButtonClicked);
            // 
            // strokeColorToolStripMenuItem1
            // 
            this.strokeColorToolStripMenuItem1.Name = "strokeColorToolStripMenuItem1";
            this.strokeColorToolStripMenuItem1.Size = new System.Drawing.Size(172, 26);
            this.strokeColorToolStripMenuItem1.Text = "Stroke color";
            this.strokeColorToolStripMenuItem1.Click += new System.EventHandler(this.StrokeColorPickerClicked);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(166, 24);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.DeletePrimitive);
            // 
            // transformToolStripMenuItem1
            // 
            this.transformToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotateToolStripMenuItem1});
            this.transformToolStripMenuItem1.Name = "transformToolStripMenuItem1";
            this.transformToolStripMenuItem1.Size = new System.Drawing.Size(166, 24);
            this.transformToolStripMenuItem1.Text = "Transform";
            // 
            // rotateToolStripMenuItem1
            // 
            this.rotateToolStripMenuItem1.Name = "rotateToolStripMenuItem1";
            this.rotateToolStripMenuItem1.Size = new System.Drawing.Size(136, 26);
            this.rotateToolStripMenuItem1.Text = "Rotate";
            this.rotateToolStripMenuItem1.Click += new System.EventHandler(this.rotatePrimitiveSpeedButton_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyObj);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteObj);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.txtY);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtStrokeColor);
            this.panel1.Controls.Add(this.txtX);
            this.panel1.Controls.Add(this.txtFillColor);
            this.panel1.Location = new System.Drawing.Point(21, 209);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 194);
            this.panel1.TabIndex = 21;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.ViewPortPaint);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(69, 148);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(87, 30);
            this.button9.TabIndex = 22;
            this.button9.Text = "Приложи";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.btnApplyChanges_Click);
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(133, 108);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(100, 22);
            this.txtY.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Stroke color";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Fill color";
            // 
            // txtStrokeColor
            // 
            this.txtStrokeColor.Location = new System.Drawing.Point(133, 52);
            this.txtStrokeColor.Name = "txtStrokeColor";
            this.txtStrokeColor.Size = new System.Drawing.Size(100, 22);
            this.txtStrokeColor.TabIndex = 2;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(133, 80);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(100, 22);
            this.txtX.TabIndex = 1;
            // 
            // txtFillColor
            // 
            this.txtFillColor.Location = new System.Drawing.Point(133, 24);
            this.txtFillColor.Name = "txtFillColor";
            this.txtFillColor.Size = new System.Drawing.Size(100, 22);
            this.txtFillColor.TabIndex = 0;
            // 
            // comboBoxShapes
            // 
            this.comboBoxShapes.FormattingEnabled = true;
            this.comboBoxShapes.Location = new System.Drawing.Point(33, 432);
            this.comboBoxShapes.Name = "comboBoxShapes";
            this.comboBoxShapes.Size = new System.Drawing.Size(121, 24);
            this.comboBoxShapes.TabIndex = 22;
            this.comboBoxShapes.SelectedIndexChanged += new System.EventHandler(this.comboBoxShapes_SelectedIndexChanged);
            // 
            // viewPort
            // 
            this.viewPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPort.Location = new System.Drawing.Point(0, 55);
            this.viewPort.Margin = new System.Windows.Forms.Padding(5);
            this.viewPort.Name = "viewPort";
            this.viewPort.Size = new System.Drawing.Size(1328, 618);
            this.viewPort.TabIndex = 4;
            this.viewPort.Paint += new System.Windows.Forms.PaintEventHandler(this.ViewPortPaint);
            this.viewPort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseDown);
            this.viewPort.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseMove);
            this.viewPort.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseUp);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(244, 71);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 23;
            this.button10.Text = "NewShape";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.drawNewShapeButton_Clicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 695);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.comboBoxShapes);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBoxNameObject);
            this.Controls.Add(this.RemoveGradientsButton);
            this.Controls.Add(this.numericUpDownScale);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackBarTransparency);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarStroke);
            this.Controls.Add(this.viewPort);
            this.Controls.Add(this.speedMenu);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Draw";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.speedMenu.ResumeLayout(false);
            this.speedMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStroke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTransparency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScale)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
		private System.Windows.Forms.ToolStripStatusLabel currentStatusLabel;
		private Draw.DoubleBufferedPanel viewPort;
		private System.Windows.Forms.ToolStripButton pickUpSpeedButton;
		private System.Windows.Forms.ToolStripButton drawRectangleSpeedButton;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStrip speedMenu;
		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripButton drawStarSpeedButton;
        private System.Windows.Forms.ToolStripButton ColorPickerButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TrackBar trackBarStroke;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarTransparency;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripButton drawElipseSpeedButton;
        private System.Windows.Forms.ColorDialog colorDialog3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripButton drawLineSpeedButton;
        private System.Windows.Forms.ToolStripButton drawPointSpeedButton;
        private System.Windows.Forms.ToolStripButton CheckPointInPolygonButton;
        private System.Windows.Forms.ToolStripButton rotatePrimitiveSpeedButton;
        private System.Windows.Forms.NumericUpDown numericUpDownScale;
        private System.Windows.Forms.Button RemoveGradientsButton;
        private System.Windows.Forms.ToolStripButton GroupingButton;
		private System.Windows.Forms.TextBox textBoxNameObject;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ToolStripMenuItem transformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ungroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem strokeColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem changeColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillColorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem strokeColorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem transformToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rotateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtStrokeColor;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtFillColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jSONFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jSONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxShapes;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rectangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ellipseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem starToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointToolStripMenuItem;
        private System.Windows.Forms.Button button10;
    }
}
