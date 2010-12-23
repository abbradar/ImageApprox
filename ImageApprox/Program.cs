using System;
using System.Windows.Forms;

namespace ImageApprox
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmMain());
		}
	}

    /// <summary>
    /// Делегат функции, не получающей и не отдающей значения.
    /// </summary>
    public delegate void Action();
    /// <summary>
    /// Делегат функции, принимающей одно значение и возвращающей результат.
    /// </summary>
    /// <typeparam name="T">Принимаемое значение</typeparam>
    /// <typeparam name="D">Отдаваемое значение</typeparam>
    /// <param name="arg">Принимаемое значение</param>
    /// <returns>Отдаваемое значение</returns>
    public delegate D Func<T, D>(T arg);
}
