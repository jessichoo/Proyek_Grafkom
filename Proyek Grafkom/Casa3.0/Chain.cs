#define drawCenter
using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Chain.
	/// </summary>
	public class Chain : GlObject
	{
		protected Point3D position;
		protected double length;
		protected double grosor=1;
		protected double yeslabon=10;
		protected double breadth = 1;
		public double Length { get { return length;}}
		public Chain(Point3D position, double length)
		{
			this.length=length;
			this.position=position;
		}
		public override void Render() 
		{
			pintaCadena();
		}
		protected void pintaCadena() 
		{		
#if drawCenter
			Gl.glPushMatrix();
			Gl.glTranslated(position.X,position.Y,position.Z);
			Glu.GLUquadric q = Glu.gluNewQuadric();
			Gl.glColor3d(1,1,0);
			Glu.gluSphere(q,2,5,5);
			Glu.gluDeleteQuadric(q);
			Gl.glPopMatrix();
#endif
			Gl.glPushMatrix();

			Gl.glTranslated(position.X,position.Y+yeslabon,position.Z);
			Gl.glColor3d(128/256.0,64/256.0,64/256.0);
			int cantidadEslabones = (int)Math.Ceiling(length/(yeslabon-breadth));
			//for (double d =0; d <= length; d+=yeslabon+breadth)
			for (int i =0; i < cantidadEslabones; i++)
			{
				Gl.glTranslated(0,-yeslabon+breadth,0);
				Gl.glRotated(90,0,1,0);
				pintaEslabon();
			}
			Gl.glPopMatrix();
		}
		protected void pintaEslabon() 
		{
			Gl.glPushMatrix();
			Gl.glScaled(1,2,1);
			Gl.glColor3d(128/256.0,64/256.0,64/256.0);
			Glut.glutSolidTorus(grosor/2,(yeslabon+grosor)/4,10,10);
			Gl.glPopMatrix();
		}
	}
}
