using System;
using System.Collections;

namespace TareaGL
{
	/// <summary>
	/// Summary description for WindowArray.
	/// </summary>
	public class WindowArray : Window
	{
		//protected Window[] ventanas;
		protected GlObjectList ventanas=new GlObjectList();
		protected double width=0;
		protected double height=0;
		public WindowArray(params Window[] ventanas)
		{
			//this.ventanas=ventanas;
			foreach (Window w in ventanas)
			{
				this.ventanas.Add(w);
				width+=w.Width;
				height = Math.Max(height,w.Height);
			}
		}

		public override void Split(ArrayList far, ArrayList near) 
		{
			ventanas.Split(far,near);
		}
		public override void Prepare (Avatar observer) 
		{
			ventanas.Prepare(observer);
		}
		public override void Render () 
		{
			ventanas.Render();
		}
		public override double Width { get { return width; } }
		public override double Height { get { return height; } }
		public override double Angle 
		{ 
			get { return angle;}
			set 
			{ 
				angle=value; 
				foreach (Window w in ventanas)
					w.Angle=value;
				Location = this.start;
			}
		}
		public override Point3D Location
		{ 
			get { return start;}
			set 
			{
				start=value; 
				double length=0;
				Point3D dir = new Point3D(Math.Cos(angle*Math.PI/180),0,Math.Sin(angle*Math.PI/180));
				foreach (Window w in ventanas)
				{
					w.Location=value+dir.Scaled(length);
					length+=w.Width;
				}
			}
		}
		public override void FindTargetsFor(char c, ArrayList result) 
		{
			this.ventanas.FindTargetsFor(c,result);
		}
	}
}
