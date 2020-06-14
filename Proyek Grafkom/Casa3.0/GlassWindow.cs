using System;
using Tao.OpenGl;
using System.Collections;

namespace TareaGL
{
	/// <summary>
	/// Summary description for GlassWindow.
	/// </summary>
	public class GlassWindow:Window,MetricObject
	{
		protected static int id = -1;
		protected static int textureIndex=-1;
		public GlassWindow()
		{
			if (id<0) 
			{
				textureIndex = GlUtils.Texture("WOOD1");
				renderWindow();
			}
		}
		double xscale=.8;
		double yscale=.8;

		#region Width and Height
		public override double Width 
		{
			get { return 50*xscale; }
		}
		public override double Height 
		{
			get { return 75*2*yscale; }
		}
		#endregion
		protected void renderWindow() 
		{
			id = Gl.glGenLists(1);
			Gl.glNewList(id,Gl.GL_COMPILE);
			Gl.glPushMatrix();
			Gl.glScaled(xscale,yscale,1);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,textureIndex);
			Gl.glColor3d(0.8,0.8,0.8);
			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,1);Gl.glVertex3d(-0.25*100,0.75*100,0.05*100);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,0);Gl.glVertex3d(-0.25*100,-0.75*100,0.05*100);
				
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0.25,1);Gl.glVertex3d(-0.20*100,0.75*100,0.05*100);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0.25,0);Gl.glVertex3d(-0.20*100,-0.75*100,0.05*100);
				
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0.5,1);Gl.glVertex3d(-0.20*100,0.75*100,-0.05*100);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0.5,0);Gl.glVertex3d(-0.20*100,-0.75*100,-0.05*100);
				
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(.75,1);Gl.glVertex3d(-0.25*100,0.75*100,-0.05*100);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(.75,0);Gl.glVertex3d(-0.25*100,-0.75*100,-0.05*100);
			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0,1);Gl.glVertex3d(0.25*100,0.75*100,-0.05*100);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0,0);Gl.glVertex3d(0.25*100,-0.75*100,-0.05*100);
				
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0.25,1);Gl.glVertex3d(0.20*100,0.75*100,-0.05*100);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0.25,0);Gl.glVertex3d(0.20*100,-0.75*100,-0.05*100);
				
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0.5,1);Gl.glVertex3d(0.20*100,0.75*100,0.05*100);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0.5,0);Gl.glVertex3d(0.20*100,-0.75*100,0.05*100);
				
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(.75,1);Gl.glVertex3d(0.25*100,0.75*100,0.05*100);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(.75,0);Gl.glVertex3d(0.25*100,-0.75*100,0.05*100);
			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0,1);Gl.glVertex3d(0.20*100,-0.75*100,-0.05*100);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0,0);Gl.glVertex3d(-0.20*100,-0.75*100,-0.05*100);
				
			Gl.glNormal3d(0,0.7,-0.7);Gl.glTexCoord2d(0.25,1);Gl.glVertex3d(0.20*100,-0.70*100,-0.05*100);
			Gl.glNormal3d(0,0.7,-0.7);Gl.glTexCoord2d(0.25,0);Gl.glVertex3d(-0.20*100,-0.70*100,-0.05*100);
				
			Gl.glNormal3d(0,0.7,0.7);Gl.glTexCoord2d(0.5,1);Gl.glVertex3d(0.20*100,-0.70*100,0.05*100);
			Gl.glNormal3d(0,0.7,0.7);Gl.glTexCoord2d(0.5,0);Gl.glVertex3d(-0.20*100,-0.70*100,0.05*100);
				
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(.75,1);Gl.glVertex3d(0.20*100,-0.75*100,0.05*100);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(.75,0);Gl.glVertex3d(-0.20*100,-0.75*100,0.05*100);
			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0,0);Gl.glVertex3d(-0.20*100,0.75*100,-0.05*100);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0,1);Gl.glVertex3d(0.20*100,0.75*100,-0.05*100);
				
			Gl.glNormal3d(0,-0.7,-0.7);Gl.glTexCoord2d(0.25,0);Gl.glVertex3d(-0.20*100,0.70*100,-0.05*100);
			Gl.glNormal3d(0,-0.7,-0.7);Gl.glTexCoord2d(0.25,1);Gl.glVertex3d(0.20*100,0.70*100,-0.05*100);
				
			Gl.glNormal3d(0,-0.7,0.7);Gl.glTexCoord2d(0.5,0);Gl.glVertex3d(-0.20*100,0.70*100,0.05*100);
			Gl.glNormal3d(0,-0.7,0.7);Gl.glTexCoord2d(0.5,1);Gl.glVertex3d(0.20*100,0.70*100,0.05*100);
				
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(.75,0);Gl.glVertex3d(-0.20*100,0.75*100,0.05*100);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(.75,1);Gl.glVertex3d(0.20*100,0.75*100,0.05*100);
			Gl.glEnd();
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);

			//cristal
			int cullFace=0;
			Gl.glGetBooleanv(Gl.GL_CULL_FACE,out cullFace);
			Gl.glDisable(Gl.GL_CULL_FACE);

			Gl.glEnable(Gl.GL_BLEND);
			Gl.glDisable(Gl.GL_TEXTURE_2D);
			Gl.glColor4d(1,1,1,0.4);
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glVertex3d(-0.20*100,-0.70*100,0);
			Gl.glVertex3d(0.20*100,-0.70*100,0);
	
			Gl.glVertex3d(0.20*100,0.70*100,0);
			Gl.glVertex3d(-0.20*100,0.70*100,0);
			Gl.glEnd();
			Gl.glColor3d(1,1,1);
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			Gl.glDisable(Gl.GL_BLEND);
			Gl.glPopMatrix();
			Gl.glEndList();
			if (cullFace!=Gl.GL_FALSE) 
				Gl.glEnable(Gl.GL_CULL_FACE);
		}

		public override void Split(ArrayList far, ArrayList near) 
		{
			near.Add(this);
		}
		public double DistanceTo(Point3D other) 
		{
			return (this.Location-other).Norm;
		}

		#region Prepare and Render
		public override void Prepare (Avatar observer) 
		{
		}
		public override void Render () 
		{
			Gl.glPushMatrix();
			Gl.glTranslated(start.X,start.Y,start.Z);
			Gl.glRotated(angle,0,-1,0);
			Gl.glTranslated(Width/2,Height/2,0);
			Gl.glCallList(id);
			Gl.glPopMatrix();
		}
		#endregion
	}
}
