using System;
using System.Collections;

namespace TareaGL
{
	/// <summary>
	/// Summary description for InvertedWall.
	/// </summary>
	public class InvertedWall:Wall
	{
		protected Wall real;
		public InvertedWall(Wall w): base(w.To,w.From,w.Bottom,w.Height)
		{
			real=w;
		}
		public override void Render() { real.Render(); }
		public override void Prepare (Avatar observer) {real.Prepare(observer);}
		public override void Split (ArrayList far, ArrayList near) 
		{
			real.Split(far,near);
		}
		public override void CloseFrom(bool close) 
		{
			real.CloseTo(close);
		}
		public override void CloseTo(bool close) 
		{
			real.CloseFrom(close);
		}

		public override void FindTargetsFor(char c, ArrayList result) 
		{
			real.FindTargetsFor(c,result);
		}

		public override Point3D after { set { real.before=value; }}
		public override Point3D before { set { real.after=value; }}
	}
}
