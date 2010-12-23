namespace ImageApprox
{
	partial class frmRandGen
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericSignal = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericLines = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.showGenCheck = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numbersNum = new System.Windows.Forms.NumericUpDown();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelGenNum = new System.Windows.Forms.Label();
            this.buttonGen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.saveRandNums = new System.Windows.Forms.SaveFileDialog();
            this.numGenWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSignal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numbersNum)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numericSignal);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericLines);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.showGenCheck);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numbersNum);
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.labelGenNum);
            this.groupBox1.Controls.Add(this.buttonGen);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 222);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Генерация последовательностей";
            // 
            // numericSignal
            // 
            this.numericSignal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numericSignal.Location = new System.Drawing.Point(9, 157);
            this.numericSignal.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericSignal.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericSignal.Name = "numericSignal";
            this.numericSignal.Size = new System.Drawing.Size(85, 20);
            this.numericSignal.TabIndex = 13;
            this.numericSignal.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Средняя амплитуда сигнала:";
            // 
            // numericLines
            // 
            this.numericLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numericLines.Location = new System.Drawing.Point(9, 118);
            this.numericLines.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericLines.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericLines.Name = "numericLines";
            this.numericLines.Size = new System.Drawing.Size(85, 20);
            this.numericLines.TabIndex = 10;
            this.numericLines.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Количество линий (неслучайных примесей):";
            // 
            // showGenCheck
            // 
            this.showGenCheck.Location = new System.Drawing.Point(9, 183);
            this.showGenCheck.Name = "showGenCheck";
            this.showGenCheck.Size = new System.Drawing.Size(294, 36);
            this.showGenCheck.TabIndex = 8;
            this.showGenCheck.Text = "Показывать количество посчитанных чисел (сильно замедляет работу)";
            this.showGenCheck.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Размер последовательности:";
            // 
            // numbersNum
            // 
            this.numbersNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numbersNum.Location = new System.Drawing.Point(9, 79);
            this.numbersNum.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numbersNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numbersNum.Name = "numbersNum";
            this.numbersNum.Size = new System.Drawing.Size(85, 20);
            this.numbersNum.TabIndex = 6;
            this.numbersNum.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(155, 19);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(123, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelGenNum
            // 
            this.labelGenNum.AutoSize = true;
            this.labelGenNum.Location = new System.Drawing.Point(101, 45);
            this.labelGenNum.Name = "labelGenNum";
            this.labelGenNum.Size = new System.Drawing.Size(13, 13);
            this.labelGenNum.TabIndex = 2;
            this.labelGenNum.Text = "0";
            // 
            // buttonGen
            // 
            this.buttonGen.Location = new System.Drawing.Point(9, 19);
            this.buttonGen.Name = "buttonGen";
            this.buttonGen.Size = new System.Drawing.Size(133, 23);
            this.buttonGen.TabIndex = 1;
            this.buttonGen.Text = "Начать";
            this.buttonGen.UseVisualStyleBackColor = true;
            this.buttonGen.Click += new System.EventHandler(this.buttonGen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сгенерировано:";
            // 
            // saveRandNums
            // 
            this.saveRandNums.DefaultExt = "rdb";
            this.saveRandNums.Filter = "Файлы последовательностей|*.rdb";
            this.saveRandNums.Title = "Сохранение последовательности";
            this.saveRandNums.FileOk += new System.ComponentModel.CancelEventHandler(this.saveRandNums_FileOk);
            // 
            // numGenWorker
            // 
            this.numGenWorker.WorkerReportsProgress = true;
            this.numGenWorker.WorkerSupportsCancellation = true;
            this.numGenWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.numGenWorker_DoWork);
            this.numGenWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.numGenWorker_RunWorkerCompleted);
            // 
            // frmRandGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 249);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRandGen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Создание последовательности";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSignal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numbersNum)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label labelGenNum;
		private System.Windows.Forms.Button buttonGen;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.NumericUpDown numbersNum;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.SaveFileDialog saveRandNums;
		private System.ComponentModel.BackgroundWorker numGenWorker;
		private System.Windows.Forms.CheckBox showGenCheck;
		private System.Windows.Forms.NumericUpDown numericLines;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericSignal;
		private System.Windows.Forms.Label label5;

	}
}

