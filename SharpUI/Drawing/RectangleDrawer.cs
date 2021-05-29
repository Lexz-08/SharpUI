using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SharpUI.Drawing
{
	public class RectangleDrawer
	{
		private Graphics gfx;

		public RectangleDrawer(Graphics Graphics)
		{
			gfx = Graphics;
		}

		public void FillArea(Rectangle area, Color fillColor)
		{
			gfx.FillRectangle(fillColor.ToBrush(), area);
		}
		public void DrawBorder(Rectangle area, Color fillColor, int width)
		{
			int dw = width * 2;

			gfx.FillRectangle(fillColor.ToBrush(),
				area.X, area.Y, width, width);                                            // top left
			gfx.FillRectangle(fillColor.ToBrush(),
				area.X + width, area.Y, area.Width - dw, width);                          // top
			gfx.FillRectangle(fillColor.ToBrush(),
				area.X + area.Width - width, area.Y, width, width);                       // top right
			gfx.FillRectangle(fillColor.ToBrush(),
				area.X, area.Y + width, width, area.Height - dw);                         // left
			gfx.FillRectangle(fillColor.ToBrush(),
				area.X + area.Width - width, area.Y + width, width, area.Height - dw);    // right
			gfx.FillRectangle(fillColor.ToBrush(),
				area.X, area.Y + area.Height - width, width, width);                      // bottom left
			gfx.FillRectangle(fillColor.ToBrush(),
				area.X + width, area.Y + area.Height - width, area.Width - dw, width);    // bottom
			gfx.FillRectangle(fillColor.ToBrush(),
				area.X + area.Width - width, area.Y + area.Height - width, width, width); // bottom right
		}
		public void FillGradientArea(Rectangle area, Color fillColor1, Color fillColor2, int angle)
		{
			Brush gb = Converter.ToGradientBrush(fillColor1, fillColor2, area, angle);
			gfx.FillRectangle(gb, area);
		}

		public void FillArea(Point location, Size size, Color fillColor) => FillArea(new Rectangle(location, size), fillColor);
		public void DrawBorder(Point location, Size size, Color fillColor, int width) => DrawBorder(new Rectangle(location, size), fillColor, width);
		public void FillGradientArea(Point location, Size size, Color fillColor1, Color fillColor2, int angle) => FillGradientArea(new Rectangle(location, size), fillColor1, fillColor2, angle);

		public void FillArea(int x, int y, Size size, Color fillColor) => FillArea(new Point(x, y), size, fillColor);
		public void DrawBorder(int x, int y, Size size, Color fillColor, int width) => DrawBorder(new Point(x, y), size, fillColor, width);
		public void FillGradientArea(int x, int y, Size size, Color fillColor1, Color fillColor2, int angle) => FillGradientArea(new Point(x, y), size, fillColor1, fillColor2, angle);

		public void FillArea(Point location, int width, int height, Color fillColor) => FillArea(location, new Size(width, height), fillColor);
		public void DrawBorder(Point location, int sz_width, int height, Color fillColor, int width) => DrawBorder(location, new Size(sz_width, height), fillColor, width);
		public void FillGradientArea(Point location, int width, int height, Color fillColor1, Color fillColor2, int angle) => FillGradientArea(location, new Size(width, height), fillColor1, fillColor2, angle);

		public void FillArea(int x, int y, int width, int height, Color fillColor) => FillArea(new Rectangle(x, y, width, height), fillColor);
		public void DrawBorder(int x, int y, int sz_width, int height, Color fillColor, int width) => DrawBorder(new Rectangle(x, y, sz_width, height), fillColor, width);
		public void FillGradientArea(int x, int y, int width, int height, Color fillColor1, Color fillColor2, int angle) => FillGradientArea(new Rectangle(x, y, width, height), fillColor1, fillColor2, angle);

		public Color ColorFromBitmap(int x, int y, Bitmap bmp)
		{
			return bmp.GetPixel(x, y);
		}
		public Color BlendColors(Color color1, Color color2, int blendYPos)
		{
			int bmpSize = 200;
			Bitmap bmp = new Bitmap(bmpSize, bmpSize);
			using (Graphics graphics = Graphics.FromImage(bmp))
			{
				Brush gb = new LinearGradientBrush(new Rectangle(0, 0, bmpSize, bmpSize), color1, color2, 90);
				graphics.FillRectangle(gb, new Rectangle(0, 0, bmpSize, bmpSize));
			}
			if (blendYPos < 0)
				throw new ArgumentOutOfRangeException("blendYPos", "Cannot go out of the color area.");
			else if (blendYPos >= 0 || blendYPos <= bmpSize)
				return ColorFromBitmap(bmpSize / 2, blendYPos, bmp);
			else if (blendYPos > bmpSize)
				throw new ArgumentOutOfRangeException("blendYPos", "Cannot go out of the color area.");
			return Color.Empty;
		}
		public Color BlendColors(Color color1, Color color2) => BlendColors(color1, color2, 85);

		public static Rectangle DeflateRect(Rectangle rect, Padding padding)
		{
			rect.X += padding.Left;
			rect.Y += padding.Top;
			rect.Width -= padding.Horizontal;
			rect.Height -= padding.Vertical;
			return rect;
		}
		public static RectangleF DeflateRectF(RectangleF rect, Padding padding)
		{
			rect.X += padding.Left;
			rect.Y += padding.Top;
			rect.Width -= padding.Horizontal;
			rect.Height -= padding.Vertical;
			return rect;
		}
	}
}
