using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Mesa.
	/// </summary>
	public class Mesa : Plantilla
	{
		public Mesa(Point3D center,double angle):base(center,angle){}

		public Mesa(Point3D center):this(center,0){}

		protected override void Particular()
		{
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("madera"));
			Gl.glColor3d(.3,.3,.3);
			double alto = 45;
			Gl.glPushMatrix();
			//Gl.glColor3d(0.4,0.4,0.4);
			Gl.glTranslated(-40,0,-90);
			pintaPata(alto);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
			Gl.glTranslated(40,0,-90);
			pintaPata(alto);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
			Gl.glTranslated(-40,0,90);
			pintaPata(alto);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
			Gl.glTranslated(40,0,90);
			pintaPata(alto);
			Gl.glPopMatrix();

			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("rose"));    
			Gl.glTranslated(0,alto,0);
			Gl.glColor3d(.8,.6,.6);
			GlUtils.PintaOrtoedro(50,alto/10,100);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
			height = 94.5;
			yInc = 45;
			Gl.glColor3d(1,1,1);
		}
				
		void pintaPata(double alto)
		{
			Glu.GLUquadric q = Glu.gluNewQuadric();
			Glu.gluQuadricTexture(q,Gl.GL_TRUE);
			Gl.glTranslated(0,-alto,0);
			Gl.glRotated(90,-1,0,0);
			Glu.gluCylinder(q,5,8,2*alto,20,20);
			Gl.glRotated(-90,-1,0,0);
			Gl.glTranslated(0,alto,0);
			Glu.gluDeleteQuadric(q);
		}
	}
}
