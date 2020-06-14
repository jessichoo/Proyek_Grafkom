#define drawCenter
using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Lamp.
	/// </summary>
	public class Lamp : GlObject
	{
		protected Point3D position = new Point3D(0,0,0);
		protected double length=120;
		protected Chain c;

		public Lamp(Point3D position, double length)
		{
			this.position=position;
			this.length=length;
			c = new Chain(position,length);
		}

		public override void Render() 
		{
#if drawCenter
			Gl.glPushMatrix();
			Gl.glTranslated(position.X,position.Y,position.Z);
			Glu.GLUquadric qu = Glu.gluNewQuadric();
			Gl.glColor3d(1,1,0);
			Glu.gluSphere(qu,2,5,5);
			Glu.gluDeleteQuadric(qu);
			Gl.glPopMatrix();
#endif
			Glu.GLUquadric q = Glu.gluNewQuadric();
			Gl.glPushMatrix();
			Gl.glTranslated(position.X,position.Y,position.Z);
			Gl.glColor3d(128/256.0,64/256.0,64/256.0);
			Gl.glColor3d(.55,.425,.425);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(q,10,1,3,10,5);
			Gl.glPopMatrix();
			Glu.gluDeleteQuadric(q);
			c.Render();
			Gl.glTranslated(position.X,position.Y-length-30,position.Z);
			renderLamp();
			Gl.glPopMatrix();
		}

		double[] plano = {0, 1, 0, 0}; 
		protected int cullFace;
		protected void renderLamp()
		{
			Gl.glGetBooleanv(Gl.GL_CULL_FACE,out cullFace);
			Gl.glDisable(Gl.GL_CULL_FACE);

			Glu.GLUquadric quadric;
			quadric = Glu.gluNewQuadric();
			
//			Glu.gluQuadricDrawStyle(quadric, Glu.GLU_FILL); // smooth shaded
			Gl.glPushMatrix();
			Gl.glTranslated(0, 0, 0);
			#region cuerpo
//			Gl.glColor3d(1, 0, 0);
			Gl.glColor3d(1, 0, 0);

			Gl.glPushMatrix();
			Gl.glTranslated(0, 25, 0);
			Gl.glRotated(-90, 1, 0, 0);
			Glu.gluCylinder(quadric, 5, 10, 10, 8, 10);
			Gl.glPopMatrix();
			#endregion cuerpo
			#region bombillo 
			Gl.glPushMatrix();
			Gl.glTranslated(0, 25, 0);
			Gl.glRotated(90, 1, 0, 0);
			Gl.glColor3d(.5,.5,.5);
			Glu.gluCylinder(quadric, 5, 5, 12, 8, 8);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
			Gl.glColor3d(1, 1, 0);
			Gl.glTranslated(0, 8, 0);
			Glut.glutSolidSphere(10, 8, 8);
			Gl.glPopMatrix();
			#endregion bombillo;
			Gl.glPushMatrix();
//			Gl.glEnable(Gl.GL_BLEND);
			Gl.glColor3d(.98, .58, .35);
			Gl.glClipPlane(Gl.GL_CLIP_PLANE0, plano);
			Gl.glEnable(Gl.GL_CLIP_PLANE0);
			Gl.glLightModeli(Gl.GL_LIGHT_MODEL_TWO_SIDE,1);
			Glu.gluSphere(quadric,30,15,10);
			Gl.glLightModeli(Gl.GL_LIGHT_MODEL_TWO_SIDE,0);
			Gl.glPopMatrix();
//			Gl.glDisable(Gl.GL_BLEND);
			Gl.glDisable(Gl.GL_CLIP_PLANE0);
			Gl.glPopMatrix();
			if (cullFace!=Gl.GL_FALSE) 
				Gl.glEnable(Gl.GL_CULL_FACE);                				
		}

	}
}

