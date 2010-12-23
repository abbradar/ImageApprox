using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text;
using System.IO;

namespace ImageApprox
{
	/// <summary>
	/// Делегат функции, не принимающей и не отдающей значений.
	/// </summary>
	public delegate void Procedure();

	/// <summary>
	/// Форма генерации последовательностей случайных чисел.
	/// </summary>
	public partial class frmRandGen : Form
	{
		private double[] rndlist;
		private static Random rndgen;

		/// <summary>
		/// Получает случайное число с распределением по Гауссу.
		/// </summary>
		/// <returns>Случайное число.</returns>
		public static double GetNextRandom()
		{
            double x1, x2, w;
		    do
		    {
			    x1 = 2.0 * rndgen.NextDouble() - 1.0;
			    x2 = 2.0 * rndgen.NextDouble() - 1.0;
			    w = x1 * x1 + x2 * x2;
		    } while (w >= 1.0);
		    w = Math.Sqrt((-2.0 * Math.Log(w)) / w);
		    return x2 * w;
		}

		/// <summary>
		/// Создает экземпляр класса frmRandGen
		/// </summary>
		public frmRandGen()
		{
			InitializeComponent();
			rndgen = new Random();
		}		

		private void buttonGen_Click(object sender, EventArgs e)
		{
			if (numGenWorker.IsBusy)
			{
				numGenWorker.CancelAsync();
			}
			else
			{
				numGenWorker.RunWorkerAsync();
				buttonGen.Text = "Стоп";
				buttonSave.Enabled = false;
			}
		}

		private void numGenWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			double[] ampl, broad, centr;
			int lines = (int)numericLines.Value;
			int num = (int)numbersNum.Value;
			double ak = (double)numericSignal.Value;
			ampl = new double[lines];
			centr = new double[lines];
			broad = new double[lines];
			rndlist = new double[num];
			for (int i = 0; i < numericLines.Value; i++)
			{
                ampl[i] = ak * (1 + rndgen.NextDouble()) / 2.0;
                broad[i] = (num / 20.0) * (rndgen.NextDouble() + 0.1) ;
				centr[i] = 0.1 * num + (num * 0.8) * rndgen.NextDouble();
			}
			for (int gennum = 0; gennum < num; gennum++)
			{
                if (numGenWorker.CancellationPending)
				{
                    e.Cancel = true;
					return;
				}
				double rn = GetNextRandom();
				for (int i = 0; i < lines; i++)
				{
					 rn += ampl[i] * Math.Exp(-(centr[i] - gennum) * (centr[i] - gennum) / broad[i] / broad[i]);
				}
				rndlist[gennum] = rn;
			}
		}

		private void UpdateGenNum(int gennum)
		{
			if (showGenCheck.Checked)
			{
				labelGenNum.Text = gennum.ToString();
			}
		}

		private void numGenWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				labelGenNum.Text = "0";
			}
			else
			{
				labelGenNum.Text = rndlist.Length.ToString();
				buttonSave.Enabled = true;
			}
			buttonGen.Text = "Начать";
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			saveRandNums.ShowDialog();
		}

		private void saveRandNums_FileOk(object sender, CancelEventArgs e)
		{
			if (File.Exists(saveRandNums.FileName))
			{
				File.Delete(saveRandNums.FileName);
			}
			BinaryWriter file = new BinaryWriter(saveRandNums.OpenFile());
			file.Write(Encoding.Default.GetBytes("RNDB"));
			file.Write((ushort)sizeof(double));
			file.Write(rndlist.Length);
			foreach (double num in rndlist)
			{
				file.Write(num);
			}
			file.Close();
		}
	}
}

