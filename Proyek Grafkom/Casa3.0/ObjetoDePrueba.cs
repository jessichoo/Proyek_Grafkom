using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for ObjetoDePrueba.
	/// </summary>
	public class ObjetoDePrueba:GlObject
	{
//		protected double h = 10;
//		protected double d = 50;
//		protected double w = 30;
//		protected double l = 20;
//		protected double e = 3;
//		protected double a = 15;
//		protected double p = 6;
		protected double[][] puntos;
		protected Point3D[] normales;
		protected double[][] texturas;
		public ObjetoDePrueba()
		{
		}
		double xscale;
		double yscale;
		protected void ponPunto(int i) 
		{
			Gl.glNormal3dv(normales[i-1].Normalized.Coords);
			Gl.glTexCoord2d(texturas[i-1][0]*xscale,texturas[i-1][1]*yscale);
//			Gl.glTexCoord2d(texturas[i-1][0],texturas[i-1][1]);
			Gl.glVertex3dv(puntos[i-1]);
		}

		protected Point3D pointNormal(int point, params int[] others) 
		{
			Point3D normal = new Point3D(0,0,0);
			for (int i =0; i<others.Length-1; i++)
				normal+=this.planeNormal(others[i],point,others[i+1]);
			return normal.Normalized;
		}
		
		protected void ponPuntos(params int[] p) 
		{
			for (int i =0; i < p.Length; i++)
				ponPunto(p[i]);
		}
		protected Point3D planeNormal(int before, int center, int after) 
		{
			Point3D v1 = (new Point3D(puntos[before-1]))-new Point3D(puntos[center-1]);
			Point3D v2 = (new Point3D(puntos[after-1]))-new Point3D(puntos[center-1]);
			//return ((new Point3D(puntos[before]))-new Point3D(puntos[center])).CrossProduct(((new Point3D(puntos[before]))-new Point3D(puntos[center])));
			return v1.CrossProduct(v2).Normalized;
		}
		
		public void pintaColchon(double w, double h, double d) 
		{
			double l = 20;
			double e = 3;
			double a = 15;
			double p = 6;
//			xscale=.02;//*w;
//			yscale=.02;//*h;
			xscale = 1;//2*w/100;
			yscale = 1;//2*d/100;
			#region arreglo de puntos
			puntos = new double [][]
			{
				new double[] {-w,-h,-d},
				new double[] {w,-h,-d},
				new double[] {w,h,-d},
				new double[] {w-l,h,-d},
				new double[] {-w+l,h,-d},
				new double[] {-w,h,-d},
				new double[] {-w+l,h+p,-d+e},
				new double[] {w-l,h+p,-d+e},
				new double[] {-w+l,h+p,-d+e+a},
				new double[] {w-l,h+p,-d+e+a},
				new double[] {-w,h,-d+e+a+l},
				new double[] {-w+l,h,-d+e+a+l},
				new double[] {w-l,h,-d+e+a+l},
				new double[] {w,h,-d+e+a+l},
				new double[] {-w,h,d},
				new double[] {w,h,d},
				new double[] {-w,-h,d},
				new double[] {w,-h,d},
				new double[] {0,h,-d+e+a+l},
				new double[] {0,h,-d}
			};
			#endregion

			#region arreglo de normales
			normales=new Point3D[] 
				{
					pointNormal(1,17,6,2),
					pointNormal(2,1,3,18),
					pointNormal(3,4,8,14,2),
					pointNormal(4,5,7,8,3,1),
					pointNormal(5,6,7,4,2),
					pointNormal(6,1,15,7,5),
					pointNormal(7,6,9,8,4,5),
					pointNormal(8,7,10,3,4,5),				
					pointNormal(9,6,11,12,10,8),
					pointNormal(10,9,13,14,8,7),
					pointNormal(11,17,15,12,9,6),
					pointNormal(12,16,13,9,11),
					pointNormal(13,16,14,10,12),
					pointNormal(14,2,3,10,13,16),
					pointNormal(15,14,11,17,18),
					pointNormal(16,11,15,18,14),
					pointNormal(17,18,15,1),
					pointNormal(18,2,16,17),
					pointNormal(19,16,14,10),
					pointNormal(20,1,5,8)
				};
			#endregion

//			double rx=1/(2*h+d);
//			double ry=1/(2*h+w);
//			#region	arreglo de texturas
//			texturas = new double[][]
//				{
//					new double[]{w*rx,0},
//					new double[]{1-w*rx,0},
//					new double[]{1-w*rx,h*ry},
//
//////					new double[]{0,0},
//////					new double[]{1,0},
//////					new double[]{1,1},
//
//					new double[]{1-l*rx-w*rx,h*ry},
//					new double[]{w+l,h},
//
////					new double[]{0,1},					
//					new double[]{w,h},
//
//					new double[]{w+l,h+e},
//					new double[]{1-l-w,h+e},
//					new double[]{w+l,h+e+a},
//					new double[]{1-w-l,h+e+a},
//					new double[]{w,h+e+a+l},
//					new double[]{w+l,h+e+a+l},
//					new double[]{1-w-l,h+e+a+l},
//					new double[]{1-w,h+e+a+l},
//					new double[]{w,1-h},
//					new double[]{1-w,1-h},
//					new double[]{1,0},
//					new double[]{1,1},
//					new double[]{h+e+a+l,.5},
//					new double[]{h,.5},
//			};
//			#endregion

			#region Arreglo de texturas
			h=h/2;
			w=w/2;
			d=d/2;
			xscale = 1/(2*h+w);
			yscale = 1/(2*h+d);
			texturas = new double[][]
				{
					new double[]{h,0},
					new double[]{h+w,0},
					new double[]{h+w,h},
					new double[]{h+w-l,h},
					new double[]{h+l,h},
					new double[]{h,h},
					new double[]{h+l,h+e},
					new double[]{h+w-l,h+e},

					new double[]{h+l,h+e+a},
					new double[]{h+w-l,h+e+a},

					new double[]{h,h+e+a+l},
					new double[]{h+l,h+e+a+l},
					new double[]{h+w-l,h+e+a+l},
					new double[]{h+w,h+e+a+l},
					
					new double[]{h,h+d},
					new double[]{h+w,h+d},

					new double[]{2*h,h+d},
					new double[]{2*h+w,h+d},

					new double[]{h+w/2,h+e+a+l},
					new double[]{h+w/2,h},

				};
			#endregion
		
			pinta();
		}
		public void pinta() 
		{
//Gl.glDisable(Gl.GL_TEXTURE_2D);
//Gl.glEnable(Gl.GL_TEXTURE_2D);
//
			Gl.glBegin(Gl.GL_TRIANGLE_FAN);
			ponPuntos(20,1,2,3,4,5,6,1);
			Gl.glEnd();

////		Gl.glBegin(Gl.GL_QUADS);
////		ponPuntos(1,2,3,4);
////
//////			Gl.glTexCoord2d(0,0);
//////            Gl.glVertex3d(-50,100,0);
//////			Gl.glTexCoord2d(.5,0);
//////			Gl.glVertex3d(100,100,0);
//////			Gl.glTexCoord2d(1,1);
//////			Gl.glVertex3d(100,-100,0);
//////			Gl.glTexCoord2d(0,1);
//////			Gl.glVertex3d(-100,-100,0);
////		Gl.glEnd();

//
//Gl.glDisable(Gl.GL_TEXTURE_2D);
//Gl.glEnable(Gl.GL_TEXTURE_2D);
//
			Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
			ponPuntos(6,5,7,4,8,3);
			Gl.glEnd();

Gl.glDisable(Gl.GL_TEXTURE_2D);
Gl.glEnable(Gl.GL_TEXTURE_2D);

			Gl.glBegin(Gl.GL_QUAD_STRIP);
			ponPuntos(6,11,7,9,8,10,3,14);
			Gl.glEnd();

Gl.glDisable(Gl.GL_TEXTURE_2D);
Gl.glEnable(Gl.GL_TEXTURE_2D);

			Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
			ponPuntos(11,9,12,10,13,14);
			Gl.glEnd();

Gl.glDisable(Gl.GL_TEXTURE_2D);
Gl.glEnable(Gl.GL_TEXTURE_2D);

			Gl.glBegin(Gl.GL_TRIANGLE_FAN);
			ponPuntos(19,14,16,15,11,6,3,14);
			Gl.glEnd();

Gl.glDisable(Gl.GL_TEXTURE_2D);
Gl.glEnable(Gl.GL_TEXTURE_2D);

			Gl.glBegin(Gl.GL_TRIANGLE_FAN);
			ponPuntos(11,6,1,17,15);
			Gl.glEnd();

Gl.glDisable(Gl.GL_TEXTURE_2D);
Gl.glEnable(Gl.GL_TEXTURE_2D);

			Gl.glBegin(Gl.GL_QUADS);
			ponPuntos(17,15,16,18);
			Gl.glEnd();

Gl.glDisable(Gl.GL_TEXTURE_2D);
Gl.glEnable(Gl.GL_TEXTURE_2D);

			Gl.glBegin(Gl.GL_TRIANGLE_FAN);
			ponPuntos(14,3,2,18,16);
			Gl.glEnd();
		}
		public override void Render() 
		{
			Gl.glColor3d(1,1,1);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("COLCHON"));
			double h = 10;
			double d = 50;
			double w = 30;
			this.pintaColchon(w,h,d);
		}
	}
}
