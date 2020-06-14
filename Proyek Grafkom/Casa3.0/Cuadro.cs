using System;
using Tao.OpenGl;


namespace TareaGL
{
	/// <summary>
	/// Summary description for Cuadro.
	/// </summary>
	public class Cuadro : Plantilla
	{
		public double ancho, alto;
		public string textura;
		/// center especifica la coordenada del extremo inferior izquierdo frontal del cubo

	
		public Cuadro(Point3D center, double angle, string textura, double alto, double ancho ):base(center, angle, textura, alto, ancho)
		{
		}

		public Cuadro(Point3D center, string textura):this(center, 0, textura, 50, 30)
		{
		}

		public override void setInitialParams(params object[] iniciales) 
		{
			this.alto = 50;
			this.ancho = 30;
			try 
			{
				textura = (string) iniciales[0];
				alto = (double) iniciales[1];
				ancho = (double) iniciales[2];
			}
			catch (Exception) {;}
		}

		public override void Particular(){
            //Gl.glRotated(angle, 0, 1, 0);
			
			marco();
			Gl.glNormal3d(0,0,1);
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, GlUtils.Texture(textura)); 
			Gl.glColor3d(1,1,1);
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glTexCoord2d(0,0);
			Gl.glVertex3d(center.X, center.Y, center.Z);

			Gl.glTexCoord2d(1,0);
			Gl.glVertex3d(center.X + ancho, center.Y, center.Z);
		
			Gl.glTexCoord2d(1,1);
			Gl.glVertex3d(center.X + ancho, center.Y + alto, center.Z);

			Gl.glTexCoord2d(0,1);
			Gl.glVertex3d(center.X, center.Y + alto, center.Z);
			
			Gl.glEnd();
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0); 
		}

		private void marco()
		{
			Gl.glDisable(Gl.GL_TEXTURE_2D);
			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glColor3f (1.0f, 1.0f, 1.0f);
			Gl.glNormal3d(0,0,1);
			Gl.glVertex3d(center.X, center.Y, center.Z);
			Gl.glVertex3d(center.X - 5, center.Y - 5,center.Z);
		
			Gl.glVertex3d(center.X  + ancho, center.Y, center.Z);
			Gl.glVertex3d(center.X  + ancho + 5,center.Y - 5, center.Z);
			
			Gl.glVertex3d(center.X + ancho, center.Y + alto, center.Z);
			Gl.glVertex3d(center.X + ancho + 5, center.Y + alto + 5, center.Z);
			Gl.glVertex3d(center.X, center.Y + alto, center.Z);
			Gl.glVertex3d(center.X - 5, center.Y + alto + 5, center.Z);
			Gl.glVertex3d(center.X, center.Y, center.Z);
			Gl.glVertex3d(center.X - 5, center.Y - 5, center.Z);
			Gl.glEnd();

			Enmarco(new Point3D(center.X - 8, center.Y - 8, center.Z), 3, alto + 16, 3);
			Enmarco(new Point3D(center.X + ancho + 5, center.Y - 8, center.Z), 3, alto + 16, 3);
			Enmarco(new Point3D(center.X - 5, center.Y + alto + 5, center.Z), ancho + 10, 3, 3);
			Enmarco(new Point3D(center.X - 5, center.Y - 8, center.Z), ancho + 10, 3, 3);
		}
		
		private void Enmarco(Point3D punto, double ancho, double alto, double prof)
		{
			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glColor3f(0.0f,0.0f,0.0f);
			Gl.glNormal3d(0,0,1);
			Gl.glVertex3d(punto.X, punto.Y, punto.Z + prof);
			Gl.glVertex3d(punto.X, punto.Y + alto, punto.Z + prof);
			Gl.glVertex3d(punto.X + ancho, punto.Y, punto.Z + prof);
			Gl.glVertex3d(punto.X + ancho, punto.Y + alto, punto.Z + prof);
			Gl.glNormal3d(1,0,0);
			Gl.glVertex3d(punto.X + ancho, punto.Y, punto.Z);
			Gl.glVertex3d(punto.X + ancho, punto.Y + alto, punto.Z);
			Gl.glNormal3d(0,0,-1);
			Gl.glVertex3d(punto.X, punto.Y, punto.Z);
			Gl.glVertex3d(punto.X, punto.Y + alto, punto.Z);
			Gl.glNormal3d(-1,0,0);
			Gl.glVertex3d(punto.X, punto.Y, punto.Z + prof);
			Gl.glVertex3d(punto.X, punto.Y + alto, punto.Z + prof);
			Gl.glEnd();
			
			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glColor3f(0.0f,0.0f,0.0f);
			Gl.glNormal3d(0,-1,0);
			Gl.glVertex3d(punto.X, punto.Y, punto.Z + prof);
			Gl.glVertex3d(punto.X + ancho, punto.Y, punto.Z + prof);
			Gl.glVertex3d(punto.X, punto.Y, punto.Z);
			Gl.glVertex3d(punto.X + ancho, punto.Y, punto.Z);
			Gl.glEnd();
			
			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glColor3f(0.0f,0.0f,0.0f);
			Gl.glNormal3d(0,1,0);
			Gl.glVertex3d(punto.X, punto.Y + alto, punto.Z + prof);
			Gl.glVertex3d(punto.X + ancho, punto.Y + alto, punto.Z + prof);
			Gl.glVertex3d(punto.X, punto.Y + alto, punto.Z);
			Gl.glVertex3d(punto.X + ancho, punto.Y + alto, punto.Z);
			Gl.glEnd();
		
		}

	}
}
