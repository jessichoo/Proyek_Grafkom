using System;
using System.Collections;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for TranslatedObject.
	/// </summary>
	public class TranslatedObject: GlObject, InteractiveObject
	{
		protected Point3D origin;
		protected GlObject obj;
		public TranslatedObject(Point3D origin, GlObject obj)
		{
			this.origin=origin; this.obj=obj;
		}
		public override void Prepare (Avatar observer) 
		{
			observer.Camera.Translate(-origin);
			obj.Prepare(observer);
			observer.Camera.Translate(origin);
		}
		public override Point3D ColisionNormal (Point3D point, Point3D direction, double radius) 
		{
			return obj.ColisionNormal(point.Translated(-origin),direction,radius);
		}
		public override void Render() 
		{
			Gl.glPushMatrix();
			Gl.glTranslated(origin.X,origin.Y,origin.Z);
			obj.Render();
			Gl.glPopMatrix();
		}
		public override void Split(ArrayList far, ArrayList near) 
		{
			ArrayList f = new ArrayList();
			ArrayList n = new ArrayList();
			obj.Split(f,n);
			foreach (GlObject o in f)
				far.Add(new TranslatedObject(origin,o));
			foreach (GlObject o in n)
				near.Add(new TranslatedObject(origin,o));
		}
		public double DistanceTo(Point3D other) 
		{
			try 
			{
				return (obj as MetricObject).DistanceTo(other-origin);
			} 
			catch (Exception) 
			{
				return double.MaxValue;
			}
		}
		public bool HasActionFor(char c) 
		{
			try 
			{
				return (obj as InteractiveObject).HasActionFor(c);
			} 
			catch (Exception) 
			{
				return false;
			}
		}

		public void Act(char c) 
		{
			try 
			{
				(obj as InteractiveObject).Act(c);
			} 
			catch (Exception) {;}
		}

		public override void FindTargetsFor(char c, ArrayList result) 
		{
			ArrayList tmp = new ArrayList();
			obj.FindTargetsFor(c,tmp);
			foreach (GlObject o in tmp)
				result.Add(new TranslatedObject(origin,o));
		}
	}
}
