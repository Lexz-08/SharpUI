using SharpUI.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace SharpUI.Controls
{
	public class TabControl : System.Windows.Forms.TabControl
	{
		private Color activeTabColor;
		private Color inactiveTabColor;
		private Color activeTabForeColor;
		private Color inactiveTabForeColor;
		private Font tabFont;

		public event EventHandler
			ActiveTabColorChanged,
			InactiveTabColorChanged,
			ActiveTabForeColorChanged,
			InactiveTabForeColorChanged,
			TabFontChanged,
			TabAlignmentChanged;

		protected virtual void OnActiveTabColorChanged(EventArgs e)
		{
			ActiveTabColorChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnInactiveTabColorChanged(EventArgs e)
		{
			InactiveTabColorChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnActiveTabForeColorChanged(EventArgs e)
		{
			ActiveTabForeColorChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnInactiveTabForeColorChanged(EventArgs e)
		{
			InactiveTabForeColorChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnTabFontChanged(EventArgs e)
		{
			TabFontChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnTabAlignmentChanged(EventArgs e)
		{
			TabAlignmentChanged?.Invoke(this, e);
			Invalidate();
		}

		/// <summary>
		/// The color of the currently active tab.
		/// </summary>
		[Category("Appearance")]
		[Description("The color of the currently active tab.")]
		public Color ActiveTabColor
		{
			get { return activeTabColor; }
			set { activeTabColor = value; OnActiveTabColorChanged(null); }
		}

		/// <summary>
		/// The color of the inactive tab/s.
		/// </summary>
		[Category("Appearance")]
		[Description("The color of the inactive tab/s.")]
		public Color InactiveTabColor
		{
			get { return inactiveTabColor; }
			set { inactiveTabColor = value; OnInactiveTabColorChanged(null); }
		}

		/// <summary>
		/// The fore color of the currently active tab.
		/// </summary>
		[Category("Appearance")]
		[Description("The fore color of the currently active tab.")]
		public Color ActiveTabForeColor
		{
			get { return activeTabForeColor; }
			set { activeTabForeColor = value; OnActiveTabForeColorChanged(null); }
		}

		/// <summary>
		/// The fore color of the inactive tab/s.
		/// </summary>
		[Category("Appearance")]
		[Description("The fore color of the inactive tab/s.")]
		public Color InactiveTabForeColor
		{
			get { return inactiveTabForeColor; }
			set { inactiveTabForeColor = value; OnInactiveTabForeColorChanged(null); }
		}

		/// <summary>
		/// The font of each tab on the control.
		/// </summary>
		[Category("Appearance")]
		[Description("The font of each tab on the control.")]
		public Font TabFont
		{
			get { return tabFont; }
			set { tabFont = value; OnTabFontChanged(null); }
		}

		public override Rectangle DisplayRectangle
		{
			get
			{
				Rectangle rect = base.DisplayRectangle;
				return new Rectangle(
					rect.Left - 4,
					rect.Top - 4,
					rect.Width + 8,
					rect.Height + 8
					);
			}
		}
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;
				return cp;
			}
		}

		/// <summary>
		/// The alignment of the tabs on the control.
		/// </summary>
		[Category("Appearance")]
		[Description("The alignment of the tabs on the control.")]
		public TabAlignment Alignment
		{
			get { return base.Alignment; }
			set
			{
				switch (value)
				{
					case TabAlignment.Top:
						base.Alignment = value;
						break;
					case TabAlignment.Bottom:
						base.Alignment = value;
						break;
					case TabAlignment.Left:
						value = TabAlignment.Top;
						base.Alignment = value;
						break;
					case TabAlignment.Right:
						value = TabAlignment.Bottom;
						base.Alignment = value;
						break;
				}
				OnTabAlignmentChanged(null);
			}
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font => new Font(FontFamily.GenericSansSerif, 1.0f);

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var gfx = e.Graphics;

			gfx.SmoothingMode = SmoothingMode.HighQuality;
			gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
			gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			gfx.Clear(Parent.BackColor);

			for (int i = 0; i < TabCount; i++)
			{
				if (i == SelectedIndex)
				{
					Rectangle tabRect = GetTabRect(i);
					switch (Alignment)
					{
						case TabAlignment.Top:
							gfx.FillRectangle(activeTabColor.ToBrush(), new Rectangle(
								tabRect.X + 3, ItemSize.Height - 3, ItemSize.Width - 6, 3
								));
							gfx.DrawString(TabPages[i].Text, tabFont, activeTabForeColor.ToBrush(),
								new Rectangle(
									tabRect.X + 1, tabRect.Y + 1,
									tabRect.Width, tabRect.Height
									), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
							break;
						case TabAlignment.Bottom:
							gfx.FillRectangle(activeTabColor.ToBrush(), new Rectangle(
								tabRect.X, tabRect.Y + 2, ItemSize.Width - 6, 3
								));
							gfx.DrawString(TabPages[i].Text, tabFont, activeTabForeColor.ToBrush(),
								new Rectangle(
									tabRect.X - 1, tabRect.Y + 1,
									tabRect.Width, tabRect.Height
									), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
							break;
					}
				}
				else if (i != SelectedIndex)
				{
					Rectangle tabRect = GetTabRect(i);
					switch (Alignment)
					{
						case TabAlignment.Top:
							gfx.FillRectangle(inactiveTabColor.ToBrush(), new Rectangle(
								tabRect.X + 3, ItemSize.Height - 3, ItemSize.Width - 6, 3
								));
							gfx.DrawString(TabPages[i].Text, tabFont, inactiveTabForeColor.ToBrush(),
								new Rectangle(
									tabRect.X + 1, tabRect.Y + 1,
									tabRect.Width, tabRect.Height
									), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
							break;
						case TabAlignment.Bottom:
							gfx.FillRectangle(inactiveTabColor.ToBrush(), new Rectangle(
								tabRect.X, tabRect.Y + 2, ItemSize.Width - 6, 3
								));
							gfx.DrawString(TabPages[i].Text, tabFont, inactiveTabForeColor.ToBrush(),
								new Rectangle(
									tabRect.X - 1, tabRect.Y + 1,
									tabRect.Width, tabRect.Height
									), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
							break;
					}
				}
			}
		}

		public TabControl()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;

			activeTabColor = Color.DodgerBlue;
			activeTabForeColor = Color.Black;

			inactiveTabColor = Parent != null ? Parent.BackColor : Color.FromArgb(50, 0, 0, 0);
			inactiveTabForeColor = Color.FromArgb(150, 0, 0, 0);
			tabFont = FontLoader.Medium11;

			SizeMode = TabSizeMode.Fixed;
			ItemSize = new Size(120, 40);
		}
	}
}
