using SharpUI.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SharpUI.Controls
{
	public class PictureBox : Control
	{
		private Image image;
		private Color noImageColor;

		public event EventHandler
			ImageChanged,
			NoImageColorChanged;

		protected virtual void OnImageChanged(EventArgs e)
		{
			ImageChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnNoImageColorChanged(EventArgs e)
		{
			NoImageColorChanged?.Invoke(this, e);
			Invalidate();
		}

		/// <summary>
		/// The image to display on the control.
		/// </summary>
		[Category("Appearance")]
		[Description("The image to display on the control.")]
		public Image Image
		{
			get { return image; }
			set { image = value; OnImageChanged(null); }
		}

		/// <summary>
		/// The color that will be used if there is no image being displayed on the control.
		/// </summary>
		[Category("Appearance")]
		[Description("The color that will be used if there is no image being displayed on the control.")]
		public Color NoImageColor
		{
			get { return noImageColor; }
			set { noImageColor = value; OnNoImageColorChanged(null); }
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor => Color.Empty;

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor => Color.Empty;

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text => string.Empty;

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font => new Font(FontFamily.GenericSansSerif, 1.0f);

		public PictureBox()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;

			Size = new Size(100, 75);
			noImageColor = SystemColors.ControlLight;
			image = null;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
		}
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			base.OnResize(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var gfx = e.Graphics;

			if (image == null)
			{
				gfx.FillRectangle(noImageColor.ToBrush(), new Rectangle(0, 0, Width, Height));
			}
			else
			{
				Rectangle result = RectangleDrawer.DeflateRect(ClientRectangle, Padding);

				result.Size = image.Size;
				gfx.DrawImage(image, result);
			}
		}
	}
}
