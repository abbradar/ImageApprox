using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace ImageApprox
{
	/// <summary>
	/// Перечисление возможных режимов работы программы. (разные режимы нужны для работы с разными видами данных)
	/// </summary>
	public enum DataType {
		/// <summary>
		/// Числовой тип данных. (Числовая последовательность)
		/// </summary>
		Numeric,
		/// <summary>
		/// Графический тип данных. (Изображение)
		/// </summary>
		Image,
		/// <summary>
		/// Нулевой тип данных.
		/// </summary>
		Nothing
	}

	/// <summary>
	/// Основное окно программы.
	/// </summary>
	public partial class frmMain : Form
	{
		private double[] leftList, rightList;
		private DataType dataType;
		private Image leftImg;
		private Image rightImg;
		private bool hasRight;
		private string fileName;
		private const int unknownNum = 4;
		private int graphY;
        private float k;
		private Graph leftGraph, rightGraph, diffGraph;
        private int made, total, percent;

		/// <summary>
		/// Основной конструктор для формы.
		/// </summary>
		public frmMain()
		{
			InitializeComponent();
		}

		private bool Match(byte[] needle, byte[] std)
		{
			for (int i = 0; i < std.Length; i++)
			{
				if (needle[i] != std[i])
				{
					return false;
				}
			}
			return true;
		}

		private void OpenFile(string newf)
		{
			Stream file = null;
			try
			{
				file = new FileStream(newf, FileMode.Open);
				if (IsNumeric(file))
				{
					file.Seek(0, SeekOrigin.Begin);
					BinaryReader reader = new BinaryReader(file);
					reader.ReadChars(4);
					if (reader.ReadUInt16() != 8)
					{
						throw new ArgumentException("Неверный файл последовательности.");
					}
					int length = reader.ReadInt32();
					leftList = new double[length];
					double val;
					for (UInt32 i = 0; i < length; i++)
					{
						try
						{
							val = reader.ReadDouble();
						}
						catch
						{
							throw new ArgumentException("Неверный файл последовательности.");
						}
						leftList[i] = val;
					}
					rightList = (double[])leftList.Clone();
					mitemShowGraph.Enabled = false;
					CloseFile();
					HideGraph();
					leftGraph.FirstValues = leftList;
					leftGraph.HasCenter = true;
					rightGraph.HasCenter = true;
                    toolSize.Text = rightList.Length.ToString();
					leftGraph.VerticalAnnotation = "Ордината";
					leftGraph.HorizontalAnnotation = "Точки последовательности";
					rightGraph.VerticalAnnotation = "Ордината";
					rightGraph.HorizontalAnnotation = "Точки последовательности";
					dataType = DataType.Numeric;
					UpdateLeftImage();
				}
				else
				{
					CloseFile();
					leftImg = Image.FromStream(file);
					rightImg = (Image)leftImg.Clone();
					graphY = leftImg.Height / 2;
                    toolSelected.Visible = true;
                    selectedStringLabel.Visible = true;
                    toolSize.Text = leftImg.Width.ToString() + "x" + leftImg.Height.ToString();
					dataType = DataType.Image;
                    pictureLeft.Image = leftImg;
                    diffGraph.Width = leftImg.Width;
                    total = (leftImg.Width + leftImg.Height) * 9;
                    UpdateGraphData();
                    UpdateGraph();
				}
				file.Close();
				fileName = newf;
				Text = fileName + " - " + Application.ProductName;
				mitemApproximation.Enabled = true;
				toolApproximation.Enabled = true;
                toolCoord.Visible = true;
                coordsLabel.Visible = true;
				mitemClose.Enabled = true;
                toolSize.Visible = true;
                sizeLabel.Visible = true;
			}
			catch (Exception e)
			{
				try
				{
					file.Close();
				}
				catch
				{
				}

				MessageBox.Show(this, e.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Получает заданную строку из изображения в виде массива.
		/// </summary>
		/// <param name="img">Изображение, из которого необходимо извлечь строку.</param>
		/// <param name="line">Номер строки для извлечения.</param>
		/// <returns></returns>
		public double[] GetLine(Image img, int line)
		{
			Bitmap tmp = new Bitmap(img);
			double[] list = new double[tmp.Width];
			for (int i = 0; i < leftImg.Width; i++)
			{
				Color pixel = tmp.GetPixel(i, line);
				list[i] = (double)((pixel.R + (double)pixel.G + (double)pixel.B) / 3);
			}
			return list;
		}

		private bool IsNumeric(Stream file)
		{
			byte[] rndb = { 82, 78, 68, 66 };

			BinaryReader reader = new BinaryReader(file);
			byte[] format = reader.ReadBytes(4);
			return Match(format, rndb);
		}

		private void CloseFile()
		{
			dataType = DataType.Nothing;
			imageTabs.SelectedIndex = 0;
            hasRight = false;
            fileName = "";
            toolCoord.Text = "";
            approximationCombo.SelectedIndex = 3;
			Text = Application.ProductName;
            selectedStringLabel.Visible = false;
            sizeLabel.Visible = false;
            toolSelected.Visible = false;
            toolSize.Visible = false;
            toolCoord.Visible = false;
            coordsLabel.Visible = false;
			pictureLeft.Image = null;
			pictureGraph.Image = null;
            diffGraph.FirstValues = null;
            diffGraph.SecondValues = null;
			ShowGraph();
			mitemSave.Enabled = false;
			mitemClose.Enabled = false;
			toolSave.Enabled = false;
			mitemApproximation.Enabled = false;
			toolApproximation.Enabled = false;
		}

		private void HideFolderTree()
		{
			splitFoldersTree.Panel1Collapsed = true;
		}

		private void ShowFolderTree()
		{
			splitFoldersTree.Panel1Collapsed = false;
		}

		private void HideFilesExplorer()
		{
			splitFilesExplorer.Panel1Collapsed = true;
		}

		private void ShowFilesExplorer()
		{
			splitFilesExplorer.Panel1Collapsed = false;
		}

		private void ShowGraph()
		{
			splitDiffGraph.Panel2Collapsed = false;
		}

		private void HideGraph()
		{
			splitDiffGraph.Panel2Collapsed = true;
		}

		private void HideDiffGraph()
		{
			splitDiffGraph.Panel2Collapsed = true;
		}

		private void ShowDiffGraph()
		{
			splitDiffGraph.Panel2Collapsed = false;
		}

		private void FileSave()
		{
			if (dataType == DataType.Nothing)
			{
				return;
			}
			if (dataType == DataType.Image)
			{
				saveFile.DefaultExt = "bmp";
				saveFile.Filter = "Точечный рисунок (*.bmp)|*.bmp|Файл JPEG (*.jpg)|*.jpg|Файл GIF (*.gif)|*.gif|Файл PNG (*.png)|*.png";
			}
			else
			{
				saveFile.DefaultExt = "rdb";
				saveFile.Filter = "Файлы последовательностей|*.rdb";
			}
			saveFile.ShowDialog();
		}

		private void DoApproximation()
		{
            toolStatusLabel.Text = "Сглаживается...";
			mitemApproximation.Enabled = false;
			toolApproximation.Enabled = false;
			dirsTree.Enabled = false;
			filesView.Enabled = false;
			mitemSave.Enabled = false;
			mitemClose.Enabled = false;
            approximationCombo.Enabled = false;
            switch (approximationCombo.SelectedIndex)
            {
                case 0:
                    k = 1.0F / 2.0F / (float)Math.Sqrt(2);
                    break;
                case 1:
                    k = 1.0F / 2.0F;
                    break;
                case 2:
                    k = 1.0F / (float)Math.Sqrt(2);
                    break;
                case 4:
                    k = (float)Math.Sqrt(2);
                    break;
                case 5:
                    k = 2.0F;
                    break;
                case 6:
                    k = 2.0F * (float)Math.Sqrt(2);
                    break;
                default:
                    k = 1.0F;
                    break;
            }
			backgroundWorker.RunWorkerAsync();
		}

		private void pictureRight_Resize(object sender, EventArgs e)
		{
			UpdateRightImage();
		}

		private void approximateTool_Click(object sender, EventArgs e)
		{
			DoApproximation();
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
            toolStatusLabel.Text = "Готово";
			mitemApproximation.Enabled = true;
			toolApproximation.Enabled = true;
			mitemSave.Enabled = true;
			toolSave.Enabled = true;
			mitemClose.Enabled = true;
			dirsTree.Enabled = true;
            approximationCombo.Enabled = true;
            toolPercentage.Visible = false;
			filesView.Enabled = true;
            hasRight = true;
			if (dataType == DataType.Image)
			{
				pictureRight.Image = rightImg;
                UpdateGraphData();
                UpdateGraph();
			}
			else
			{
				rightGraph.FirstValues = rightList;
                UpdateRightImage();
			}
			imageTabs.SelectedIndex = 1;
            imageTabs_SelectedIndexChanged(imageTabs, new EventArgs());
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			if (dataType == DataType.Numeric)
			{
				rightList = Approximation.ApproximateSequence(leftList, unknownNum, k);
			}
			else if (dataType == DataType.Image)
			{
                toolPercentage.Text = "0%";
                toolPercentage.Visible = true;
                made = 0;
                percent = 0;
				rightImg = Approximation.ApproximateImage(leftImg, unknownNum, k, new Action(AddPercentage));
			}
		}

        private void AddPercentage()
        {
            made++;
            if (percent < (int)((float)made / (float)total * 100))
            {
                percent++;
                BeginInvoke(new Action(SetPercentageText), null);
            }
        }

        private void SetPercentageText()
        {
            toolPercentage.Text = percent.ToString() + "%";
        }
		
		private void saveFile_FileOk(object sender, CancelEventArgs e)
		{
			if (dataType == DataType.Image)
			{
				ImageFormat fmt;
				switch (saveFile.FileName.Substring(saveFile.FileName.LastIndexOf('.')))
				{
					case "jpg":
						fmt = ImageFormat.Jpeg;
						break;
					case "gif":
						fmt = ImageFormat.Gif;
						break;
					case "png":
						fmt = ImageFormat.Png;
						break;
					default:
						fmt = ImageFormat.Bmp;
						break;
				}
				ImageSaveTo(saveFile.FileName, fmt);
			}
			else
			{
				RNDBSaveTo(saveFile.FileName);
			}
		}

		private void ImageSaveTo(string addr, ImageFormat fmt)
		{
			try
			{
				rightImg.Save(addr, fmt);
			}
			catch (Exception e)
			{
				MessageBox.Show(this, "Не удается сохранить файл: " + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FileSave();
			}
		}

		private void RNDBSaveTo(string addr)
		{
			try
			{
				if (File.Exists(addr))
				{
					File.Delete(addr);
				}
				BinaryWriter file = new BinaryWriter(new FileStream(addr, FileMode.OpenOrCreate));
				file.Write(Encoding.Default.GetBytes("RNDB"));
				file.Write((ushort)sizeof(double));
				file.Write(rightList.Length);
				foreach (double num in rightList)
				{
					file.Write(num);
				}
				file.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show(this, "Не удается сохранить файл: " + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FileSave();
			}
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			dataType = DataType.Nothing;
			leftGraph = new Graph();
			rightGraph = new Graph();
			diffGraph = new Graph();
			leftGraph.Annotations = true;
			rightGraph.Annotations = true;
            leftGraph.FirstPen = new Pen(new SolidBrush(Color.Red), 3);
            rightGraph.FirstPen = new Pen(new SolidBrush(Color.Blue), 1);
			diffGraph.Annotations = true;
			diffGraph.HasCenter = false;
			diffGraph.AutoMax = false;
			diffGraph.Max = 255;
            diffGraph.FirstPen = new Pen(new SolidBrush(Color.Red), 3);
            diffGraph.SecondPen = new Pen(new SolidBrush(Color.Blue), 1);
            approximationCombo.SelectedIndex = 3;
			hasRight = false;
            toolCoord.Text = "";
			imageTabs.TabIndex = 0;
			tabLeft.Select();
			UpdateDirTree();
		}

		private void UpdateDirTree()
		{
			dirsTree.Nodes.Clear();
			DriveInfo[] dInfo = DriveInfo.GetDrives();
			foreach (DriveInfo disk in dInfo)
			{
				TreeNode node = new TreeNode(disk.Name);
				try
				{
					if (disk.RootDirectory.GetDirectories().Length > 0)
					{
						node.Nodes.Add("|");
					}
				}
				catch
				{
					node.Nodes.Add("/");
				}
				node.ImageIndex = 0;
				dirsTree.Nodes.Add(node);
			}
		}

		private void dirsTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node.Nodes.Count > 0)
			{
				if (e.Node.Nodes[0].Text == "|")
				{
                    if (dirEnumerator.IsBusy)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        toolStatusLabel.Text = "Открывается";
                        dirEnumerator.RunWorkerAsync(e.Node);
                    }
				}
				else if (e.Node.Nodes[0].Text == "/")
				{
					e.Cancel = true;
				}
			}
		}

		private void dirsTree_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node.Level > 0)
			{
				e.Node.ImageIndex = 1;
			}
		}

		private void OpenDirectory(TreeNode e)
		{
            if (fileEnumerator.IsBusy) return;
            if (e.Nodes.Count > 0)
            {
                if (e.Nodes[0].Text == "/")
                {
                    return;
                }
            }
            toolStatusLabel.Text = "Открывается";
            fileEnumerator.RunWorkerAsync(e.FullPath);
		}

		private void dirsTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
            if (e.Button == MouseButtons.Left)
            {
                OpenDirectory(e.Node);
            }
		}

		private void dirsTree_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
			{
				OpenDirectory(dirsTree.SelectedNode);
			}
		}

		private void filesView_DoubleClick(object sender, EventArgs e)
		{
			if (filesView.SelectedItems.Count > 0)
			{
				string file = labelCurrectDir.Text + filesView.SelectedItems[0].Text;
				OpenFile(file);
			}
		}

		private void filesView_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				string file = labelCurrectDir.Text + filesView.SelectedItems[0].Text;
				OpenFile(file);
			}
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (hasRight)
			{
				DialogResult answer = AskToSave();
				if (answer == DialogResult.Yes)
				{
					FileSave();
				}
				else if (answer == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
			}
		}

		private DialogResult AskToSave()
		{
			return MessageBox.Show(this, "Вы хотите сохранить результаты работы?", "Выход из программы", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
		}

		private void mitemClose_Click(object sender, EventArgs e)
		{
			if (hasRight)
			{
				DialogResult answer = AskToSave();
				if (answer == DialogResult.Yes)
				{
					FileSave();
				}
				else if (answer == DialogResult.Cancel)
				{
					return;
				}
			}
			CloseFile();
		}

		private void mitemSave_Click(object sender, EventArgs e)
		{
			FileSave();
		}

		private void UpdateLeftImage()
		{
			if (dataType == DataType.Numeric && pictureLeft.Width > 0 && pictureLeft.Height > 0)
			{
				leftGraph.Width = pictureLeft.Width;
				leftGraph.Height = pictureLeft.Height;
				pictureLeft.Image = leftGraph.GetGraph();
			}
		}

		private void UpdateRightImage()
		{
			if (dataType == DataType.Numeric && hasRight && pictureRight.Width > 0 && pictureRight.Height > 0)
			{
				rightGraph.Width = pictureRight.Width;
				rightGraph.Height = pictureRight.Height;
				pictureRight.Image = rightGraph.GetGraph();
			}
		}

		private void UpdateGraph()
		{
			if (dataType == DataType.Image)
			{
				int height = pictureGraph.Height;
                if (pictureGraph.HorizontalScroll.Visible)
                {
                    height -= 17;
                }
                if (height > 0)
                {
                    diffGraph.Height = height;
                    pictureGraph.Image = diffGraph.GetGraph();
                    pictureGraph.VerticalScroll.Visible = false;
                    if (imageTabs.SelectedIndex == 0)
                    {
                        pictureGraph.HScrollValue = pictureLeft.HScrollValue;
                    }
                    else
                    {
                        pictureGraph.HScrollValue = pictureRight.HScrollValue;
                    }
                }
			}
		}

		private void pictureLeft_Resize(object sender, EventArgs e)
		{
			UpdateLeftImage();
		}

		private void mitemExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void mitemApproximation_Click(object sender, EventArgs e)
		{
			DoApproximation();
		}

		private void mitemRandGen_Click(object sender, EventArgs e)
		{
			CallRandGen();
		}

		private void CallRandGen()
		{
			frmRandGen rand = new frmRandGen();
			rand.ShowDialog(this);
		}

		private void mitemAbout_Click(object sender, EventArgs e)
		{
			CallAbout();
		}

		private void CallAbout()
		{
			frmAbout about = new frmAbout();
			about.ShowDialog(this);
		}

		private void toolSave_Click(object sender, EventArgs e)
		{
			FileSave();
		}

		private void toolApproximation_Click(object sender, EventArgs e)
		{
			DoApproximation();
		}

		private void pictureGraph_Resize(object sender, EventArgs e)
		{
			UpdateGraph();
		}

		private void mitemRefreshFolders_Click(object sender, EventArgs e)
		{
			UpdateDirTree();
		}

		private void mitemShowFolders_Click(object sender, EventArgs e)
		{
			mitemShowFolders.Checked = !mitemShowFolders.Checked;
			if (mitemShowFolders.Checked)
			{
				ShowFolderTree();
			}
			else
			{
				HideFolderTree();
			}
		}

		private void mitemShowFiles_Click(object sender, EventArgs e)
		{
			mitemShowFiles.Checked = !mitemShowFiles.Checked;
			if (mitemShowFiles.Checked)
			{
				ShowFilesExplorer();
			}
			else
			{
				HideFilesExplorer();
			}
		}

		private void mitemShowGraph_Click(object sender, EventArgs e)
		{
			mitemShowGraph.Checked = !mitemShowGraph.Checked;
			if (mitemShowGraph.Checked)
			{
				ShowGraph();
			}
			else
			{
				HideGraph();
			}
		}

		private void pictureRight_Scroll(object sender, ScrollEventArgs e)
		{
            if (dataType == DataType.Image)
            {
                if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll && e.NewValue >= 0 && e.NewValue <= pictureRight.HorizontalScroll.Maximum)
                {
                    pictureGraph.HScrollValue = e.NewValue;
                }
			}
		}

		private void pictureLeft_Scroll(object sender, ScrollEventArgs e)
		{
            if (dataType == DataType.Image)
			{
                if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll && e.NewValue >= 0 && e.NewValue <= pictureLeft.HorizontalScroll.Maximum)
                {
                    pictureGraph.HScrollValue = e.NewValue;
                }
            }
		}

		private void pictureGraph_Scroll(object sender, ScrollEventArgs e)
		{
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll && e.NewValue >= 0 && e.NewValue <= pictureGraph.HorizontalScroll.Maximum)
			{
                if (imageTabs.SelectedIndex == 0)
                {
                    pictureLeft.HScrollValue = e.NewValue;
                }
                else
                {
                    pictureRight.HScrollValue = e.NewValue;
                }
			}
		}

		private void pictureLeft_MouseUp(object sender, MouseEventArgs e)
		{
            if (dataType == DataType.Image && e.Y >= 0 && e.Y < pictureLeft.Height)
			{
				graphY = e.Y + pictureLeft.VScrollValue;
				UpdateGraphData();
				UpdateGraph();
			}
		}

		private void UpdateGraphData()
		{
            if (dataType == DataType.Image)
            {
                diffGraph.FirstValues = GetLine(leftImg, graphY);
                if (hasRight)
                {
                    diffGraph.SecondValues = GetLine(rightImg, graphY);
                    pictureRight.HighlightPoint = new Point(-1, graphY);
                }
                else
                {
                    diffGraph.SecondValues = null;
                }
                pictureLeft.HighlightPoint = new Point(-1, graphY);
                toolSelected.Text = graphY.ToString();
            }
		}

		private void pictureRight_MouseUp(object sender, MouseEventArgs e)
		{
			if (dataType == DataType.Image && e.Y >= 0 && e.Y < pictureRight.Height)
			{
				graphY = e.Y + pictureRight.VScrollValue;
				UpdateGraphData();
				UpdateGraph();
			}
		}

		private void imageTabs_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (!hasRight && imageTabs.SelectedIndex == 1)
            {
                imageTabs.SelectedIndex = 0;
                return;
            }
            else if (hasRight)
            {
                if (imageTabs.SelectedIndex == 0)
                {
                    pictureLeft.HScrollValue = pictureRight.HScrollValue;
                    pictureLeft.VScrollValue = pictureRight.VScrollValue;
                }
                else
                {
                    pictureRight.HScrollValue = pictureLeft.HScrollValue;
                    pictureRight.VScrollValue = pictureLeft.VScrollValue;
                }
            }
		}

        private void UpdateCoordinates(object sender, MouseEventArgs e)
        {
            if (dataType == DataType.Image)
            {
                int x = e.X + 1 + ((PictureBoxEx)sender).HScrollValue;
                if (x > ((PictureBoxEx)sender).Image.Width)
                {
                    x = ((PictureBoxEx)sender).Image.Width;
                }
                int y = e.Y + 1 + ((PictureBoxEx)sender).VScrollValue;
                if (y > ((PictureBoxEx)sender).Image.Height)
                {
                    y = ((PictureBoxEx)sender).Image.Height;
                }
                toolCoord.Text = x.ToString() + "x" + y.ToString();
            }
            else if (dataType == DataType.Numeric)
            {
                toolCoord.Text = Math.Round(e.X / leftGraph.Zoom + 1).ToString("F0");
            }
        }

        private void dirEnumerator_DoWork(object sender, DoWorkEventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            TreeNode node = (TreeNode)e.Argument;
            node.Nodes.Clear();
            if (node.Level > 0)
            {
                node.ImageIndex = 2;
            }
            string[] dirs = Directory.GetDirectories(node.FullPath + "\\");
            Func<TreeNode, int> send = new Func<TreeNode,int>(node.Nodes.Add);
            foreach (string dir in dirs)
            {
                TreeNode entry = new TreeNode(dir.Substring(node.FullPath.Length + 1));
                try
                {
                    if (Directory.GetDirectories(dir).Length > 0)
                    {
                        entry.Nodes.Add("|");
                    }
                }
                catch
                {
                    entry.Nodes.Add("/");
                }
                entry.ImageIndex = 1;
                entry.SelectedImageIndex = 1;
                BeginInvoke(send, new object[1] { entry });
            }
            CheckForIllegalCrossThreadCalls = false;
        }

        private void fileEnumerator_DoWork(object sender, DoWorkEventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            string dir = (string)e.Argument;
            filesView.Items.Clear();
            while (fileImageList.Images.Count > 1)
            {
                fileImageList.Images.RemoveAt(1);
            }
            string[] files1 = Directory.GetFiles(dir + "\\", "*.bmp");
            string[] files2 = Directory.GetFiles(dir + "\\", "*.jpg");
            string[] files3 = Directory.GetFiles(dir + "\\", "*.jpeg");
            string[] files4 = Directory.GetFiles(dir + "\\", "*.gif");
            string[] files5 = Directory.GetFiles(dir + "\\", "*.png");
            string[] files6 = Directory.GetFiles(dir + "\\", "*.rdb");
            string[] files = new string[files1.Length + files2.Length + files3.Length + files4.Length + files5.Length
                + files6.Length];
            files1.CopyTo(files, 0);
            files2.CopyTo(files, files1.Length);
            files3.CopyTo(files, files1.Length + files2.Length);
            files4.CopyTo(files, files1.Length + files2.Length + files3.Length);
            files5.CopyTo(files, files1.Length + files2.Length + files3.Length + files4.Length);
            files6.CopyTo(files, files1.Length + files2.Length + files3.Length + files4.Length + files5.Length);
            Array.Sort(files);
            labelCurrectDir.Text = (dir + "\\").Replace("\\\\", "\\");
            Func<string, ListViewItem> text = new Func<string, ListViewItem>(filesView.Items.Add);
            foreach (string file in files)
            {
                Invoke(text, new object[1] { file.Substring(dir.Length + 1) });
            }
            Action<Image> addimage = new Action<Image>(fileImageList.Images.Add);
            foreach (ListViewItem file in filesView.Items)
            {
                try
                {
                    Stream stream = new FileStream(dir + "\\" + file.Text, FileMode.Open);
                    if (IsNumeric(stream))
                    {
                        file.ImageIndex = 0;
                    }
                    else
                    {
                        Image img = Image.FromStream(stream);
                        int sgn = Math.Sign((float)fileImageList.ImageSize.Width / (float)fileImageList.ImageSize.Height - (float)img.Width / (float)img.Height);
                        if (sgn > 0)
                        {
                            Image newImg = new Bitmap(fileImageList.ImageSize.Width, fileImageList.ImageSize.Height);
                            float size = (float)newImg.Height / img.Height * img.Width;
                            Graphics.FromImage(newImg).DrawImage(img, (newImg.Width - size) / 2.0F, 0, size, newImg.Height);
                            Invoke(addimage, new object[1] { newImg });
                        }
                        else if (sgn < 0)
                        {
                            Image newImg = new Bitmap(fileImageList.ImageSize.Width, fileImageList.ImageSize.Height);
                            float size = (float)newImg.Width / img.Width * img.Height;
                            Graphics.FromImage(newImg).DrawImage(img, 0, (newImg.Height - size) / 2.0F, newImg.Width, size);
                            Invoke(addimage, new object[1] { newImg });
                        }
                        else
                        {
                            BeginInvoke(addimage, new object[1] { img });
                        }
                        file.ImageIndex = fileImageList.Images.Count - 1;
                    }
                    stream.Close();
                }
                catch
                {
                }
            }
            CheckForIllegalCrossThreadCalls = true;
        }

        private void dirEnumerator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStatusLabel.Text = "Готов";
        }

        private void fileEnumerator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStatusLabel.Text = "Готов";
        }
	}
}
