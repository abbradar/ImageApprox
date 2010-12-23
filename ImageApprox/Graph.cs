using System;
using System.Drawing;

namespace ImageApprox
{
	/// <summary>
	/// Реализует возможность строить графики на основе заданного массива чисел и некоторых параметров.
	/// График строится только в первой четверти оси координат.
	/// </summary>
	public class Graph
	{
		private double[] data1, data2;
		private double max, setmax;
		private Bitmap result;
		private bool inited, calc, haxis, vaxis, underfill, strings, hascenter, automax;
		private int width, height;
		private Font font;
		private Brush bkg, fillbrush1, fillbrush2, fontbrush;
		private string xstr, ystr;
		private Pen axispen, graphpen1, graphpen2;
        private double step;

		/// <summary>
		/// Инициализирует класс Graph.
		/// </summary>
		public Graph()
		{
			width = 0;
			height = 0;
			inited = false;
			calc = false;
			haxis = false;
			vaxis = false;
			underfill = false;
			strings = false;
			hascenter = true;
			automax = true;
			xstr = "";
			ystr = "";
			setmax = -1;
            step = 0;
			bkg = new SolidBrush(Color.White);
			axispen = new Pen(Color.Blue);
			fillbrush1 = new SolidBrush(Color.Black);
			fillbrush2 = new SolidBrush(Color.Blue);
			fontbrush = new SolidBrush(Color.Red);
			graphpen1 = new Pen(Color.Black);
			graphpen2 = new Pen(Color.Blue);
			font = new Font(FontFamily.GenericMonospace, 10);
		}


        /// <summary>
        /// Коеффициент увеличения для графика.
        /// </summary>
        public double Zoom
        {
            get
            {
                return step;
            }
        }

		/// <summary>
		/// Включает или отключает подписи на графике
		/// </summary>
		public bool Annotations
		{
			get
			{
				return strings;
			}
			set
			{
				strings = value;
				inited = false;
			}
		}

		/// <summary>
		/// Название оси по вертикали.
		/// </summary>
		public string VerticalAnnotation
		{
			get
			{
				return ystr;
			}
			set
			{
				ystr = value;
				inited = false;
			}
		}

		/// <summary>
		/// Включает или отключает расположение оси абсцисс посередине.
		/// </summary>
		public bool HasCenter
		{
			get
			{
				return hascenter;
			}
			set
			{
				hascenter = value;
				inited = false;
			}
		}

		/// <summary>
		/// Название оси по горизонтали.
		/// </summary>
		public string HorizontalAnnotation
		{
			get
			{
				return xstr;
			}
			set
			{
				xstr = value;
			}
		}

		/// <summary>
		/// Устанавливает свойства фона под графиком.
		/// </summary>
		public Brush Background
		{
			get
			{
				return bkg;
			}
			set
			{
				bkg = (Brush)value.Clone();
				inited = false;
			}
		}

		/// <summary>
		/// Устанавливает свойства кисти для текста на осях координат.
		/// </summary>
		public Brush AxisFontBrush
		{
			get
			{
				return fontbrush;
			}
			set
			{
				fontbrush = (Brush)value.Clone();
				inited = false;
                inited = true;
			}
		}

		/// <summary>
		/// Устанавливает свойства линий осей координат. Не имеет значения, если не включено построение осей.
		/// </summary>
		public Pen AxisPen
		{
			get
			{
				return axispen;
			}
			set
			{
				axispen = (Pen)value.Clone();
				inited = false;
			}
		}

		/// <summary>
		/// Устанавливает свойства заполнения под вторым графиком. Не имеет значения, если не включено заполнение.
		/// </summary>
		public Brush SecondBrush
		{
			get
			{
				return fillbrush1;
			}
			set
			{
				fillbrush1 = (Brush)value.Clone();
				inited = false;
			}
		}

		/// <summary>
		/// Устанавливает свойства заполнения под первым графиком. Не имеет значения, если не включено заполнение.
		/// </summary>
		public Brush FirstBrush
		{
			get
			{
				return fillbrush1;
			}
			set
			{
				fillbrush1 = (Brush)value.Clone();
				inited = false;
			}
		}

