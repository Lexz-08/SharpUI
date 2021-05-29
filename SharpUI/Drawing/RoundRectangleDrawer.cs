using System.Drawing;
using System.Drawing.Drawing2D;

namespace SharpUI.Drawing
{
	public class RoundRectangleDrawer
    {
        private Graphics gfx;

        public RoundRectangleDrawer(Graphics Graphics)
		{
            gfx = Graphics;
		}

        public void FillArea(Rectangle area, int radius, Color fillColor)
		{
            GraphicsPath gfxPath = RoundRect1.CreateRoundRect(area.X, area.Y, area.Width, area.Height, radius);
            Region region = new Region(gfxPath);

            SmoothingMode originalAliasing = gfx.SmoothingMode;
            gfx.SmoothingMode = SmoothingMode.HighQuality;
            gfx.FillPath(fillColor.ToBrush(), gfxPath);
            gfx.DrawPath(new Pen(fillColor), gfxPath);
            gfx.SetClip(region, CombineMode.Replace);
            gfx.SmoothingMode = originalAliasing;
		}
        public void FillGradientArea(Rectangle area, int radius, Color fillColor1, Color fillColor2, int angle)
		{
            GraphicsPath gfxPath1 = RoundRect1.CreateRoundRect(area.X, area.Y, area.Width - 3, area.Height - 3, radius);
            GraphicsPath gfxPath2 = RoundRect1.CreateRoundRect(area.X, area.Y, area.Width - 2, area.Height - 2, radius);
            Region region = new Region(gfxPath2);

            SmoothingMode originalAliasing = gfx.SmoothingMode;
            gfx.SmoothingMode = SmoothingMode.HighQuality;
            gfx.FillPath(new LinearGradientBrush(area, fillColor1, fillColor2, angle), gfxPath1);
            gfx.DrawPath(new LinearGradientBrush(area, fillColor1, fillColor2, angle).ToPen(), gfxPath2);
            gfx.SetClip(region, CombineMode.Replace);
            gfx.SmoothingMode = originalAliasing;
		}
    }
}
