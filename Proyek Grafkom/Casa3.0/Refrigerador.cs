using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for RefrigeradorGl.
	/// </summary>
	public class Refrigerador : Plantilla
	{
		public Refrigerador(Point3D center,double angle):base(center,angle){}

		public Refrigerador(Point3D center):this(center,0){}
		
		protected override void Particular()
		{
			Gl.glDisable(Gl.GL_TEXTURE_2D);
			Gl.glColor3d(1,1,1);
			
			GlUtils.PintaOrtoedro(0.7f*40,1.0f*40,0.7f*40);
			
			Gl.glPushMatrix();
			Gl.glTranslated(0,1.50f*40,0);
			GlUtils.PintaOrtoedro(0.7f*40,0.5f*40,0.7f*40);
			Gl.glPopMatrix();
			
			Gl.glColor3d(0,0,0);
			
			GlUtils.pintaLineas(0.7f*40,1.0f*40,0.7f*40);
			//pintaLineas(0.7f*40,1,0.7f);
			
			Gl.glPushMatrix();
			Gl.glTranslatef(0,1.50f*40,0);
			GlUtils.pintaLineas(0.7f*40,0.5f*40,0.7f*40);
			//pintaLineas(0.7f*40,0.5f*40,0.7f);
			Gl.glColor3d(1,1,1);
			Gl.glTranslatef(0,0,0.8f*40);
			Gl.glRotated(90,0,0,1);
			
			GlUtils.PintaOrtoedro(0.5f*40,0.7f*40,0.1f*40);
			Gl.glColor3d(0,0,0);
			
			GlUtils.pintaLineas(0.5f*40,0.7f*40,0.1f*40);
			//pintaLineas(0.5f*40,0.7f*40,0.1f);
			Gl.glColor3d(0,0,0);
			Gl.glTranslatef(-0.2f*40,0.5f*40,0.1f*40);
			Gl.glRotated(90,0,1,0);
			Glu.gluCylinder(Glu.gluNewQuadric(),0.03f*40,0.05f*40,0.4f*40,10,10);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
			Gl.glTranslatef(0,0,0.8f*40);
			Gl.glColor3d(1,1,1);
			
			GlUtils.PintaOrtoedro(0.7f*40,1.0f*40,0.1f*40);
			Gl.glColor3d(0,0,0);
			
			GlUtils.pintaLineas(0.7f*40,1.0f*40,0.1f*40);
			//pintaLineas(0.7f*40,1,0.1f);
			Gl.glTranslatef(-0.5f*40,0,0.1f*40);
			Gl.glRotated(90,-1,0,0);
			Glu.gluCylinder(Glu.gluNewQuadric(),0.03f*40,0.05f*40,0.4f*40,10,10);
			Gl.glPopMatrix();
			

			Gl.glEnable(Gl.GL_TEXTURE_2D);
			yInc = 40; 

	
		}
	}
}