		/// <summary>
		/// Устанавливает свойства линии первого графика.
		/// </summary>
		public Pen FirstPen
		{
			get
			{
				return graphpen1;
			}
			set
			{
				graphpen1 = (Pen)value.Clone();
				inited = false;
			}
		}

		/// <summary>
		/// Устанавливает свойства линии второго графика.
		/// </summary>
		public Pen SecondPen
		{
			get
			{
				return graphpen2;
			}
			set
			{
				graphpen2 = (Pen)value.Clone();
				inited = false;
			}
		}

		/// <summary>
		/// Включает или отключает автоматический расчет максимума функции.
		/// </summary>
		public bool AutoMax
		{
			get
			{
				return automax;
			}
			set
			{
				if (value != automax)
				{
					automax = value;
					inited = false;
				}
			}
		}

		/// <summary>
		/// Максимум функции. Используется при отключенном автовычислении максимума для отрисовки графика.
		/// </summary>
		public double Max
		{
			get
			{
				return setmax;
			}
			set
			{
				if (value != setmax)
				{
					if (value <= 0)
					{
						throw new ArgumentOutOfRangeException("Максимум функции должен быть больше нуля.");
					}
					setmax = value;
					if (!automax)
					{
						inited = false;
					}
				}
			}
		}

		/// <summary>
		/// Устанавливает опцию заполнения места под графиком.
		/// </summary>
		public bool Underfill
		{
			get
			{
				return underfill;
			}
			set
			{
				underfill = value;
				inited = false;
			}
		}

		/// <summary>
		/// Устанавливает построение горизонтальной оси координат.
		/// </summary>
		public bool HorizontalAxis
		{
			get
			{
				return haxis;
			}
			set
			{
				haxis = value;
				inited = false;
			}
		}

		/// <summary>
		/// Устанавливает построение вертикальной оси координат.
		/// </summary>
		public bool VerticalAxis
		{
			get
			{
				return vaxis;
			}
			set
			{
				vaxis = value;
				inited = false;
			}
		}

		/// <summary>
		/// Шрифт для подписей рядом с осями координат.
		/// </summary>
		public Font AxisFont
		{
			get
			{
				return font;
			}
			set
			{
				font = (Font)value.Clone();
				inited = false;
			}
		}

		/// <summary>
		/// Значения функции, для которой необходимо построить график. Располагаются спереди.
		/// </summary>
		public double[] SecondValues
		{
			get
			{
				return data2;
			}
			set
			{
                if (value != null)
                {
                    if (value.Length <= 0)
                    {
                        throw new ArgumentException();
                    }
                    data2 = (double[])value.Clone();
                    inited = false;
                    calc = false;
                }
                else
                {
                    data2 = null;
                }
			}
		}

		/// <summary>
		/// Значения функции, для которой необходимо построить график. Располагаются сзади.
		/// </summary>
		public double[] FirstValues
		{
			get
			{
				return data1;
			}
			set
			{
                if (value != null)
                {
                    if (value.Length <= 0)
                    {
                        throw new ArgumentException();
                    }
                    data1 = (double[])value.Clone();
                    inited = false;
                    calc = false;
                }
                else
                {
                    data1 = null;
                }
			}
		}

		/// <summary>
		/// Ширина получаемого графика.
		/// </summary>
		public int Width
		{
			get
			{
				return width;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				width = value;
				inited = false;
			}
		}

		/// <summary>
		/// Высота получаемого графика.
		/// </summary>
		public int Height
		{
			get
			{
				return height;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				height = value;
				inited = false;
			}
		}

