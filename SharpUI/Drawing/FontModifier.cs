using System.Drawing;

namespace SharpUI.Drawing
{
	public class FontModifier
	{
		private Font font;

		private string name;
		private float size;
		private FontStyle style;

		public FontModifier(Font font)
		{
			this.font = font;

			name = font.Name;
			size = font.Size;
			style = font.Style;
		}

		public void SetName(string fontName)
		{
			font = new Font(fontName, size, style);
			name = fontName;
		}
		public void SetSize(float fontSize)
		{
			font = new Font(name, fontSize, style);
			size = fontSize;
		}
		public void SetStyle(FontStyle fontStyle)
		{
			font = new Font(name, size, fontStyle);
			style = fontStyle;
		}
		public void AddStyle(FontStyle fontStyle)
		{
			font = new Font(name, size, style | fontStyle);
			style |= fontStyle;
		}
		public void RemoveStyle(FontStyle fontStyle)
		{
			font = new Font(name, size, style & ~fontStyle);
			style = style & ~fontStyle;
		}

		public string GetFontName() => name;
		public float GetFontSize() => size;
		public FontStyle GetStyle() => style;
	}
}
