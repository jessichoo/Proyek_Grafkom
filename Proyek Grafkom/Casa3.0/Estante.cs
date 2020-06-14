using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Estante2Gl.
	/// </summary>
	public class Estante:Plantilla
	{
		

		public Estante(Point3D center, double angle):base(center, angle)
		{
			this.canCullFace=true;
		}

		public Estante(Point3D center):this(center,0){}


		protected override void Particular()
		{
//			Gl.glColor4d(0,.5,.5,.4);
//			Gl.glColor3d(0,.5,.5);
			Gl.glColor3d(1,1,1);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("WOOD1"));
			Gl.glPushMatrix();
			Gl.glTranslated(0.8f*30,0,0);
			GlUtils.PintaOrtoedro(0.8f*30,0.6f*30,0.4f*30);
			Gl.glTranslated(0,0,0.45f*30);
			GlUtils.PintaOrtoedro(0.8f*30,0.6f*30,0.05f*30);
			Gl.glTranslated(-0.7f*30,0,0.06f*30);
			Gl.glColor3d(.5,.5,.5);
			Glut.glutSolidSphere(0.05f*30,10,10);
			Gl.glColor3d(1,1,1);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
			Gl.glTranslated(-0.8f*30,0,0);
			GlUtils.PintaOrtoedro(0.8f*30,0.6f*30,0.4f*30);
			Gl.glTranslated(0,0,0.45f*30);
			GlUtils.PintaOrtoedro(0.8f*30,0.6f*30,0.05f*30);
			Gl.glTranslated(0.7f*30,0,0.06f*30);
			Gl.glColor3d(.5,.5,.5);
			Glut.glutSolidSphere(0.05f*30,10,10);
			Gl.glPopMatrix();
			height = 36;
			Gl.glColor3d(1,1,1);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
		}
	}
}
