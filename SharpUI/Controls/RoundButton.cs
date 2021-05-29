using SharpUI.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Transitions;

namespace SharpUI.Controls
{
	public class RoundButton : Control
	{
		private int radius = 3;
		private Color backcolor_current;
		private Color backcolor_focused;
		private Color backcolor_unfocused;

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

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public Color BackColorCurrent
		{
			get { return backcolor_current; }
			set { backcolor_current = value; Invalidate(); }
		}

		public RoundButton()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;

			Size = new Size(143, 41);
			BackColor = Color.DodgerBlue;
			ForeColor = Color.White;

			backcolor_current = BackColor;
			backcolor_focused = BackColor.Darken(20);
			backcolor_unfocused = BackColor.Brighten(20);

			Font = FontLoader.Regular12;
		}

		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);

			backcolor_unfocused = BackColor.CanBrighten(20) ? BackColor.Brighten(20) : BackColor.Darken(20);
			backcolor_focused = BackColor.CanDarken(20) ? BackColor.Darken(20) : BackColor.Brighten(20);

			Transition.run(this, "BackColorCurrent", BackColor,
				new TransitionType_Linear(100));
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			Transition.run(this, "BackColorCurrent", backcolor_focused, new TransitionType_Linear(100));
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			Transition.run(this, "BackColorCurrent", backcolor_unfocused, new TransitionType_Linear(100));
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

			gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			gfx.TextContrast = 0;

			GraphicsPath gfxPath = RoundRect1.CreateRoundRect(0, 0, Width - 1, Height - 1, radius);
			Region region = new Region(gfxPath);

			gfx.SmoothingMode = SmoothingMode.HighQuality;
			gfx.FillPath(backcolor_current.ToBrush(), gfxPath);
			gfx.DrawPath(backcolor_current.ToPen(), gfxPath);
			gfx.SetClip(region, CombineMode.Replace);
			gfx.SmoothingMode = SmoothingMode.None;

			gfx.DrawString(Text, Font, ForeColor.ToBrush(),
				new Rectangle(0, 2, Width, Height),
				new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
		}
	}
}
