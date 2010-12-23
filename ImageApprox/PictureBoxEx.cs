using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ImageApprox
{
	/// <summary>
	/// Предоставляет элемент управления для показа изображений с расширенными возможностями.
	/// </summary>
	public class PictureBoxEx : ScrollableControl
	{
		/// <summary>
		/// Инициализирует новый экземпляр класса PictureBoxEx.
		/// </summary>
		public PictureBoxEx()
		{
			InitComponent();

			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            scl = new Point(0, 0);
			base.AutoScroll = true;
            this.Paint += new PaintEventHandler(PictureBoxEx_Paint);
            this.Scroll += new ScrollEventHandler(PictureBoxEx_Scroll);
		}

        private void PictureBoxEx_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                scl.X = e.NewValue;
            }
            else
            {
                scl.Y = e.NewValue;
            }
        }

        private Point scl;

        /// <summary>
        /// Значение горизонтальной полосы прокрутки.
        /// </summary>
        [Browsable(false)]
        public int HScrollValue
        {
            get
            {
                return scl.X;
            }
            set
            {
                if (value < HorizontalScroll.Minimum || value > HorizontalScroll.Maximum)
                {
                    throw new ArgumentOutOfRangeException("Значение полосы прокрутки должно принадлежать границам от минимума до максимума.");
                }
                scl.X = value;
                AutoScrollPosition = scl;
            }
        }

        /// <summary>
        /// Значение вертикальной полосы прокрутки.
        /// </summary>
        [Browsable(false)]
        public int VScrollValue
        {
            get
            {
                return scl.Y;
            }
            set
            {
                if (value < VerticalScroll.Minimum || value > VerticalScroll.Maximum)
                {
                    throw new ArgumentOutOfRangeException("Значение полосы прокрутки должно принадлежать границам от минимума до максимума.");
                }
                scl.Y = value;
                AutoScrollPosition = scl;
            }
        }

		private void InitComponent()
		{
			this.SuspendLayout();
			this.ResumeLayout(false);
		}

		private Image image, output;

		/// <summary>
		/// Устанавливает тип рамки вокруг элемента управления.
		/// </summary>
		[DefaultValue(BorderStyle.None)]
		public BorderStyle BorderStyle
		{
			get
			{
				return _borderStyle;
			}
			set
			{
				if (_borderStyle != value)
				{
					_borderStyle = value;

					Invalidate();

					if (BorderStyleChanged != null)
						BorderStyleChanged(this, new EventArgs());
				}
			}
		}
		private BorderStyle _borderStyle;
        
        private Point _hPoint;

        /// <summary>
        /// Точка, которая будет подсвечена пересечением линий на изображении.
        /// </summary>
        [Browsable(false)]
        public Point HighlightPoint
        {
            get
            {
                return _hPoint;
            }
            set
            {
                if (_hPoint != value)
                {
                    _hPoint = value;
                    UpdateOutput();
                    Invalidate();
                }
            }
        }

        private void UpdateOutput()
        {
            if (image != null)
            {
                Bitmap temp = new Bitmap(image);
                if (HighlightPoint.X >= 0 && HighlightPoint.X < temp.Width)
                {
                    for (int i = 0; i < temp.Height; i++)
                    {
                        Color pixel = temp.GetPixel(HighlightPoint.X, i);
                        temp.SetPixel(HighlightPoint.X, i, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                    }
                }
                if (HighlightPoint.Y >= 0 && HighlightPoint.Y < temp.Height)
                {
                    for (int i = 0; i < temp.Width; i++)
                    {
                        Color pixel = temp.GetPixel(i, HighlightPoint.Y);
                        temp.SetPixel(i, HighlightPoint.Y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                    }
                }
                output = temp;
            }
            else
            {
                output = null;
            }
        }

		/// <summary>
		/// Вызывается при изменении стиля рамки.
		/// </summary>
		public event EventHandler BorderStyleChanged;

		/// <summary>
		/// Устанавливает изображение для просмотра.
		/// </summary>
		[DefaultValue(null)]
		public Image Image
		{
			get
			{
				return image;
			}
			set
			{
				if (image != value)
				{
					if (value == null)
					{
						image.Dispose();
						image = null;
                        if (ImageChanged != null)
                        {
                            ImageChanged(this, new EventArgs());
                        }
					}
					else
					{
						image = value;
                        this.AutoScrollMinSize = image.Size;
                        this.AutoScrollPosition = new Point(0, 0);
                        scl = new Point(0, 0);
                        _hPoint = new Point(-1, -1);
					}
                    if (ImageChanged != null)
                    {
                        ImageChanged(this, new EventArgs());
                    }
                    UpdateOutput();
                    Invalidate();
				}
			}
		}

		/// <summary>
		/// Вызывается при смене показываемого изображения.
		/// </summary>
		public event EventHandler ImageChanged;

		private void PictureBoxEx_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);

			if (image != null)
			{
                e.Graphics.DrawImage(output, AutoScrollPosition.X + (BorderStyle == BorderStyle.FixedSingle ? 1 : 0) +
                    (BorderStyle == BorderStyle.Fixed3D ? 2 : 0), AutoScrollPosition.Y + (BorderStyle == BorderStyle.FixedSingle ? 1 : 0) +
                    (BorderStyle == BorderStyle.Fixed3D ? 2 : 0), output.Width, output.Height);
			}

			DrawBorder(e.Graphics);

            //HorizontalScroll.Value = hscl;
            //VerticalScroll.Value = vscl;
		}

		private void DrawBorder(Graphics g)
		{
			int hScrollHeight  = HScroll ? 17 : 0;
			int vScrollWidth   = VScroll ? 17 : 0;

			switch (BorderStyle)
			{
				case BorderStyle.Fixed3D :
					Pen outsideUpperLeft = new Pen(Color.FromArgb(172, 168, 152), 1);
					g.DrawLine(outsideUpperLeft, new Point(0, Height - 1), new Point(0, 0));
					g.DrawLine(outsideUpperLeft, new Point(0, 0), new Point(Width - 1));

					Pen insideUpperLeft = new Pen(Color.FromArgb(113, 111, 110), 1);
					g.DrawLine(insideUpperLeft, new Point(1, Height - 2), new Point(1, 1));
					g.DrawLine(insideUpperLeft, new Point(1, 1), new Point(Width - 2, 1));

					Pen outsideLowerRight = new Pen(Color.FromArgb(255, 255, 255), 1);
					g.DrawLine(outsideLowerRight, new Point(0, Height - 1 - hScrollHeight), 
						new Point(Width - 1, Height - 1 - hScrollHeight));
					g.DrawLine(outsideLowerRight, new Point(Width - 1 - vScrollWidth, 
						Height - 1), new Point(Width - 1 - vScrollWidth, 0));

					Pen insideLowerRight = new Pen(Color.FromArgb(241, 239, 226), 1);
					g.DrawLine(insideLowerRight, new Point(1, Height - 2 - hScrollHeight), 
						new Point(Width - 2, Height - 2 - hScrollHeight));
					g.DrawLine(insideLowerRight, new Point(Width - 2 - vScrollWidth, 
						Height - 2), new Point(Width - 2 - vScrollWidth, 2));

					break;
				case BorderStyle.FixedSingle:
					Pen fixedSingle = new Pen(Color.Black, 1);
					g.DrawRectangle(fixedSingle, 0, 0, Width - 1 - vScrollWidth, Height - 1 - hScrollHeight);
					break;
			}
		}
	}

	/// <summary>
	/// Перечисляет возможные направления передвижения бегунков.
	/// </summary>
	public enum ScrollDirection
	{
		/// <summary>
		/// Влево.
		/// </summary>
		Left = 37,
		/// <summary>
		/// Вверх.
		/// </summary>
		Up = 38,
		/// <summary>
		/// Вправо.
		/// </summary>
		Right = 39,
		/// <summary>
		/// Вниз.
		/// </summary>
		Down = 40,
	}
}