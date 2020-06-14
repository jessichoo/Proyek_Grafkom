using System;
using System.Runtime.InteropServices;

namespace Platform.Windows
{
	/// <summary>
	/// Summary description for WinApi.
	/// </summary>
	public class WinApi
	{
		[DllImport("user32.dll")]
		public static extern void SetCursorPos(int x,int y);
		[DllImport("user32.dll")]
		public static extern void GetCursorPos(ref Point point);
		/* Codigo escrito por roman para llamar al API */
		[DllImport("kernel32.dll")]
		public static extern void Beep(int freq,int dur);

		/**/
		public static Point GetCursorPos() 
		{
			Point p=new Point();
			WinApi.GetCursorPos(ref p);
			return p;
		}

		public struct Point 
		{
			public int x;
			public int y;
		}
	}
}
