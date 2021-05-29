using SharpUI.Drawing;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharpUI.Controls
{
	public class Label : System.Windows.Forms.Label
	{
		public Label()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;

			ForeColor = Color.FromArgb(25, 25, 25);
			Font = FontLoader.Regular12;
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
		}
	}
}
