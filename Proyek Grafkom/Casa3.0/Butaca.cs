using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Butaca.
	/// </summary>
	public class Butaca:Plantilla
	{
		protected int puestos=1;
		public Butaca(Point3D center, double angle,int puestos):base(center,angle,puestos)
		{
			this.canCullFace=true;
			yInc = h;
		}
		protected override void setInitialParams(object[] p) 
		{
			this.puestos=(int) p[0];
			l=37*puestos;
		}
		public Butaca(Point3D center, double angle): this(center,angle,1){;}
		protected double h = 37;
		protected double l = 40;
		protected double c = 7;

		protected double a = 5;
		protected override void Particular()
		{
			Gl.glColor3d(1,1,1);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("WOOD2"));
			Gl.glTranslated(-l-2,0,0);
			GlUtils.PintaOrtoedro(2,h,h);
			Gl.glTranslated(2*l+4,0,0);
			GlUtils.PintaOrtoedro(2,h,h);
			Gl.glTranslated(-l-2,0,0);
			Gl.glRotated(a,-1,0,0);
			int i =10;
			Gl.glTranslated(0,-1,i-10);
			GlUtils.PintaOrtoedro(l,2,h-i);

			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("TELA1"));
			Gl.glTranslated(0,c,3);
			GlUtils.PintaOrtoedro(l-1,c,h-i-4,true);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
			Gl.glTranslated(0,-c,-3);

			Gl.glTranslated(0,h-i,-(h-i)+2);
			Gl.glRotated(90,-1,0,0);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("WOOD2"));
			GlUtils.PintaOrtoedro(l,2,h-i);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("TELA1"));
			Gl.glTranslated(0,-c,3);
			GlUtils.PintaOrtoedro(l-1,c,h-i-4,true);

			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
		}

		void bak () 
		{
			Gl.glColor3d(1,1,1);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("WOOD2"));
			Gl.glTranslated(-l-2,0,0);
			GlUtils.PintaOrtoedro(2,h,h);
			Gl.glTranslated(2*l+4,0,0);
			GlUtils.PintaOrtoedro(2,h,h);
			Gl.glTranslated(-l-2,0,0);
			Gl.glRotated(a,-1,0,0);
			int i =10;
			Gl.glTranslated(0,-1,i-10);
			GlUtils.PintaOrtoedro(l,2,h-i);

			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("TELA1"));
			Gl.glTranslated(0,c,3);
			GlUtils.PintaOrtoedro(l-1,c,h-i-4,true);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
			Gl.glTranslated(0,-c,-3);

			Gl.glTranslated(0,h-i,-(h-i)+2);
			Gl.glRotated(90,-1,0,0);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("WOOD2"));
			GlUtils.PintaOrtoedro(l,2,h-i);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("TELA1"));
			Gl.glTranslated(0,-c,3);
			GlUtils.PintaOrtoedro(l-1,c,h-i-4,true);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
		}
	}
}
