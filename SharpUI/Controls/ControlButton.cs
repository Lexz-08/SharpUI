using SharpUI.Drawing;
using SharpUI.Enums;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SharpUI.Controls
{
	public class ControlButton : Control
	{
		private ControlButtonType buttonType = ControlButtonType.Close;
		private Color controlColor = Color.White;
		private Form parentForm = null;
		private int pos = 8;
		private int otherPos = 4;

		public event EventHandler
			ButtonTypeChanged,
			ControlColorChanged,
			ImageSizeChanged,
			RestoreImgMarginChanged;

		protected virtual void OnButtonTypeChanged(EventArgs e)
		{
			ButtonTypeChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnControlColorChanged(EventArgs e)
		{
			ControlColorChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnImageSizeChanged(EventArgs e)
		{
			ImageSizeChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnRestoreImgMarginChanged(EventArgs e)
		{
			RestoreImgMarginChanged?.Invoke(this, e);
			Invalidate();
		}

		/// <summary>
		/// The type of button this control acts as.
		/// </summary>
		[Category("Appearance")]
		[Description("The type of button this control acts as.")]
		public ControlButtonType ButtonType
		{
			get { return buttonType; }
			set { buttonType = value; OnButtonTypeChanged(null); }
		}

		/// <summary>
		/// The color of the image on the button.
		/// </summary>
		[Category("Appearance")]
		[Description("The color of the image on the button.")]
		public Color ControlColor
		{
			get { return controlColor; }
			set { controlColor = value; OnControlColorChanged(null); }
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor => Color.Empty;

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font => new Font("Microsoft Sans Serif", 0.1f);

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text => string.Empty;

		public ControlButton()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;

			Size = new Size(32, 32);
			BackColor = Color.Red;

			if (Parent != null) parentForm = FindForm();

			Anchor = AnchorStyles.Top | AnchorStyles.Right;
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);

			if (Parent != null)
			{
				parentForm = FindForm();
				switch (buttonType)
				{
					case ControlButtonType.Close:
						parentForm.Close();
						break;
					case ControlButtonType.Minimize:
						parentForm.WindowState = FormWindowState.Minimized;
						break;
					case ControlButtonType.Maximize:
						switch (parentForm.WindowState)
						{
							case FormWindowState.Normal:
								parentForm.WindowState = FormWindowState.Maximized;
								break;
							case FormWindowState.Maximized:
								parentForm.WindowState = FormWindowState.Normal;
								break;
						}
						break;
				}
			}
			Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var gfx = e.Graphics;

			SmoothingMode original = gfx.SmoothingMode;
			gfx.SmoothingMode = SmoothingMode.None;
			gfx.FillRectangle(BackColor.ToBrush(), 0, 0, Width, Height);
			gfx.SmoothingMode = original;

			Point[] closePts1 = new Point[]
			{
				new Point(pos, pos),
				new Point(Width - pos, Height - pos),
			};
			Point[] closePts2 = new Point[]
			{
				new Point(pos, Height - pos),
				new Point(Width - pos, pos),
			};
			Point[] minPts = new Point[]
			{
				new Point(pos, Height - pos),
				new Point(Width - pos, Height - pos),
			};
			Point[] maxPts = new Point[]
			{
				new Point(pos, pos),
				new Point(pos, Height - pos),
				new Point(Width - pos, Height - pos),
				new Point(Width - pos, pos),
				new Point(pos - 1, pos),
			};
			Point[] resPts1 = new Point[]
			{
				new Point(pos, pos + otherPos),
				new Point(pos, Height - pos),
				new Point(Width - pos - otherPos, Height - pos),
				new Point(Width - pos - otherPos, pos + otherPos),
				new Point(pos - 1, pos + otherPos),
			};
			Point[] resPts2 = new Point[]
			{
				new Point(pos + otherPos, pos + otherPos),
				new Point(pos + otherPos, pos),
				new Point(Width - pos, pos),
				new Point(Width - pos, Height - pos - otherPos),
				new Point(Width - pos - otherPos, Height - pos - otherPos),
				new Point(Width - pos - otherPos, pos + otherPos),
				new Point(pos + otherPos, pos + otherPos),
			};

			if (Parent != null)
			{
				parentForm = FindForm();
				switch (buttonType)
				{
					case ControlButtonType.Close:
						SmoothingMode o1 = gfx.SmoothingMode;
						gfx.SmoothingMode = SmoothingMode.HighQuality;
						gfx.DrawLines(new Pen(controlColor, 2), closePts1);
						gfx.DrawLines(new Pen(controlColor, 2), closePts2);
						gfx.SmoothingMode = o1;
						break;
					case ControlButtonType.Minimize:
						SmoothingMode o2 = gfx.SmoothingMode;
						gfx.SmoothingMode = SmoothingMode.None;
						gfx.DrawLines(new Pen(controlColor, 2), minPts);
						gfx.SmoothingMode = o2;
						break;
					case ControlButtonType.Maximize:
						switch (parentForm.WindowState)
						{
							case FormWindowState.Normal:
								SmoothingMode o3 = gfx.SmoothingMode;
								gfx.SmoothingMode = SmoothingMode.None;
								gfx.DrawLines(new Pen(controlColor, 2), maxPts);
								gfx.SmoothingMode = o3;
								break;
							case FormWindowState.Maximized:
								SmoothingMode o4 = gfx.SmoothingMode;
								gfx.SmoothingMode = SmoothingMode.None;
								gfx.DrawLines(new Pen(controlColor, 2), resPts1);
								gfx.DrawLines(new Pen(controlColor, 2), resPts2);
								gfx.SmoothingMode = o4;
								break;
						}
						break;
				}
			}
		}
	}
}
