using System;
using System.Collections;

namespace TareaGL
{
	/// <summary>
	/// Summary description for GlObject.
	/// </summary>
	public abstract class GlObject
	{
		public GlObject()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public virtual void Prepare(Avatar observer) {;}
		public abstract void Render();
		public virtual void Split (ArrayList far, ArrayList near) 
		{
			far.Add(this);
		}
		public virtual Point3D ColisionNormal(Point3D origin,Point3D direction, double radius)
		{
			return new Point3D(0,0,0);
		}
		public virtual void FindTargetsFor(char c, ArrayList result) 
		{
			if ((this is InteractiveObject) && (this as InteractiveObject).HasActionFor(c))
				result.Add(this);
		}
	}

	public interface MetricObject 
	{
		double DistanceTo(Point3D other);
	}
	public interface InteractiveObject :MetricObject
	{
		bool HasActionFor(char command);
		void Act(char command);
	}

	/**
	 * 
	 * GlObjectList es el unico que garantiza que pintara primero
	 * su far, y luego su near, ordenadamente.
	 * 
	 **/
	public class GlObjectList : GlObject,IEnumerable 
	{
		protected ArrayList far = new ArrayList();
		protected ArrayList near = new ArrayList();
		protected ArrayList objects = new ArrayList();
		public GlObjectList(){;}
		public void Add (GlObject obj) 
		{
			objects.Add(obj);
			obj.Split(far,near);
		}
		public override void Split(ArrayList far, ArrayList near) 
		{
			foreach (GlObject obj in this.objects)
				obj.Split(far,near);
		}
		public override void Prepare(Avatar observer) 
		{
			foreach (GlObject obj in objects)
				obj.Prepare(observer);
			this.sortList(near,observer.Origin);
		}

		public override void Render() 
		{
			foreach (GlObject obj in far)
				obj.Render();
			foreach (GlObject obj in near)
				obj.Render();
		}
		public override Point3D ColisionNormal(Point3D origin, Point3D direction, double radius) 
		{
			Point3D result = new Point3D(0,0,0);
			foreach (GlObject obj in objects)
				result+=obj.ColisionNormal(origin,direction+result,radius);
			return result;
		}
		
		protected void sortList(ArrayList near,Point3D location) 
		{
			// Si, estoy ordenando por burbuja a proposito.
			// En el contexto en que es llamado este metodo,
			// 'near' casi siempre esta casi ordenado, por
			// lo que burbuja lo ordena en O(n) en casi todos
			// los casos, contra O(n log n) del quicksort.
			bool cambio=true;
			double [] distances = new double[near.Count];
			while (cambio)
			{
				cambio=false;
				for (int i =0; i<near.Count-1;i++)
				{
					double d1 = 0;
					if (distances[i]!=0) d1=distances[i]-1;
					else
						try 
						{
							d1=(near[i] as MetricObject).DistanceTo(location);
						}
						catch (Exception) {d1=double.MaxValue-1;}
						finally	{distances[i]=d1+1;}
					double d2 = 0;
					if (distances[i+1]!=0) d1=distances[i+1]-1;
					else
						try 
						{
							d2=(near[i+1] as MetricObject).DistanceTo(location);
						}
						catch (Exception) {d2=double.MaxValue-1;}
						finally	{distances[i+1]=d2+1;}
					if (d1<d2) 
					{
						object tmp = near[i];
						near[i]=near[i+1];
						near[i+1]=tmp;
						double t = distances[i];
						distances[i]=distances[i+1];
						distances[i+1]=t;
						cambio=true;
					}
				}
			}
		}

		public IEnumerator GetEnumerator() { return this.objects.GetEnumerator();}
		public override void FindTargetsFor(char c, ArrayList result) 
		{
			foreach (GlObject o in this.objects)
                o.FindTargetsFor(c,result);
		}
	}
}
