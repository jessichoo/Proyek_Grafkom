using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Mesita.
	/// </summary>
	public class Mesita:Plantilla
	{
		public Mesita(Point3D center,double angle):base(center,angle){
			this.height=58;
		}

		public Mesita(Point3D center):this(center,0){}

		protected override void Particular()
		{
		    Gl.glColor3d(0.4,0.4,0.4);
			GlUtils.pintaCuadroStrip(0.5f*70,0.04f*70,0.7f*70,1);
			Gl.glPushMatrix();
			Gl.glTranslated(0,0.04f*70,0);
			GlUtils.pintaCuadro(0.5f*70,0,0.7f*70,1);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
			Gl.glTranslated(0,-0.04f*70,0);
			GlUtils.pintaCuadro(0.5f*70,0,0.7f*70,1);
			Gl.glPopMatrix();

			Gl.glPushMatrix();
			Gl.glTranslated(0.4f*70,0,0.6f*70);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(Glu.gluNewQuadric(),0.05f*70,0.03f*70,0.8f*70,20,20);
			Gl.glPopMatrix();

			Gl.glPushMatrix();
			Gl.glTranslated(-0.4f*70,0,0.6f*70);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(Glu.gluNewQuadric(),0.05f*70,0.03f*70,0.8f*70,20,20);
			Gl.glPopMatrix();

			Gl.glPushMatrix();
			Gl.glTranslated(0.4f*70,0,-0.6f*70);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(Glu.gluNewQuadric(),0.05f*70,0.03f*70,0.8f*70,20,20);
			Gl.glPopMatrix();

			Gl.glPushMatrix();
			Gl.glTranslated(-0.4f*70,0,-0.6f*70);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(Glu.gluNewQuadric(),0.05f*70,0.03f*70,0.8f*70,20,20);
			Gl.glPopMatrix();

			Gl.glPushMatrix();
			Gl.glTranslated(0,-0.4f*70,0);
			GlUtils.pintaCuadroStrip(0.45f*70,0.02f*70,0.65f*70,1);	
			Gl.glPopMatrix();
			yInc = 56;
			Gl.glColor3d(1,1,1);
		}
	}
}
