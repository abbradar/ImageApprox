namespace ImageApprox
{
	partial class frmMain
	{
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolPercentage = new System.Windows.Forms.ToolStripStatusLabel();
            this.sizeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.coordsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolCoord = new System.Windows.Forms.ToolStripStatusLabel();
            this.selectedStringLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolSelected = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolSave = new System.Windows.Forms.ToolStripButton();
            this.toolApproximation = new System.Windows.Forms.ToolStripButton();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mitemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mitemRefreshFolders = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mitemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemApproximation = new System.Windows.Forms.ToolStripMenuItem();
            this.approximateLevelMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.approximationCombo = new System.Windows.Forms.ToolStripComboBox();
            this.mitemTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemRandGen = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemViev = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemShowFolders = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemShowFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemShowGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.splitFoldersTree = new System.Windows.Forms.SplitContainer();
            this.dirsTree = new System.Windows.Forms.TreeView();
            this.dirImageList = new System.Windows.Forms.ImageList(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.splitFilesExplorer = new System.Windows.Forms.SplitContainer();
            this.filesView = new System.Windows.Forms.ListView();
            this.fileImageList = new System.Windows.Forms.ImageList(this.components);
            this.labelCurrectDir = new System.Windows.Forms.Label();
            this.splitDiffGraph = new System.Windows.Forms.SplitContainer();
            this.imageTabs = new System.Windows.Forms.TabControl();
            this.tabLeft = new System.Windows.Forms.TabPage();
            this.pictureLeft = new ImageApprox.PictureBoxEx();
            this.tabRight = new System.Windows.Forms.TabPage();
            this.pictureRight = new ImageApprox.PictureBoxEx();
            this.pictureGraph = new ImageApprox.PictureBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.dirEnumerator = new System.ComponentModel.BackgroundWorker();
            this.fileEnumerator = new System.ComponentModel.BackgroundWorker();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.splitFoldersTree.Panel1.SuspendLayout();
            this.splitFoldersTree.Panel2.SuspendLayout();
            this.splitFoldersTree.SuspendLayout();
            this.splitFilesExplorer.Panel1.SuspendLayout();
            this.splitFilesExplorer.Panel2.SuspendLayout();
            this.splitFilesExplorer.SuspendLayout();
            this.splitDiffGraph.Panel1.SuspendLayout();
            this.splitDiffGraph.Panel2.SuspendLayout();
            this.splitDiffGraph.SuspendLayout();
            this.imageTabs.SuspendLayout();
            this.tabLeft.SuspendLayout();
            this.tabRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStatusLabel,
            this.toolPercentage,
            this.sizeLabel,
            this.toolSize,
            this.coordsLabel,
            this.toolCoord,
            this.selectedStringLabel,
            this.toolSelected});
            this.statusStrip.Location = new System.Drawing.Point(0, 597);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(798, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "Статус";
            // 
            // toolStatusLabel
            // 
            this.toolStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStatusLabel.Name = "toolStatusLabel";
            this.toolStatusLabel.Size = new System.Drawing.Size(38, 17);
            this.toolStatusLabel.Text = "Готов";
            // 
            // toolPercentage
            // 
            this.toolPercentage.Name = "toolPercentage";
            this.toolPercentage.Size = new System.Drawing.Size(23, 17);
            this.toolPercentage.Text = "0%";
            this.toolPercentage.Visible = false;
            // 
            // sizeLabel
            // 
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(50, 17);
            this.sizeLabel.Text = "Размер:";
            this.sizeLabel.Visible = false;
            // 
            // toolSize
            // 
            this.toolSize.Name = "toolSize";
            this.toolSize.Size = new System.Drawing.Size(24, 17);
            this.toolSize.Text = "0x0";
            this.toolSize.Visible = false;
            // 
            // coordsLabel
            // 
            this.coordsLabel.Name = "coordsLabel";
            this.coordsLabel.Size = new System.Drawing.Size(117, 17);
            this.coordsLabel.Text = "Координаты мыши:";
            this.coordsLabel.Visible = false;
            // 
            // toolCoord
            // 
            this.toolCoord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolCoord.Name = "toolCoord";
            this.toolCoord.Size = new System.Drawing.Size(24, 17);
            this.toolCoord.Text = "0x0";
            this.toolCoord.Visible = false;
            // 
            // selectedStringLabel
            // 
            this.selectedStringLabel.Name = "selectedStringLabel";
            this.selectedStringLabel.Size = new System.Drawing.Size(112, 17);
            this.selectedStringLabel.Text = "Выбранная строка:";
            this.selectedStringLabel.Visible = false;
            // 
            // toolSelected
            // 
            this.toolSelected.Name = "toolSelected";
            this.toolSelected.Size = new System.Drawing.Size(13, 17);
            this.toolSelected.Text = "0";
            this.toolSelected.Visible = false;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSave,
            this.toolApproximation});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(798, 25);
            this.toolStrip.TabIndex = 3;
            // 
            // toolSave
            // 
            this.toolSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSave.Enabled = false;
            this.toolSave.Image = ((System.Drawing.Image)(resources.GetObject("toolSave.Image")));
            this.toolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSave.Name = "toolSave";
            this.toolSave.Size = new System.Drawing.Size(23, 22);
            this.toolSave.Text = "Сохранить";
            this.toolSave.Click += new System.EventHandler(this.toolSave_Click);
            // 
            // toolApproximation
            // 
            this.toolApproximation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolApproximation.Enabled = false;
            this.toolApproximation.Image = ((System.Drawing.Image)(resources.GetObject("toolApproximation.Image")));
            this.toolApproximation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolApproximation.Name = "toolApproximation";
            this.toolApproximation.Size = new System.Drawing.Size(23, 22);
            this.toolApproximation.Text = "Сгладить";
            this.toolApproximation.Click += new System.EventHandler(this.toolApproximation_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitemFile,
            this.mitemProcess,
            this.mitemTools,
            this.mitemViev,
            this.mitemHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(798, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "Главное";
            // 
            // mitemFile
            // 
            this.mitemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitemSave,
            this.mitemClose,
            this.toolStripSeparator1,
            this.mitemRefreshFolders,
            this.toolStripSeparator2,
            this.mitemExit});
            this.mitemFile.Name = "mitemFile";
            this.mitemFile.Size = new System.Drawing.Size(48, 20);
            this.mitemFile.Text = "Файл";
            // 
            // mitemSave
            // 
            this.mitemSave.Enabled = false;
            this.mitemSave.Name = "mitemSave";
            this.mitemSave.Size = new System.Drawing.Size(199, 22);
            this.mitemSave.Text = "Сохранить";
            this.mitemSave.Click += new System.EventHandler(this.mitemSave_Click);
            // 
            // mitemClose
            // 
            this.mitemClose.Enabled = false;
            this.mitemClose.Name = "mitemClose";
            this.mitemClose.Size = new System.Drawing.Size(199, 22);
            this.mitemClose.Text = "Закрыть";
            this.mitemClose.Click += new System.EventHandler(this.mitemClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(196, 6);
            // 
            // mitemRefreshFolders
            // 
            this.mitemRefreshFolders.Name = "mitemRefreshFolders";
            this.mitemRefreshFolders.Size = new System.Drawing.Size(199, 22);
            this.mitemRefreshFolders.Text = "Обновить древо папок";
            this.mitemRefreshFolders.Click += new System.EventHandler(this.mitemRefreshFolders_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(196, 6);
            // 
            // mitemExit
            // 
            this.mitemExit.Name = "mitemExit";
            this.mitemExit.Size = new System.Drawing.Size(199, 22);
            this.mitemExit.Text = "Выход";
            this.mitemExit.Click += new System.EventHandler(this.mitemExit_Click);
            // 
            // mitemProcess
            // 
            this.mitemProcess.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitemApproximation,
            this.approximateLevelMItem,
            this.approximationCombo});
            this.mitemProcess.Name = "mitemProcess";
            this.mitemProcess.Size = new System.Drawing.Size(79, 20);
            this.mitemProcess.Text = "Обработка";
            // 
            // mitemApproximation
            // 
            this.mitemApproximation.Enabled = false;
            this.mitemApproximation.Name = "mitemApproximation";
            this.mitemApproximation.Size = new System.Drawing.Size(211, 22);
            this.mitemApproximation.Text = "Сгладить";
            this.mitemApproximation.Click += new System.EventHandler(this.mitemApproximation_Click);
            // 
            // approximateLevelMItem
            // 
            this.approximateLevelMItem.Enabled = false;
            this.approximateLevelMItem.Name = "approximateLevelMItem";
            this.approximateLevelMItem.Size = new System.Drawing.Size(211, 22);
            this.approximateLevelMItem.Text = "Настройка сглаживания:";
            // 
            // approximationCombo
            // 
            this.approximationCombo.Items.AddRange(new object[] {
            "-3",
            "-2",
            "-1",
            "0",
            "+1",
            "+2",
            "+3"});
            this.approximationCombo.Name = "approximationCombo";
            this.approximationCombo.Size = new System.Drawing.Size(121, 23);
            // 
            // mitemTools
            // 
            this.mitemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitemRandGen});
            this.mitemTools.Name = "mitemTools";
            this.mitemTools.Size = new System.Drawing.Size(95, 20);
            this.mitemTools.Text = "Инструменты";
            // 
            // mitemRandGen
            // 
            this.mitemRandGen.Name = "mitemRandGen";
            this.mitemRandGen.Size = new System.Drawing.Size(255, 22);
            this.mitemRandGen.Text = "Генерация последовательностей";
            this.mitemRandGen.Click += new System.EventHandler(this.mitemRandGen_Click);
            // 
            // mitemViev
            // 
            this.mitemViev.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitemShowFolders,
            this.mitemShowFiles,
            this.mitemShowGraph});
            this.mitemViev.Name = "mitemViev";
            this.mitemViev.Size = new System.Drawing.Size(39, 20);
            this.mitemViev.Text = "Вид";
            // 
            // mitemShowFolders
            // 
            this.mitemShowFolders.Checked = true;
            this.mitemShowFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mitemShowFolders.Name = "mitemShowFolders";
            this.mitemShowFolders.Size = new System.Drawing.Size(196, 22);
            this.mitemShowFolders.Text = "Древо папок";
            this.mitemShowFolders.Click += new System.EventHandler(this.mitemShowFolders_Click);
            // 
            // mitemShowFiles
            // 
            this.mitemShowFiles.Checked = true;
            this.mitemShowFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mitemShowFiles.Name = "mitemShowFiles";
            this.mitemShowFiles.Size = new System.Drawing.Size(196, 22);
            this.mitemShowFiles.Text = "Обозреватель файлов";
            this.mitemShowFiles.Click += new System.EventHandler(this.mitemShowFiles_Click);
            // 
            // mitemShowGraph
            // 
            this.mitemShowGraph.Checked = true;
            this.mitemShowGraph.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mitemShowGraph.Name = "mitemShowGraph";
            this.mitemShowGraph.Size = new System.Drawing.Size(196, 22);
            this.mitemShowGraph.Text = "График";
            this.mitemShowGraph.Click += new System.EventHandler(this.mitemShowGraph_Click);
            // 
            // mitemHelp
            // 
            this.mitemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitemAbout});
            this.mitemHelp.Name = "mitemHelp";
            this.mitemHelp.Size = new System.Drawing.Size(68, 20);
            this.mitemHelp.Text = "Помощь";
            // 
            // mitemAbout
            // 
            this.mitemAbout.Name = "mitemAbout";
            this.mitemAbout.Size = new System.Drawing.Size(149, 22);
            this.mitemAbout.Text = "О программе";
            this.mitemAbout.Click += new System.EventHandler(this.mitemAbout_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // saveFile
            // 
            this.saveFile.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFile_FileOk);
            // 
            // splitFoldersTree
            // 
            this.splitFoldersTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitFoldersTree.Location = new System.Drawing.Point(3, 52);
            this.splitFoldersTree.Name = "splitFoldersTree";
            // 
            // splitFoldersTree.Panel1
            // 
            this.splitFoldersTree.Panel1.Controls.Add(this.dirsTree);
            this.splitFoldersTree.Panel1.Controls.Add(this.label6);
            // 
            // splitFoldersTree.Panel2
            // 
            this.splitFoldersTree.Panel2.Controls.Add(this.splitFilesExplorer);
            this.splitFoldersTree.Size = new System.Drawing.Size(792, 542);
            this.splitFoldersTree.SplitterDistance = 172;
            this.splitFoldersTree.TabIndex = 8;
            // 
            // dirsTree
            // 
            this.dirsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dirsTree.ImageIndex = 0;
            this.dirsTree.ImageList = this.dirImageList;
            this.dirsTree.Location = new System.Drawing.Point(0, 13);
            this.dirsTree.Name = "dirsTree";
            this.dirsTree.SelectedImageIndex = 0;
            this.dirsTree.Size = new System.Drawing.Size(172, 529);
            this.dirsTree.StateImageList = this.dirImageList;
            this.dirsTree.TabIndex = 9;
            this.dirsTree.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.dirsTree_BeforeCollapse);
            this.dirsTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.dirsTree_BeforeExpand);
            this.dirsTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.dirsTree_NodeMouseDoubleClick);
            this.dirsTree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dirsTree_KeyPress);
            // 
            // dirImageList
            // 
            this.dirImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("dirImageList.ImageStream")));
            this.dirImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.dirImageList.Images.SetKeyName(0, "Hard_Drive.png");
            this.dirImageList.Images.SetKeyName(1, "Folder_Closed.png");
            this.dirImageList.Images.SetKeyName(2, "Folder_Open.png");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Древо папок:";
            // 
            // splitFilesExplorer
            // 
            this.splitFilesExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitFilesExplorer.Location = new System.Drawing.Point(0, 0);
            this.splitFilesExplorer.Name = "splitFilesExplorer";
            this.splitFilesExplorer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitFilesExplorer.Panel1
            // 
            this.splitFilesExplorer.Panel1.Controls.Add(this.filesView);
            this.splitFilesExplorer.Panel1.Controls.Add(this.labelCurrectDir);
            // 
            // splitFilesExplorer.Panel2
            // 
            this.splitFilesExplorer.Panel2.Controls.Add(this.splitDiffGraph);
            this.splitFilesExplorer.Size = new System.Drawing.Size(616, 542);
            this.splitFilesExplorer.SplitterDistance = 172;
            this.splitFilesExplorer.TabIndex = 11;
            // 
            // filesView
            // 
            this.filesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesView.LargeImageList = this.fileImageList;
            this.filesView.Location = new System.Drawing.Point(0, 13);
            this.filesView.MultiSelect = false;
            this.filesView.Name = "filesView";
            this.filesView.Size = new System.Drawing.Size(616, 159);
            this.filesView.TabIndex = 6;
            this.filesView.TileSize = new System.Drawing.Size(64, 64);
            this.filesView.UseCompatibleStateImageBehavior = false;
            this.filesView.DoubleClick += new System.EventHandler(this.filesView_DoubleClick);
            this.filesView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.filesView_KeyPress);
            // 
            // fileImageList
            // 
            this.fileImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("fileImageList.ImageStream")));
            this.fileImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.fileImageList.Images.SetKeyName(0, "Generic_Document.png");
            // 
            // labelCurrectDir
            // 
            this.labelCurrectDir.AutoSize = true;
            this.labelCurrectDir.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCurrectDir.Location = new System.Drawing.Point(0, 0);
            this.labelCurrectDir.Name = "labelCurrectDir";
            this.labelCurrectDir.Size = new System.Drawing.Size(129, 13);
            this.labelCurrectDir.TabIndex = 5;
            this.labelCurrectDir.Text = "Директория не открыта";
            // 
            // splitDiffGraph
            // 
            this.splitDiffGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitDiffGraph.Location = new System.Drawing.Point(0, 0);
            this.splitDiffGraph.Name = "splitDiffGraph";
            this.splitDiffGraph.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitDiffGraph.Panel1
            // 
            this.splitDiffGraph.Panel1.Controls.Add(this.imageTabs);
            // 
            // splitDiffGraph.Panel2
            // 
            this.splitDiffGraph.Panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.splitDiffGraph.Panel2.Controls.Add(this.pictureGraph);
            this.splitDiffGraph.Panel2.Controls.Add(this.label1);
            this.splitDiffGraph.Size = new System.Drawing.Size(616, 366);
            this.splitDiffGraph.SplitterDistance = 266;
            this.splitDiffGraph.TabIndex = 2;
            // 
            // imageTabs
            // 
            this.imageTabs.Controls.Add(this.tabLeft);
            this.imageTabs.Controls.Add(this.tabRight);
            this.imageTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageTabs.Location = new System.Drawing.Point(0, 0);
            this.imageTabs.Name = "imageTabs";
            this.imageTabs.SelectedIndex = 0;
            this.imageTabs.Size = new System.Drawing.Size(616, 266);
            this.imageTabs.TabIndex = 2;
            this.imageTabs.SelectedIndexChanged += new System.EventHandler(this.imageTabs_SelectedIndexChanged);
            // 
            // tabLeft
            // 
            this.tabLeft.Controls.Add(this.pictureLeft);
            this.tabLeft.Location = new System.Drawing.Point(4, 22);
            this.tabLeft.Margin = new System.Windows.Forms.Padding(0);
            this.tabLeft.Name = "tabLeft";
            this.tabLeft.Size = new System.Drawing.Size(608, 240);
            this.tabLeft.TabIndex = 0;
            this.tabLeft.Text = "Исходное изображение";
            this.tabLeft.UseVisualStyleBackColor = true;
            // 
            // pictureLeft
            // 
            this.pictureLeft.AutoScroll = true;
            this.pictureLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureLeft.HighlightPoint = new System.Drawing.Point(0, 0);
            this.pictureLeft.HScrollValue = 0;
            this.pictureLeft.Location = new System.Drawing.Point(0, 0);
            this.pictureLeft.Name = "pictureLeft";
            this.pictureLeft.Padding = new System.Windows.Forms.Padding(3);
            this.pictureLeft.Size = new System.Drawing.Size(608, 240);
            this.pictureLeft.TabIndex = 5;
            this.pictureLeft.VScrollValue = 0;
            this.pictureLeft.Scroll += new System.Windows.Forms.ScrollEventHandler(this.pictureLeft_Scroll);
            this.pictureLeft.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateCoordinates);
            this.pictureLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureLeft_MouseUp);
            this.pictureLeft.Resize += new System.EventHandler(this.pictureLeft_Resize);
            // 
            // tabRight
            // 
            this.tabRight.Controls.Add(this.pictureRight);
            this.tabRight.Location = new System.Drawing.Point(4, 22);
            this.tabRight.Name = "tabRight";
            this.tabRight.Size = new System.Drawing.Size(608, 240);
            this.tabRight.TabIndex = 1;
            this.tabRight.Text = "Конечное изображение";
            this.tabRight.UseVisualStyleBackColor = true;
            // 
            // pictureRight
            // 
            this.pictureRight.AutoScroll = true;
            this.pictureRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureRight.HighlightPoint = new System.Drawing.Point(0, 0);
            this.pictureRight.HScrollValue = 0;
            this.pictureRight.Location = new System.Drawing.Point(0, 0);
            this.pictureRight.Name = "pictureRight";
            this.pictureRight.Size = new System.Drawing.Size(608, 240);
            this.pictureRight.TabIndex = 5;
            this.pictureRight.VScrollValue = 0;
            this.pictureRight.Scroll += new System.Windows.Forms.ScrollEventHandler(this.pictureRight_Scroll);
            this.pictureRight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateCoordinates);
            this.pictureRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureRight_MouseUp);
            this.pictureRight.Resize += new System.EventHandler(this.pictureRight_Resize);
            // 
            // pictureGraph
            // 
            this.pictureGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureGraph.AutoScroll = true;
            this.pictureGraph.BackColor = System.Drawing.SystemColors.Control;
            this.pictureGraph.HighlightPoint = new System.Drawing.Point(0, 0);
            this.pictureGraph.HScrollValue = 0;
            this.pictureGraph.Location = new System.Drawing.Point(4, 29);
            this.pictureGraph.Name = "pictureGraph";
            this.pictureGraph.Size = new System.Drawing.Size(608, 64);
            this.pictureGraph.TabIndex = 2;
            this.pictureGraph.TabStop = false;
            this.pictureGraph.VScrollValue = 0;
            this.pictureGraph.Scroll += new System.Windows.Forms.ScrollEventHandler(this.pictureGraph_Scroll);
            this.pictureGraph.Resize += new System.EventHandler(this.pictureGraph_Resize);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(616, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "График линии: (X - точки, Y - яркость, красный график - исходное изображение, син" +
                "ий - конечное)\r\nДля выбора линии щелкните на нужном месте на изображении.";
            // 
            // dirEnumerator
            // 
            this.dirEnumerator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dirEnumerator_DoWork);
            this.dirEnumerator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.dirEnumerator_RunWorkerCompleted);
            // 
            // fileEnumerator
            // 
            this.fileEnumerator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.fileEnumerator_DoWork);
            this.fileEnumerator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.fileEnumerator_RunWorkerCompleted);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 619);
            this.Controls.Add(this.splitFoldersTree);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "frmMain";
            this.Text = "ImageApprox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitFoldersTree.Panel1.ResumeLayout(false);
            this.splitFoldersTree.Panel1.PerformLayout();
            this.splitFoldersTree.Panel2.ResumeLayout(false);
            this.splitFoldersTree.ResumeLayout(false);
            this.splitFilesExplorer.Panel1.ResumeLayout(false);
            this.splitFilesExplorer.Panel1.PerformLayout();
            this.splitFilesExplorer.Panel2.ResumeLayout(false);
            this.splitFilesExplorer.ResumeLayout(false);
            this.splitDiffGraph.Panel1.ResumeLayout(false);
            this.splitDiffGraph.Panel2.ResumeLayout(false);
            this.splitDiffGraph.ResumeLayout(false);
            this.imageTabs.ResumeLayout(false);
            this.tabLeft.ResumeLayout(false);
            this.tabRight.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStatusLabel;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.SaveFileDialog saveFile;
		private System.Windows.Forms.SplitContainer splitFoldersTree;
		private System.Windows.Forms.SplitContainer splitFilesExplorer;
		private System.Windows.Forms.ListView filesView;
		private System.Windows.Forms.Label labelCurrectDir;
		private System.Windows.Forms.SplitContainer splitDiffGraph;
		private System.Windows.Forms.TreeView dirsTree;
		private System.Windows.Forms.Label label6;
		private PictureBoxEx pictureGraph;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ImageList fileImageList;
		private System.Windows.Forms.ImageList dirImageList;
		private System.Windows.Forms.ToolStripMenuItem mitemFile;
		private System.Windows.Forms.ToolStripMenuItem mitemSave;
		private System.Windows.Forms.ToolStripMenuItem mitemClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem mitemRefreshFolders;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem mitemExit;
		private System.Windows.Forms.ToolStripMenuItem mitemProcess;
		private System.Windows.Forms.ToolStripMenuItem mitemApproximation;
		private System.Windows.Forms.ToolStripMenuItem mitemTools;
		private System.Windows.Forms.ToolStripMenuItem mitemRandGen;
		private System.Windows.Forms.ToolStripMenuItem mitemViev;
		private System.Windows.Forms.ToolStripMenuItem mitemShowFolders;
		private System.Windows.Forms.ToolStripMenuItem mitemShowFiles;
		private System.Windows.Forms.ToolStripMenuItem mitemShowGraph;
		private System.Windows.Forms.ToolStripMenuItem mitemHelp;
		private System.Windows.Forms.ToolStripMenuItem mitemAbout;
		private System.Windows.Forms.ToolStripButton toolSave;
		private System.Windows.Forms.ToolStripButton toolApproximation;
		private System.Windows.Forms.TabControl imageTabs;
		private System.Windows.Forms.TabPage tabLeft;
		private PictureBoxEx pictureLeft;
		private System.Windows.Forms.TabPage tabRight;
        private PictureBoxEx pictureRight;
        private System.Windows.Forms.ToolStripMenuItem approximateLevelMItem;
        private System.Windows.Forms.ToolStripComboBox approximationCombo;
        private System.Windows.Forms.ToolStripStatusLabel toolCoord;
        private System.Windows.Forms.ToolStripStatusLabel selectedStringLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolSelected;
        private System.ComponentModel.BackgroundWorker dirEnumerator;
        private System.ComponentModel.BackgroundWorker fileEnumerator;
        private System.Windows.Forms.ToolStripStatusLabel sizeLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolSize;
        private System.Windows.Forms.ToolStripStatusLabel toolPercentage;
        private System.Windows.Forms.ToolStripStatusLabel coordsLabel;
	}
}

