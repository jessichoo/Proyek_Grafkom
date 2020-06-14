using System;
using Tao.OpenGl;
using System.Drawing;
using System.Drawing.Imaging;

namespace TareaGL
{
	/// <summary>
	/// Summary description for SkyBox.
	/// </summary>
	public class SkyBox : GlObject
	{
		int[] textures;
		const int BACK_ID   =0;									// The texture ID for the back side of the cube
		const int FRONT_ID	=1;									// The texture ID for the front side of the cube
		const int BOTTOM_ID	=2;									// The texture ID for the bottom side of the cube
		const int TOP_ID	=3;									// The texture ID for the top side of the cube
		const int LEFT_ID	=4;									// The texture ID for the left side of the cube
		const int RIGHT_ID	=5;

		double x;
		double y;
		double z;
		double width;
		double height;
		double length;
		protected static int id = -1;

		public SkyBox()
		{
			Gl.glColor3d(1,1,1);
			textures=new int[6];

			CreateTexture(textures, "right2.jpg", LEFT_ID);
			CreateTexture(textures, "back2.jpg",	BACK_ID		);
			CreateTexture(textures, "front2.jpg",	FRONT_ID	);
			CreateTexture(textures, "bottom2.jpg",	BOTTOM_ID	);
			CreateTexture(textures, "top2.jpg",		TOP_ID		);
			CreateTexture(textures, "left2.jpg",	RIGHT_ID		);
			//CreateTexture(textures, "Right.jpg",	RIGHT_ID	);CreateTexture(textures, "Back.jpg",	BACK_ID		);
			//CreateTexture(textures, "Front.jpg",	FRONT_ID	);
			//CreateTexture(textures, "Bottom.jpg",	BOTTOM_ID	);
			//CreateTexture(textures, "Top.jpg",		TOP_ID		);
			//CreateTexture(textures, "Left.jpg",	LEFT_ID		);
			//CreateTexture(textures, "Right.jpg",	RIGHT_ID	);
			this.x=0;
			this.y=1000;
			this.z=0;
			this.width=4000;
			this.height=2000;
			this.length=4000;
			if (id<0) 
			{
				id = Gl.glGenLists(1);
				Gl.glNewList(id,Gl.GL_COMPILE);
				this.CreateSkyBox(x,y,z,width,height,length);
				Gl.glEndList();
			}
		}
		
		public override void Render()
		{
			//Gl.glEnable(Gl.GL_CULL_FACE);
			Gl.glCallList(id);
			//Gl.glDisable(Gl.GL_CULL_FACE);
//			CreateSkyBox(x,y,z,width,height,length);
		}
        
		protected void CreateSkyBox(double x, double y, double z, double width, double height, double length)
		{
            Gl.glColor3d(1,1,1);
			x = x - width  / 2;
			y = y - height / 2;
			z = z - length / 2;

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[BACK_ID]);

