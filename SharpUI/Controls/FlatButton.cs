using SharpUI.Drawing;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace SharpUI.Controls
{
	public class FlatButton : Control
	{
		public FlatButton()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;

			Size = new Size(143, 41);
			BackColor = Color.DodgerBlue;
			ForeColor = Color.White;

			Font = FontLoader.Regular12;
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

			gfx.SmoothingMode = SmoothingMode.None;
			gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			gfx.TextContrast = 0;

			gfx.FillRectangle(BackColor.ToBrush(), 0, 0, Width, Height);
			gfx.DrawString(Text, Font, ForeColor.ToBrush(), new Rectangle(1, 2, Width, Height),
				new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
		}
	}
}
