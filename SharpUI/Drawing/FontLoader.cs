using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace SharpUI.Drawing
{
	public class FontLoader
	{
		public static Font LoadFont(byte[] buffer, float size)
		{
			GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			try
			{
				IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
				using (PrivateFontCollection pfc = new PrivateFontCollection())
				{
					pfc.AddMemoryFont(ptr, buffer.Length);
					return new Font(pfc.Families[0].Name, size);
				}
			}
			finally
			{
				handle.Free();
			}
		}

		public static Font Regular9 => LoadFont(Resources.Roboto_Regular, 9f);
		public static Font Medium9 => LoadFont(Resources.Roboto_Medium, 9f);
		public static Font Bold9 => LoadFont(Resources.Roboto_Bold, 9f);

		public static Font Regular10 => LoadFont(Resources.Roboto_Regular, 10f);
		public static Font Medium10 => LoadFont(Resources.Roboto_Medium, 10f);
		public static Font Bold10 => LoadFont(Resources.Roboto_Bold, 10f);

		public static Font Regular11 => LoadFont(Resources.Roboto_Regular, 11f);
		public static Font Medium11 => LoadFont(Resources.Roboto_Medium, 11f);
		public static Font Bold11 => LoadFont(Resources.Roboto_Bold, 11f);

		public static Font Regular12 => LoadFont(Resources.Roboto_Regular, 12f);
		public static Font Medium12 => LoadFont(Resources.Roboto_Medium, 12f);
		public static Font Bold12 => LoadFont(Resources.Roboto_Bold, 12f);
	}
}
