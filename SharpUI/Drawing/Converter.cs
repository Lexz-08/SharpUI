using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SharpUI.Drawing
{
	public static class Converter
	{
		public static Brush ToBrush(this Color color)
		{
			return new SolidBrush(color);
		}
		public static Brush ToGradientBrush(Color color1, Color color2, Rectangle area, int angle)
		{
			return new LinearGradientBrush(area, color1, color2, angle);
		}

		public static Color ToColor(this Brush b)
		{
			return ((SolidBrush)b).Color;
		}

		public static Pen ToPen(this Color c)
		{
			return new Pen(c);
		}
		public static Pen ToPen(this Color c, float width)
		{
			return new Pen(c, width);
		}

		public static Pen ToPen(this Brush b)
		{
			return new Pen(b);
		}
		public static Pen ToPen(this Brush b, float width)
		{
			return new Pen(b, width);
		}

		public static Color Darken(this Color color, byte amount)
		{
			return Color.FromArgb(
				color.R - amount >= 0 ? color.R - amount : color.R,
				color.G - amount >= 0 ? color.G - amount : color.G,
				color.B - amount >= 0 ? color.B - amount : color.B
				);
		}
		public static Color Brighten(this Color color, byte amount)
		{
			return Color.FromArgb(
				color.R + amount <= 255 ? color.R + amount : color.R,
				color.G + amount <= 255 ? color.G + amount : color.G,
				color.B + amount <= 255 ? color.B + amount : color.B
				);
		}

		public static Brush Darken(Brush b, byte amount)
		{
			SolidBrush sb = (SolidBrush)b;
			Color c = sb.Color;
			c = Darken(c, amount);
			return new SolidBrush(c);
		}
		public static Brush Brighten(Brush b, byte amount)
		{
			SolidBrush sb = (SolidBrush)b;
			Color c = sb.Color;
			c = Brighten(c, amount);
			return new SolidBrush(c);
		}

		public static bool CanDarken(this Color c, byte amount)
		{
			if (c.R - amount >= 0 || c.G - amount >= 0 || c.B - amount >= 0)
				return true;
			else return false;
		}
		public static bool CanBrighten(this Color c, byte amount)
		{
			if (c.R + amount <= 255 || c.G + amount <= 255 || c.B + amount <= 255)
				return true;
			else return false;
		}

		public static Color RaiseAlpha(this Color c, byte amount)
		{
			if (c.A + amount <= 255)
				return Color.FromArgb(c.A + amount, c);
			else return c;
		}
		public static Color LowerAlpha(this Color c, byte amount)
		{
			if (c.A - amount >= 0)
				return Color.FromArgb(c.A - amount, c);
			else return c;
		}
	}
}
