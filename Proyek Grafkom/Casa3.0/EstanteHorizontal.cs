using System;
using Tao.OpenGl;
namespace TareaGL
{
	/// <summary>
	/// Summary description for EstanteHorizontal.
	/// </summary>
	public class EstanteHorizontal:Plantilla
	{
		public EstanteHorizontal(Point3D position, double angle):base(position,angle)
		{
			this.height=92;
			this.yInc=35+20;
		}
		public EstanteHorizontal(Point3D position):this(position,0){}
		public EstanteHorizontal():this(new Point3D(0,0,0)){}
		protected override void Particular() 
		{
			Gl.glColor3d(.7,.7,.7);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("WOOD1"));
			Gl.glPushMatrix();
			Gl.glTranslated(-83.5,0,0);
			GlUtils.PintaOrtoedro(1.5,35,23);
			Gl.glTranslated(83.5*2,0,0);
            GlUtils.PintaOrtoedro(1.5,35,23);
			Gl.glTranslated(-83.5,0,-2);
			GlUtils.PintaOrtoedro(82,1,16);
			Gl.glTranslated(0,-35,2);
			//Gl.glPopMatrix();
			GlUtils.PintaOrtoedro(85,1.5,23);
			Gl.glTranslated(0,70,0);
			GlUtils.PintaOrtoedro(85,1.5,23);
			Gl.glTranslated(0,-35,-20);
			GlUtils.PintaOrtoedro(85,35,3);
			Gl.glTranslated(42,0,38);
			Gl.glColor3d(.5,.5,.5);
			GlUtils.PintaOrtoedro(43,32,1.5);
			Gl.glTranslated(-70,0,3.5);
			GlUtils.PintaOrtoedro(40,32,1.5);
			Gl.glPopMatrix();

			Gl.glPushMatrix();
			Glu.GLUquadric q = Glu.gluNewQuadric();
			Glu.gluQuadricTexture(q,Gl.GL_TRUE);
			Gl.glColor3d(.3,.3,.3);
			Gl.glTranslated(-70,-35,15);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(q,5,2,20,20,20);
			Gl.glRotated(-90,1,0,0);
			Gl.glTranslated(0,0,-30);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(q,5,2,20,20,20);
			Gl.glRotated(-90,1,0,0);
			Gl.glTranslated(140,0,0);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(q,5,2,20,20,20);
			Gl.glRotated(-90,1,0,0);
			Gl.glTranslated(0,0,30);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(q,5,2,20,20,20);
			Gl.glPopMatrix();

			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
		}
	}
}
