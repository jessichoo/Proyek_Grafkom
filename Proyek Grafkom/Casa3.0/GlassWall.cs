using System;
using Tao.OpenGl;
using System.Collections;

namespace TareaGL
{
	/// <summary>
	/// Summary description for GlassWall.
	/// </summary>
	public class GlassWall:Wall, MetricObject
	{
		protected int divTexture;
		protected double glasshStep=50;
		protected double glassvStep=50;
		protected SolidWall muro;
		protected double baseHeight;
		protected int cristalId;
		public GlassWall(Point3D from, Point3D to, double bottom, double height):base(from, to, bottom, height)
		{
			divTexture = GlUtils.Texture("WOOD1");
			double len = (to-from).Norm;
			this.glasshStep = len/Math.Floor(len/glasshStep);
			baseHeight= height - glassvStep*Math.Floor(height/glassvStep);
			if (baseHeight < glassvStep) 
				baseHeight+=glassvStep;
			muro = new SolidWall(from,to,bottom,baseHeight);
			muro.CloseUp(true);
			cristalId = Gl.glGenLists(1);
			Gl.glNewList(cristalId,Gl.GL_COMPILE);
			pintaCristal();
			Gl.glEndList();
		}
		public override void Prepare (Avatar observer) 
		{
			muro.Prepare(observer);
			radius = Math.Ceiling(this.DistanceTo(observer.Origin)/700);
			//radius = this.DistanceTo(observer.Origin)>1000 ? 2:1;
		}
		double radius = 1;
		public override void Render () 
		{
//			Gl.glColor3d(1,1,1);
			muro.Render();
			//pintaCristal();
			Gl.glCallList(this.cristalId);
		}
		protected void pintaCristal() 
		{
			Glu.GLUquadric q = Glu.gluNewQuadric();
			Point3D actual = new Point3D(from);
			Point3D dir = (to-from);
			double len = dir.Norm;
			dir = dir.Normalized;
			
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,divTexture);
			Glu.gluQuadricTexture(q,Gl.GL_TRUE);
			Gl.glColor3d(1,1,1);
			#region Divisiones horizontales			
			double angle = -GlUtils.VectorAngle2D(dir)+90;
			for (double d = height; d >=baseHeight; d-=glassvStep)
			{
				Gl.glPushMatrix();
				Gl.glTranslated(from.X,bottom+d,from.Z);
				Gl.glRotated(angle,0,1,0);
				Glu.gluCylinder(q,radius,radius,len,10,10);
				Gl.glPopMatrix();
			}
			#endregion
			#region Divisiones verticales
			for (double d =0; d < len; d+=glasshStep)
			{
				actual = from+dir.Scaled(d);
				Gl.glPushMatrix();
				Gl.glTranslated(actual.X,actual.Y+baseHeight,actual.Z);
				Gl.glRotated(-90,1,0,0);
				Glu.gluCylinder(q,radius,radius,this.height-this.baseHeight,10,1);
				Gl.glPopMatrix();
			}
			actual = to;
			Gl.glPushMatrix();
			Gl.glTranslated(actual.X,actual.Y+baseHeight,actual.Z);
			Gl.glRotated(-90,1,0,0);
			Glu.gluCylinder(q,radius,radius,this.height-baseHeight,10,1);
			Gl.glPopMatrix();

			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
			#endregion
			Glu.gluDeleteQuadric(q);
			
			int cullFace=0;
			Gl.glGetBooleanv(Gl.GL_CULL_FACE,out cullFace);
			Gl.glDisable(Gl.GL_CULL_FACE);
			#region Cristal
			Gl.glEnable(Gl.GL_BLEND);
			Gl.glColor4d(.8,.8,1,.4);
			Gl.glBegin(Gl.GL_QUADS);
			Point3D normal = this.planeRot(to-from,-90).Normalized;
			Gl.glNormal3dv(normal.Coords);
			Gl.glVertex3dv((from+new Point3D(0,baseHeight,0)).Coords);
			Gl.glVertex3dv((to+new Point3D(0,baseHeight,0)).Coords);
			Gl.glVertex3dv((to+new Point3D(0,height,0)).Coords);
			Gl.glVertex3dv((from+new Point3D(0,height,0)).Coords);
			Gl.glEnd();
			Gl.glDisable(Gl.GL_BLEND);
			Gl.glColor3d(1,1,1);
			#endregion
			if (cullFace!=Gl.GL_FALSE) 
				Gl.glEnable(Gl.GL_CULL_FACE);  
		}
		public override Point3D after { set {muro.after=value;}}
		public override Point3D before { set {muro.before=value;}}
		public override void Split(ArrayList far, ArrayList near)
		{
			near.Add(this);
			//far.Add(muro);
		}
		public double DistanceTo(Point3D other) 
		{
			return (other-(to-from).Scaled(.5)-from).Norm;
			//return (other-from).Norm;
		}
		public override void CloseTo(bool close) 
		{
			base.CloseTo(close);
			muro.CloseTo(close);
		}
		public override void CloseFrom(bool close) 
		{
			base.CloseFrom(close);
			muro.CloseFrom(close);
		}
	}
}
