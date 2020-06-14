using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Plato.
	/// </summary>
	public class Plato: Plantilla
	{
		public Plato(Point3D center,double angle):base(center,angle){}

		public Plato(Point3D center):this(center,0){}

		protected override void Particular()
		{
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("ALUMINIO"));
			Gl.glLightModeli(Gl.GL_LIGHT_MODEL_TWO_SIDE,1);

			Glu.GLUquadric q = Glu.gluNewQuadric();
			Glu.gluQuadricTexture(q,Gl.GL_TRUE);
			Gl.glColor3d(1,1,1);
			Gl.glPushMatrix();
			Gl.glRotated(90,1,0,0);
			Glu.gluDisk(q,0,5*2,20,20);
			Gl.glRotated(180,1,0,0);
			Glu.gluCylinder(q,5*2,8*2,3*2,20,20);
			Gl.glTranslated(0,0,2.5*2);
			Gl.glColor3d(0,0,0);
			Glu.gluDisk(q,14.8,15,20,20);
			Gl.glTranslated(0,0,-2.5*2);
			Gl.glRotated(90,1,0,0);
			Gl.glPopMatrix();
			yInc = 0;
			Gl.glColor3d(1,1,1);
			Glu.gluDeleteQuadric(q);
			Gl.glLightModeli(Gl.GL_LIGHT_MODEL_TWO_SIDE,0);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
		}
 
	}
}
