using SharpUI.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SharpUI.Controls
{
	public class GroupBox : ContainerControl
	{
		private Font titleFont = FontLoader.Regular12;
		private Color titleColor = Color.FromArgb(25, 25, 25);
		private Color borderColor = Color.FromArgb(50, 0, 0, 0);
		private Alignment textAlign = Alignment.Left;

		public event EventHandler TitleChanged;
		public event EventHandler BorderColorChanged;
		public event EventHandler TextAlignChanged;

		protected virtual void OnTitleChanged(EventArgs e)
		{
			TitleChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnBorderColorChanged(EventArgs e)
		{
			BorderColorChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			TextAlignChanged?.Invoke(this, e);
			Invalidate();
		}

		/// <summary>
		/// The font of the title.
		/// </summary>
		[Category("Appearance")]
		[Description("The font of the title.")]
		public Font TitleFont
		{
			get { return titleFont; }
			set { titleFont = value; OnTitleChanged(null); }
		}

		/// <summary>
		/// The color of the title text.
		/// </summary>
		[Category("Appearance")]
		[Description("The color of the title text.")]
		public Color TitleColor
		{
			get { return titleColor; }
			set { titleColor = value; OnTitleChanged(null); }
		}

		/// <summary>
		/// The color of the border of the group box.
		/// </summary>
		[Category("Appearance")]
		[Description("The color of the border of the group box.")]
		public Color BorderColor
		{
			get { return borderColor; }
			set { borderColor = value; OnBorderColorChanged(null); }
		}

		/// <summary>
		/// The alignment of the title text.
		/// </summary>
		[Category("Appearance")]
		[Description("The alignment of the title text.")]
		public Alignment TextAlign
		{
			get { return textAlign; }
			set { textAlign = value; OnTextAlignChanged(null); }
		}

		public GroupBox()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;

			Size = new Size(175, 150);
			titleFont = FontLoader.Regular11;
		}

		public enum Alignment
		{
			Left,
			Center,
			Right,
		}
		private StringFormat GetAlignment(Alignment a)
		{
			StringFormat sf = new StringFormat();
			switch (a)
			{
				case Alignment.Left:
					sf = new StringFormat
					{
						Alignment = StringAlignment.Near,
						LineAlignment = StringAlignment.Center,
					};
					break;
				case Alignment.Center:
					sf = new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center,
					};
					break;
				case Alignment.Right:
					sf = new StringFormat
					{
						Alignment = StringAlignment.Far,
						LineAlignment = StringAlignment.Center,
					};
					break;
			}
			return sf;
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var gfx = e.Graphics;

			RectangleDrawer drawer = new RectangleDrawer(gfx);
			drawer.DrawBorder(0, 0, Width, Height, borderColor, 1);
			gfx.FillRectangle(BackColor.ToBrush(), 1, 1, Width - 2, Height - 2);
			gfx.FillRectangle(BackColor.Darken(20).ToBrush(), new Rectangle(1, 1, Width - 2, TextRenderer.MeasureText(Text, titleFont).Height + 4));
			gfx.FillRectangle(borderColor.ToBrush(), 1, TextRenderer.MeasureText(Text, titleFont).Height + 3, Width - 2, 1);
			gfx.DrawString(Text, titleFont, titleColor.ToBrush(),
				new Rectangle(2, 1, Width - 2, TextRenderer.MeasureText(Text, titleFont).Height + 4),
				GetAlignment(textAlign));
		}
	}
}
