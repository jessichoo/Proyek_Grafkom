using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Wall.
	/// </summary>
	public abstract class Wall : GlObject
	{
		public double Deep=1;
		protected double width;
		protected const int baseDeep=5;
		protected Point3D endSideNormal;
		protected Point3D frontSideNormal;

		public Wall(Point3D from, Point3D to, double bottom, double height)
		{
			this.from=from;
			this.to=to;
			this.frontSideNormal = this.planeRot(to-from,90).Normalized;
			this.endSideNormal = (to-from).Normalized;
			this.dirFrom = frontSideNormal;
			this.dirTo=this.dirFrom;
			this.bottom=bottom;
			this.height=height;
			this.width=(to-from).Norm;
			//this.calcPoints();
		}

		protected bool closeFrom = true;
		protected bool closeTo = true;

		protected Point3D planeRot(Point3D v, double angle) 
		{
			double ro = v.Norm;
			double senAlpha= Math.Sin(angle*Math.PI/180);
			double cosAlpha= Math.Cos(angle*Math.PI/180);
			double senSita = v.Z/ro;
			double cosSita = v.X/ro;
			return new Point3D(ro*(cosSita*cosAlpha-senSita*senAlpha),0,ro*(senSita*cosAlpha+cosSita*senAlpha));
		}

		protected Point3D from;
		protected Point3D to;
		public Point3D To { get {return to;}}
		public Point3D From { get {return from;}}
		protected double bottom;
		protected double height;
		public double Bottom { get {return bottom;}}
		public double Height { get {return height+600;}}
		public abstract Point3D before	{set;}
		public abstract Point3D after {set;}
		public virtual void CloseFrom(bool close){this.closeFrom=close;} 
		public virtual void CloseTo(bool close){closeTo=close;} 


		protected Point3D dirFrom;
		protected Point3D dirTo;

		public override Point3D ColisionNormal(Point3D punto, Point3D direccion, double radius) 
		{
			//Console.WriteLine("Llamando a CN de wall");
			Point3D punto2 = punto+direccion;
			if (punto2.Y<this.bottom || punto2.Y>this.bottom+this.height)
				return new Point3D(0,0,0); //No hay colision
			punto2 = new Point3D(punto2.X,0,punto2.Z);
//			if ((punto2-from).Norm > width || (punto2-to).Norm>width)
//				return new Point3D(0,0,0);
			if (((to-from).Norm<radius/2)) //La pared es muy pequenna... Ignorare la colision con esta pared.
				return new Point3D(0,0,0);
			if ((punto2-(to-from).Scaled(.5)-from).Norm>(width+radius)/2) //No puede haber colision
				return new Point3D(0,0,0);
			Point3D walldir = (to-from).Normalized;
			Point3D exto = to+walldir.Scaled(1.1);//radius/2);
			Point3D exfrom = from-walldir.Scaled(1.1);//radius/2);
			Point3D pos = (punto2-exfrom).Normalized;
			double d=0;

			#region punto2 esta antes o despues de la pared. No hay colision.
			if (walldir.ScalarProduct(pos)<0) // El punto esta antes de from
			{
//				d=(punto2-frome).Norm;
//				if (d<radius)
//					return pos.Scaled(radius-d);
//				else 
					return new Point3D(0,0,0);
			}
			if ((punto2-exto).ScalarProduct(-walldir)<0) // El punto esta despues del to
			{
//				d=(punto2-to).Norm;
//				if (d<radius)
//					return (punto2-to).Normalized.Scaled(radius-d);
//				else 
					return new Point3D(0,0,0);
			}
			#endregion
			
			Point3D referencia;
			if ((from-punto2).Norm>(to-punto2).Norm)
				referencia=from;
			else
				referencia=to;

			Point3D pr = (to-from).CrossProduct(new Point3D(0,1,0)).Normalized;
			double d1 = this.distancePointRect(punto.X,punto.Z,from.X,from.Z,to.X,to.Z);
			double d2 = this.distancePointRect(punto2.X,punto2.Z,from.X,from.Z,to.X,to.Z);
			if (d1*d2<0) //semiplanos distintos, hay colision
				//Console.WriteLine("d1: "+d1+" d2: "+d2);
				return pr.Scaled(-Math.Sign(d2)*radius-d2);
				//return pr.Scaled(radius+d2);
				//Console.WriteLine("Semiplanos distintos, hay colision");
			else 
				if (Math.Abs(d2)<radius)
					return pr.Scaled(Math.Sign(d2)*radius-d2);
//			Point3D pr=this.planeRot(to-from,90).Normalized;;
//
//			Point3D punto1 = new Point3D(punto.X,0,punto.Z);
//			d=(punto1-referencia).Norm;			
//			Point3D v2 = pr.Scaled(d*pr.ScalarProduct((punto2-referencia).Normalized));
//			Point3D v1 = pr.Scaled(d*pr.ScalarProduct((punto1-referencia).Normalized));					
//			if (v1.Normalized.ScalarProduct(v2.Normalized)>0) // Estan en el mismo semiplano
//			{
////				double distance = distancePointRect(punto2.X,punto2.Z,to.X,to.Z,from.X,from.Z);
//				if (v2.Norm<radius)
//					return v2.Normalized.Scaled(radius-v2.Norm);
////				if (Math.Abs(distance)<radius)
////					return pr.Normalized.Scaled(radius+distance);
//				else return new Point3D(0,0,0);
//			}
//			else //Estan en semiplanos distintos ==> hubo colision
//			{
//				return -v2.Normalized.Scaled(radius)-v2;
//			}
			return new Point3D(0,0,0);
		}
		protected double distancePointRect(double x1, double y1, double x2,double y2, double x3, double y3) 
		{
			return (x1*y2-x1*y3-y1*x2+y1*x3+x2*y3-x3*y2)/Math.Sqrt((x2-x3)*(x2-x3)+(y2-y3)*(y2-y3));
		}
	}
}
