using SharpUI.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SharpUI.Controls
{
	public class ProgressBar : Control
	{
		private float progress = 0.0f;
		private float progress_max = 100.0f;
		private Color progressColor = Color.Empty;

		public event EventHandler
			ProgressChanged,
			ProgressColorChanged;

		protected virtual void OnProgressChanged(EventArgs e)
		{
			ProgressChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnProgressColorChanged(EventArgs e)
		{
			ProgressColorChanged?.Invoke(this, e);
			Invalidate();
		}

		/// <summary>
		/// The current value of the progress in the control.
		/// </summary>
		[Category("Appearance")]
		[Description("The current value of the progress in the control.")]
		public float Progress
		{
			get { return progress; }
			set
			{
				if (value > progress_max)
				{
					value = progress_max;
					progress = value;
				}
				else if (value < 0)
				{
					value = 0;
					progress = value;
				}
				else
				{
					progress = value;
				}
				OnProgressChanged(null);
			}
		}

		/// <summary>
		/// The maximum value of the progress in the control.
		/// </summary>
		[Category("Appearance")]
		[Description("The maximum value of the progress in the control.")]
		public float MaxProgress
		{
			get { return progress_max; }
			set
			{
				if (value < progress)
				{
					progress = value;
					progress_max = value;
				}
				else if (progress_max <= 0)
				{
					MessageBox.Show("Maximum progress value cannot be equal to or less than 0.", "Too Low",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					progress_max = 100.0f;
					progress = 25.0f;
				}
				else
				{
					progress_max = value;
				}
				OnProgressChanged(null);
			}
		}

		/// <summary>
		/// The color of the progress in the control.
		/// </summary>
		[Category("Appearance")]
		[Description("The color of the progress in the control.")]
		public Color ProgressColor
		{
			get { return progressColor; }
			set { progressColor = value; OnProgressColorChanged(null); }
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text => string.Empty;

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font => new Font(FontFamily.GenericSansSerif, 1f);

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor => Color.Empty;

		public ProgressBar()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;

			Size = new Size(400, 10);
			BackColor = Parent != null ? Parent.BackColor.Darken(20) : Color.FromArgb(10, 0, 0, 0);
			progressColor = Color.FromArgb(30, 0, 0, 0);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var gfx = e.Graphics;

			float prog = progress / progress_max;
			Rectangle backRect = new Rectangle(0, 0, Width, Height);
			RectangleF progRect = new RectangleF(0, 0, Width * prog, Height);

			gfx.FillRectangle(BackColor.ToBrush(), backRect);
			gfx.FillRectangle(progressColor.ToBrush(), progRect);
		}
	}
}
