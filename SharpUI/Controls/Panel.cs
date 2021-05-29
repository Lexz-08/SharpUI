using SharpUI.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace SharpUI.Controls
{
	public class Panel : System.Windows.Forms.Panel
	{
		private int radius = 3;

		public event EventHandler RadiusChanged;
		protected virtual void OnRadiusChanged(EventArgs e)
		{
			RadiusChanged?.Invoke(this, e);
			Invalidate();
		}

		/// <summary>
		/// The radius of the corners.
		/// </summary>
		[Category("Appearance")]
		[Description("The radius of the corners.")]
		public int CornerRadius
		{
			get { return radius; }
			set
			{
				if (value < 0)
				{
					MessageBox.Show("Cannot go below a radius of 0.", "Too Low", MessageBoxButtons.OK, MessageBoxIcon.Error);
					value = 0;
					radius = value;
				}
				else if (value > (Math.Min(Width, Height) / 2) - 2)
				{
					MessageBox.Show("Cannot set the radius to above the Width/Height.", "Too High", MessageBoxButtons.OK, MessageBoxIcon.Error);
					value = (Math.Min(Width, Height) / 2) - 2;
					radius = value;
				}
				else
				{
					radius = value;
				}
				OnRadiusChanged(null);
			}
		}

		public Panel()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;

			Size = new Size(250, 225);
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
			gfx.Clear(Parent.BackColor);

			gfx.SmoothingMode = SmoothingMode.HighQuality;
			gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			gfx.TextContrast = 0;

			GraphicsPath gfxPath = RoundRect1.CreateRoundRect(0, 0, Width - 1, Height - 1, radius);
			GraphicsPath gfxPath_outter = RoundRect1.CreateRoundRect(-1, -1, Width + 1, Width + 1, radius);
			Region region = new Region(gfxPath);
			Region = new Region(gfxPath_outter);

			gfx.FillPath(BackColor.ToBrush(), gfxPath);
			gfx.DrawPath(BackColor.ToPen(), gfxPath);
			gfx.SetClip(region, CombineMode.Replace);
		}
	}
}