			//			// Start drawing the side as a QUAD
			Gl.glBegin(Gl.GL_QUADS);		
			// Assign the texture coordinates and vertices for the BACK Side
			Gl.glNormal3d(-1,1,1);
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x + width,	y,			z);
			Gl.glNormal3d(-1,-1,1);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x + width,	y + height,	z);
			Gl.glNormal3d(1,-1,1);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x,			y + height, z);
			Gl.glNormal3d(1,1,1);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x,			y,			z);	
			Gl.glEnd();
			//
			//			// Bind the FRONT texture of the sky map to the FRONT side of the box
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[FRONT_ID]);
			// Start drawing the side as a QUAD
			Gl.glBegin(Gl.GL_QUADS);	
			// Assign the texture coordinates and vertices for the FRONT Side
			Gl.glNormal3d(1,1,-1);
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x,			y,			z + length);
			Gl.glNormal3d(1,-1,-1);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x,			y + height, z + length);
			Gl.glNormal3d(-1,-1,-1);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x + width, y + height, z + length); 
			Gl.glNormal3d(-1,1,-1);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width, y,			z + length);
			Gl.glEnd();

			//			// Bind the BOTTOM texture of the sky map to the BOTTOM side of the box
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[BOTTOM_ID]);

			// Start drawing the side as a QUAD
			Gl.glBegin(Gl.GL_QUADS);		
	
			// Assign the texture coordinates and vertices for the BOTTOM Side
			Gl.glNormal3d(1,1,1);
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x,			y,			z);
			Gl.glNormal3d(1,1,-1);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x,			y,			z + length);
			Gl.glNormal3d(-1,1,-1);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x + width, y,			z + length); 
			Gl.glNormal3d(-1,1,1);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width, y,			z);			
			Gl.glEnd();
			//
			//			// Bind the TOP texture of the sky map to the TOP side of the box
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[TOP_ID]);
			//	
			// Start drawing the side as a QUAD
			Gl.glBegin(Gl.GL_QUADS);		
		
			// Assign the texture coordinates and vertices for the TOP Side
			Gl.glNormal3d(-1,-1,1);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x + width, y + height, z);
			Gl.glNormal3d(-1,-1,-1);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width, y + height, z + length); 
			Gl.glNormal3d(1,-1,-1);
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x,			y + height,	z + length);
			Gl.glNormal3d(1,-1,1);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x,			y + height,	z);

			Gl.glEnd();
			//
			//			// Bind the LEFT texture of the sky map to the LEFT side of the box
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[LEFT_ID]);
	
			// Start drawing the side as a QUAD
			Gl.glBegin(Gl.GL_QUADS);		
		
			// Assign the texture coordinates and vertices for the LEFT Side
			Gl.glNormal3d(1,-1,1);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x,			y + height,	z);	
			Gl.glNormal3d(1,-1,-1);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x,			y + height,	z + length);
			Gl.glNormal3d(1,1,-1);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x,			y,			z + length);
			Gl.glNormal3d(1,1,1);
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x,			y,			z); 
			Gl.glEnd();
			//
			//			// Bind the RIGHT texture of the sky map to the RIGHT side of the box
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[RIGHT_ID]);

			// Start drawing the side as a QUAD
			Gl.glBegin(Gl.GL_QUADS);		

			// Assign the texture coordinates and vertices for the RIGHT Side
			Gl.glNormal3d(-1,1,1);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width, y,			z);
			Gl.glNormal3d(-1,1,-1);
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x + width, y,			z + length);
			Gl.glNormal3d(-1,-1,-1);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x + width, y + height,	z + length); 
			Gl.glNormal3d(-1,-1,1);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x + width, y + height,	z);
			Gl.glEnd();
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);
//			Gl.glDisable(Gl.GL_TEXTURE_2D);
		}
	
		void CreateTexture(int[] textureArray, string filename, int textureID)
		{
			textureArray[textureID] = GlUtils.CreateTexture(filename);
		}
		public override Point3D ColisionNormal(Point3D origin, Point3D direction, double radius)
		{
			Point3D result = new Point3D(0,0,0);
			Point3D p = origin+direction;
			if (p.Y<radius) result.Translate(new Point3D(0,radius-p.Y,0));
			if (p.Y>this.height-radius) result.Translate(new Point3D(0,-p.Y+height-radius,0));
			if (p.X<-width/2+radius) result.Translate(new Point3D(-width/2+radius-p.X,0,0));
			if (p.X>width/2-radius) 
				result.Translate(new Point3D(width/2-radius-p.X,0,0));
			if (p.Z<-length/2+radius) result.Translate(new Point3D(0,0,-length/2+radius-p.Z));
			if (p.Z>length/2-radius) result.Translate(new Point3D(0,0,length/2-radius-p.Z));
//			if ((p+result).Y<radius) 
//				Console.WriteLine((p+result).Y);
			return result;
			#region Commented out
//			//			double distance=origin.DistanceToPlane(new Point3D(x-width/2,y-height/2,z+length/2),new Point3D(x+width/2,y-height/2,z-length/2),new Point3D(x+width/2,y-height/2,z+length/2));
//			//			if (distance<=radius)
//			//				result+= (new Point3D(0,1,0)).Scaled(radius-distance);
//			if (origin.Y-radius<=y-height/2) result+=new Point3D(0,-origin.Y+(y-height/2)+radius,0);//radius+y-height/2-origin.Y,0);
//			if (origin.Y+radius>=y+height/2) result+=new Point3D(0,-origin.Y+(y+height/2)-radius,0);
//			//			if (origin.Z-radius<=z-length/2) 
//			//			{
//			//				Point3D r = new Point3D(0,0,origin.Z+(z+length/2)-radius);//-origin.Z+(z-length/2)+radius);//radius+y-height/2-origin.Y,0);
//			//				result-=r;
//			//			}
//			//			if (origin.Z+radius>=z+length/2) Platform.Windows.WinApi.Beep(100,100);//result+=new Point3D(0,0,-origin.Z+(z+length/2)-radius);
//			//			if (origin.X-radius<=x-width/2) result+=new Point3D(-origin.X+(x-width/2)+radius,0,0);//radius+y-height/2-origin.Y,0);
//			//			if (origin.X+radius>=x+width/2) result+=new Point3D(-origin.X+(x+width/2)-radius,0,0);
//			return result;
			#endregion
		}
	}

}
