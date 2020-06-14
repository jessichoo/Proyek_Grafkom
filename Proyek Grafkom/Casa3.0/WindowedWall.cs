using System;
using Tao.OpenGl;
using System.Collections;

namespace TareaGL
{
	/// <summary>
	/// Summary description for WindowedWall.
	/// </summary>
	public class WindowedWall : Wall
	{
		protected double windowBase =105;
		protected SolidWall izq;
		protected SolidWall der;
		protected SolidWall up;
		protected SolidWall down;
		protected Window ventana;
		public WindowedWall(Point3D from, Point3D to, double bottom, double height,Window window):base(from,to,bottom,height)
		{
			this.ventana=window;
			Point3D dir = (to-from).Normalized;

			ventana.Angle=GlUtils.VectorAngle2D(dir);
			double dist = (to-from).Norm;
			Point3D d = dir.Scaled((dist-ventana.Width)/2);
			izq = new SolidWall(from,from+d,bottom,height);
			der = new SolidWall(to-d,to,bottom,height);
			up = new SolidWall(from+d.Scaled(.9),to-d.Scaled(.9),bottom,windowBase);
			down = new SolidWall(from+d.Scaled(.9),to-d.Scaled(.9),windowBase+this.ventana.Height,(height-windowBase-ventana.Height));
			izq.after=to;
            up.after=to;
			down.after=to;
			der.before=from;
			up.before=from;
			down.before=from;
			Point3D wstart = from+d;
			this.ventana.Location=new Point3D(wstart.X,windowBase,wstart.Z);
			izq.CloseTo(false);
			der.CloseFrom(false);
			up.CloseFrom(false);
			up.CloseTo(false);
			down.CloseFrom(false);
			down.CloseTo(false);
		}

		public WindowedWall(Point3D from, Point3D to, double bottom, double height):this(from,to,bottom,height,new WindowArray(new WodenWindow(),new GlassWindow(),new WodenWindow()))
		{
		}

		public override Point3D after 
		{
			set 
			{
				der.after=value;
			}
		}
		public override Point3D before 
		{
			set 
			{
				izq.before=value;
			}
		}
		
		public override void Split(ArrayList far, ArrayList near) 
		{
			izq.Split(far,near);
			der.Split(far,near);
			up.Split(far,near);
			down.Split(far,near);
			ventana.Split(far,near);
		}		
		public override void Prepare (Avatar observer) 
		{		
			izq.Prepare(observer);
			der.Prepare(observer);
			up.Prepare(observer);
			down.Prepare(observer);
			ventana.Prepare(observer);
		}
		public override void Render () 
		{
			izq.Render();
			der.Render();
			up.Render();
			down.Render();
			ventana.Render();
		}
		public override void CloseFrom(bool close) 
		{
			this.izq.CloseFrom(close);
		}
		public override void CloseTo(bool close) 
		{
			this.der.CloseTo(close);
		}
		public override void FindTargetsFor(char c, ArrayList result) 
		{
			this.ventana.FindTargetsFor(c,result);
		}
	}
}

