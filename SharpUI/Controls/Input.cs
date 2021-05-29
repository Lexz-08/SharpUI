using SharpUI.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SharpUI.Controls
{
	public class Input : Control
	{
		private TextBox input;
		private Label lbl;
		private Color underline_current = Color.Gray;
		private Color underline_focused = Color.DodgerBlue;
		private Color underline_unfocused = Color.Gray;
		private int maxLength = 32767;
		private bool readOnly = false;
		private bool useSystemPasswordChar = false;

		public event EventHandler
			UnderlineChanged,
			MaxLengthChanged,
			ReadOnlyChanged,
			UseSystemPasswordCharChanged;

		protected virtual void OnUnderlineChanged(EventArgs e)
		{
			UnderlineChanged?.Invoke(this, e);

			if (Focused)
			{
				underline_current = underline_focused;
			}
			else
			{
				underline_current = underline_unfocused;
			}

			Invalidate();
		}
		protected virtual void OnMaxLengthChanged(EventArgs e)
		{
			MaxLengthChanged?.Invoke(this, e);
			input.MaxLength = maxLength;
			Invalidate();
		}
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			ReadOnlyChanged?.Invoke(this, e);
			Invalidate();
		}
		protected virtual void OnUseSystemPasswordCharChanged(EventArgs e)
		{
			UseSystemPasswordCharChanged?.Invoke(this, e);
			input.UseSystemPasswordChar = useSystemPasswordChar;
			Invalidate();
		}

		/// <summary>
		/// The color of the underline when focused.
		/// </summary>
		[Category("Appearance")]
		[Description("The color of the underline when focused.")]
		public Color UnderlineFocused
		{
			get { return underline_focused; }
			set { underline_focused = value; OnUnderlineChanged(null); }
		}

		/// <summary>
		/// The color of the underline when not focused.
		/// </summary>
		[Category("Appearance")]
		[Description("The color of the underline when not focused.")]
		public Color UnderlineUnfocused
		{
			get { return underline_unfocused; }
			set { underline_unfocused = value; OnUnderlineChanged(null); }
		}

		/// <summary>
		/// How many characters are allowed in the text box.
		/// </summary>
		[Category("Appearance")]
		[Description("How many characters are allowed in the text box.")]
		public int MaxLength
		{
			get { return maxLength; }
			set { maxLength = value; OnMaxLengthChanged(null); }
		}

		/// <summary>
		/// Determines whether or not the text box's text can be edited.
		/// </summary>
		[Category("Appearance")]
		[Description("Determines whether or not the text box's text can be edited.")]
		public bool ReadOnly
		{
			get { return readOnly; }
			set { readOnly = value; OnReadOnlyChanged(null); }
		}

		/// <summary>
		/// Determines whether or not the <em><strong><see langword="SYSTEM PASSWORD"/></strong></em> character will be used when displaying text in the text box.
		/// </summary>
		[Category("Appearance")]
		[Description("Determines whether or not the SYSTEM PASSWORD character will be used when displaying text in the text box.")]
		public bool UseSystemPasswordChar
		{
			get { return useSystemPasswordChar; }
			set { useSystemPasswordChar = value; OnUseSystemPasswordCharChanged(null); }
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public Color UnderlineCurrent
		{
			get { return underline_current; }
			set { underline_current = value; OnUnderlineChanged(null); }
		}

		public Input()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;

			ForeColor = Color.FromArgb(25, 25, 25);
			BackColor = SystemColors.Control;
			Font = FontLoader.Regular11;

			Width = 425;
			AddTextBox();

			Timer t = new Timer { Interval = 1 };
			t.Tick += (sender, e) =>
			{
				input.ForeColor = ForeColor;
				input.BackColor = BackColor;
				input.Font = Font;
				input.Width = Width - 6;
			};
			t.Start();

			input.Text = Text = string.Empty;

			input.TextChanged += (sender, e) => Text = input.Text;
			TextChanged += (sender, e) => input.Text = Text;
		}

		private void AddTextBox()
		{
			lbl = new Label
			{
				BackColor = BackColor,
				ForeColor = BackColor,
				Font = Font,
				Location = new Point(Bottom + 10000000, Right + 10000000),
			};
			input = new TextBox
			{
				Location = new Point(3, 2),
				Font = FontLoader.Regular11,
				ForeColor = ForeColor,
				BackColor = BackColor,
				BorderStyle = BorderStyle.None,
			};

			input.GotFocus += (sender, e) => OnGotFocus(e);
			input.LostFocus += (sender, e) => OnLostFocus(e);

			Controls.Add(input);
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);

			if (readOnly)
			{
				Parent.Controls.Add(lbl);
				lbl.Focus();
			}
			else
			{
				underline_current = underline_focused;
				Invalidate();
			}
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);

			underline_current = underline_unfocused;
			Invalidate();
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

			gfx.FillRectangle(BackColor.ToBrush(), 0, 0, Width, Height - 2);
			gfx.FillRectangle(underline_current.ToBrush(), 0, input.Bottom, Width, 2);
		}
	}
}