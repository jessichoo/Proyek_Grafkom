using System;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Point3D.
	/// </summary>
	public class Point3D 
	{
		protected double x;
		protected double y;
		protected double z;
		public double X {get {return x;}}
		public double Y {get {return y;}}
		public double Z {get {return z;}}

		public Point3D(double x, double y, double z)
		{
			this.x=x; this.y=y; this.z=z;
		}
		public Point3D(double[] puntos):this(puntos[0],puntos[1],puntos[2])
		{
		}
		public Point3D(Point3D original):this(original.x,original.y,original.z){;}
		//protected double norma=-1;
		protected double norma=-1;
		public double Norm
		{
			get 
			{
				if (norma==-1) 
					norma=Math.Sqrt(x*x+y*y+z*z);
				return norma;				
			}
		}

		protected Point3D normalized=null;
		public Point3D Normalized 
		{
			get
			{
				if (normalized!=null) return normalized;
				double norma=Norm;
				if (norma==0) 				
				{
					throw new InvalidOperationException("No se puede normalizar el vector nulo");
				}

				normalized=new Point3D(X/norma,Y/norma,Z/norma); 
				return normalized;
			}
		}

		public Point3D Add(Point3D other) 
		{
			return new Point3D(X+other.X,Y+other.Y,Z+other.Z);
		}
		public static Point3D operator +(Point3D first, Point3D other) 
		{
			return first.Add(other);
		}
		public Point3D Inverse() {return new Point3D(-X,-Y,-Z);}
		public static Point3D operator -(Point3D first) {return first.Inverse();}
		public static Point3D operator -(Point3D first, Point3D second) {return first.Add(second.Inverse());}
		public Point3D CrossProduct(Point3D up) 
		{
			double x = ((this.y * up.z) - (this.z * up.y));													
			double y = ((this.z * up.x) - (this.x * up.z));
			double z = ((this.x * up.y) - (this.y * up.x));
			return new Point3D(x,y,z);
		}
		public void Translate(Point3D direction) 
		{
			this.x+=direction.x;
			this.y+=direction.y;
			this.z+=direction.z;
			invalidateCaches();
		}
		public Point3D Translated(Point3D direction) 
		{
			return this+direction;
		}
		protected void invalidateCaches() 
		{
			this.norma=-1;
			this.normalized=null;
		}
		public Point3D Scaled(double factor) 
		{
			return new Point3D(x*factor,y*factor,z*factor);
		}
		public Point3D Rotated(double angle, Point3D axis) 
		{
			//			if (angle==0 || axis.Norm==0) return new Point3D(this); 
			Point3D l = axis.Scaled(this.ScalarProduct(axis));
			Point3D r = this-l;
			if (r.Norm==0) return new Point3D(this); 
			double cosBeta=Math.Cos(angle*Math.PI/180);
			double sinBeta=Math.Sin(angle*Math.PI/180);
			return l+r.Scaled(cosBeta)+axis.CrossProduct(r).Normalized.Scaled(r.Norm*sinBeta);
			//return null;
		}
		public void Rotate(double angle, Point3D axis) 
		{
			Point3D temp = this.Rotated(angle,axis);
			this.x=temp.x;
			this.y=temp.y;
			this.z=temp.z;			
			invalidateCaches();
		}
		public double ScalarProduct(Point3D other) 
		{
			return x*other.x+y*other.y+z*other.z;
		}
		public override string ToString() 
		{
			return "X: "+x+" Y: "+y+" Z: "+z;
		}
		public double DistanceToPoint(Point3D other) 
		{
			return Math.Sqrt( (other.x - x) * (other.x - x) +
				(other.y - y) * (other.y - y) +
				(other.z - z) * (other.z - z) );
		}

		public double DistanceToSegment(Point3D v1, Point3D v2) 
		{
			double d1=this.DistanceToPoint(v1);
			double d2=this.DistanceToPoint(v2);
			double cosAlpha= (this-v1).Normalized.ScalarProduct((v2-v1).Normalized);
			double sinAlpha=Math.Sqrt(1-cosAlpha*cosAlpha);
			double dr=d1*sinAlpha;
			if (d1<d2) return Math.Min(d1,dr);
			else return Math.Min(d2,dr);
		}
		public double DistanceToPlane (Point3D v1, Point3D v2, Point3D v3) 
		{
			Point3D normal = (v1-v3).CrossProduct(v2-v3);
			double d1 = this.DistanceToPoint(v3);
			double cosAlpha=normal.Normalized.ScalarProduct((this-v3).Normalized);
			return Math.Abs(d1*cosAlpha);
		}
		public override bool Equals (Object o) 
		{
			Point3D p = o as Point3D;
			return (p!=null && p.x==x &&p.y==y && p.z==z);
		}

		public override int GetHashCode() 
		{
			return (int)(100*x+100*y+100*z);
		}
		public double[] Coords 
		{
			get 
			{
				double[] res = new double[3];
				res[0]=x;
				res[1]=y;
				res[2]=z;
				return res;
			}
		}
	}

	
	
}
