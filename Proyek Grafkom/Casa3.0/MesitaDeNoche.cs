using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for MesitaDeNoche2Gl.
	/// </summary>
	public class MesitaDeNoche:Plantilla
	{
		public MesitaDeNoche(Point3D center,double angle):base(center,angle){}

		public MesitaDeNoche(Point3D center):this(center,0){}

		protected override void Particular()
		{
Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("WOOD2"));
			//Gl.glColor3d(0.5,0.25,0);
			Glu.GLUquadric q = Glu.gluNewQuadric();
			Glu.gluQuadricNormals(q,Glu.GLU_SMOOTH);
			Glu.gluQuadricTexture(q,Gl.GL_TRUE);

			Gl.glColor3d(0.5,0.25,0);
			Gl.glPushMatrix();
			Gl.glTranslated(0.8f*20,0,0);
			GlUtils.PintaOrtoedro(0.8f*20,1f*20,0.4f*40);
			Gl.glTranslated(0,0,0.45f*40);
			GlUtils.PintaOrtoedro(0.8f*20,1f*20,0.05f*40);
			Gl.glTranslated(-0.7f*20,4,0.06f*40);
			//Glut.glutSolidSphere(0.05f*20,10,10);
			Gl.glColor3d(0.25,0.25/2,0);
			Glu.gluSphere(q,1,10,10);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
			Gl.glColor3d(0.5,0.25,0);
			Gl.glTranslated(-0.8f*20,0,0);
			GlUtils.PintaOrtoedro(0.8f*20,1f*20,0.4f*40);
			Gl.glTranslated(0,0,0.45f*40);
			GlUtils.PintaOrtoedro(0.8f*20,1f*20,0.05f*40);
			Gl.glTranslated(0.7f*20,4,0.06f*40);
			Gl.glColor3d(0.25,0.25/2,0);
			Glu.gluSphere(q,1,10,10);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
			Gl.glColor3d(0.5,0.25,0);
			Gl.glTranslated(-26,-20,12);
			Gl.glRotated(90,1,0,0);
			Gl.glColor3d(0.25,0.25/2,0);
			Glu.gluCylinder(q,3,2,12,20,20);
			Gl.glTranslated(52,0,0);
			Glu.gluCylinder(q,3,2,12,20,20);
			Gl.glTranslated(0,-22,0);
			Glu.gluCylinder(q,3,2,12,20,20);
			Gl.glTranslated(-52,0,0);
			Glu.gluCylinder(q,3,2,12,20,20);
			Gl.glRotated(-90,1,0,0);
			Gl.glTranslated(-26,20,2);
			Gl.glPopMatrix();
			yInc = 32;
			Gl.glColor3d(1,1,1);
			Glu.gluDeleteQuadric(q);
Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("WOOD1"));
		}
	}
}

