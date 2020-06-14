//#define drawCenter
using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Estante.
	/// </summary>
	public class Librero: GlObject
	{
		protected static int idEstante = -1;
		protected Point3D location;
		public static double Width { get { return 72; }}
		public static double Height { get { return 76; }}
		public static double Depth { get { return 34; }}
		protected double angle;
		public Librero():this(new Point3D(0,0,0),0)
		{
		}
		public Librero(Point3D location, double angle) 
		{
			this.location=location;
			this.angle=angle;
            if (idEstante<0)
			{
				idEstante=Gl.glGenLists(1);
				Gl.glNewList(idEstante,Gl.GL_COMPILE);
				this.pintaEstante();
				Gl.glEndList();
			}
		}
		protected void pintaEstante() 
		{
			Gl.glColor3d(.7,.6,0);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("ESTANTE"));
			Gl.glPushMatrix();
				Gl.glTranslated(0,-33,0);
				GlUtils.PintaOrtoedro(72/2,1,15);
				Gl.glTranslated(0,22,0);
				GlUtils.PintaOrtoedro(72/2,1,15);
				Gl.glTranslated(0,22,0);
				GlUtils.PintaOrtoedro(72/2,1,15);
				Gl.glTranslated(0,22,0);
				GlUtils.PintaOrtoedro(72/2,1,15);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
				Gl.glTranslated(35,0,0);
				GlUtils.PintaOrtoedro(1,76/2,17);
				Gl.glTranslated(-35*2,0,0);
				GlUtils.PintaOrtoedro(1,76/2,17);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
				Gl.glTranslated(0,0,-15);
				GlUtils.PintaOrtoedro(72/2,34,1.5);			
				Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
			Gl.glPopMatrix();
		}
		
		public override void Render() 
		{
#if drawCenter
			Gl.glPushMatrix();
			Gl.glTranslated(location.X,location.Y,location.Z);
			Glu.GLUquadric q = Glu.gluNewQuadric();
			Gl.glColor3d(1,0,0);
			Glu.gluSphere(q,2,5,5);
			Glu.gluDeleteQuadric(q);
			Gl.glPopMatrix();
#endif
			//Gl.glEnable(Gl.GL_CULL_FACE);
			Gl.glPushMatrix();
			Gl.glTranslated(location.X,location.Y+Height/2,location.Z+Depth/2);
			Gl.glRotated(angle,0,1,0);
			Gl.glCallList(idEstante);
			Gl.glPopMatrix();
			//Gl.glDisable(Gl.GL_CULL_FACE);
		}
	}
}
