using System.Drawing;
using System.Drawing.Drawing2D;

namespace SharpUI.Drawing
{
	public class AdvancedDrawer
	{
		private Graphics graphics;
		private int pos;
		private Color color;

		public AdvancedDrawer(Graphics Graphics, int Position, Color Color)
		{
			graphics = Graphics;
			pos = Position;
			color = Color;
		}

		public void DrawX(Rectangle bounds)
		{
			SmoothingMode original = graphics.SmoothingMode;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.DrawLines(new Pen(color, 2), new Point[]
			{
				new Point(pos, pos),
				new Point(bounds.Width - pos, bounds.Height - pos),
			});
			graphics.DrawLines(new Pen(color, 2), new Point[]
			{
				new Point(pos, bounds.Height - pos),
				new Point(bounds.Width - pos, pos),
			});
			graphics.SmoothingMode = original;
		}
		public void Draw_(Rectangle bounds)
		{
			SmoothingMode original = graphics.SmoothingMode;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.DrawLines(new Pen(color, 2), new Point[]
			{
				new Point(pos, bounds.Height - pos),
				new Point(bounds.Width - pos, bounds.Height - pos),
			});
			graphics.SmoothingMode = original;

		}
		public void DrawO(Rectangle bounds)
		{
			SmoothingMode original = graphics.SmoothingMode;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.DrawLines(new Pen(color, 2), new Point[]
			{
				new Point(pos, pos),
				new Point(pos, bounds.Height - pos),
				new Point(bounds.Width - pos, bounds.Height - pos),
				new Point(bounds.Width - pos, pos),
				new Point(pos, pos),
			});
			graphics.SmoothingMode = original;
		}
		public void DrawOO(Rectangle bounds)
		{
			SmoothingMode original = graphics.SmoothingMode;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.DrawLines(new Pen(color, 2), new Point[]
			{
				new Point(pos, pos + 4),
				new Point(pos, bounds.Height - pos),
				new Point(bounds.Width - pos - 4, bounds.Height - pos),
				new Point(bounds.Width - pos - 4, pos + 4),
				new Point(pos, pos + 4),
			});
			graphics.DrawLines(new Pen(color, 2), new Point[]
			{
				new Point(pos + 4, pos + 4),
				new Point(pos + 4, pos),
				new Point(bounds.Width - pos, pos),
				new Point(bounds.Width - pos, bounds.Height - pos - 4),
				new Point(bounds.Width - pos - 4, bounds.Height - pos - 4),
				new Point(bounds.Width - pos - 4, pos + 4),
				new Point(pos + 4, pos + 4),
			});
			graphics.SmoothingMode = original;
		}
	}
}
