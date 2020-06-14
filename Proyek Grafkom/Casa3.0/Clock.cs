using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Clock.
	/// </summary>
	public class Clock : Plantilla
	{
		public Clock(Point3D center, double angle):base(center,angle)
		{
			this.canCullFace=true;
			this.customRendering=true;
		}

		protected override void Particular() 
		{
			Gl.glPushMatrix();
			double w = 17;
			Gl.glColor3d(1,1,1);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("rose"));
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glNormal3d(0,0,1);
			Gl.glTexCoord2d(0,0); Gl.glVertex3d(-w,-w,0);
			Gl.glTexCoord2d(1,0); Gl.glVertex3d(w,-w,0);
			Gl.glTexCoord2d(1,1); Gl.glVertex3d(w,w,0);
			Gl.glTexCoord2d(0,1); Gl.glVertex3d(-w,w,0);
			Gl.glEnd();
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);

			Gl.glColor3d(.1,0,0);
			Glut.glutSolidTorus(3,19,30,40);
			Glut.glutSolidTorus(4,23,10,6);
//			Gl.glColor3d(.7,.7,.7);
//            Glut.glutSolidCone(1,2,10,2);
			Gl.glPopMatrix();
		}

		protected override void customRenderer() 
		{
//			Gl.glTranslated(0,0,(int)distance/50);
			//Gl.glTranslated(0,0,0);
			Gl.glCallList(this.idObject);
//			Gl.glColor3d(1,1,1);
//			GlUtils.PintaOrtoedro(10,10,10);
			if (distance<700) drawTime();
		}
		int hours;
		int minutes;
		int seconds;
		double distance;
		Point3D dir;
		public override void Prepare(Avatar observer) 
		{
			DateTime h = observer.CurrentTime;//DateTime.Now;
			hours= h.Hour%12;
			minutes= h.Minute;
			seconds= h.Second;
			distance = (observer.Origin-this.center).Norm;
		}

		protected void drawTime() 
		{
			Gl.glPushMatrix();		
				Gl.glTranslated(0,0,1+(int)distance/50);		
				Gl.glNormal3d(0,0,1);

				Gl.glColor3d(.2,.2,.2);
				Gl.glRotated(seconds/60.0*360,0,0,-1);
					Gl.glBegin(Gl.GL_QUADS);
					Gl.glVertex3d(0,14,0);
					Gl.glVertex3d(-.5,0,0);
					Gl.glVertex3d(0,-.5,0);
					Gl.glVertex3d(.5,0,0);
					Gl.glEnd();		
				Gl.glRotated(-seconds/60.0*360,0,0,-1);
				
				Gl.glColor3d(1,1,1);
				Gl.glRotated(minutes/60.0*360,0,0,-1);
					Gl.glBegin(Gl.GL_QUADS);
					Gl.glNormal3d(0,0,1);
					Gl.glColor3d(0,0,0);
					Gl.glVertex3d(0,14,0);
					Gl.glVertex3d(-1,0,0);
					Gl.glVertex3d(0,-1,0);
					Gl.glVertex3d(1,0,0);
					Gl.glEnd();
		
			Gl.glRotated(-minutes/60.0*360+hours/12.0*360+minutes/60.0*30,0,0,-1);

				Gl.glBegin(Gl.GL_QUADS);
				Gl.glNormal3d(0,0,1);
				Gl.glColor3d(0,0,0);
				Gl.glVertex3d(0,8,0);
				Gl.glVertex3d(-1,0,0);
				Gl.glVertex3d(0,-1,0);
				Gl.glVertex3d(1,0,0);
				Gl.glEnd();

			Gl.glColor3d(.7,.7,.7);
			Gl.glTranslated(0,0,-1);
			Glut.glutSolidCone(1,2,10,2);

			Gl.glPopMatrix();
		}
	}
}