		/// <summary>
		/// Обновляет построенный график.
		/// </summary>
		private void UpdateGraph()
		{
			if (Width <= 0 || Height <= 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			double max = automax ? this.max : this.setmax;
			result = new Bitmap(Width, Height);
			Graphics graph = Graphics.FromImage(result);
			graph.FillRectangle(Background, 0, 0, Width, Height);
			double hpos = 0;
			int vbottom = hascenter ? (Height - 1) / 2 : Height - 1;
			if (VerticalAxis)
			{
				graph.DrawLine(AxisPen, 0, 0, 0, Height - 1);
				hpos += AxisPen.Width;
			}
			if (HorizontalAxis)
			{
				graph.DrawLine(AxisPen, 0, vbottom, Width - 1, vbottom);
			}
			Region fill1 = null;
			Region fill2 = null;
			if (max > 0)
			{
				double k = hascenter ? (Height - 1) / max / 2 : (Height - 1) / max;
				if (FirstValues != null)
				{
					fill1 = DrawGraphLine(graph, FirstValues, hpos, Width, vbottom, k, FirstPen);
				}
				if (SecondValues != null)
				{
					fill2 = DrawGraphLine(graph, SecondValues, hpos, Width, vbottom, k, SecondPen);
				}
			}
			if (underfill)
			{
				graph.FillRegion(FirstBrush, fill1);
				graph.FillRegion(SecondBrush, fill2);
			}
			if (strings)
			{
				graph.DrawString("0", AxisFont, AxisFontBrush, AxisPen.Width + 1,
					vbottom - graph.MeasureString("0", AxisFont).Height);
				graph.DrawString(max.ToString("F1"), AxisFont, AxisFontBrush, AxisPen.Width + 1, 1);
				StringFormat vertical = new StringFormat(StringFormatFlags.DirectionVertical);
				if (VerticalAnnotation != "")
				{
					SizeF size = graph.MeasureString(VerticalAnnotation, AxisFont, AxisFont.Height,
						vertical);
					graph.DrawString(VerticalAnnotation, AxisFont, AxisFontBrush, Width - 2 - size.Width,
						(Height - graph.MeasureString(VerticalAnnotation, AxisFont, AxisFont.Height,
						vertical).Height) / 2, vertical);
				}
				if (HorizontalAnnotation != "")
				{
					SizeF size = graph.MeasureString(HorizontalAnnotation, AxisFont);
					graph.DrawString(HorizontalAnnotation, AxisFont, AxisFontBrush, (Width - size.Width) / 2,
						Height - 4 - size.Height);
				}
			}
			inited = true;
		}

		private Region DrawGraphLine(Graphics graph, double[] data, double hpos, double width, int vbottom,
			double k, Pen pen)
		{
			Point start, end;
            start = new Point((int)Math.Floor(hpos), (int)Math.Round(vbottom - data[0] * k));
			if (data.Length == 1)
			{
				end = new Point((int)(hpos + width), start.Y);
				graph.DrawLine(pen, start, end);
				return new Region(new Rectangle(start, new Size((int)width, (int)(data[0] * k))));
			}
			step = (width - hpos) / data.Length;
			start = new Point((int)Math.Floor(hpos), (int)Math.Round(vbottom - data[0] * k));
            end = Point.Empty;
			Rectangle add = new Rectangle(start.X, start.Y, 1, (int)(vbottom - start.Y));
			Region fill = new Region(add);
			for (int i = 1; i < data.Length; i++)
			{
				end = new Point((int)Math.Floor(hpos + step * i), (int)Math.Round(vbottom - data[i] * k));
				graph.DrawLine(pen, start, end);
				start = end;
				add = new Rectangle(start.X, start.Y, 1, (int)(vbottom - start.Y));
				fill.Union(add);
			}
			return fill;
		}

		/// <summary>
		/// Находит максимум из двух массивов данных.
		/// </summary>
		private void CalcMax()
		{
			double max = 0;
			if (FirstValues != null)
			{
				for (int i = 0; i < FirstValues.Length; i++)
				{
					double mod = Math.Abs(FirstValues[i]);
					if (mod > max)
					{
						max = mod;
					}
				}
			}
			if (SecondValues != null)
			{
				for (int i = 0; i < SecondValues.Length; i++)
				{
					double mod = Math.Abs(SecondValues[i]);
					if (mod > max)
					{
						max = mod;
					}
				}
			}
			this.max = max;
			calc = true;
		}

		/// <summary>
		/// Обновляет кэш графика при изменении параметров построения и возвращает результат.
		/// </summary>
		/// <returns>Изображение графика.</returns>
		public Image GetGraph()
		{
            if (FirstValues != null || SecondValues != null)
            {
                if (automax)
                {
                    if (!calc)
                    {
                        CalcMax();
                    }
                }
                else if (setmax < 0)
                {
                    throw new InvalidOperationException("Необходимо задать максимум функции вручную.");
                }
                if (!inited)
                {
                    UpdateGraph();
                }
                return result;
            }
            else
            {
                throw new ArgumentNullException("Необходимо задать значения для работы.");
            }
		}
	}
}

