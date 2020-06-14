using System;
using Tao.OpenGl;
using System.Collections;

namespace TareaGL
{
	/// <summary>
	/// Summary description for WindowedWall.
	/// </summary>
	public class DoorWall : Wall
	{
		protected SolidWall up;
		protected SolidWall left;
		protected SolidWall right;
		protected Puerta puerta;
		public DoorWall(Point3D from, Point3D to, double bottom, double height):this(from,to,bottom,height,false,false,true)
		{
		}

		public DoorWall(Point3D from, Point3D to, double bottom, double height,bool soloMarco,bool reversed, bool opened):base(from,to,bottom,height)
		{
			Point3D dir = (to-from);
			puerta = new Puerta();
			puerta.Angle= GlUtils.VectorAngle2D(dir);
			puerta.reversed=reversed;
			if (opened) puerta.Apertura=85;
			else puerta.Apertura=0;

			//puerta.Width = dir.Norm;
			double dist = dir.Norm;
			if (dist<=puerta.Width)
			{
				puerta.Location=from;
				puerta.Width=dist;
			}
			else 
			{
				Point3D d = dir.Normalized.Scaled((dist-puerta.Width)/2);
				left = new SolidWall(from,from+d,bottom,bottom+puerta.Height*1.1);
				right = new SolidWall(to-d,to,bottom,bottom+puerta.Height*1.1);
				left.after=to;
				right.before=from;
				puerta.Location=from+d;
			}
			up = new SolidWall(from,to,puerta.Height,height-puerta.Height);
			puerta.SoloMarco=soloMarco;
		}

		public override Point3D after 
		{
			set 
			{
				try 
				{
					up.after=value;
					right.after=value;
				} 
				catch (Exception){;}
			}
		}
		public override Point3D before 
		{
			set 
			{
				try 
				{
					up.before=value;
					left.before=value;
				} 
				catch (Exception){;}
			}
		}
		
		public override void Split(ArrayList far, ArrayList near) 
		{
			up.Split(far,near);
			puerta.Split(far,near);
			try
			{ 
				left.Split(far,near);
				right.Split(far,near);
			} 
			catch (Exception){;}
		}
		public override void Prepare (Avatar observer) 
		{		
			up.Prepare(observer);
			puerta.Prepare(observer);
			try 
			{
				left.Prepare(observer);
				right.Prepare(observer);
			} 
			catch (Exception){;}
		}
		public override void Render () 
		{
			up.Render();
			puerta.Render();
			try 
			{
				left.Render();
				right.Render();
			} 
			catch (Exception){;}
		}
		public override void CloseFrom(bool close) 
		{
			this.up.CloseFrom(close);
		}
		public override void CloseTo(bool close) 
		{
			this.up.CloseTo(close);
		}

		public override void FindTargetsFor(char c, ArrayList result) 
		{
			puerta.FindTargetsFor(c,result);
		}
		public override Point3D ColisionNormal(Point3D punto, Point3D direccion, double radio) 
		{
			if (!puerta.IsOpened)
				return base.ColisionNormal(punto,direccion,radio);
			else 
			{
				Point3D result = new Point3D(0,0,0);
				result+=up.ColisionNormal(punto,direccion,radio);
				if (this.left!=null) result+=left.ColisionNormal(punto,direccion+result,radio);
				if (this.right!=null) result+=right.ColisionNormal(punto,direccion+result,radio);
				return result;
			}
		}
	}
}



