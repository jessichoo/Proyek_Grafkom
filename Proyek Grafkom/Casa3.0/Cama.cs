using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Cama.
	/// </summary>
	public class Cama: Plantilla
	{
		protected double ancho;
		protected double largo;

		protected override void setInitialParams(params object[] iniciales) 
		{
			this.largo = 100;
			this.ancho = 60;
			try 
			{
				largo = (double) iniciales[0];
				ancho = (double) iniciales[1];
			}
			catch (Exception e) {;}
		}
		public Cama(Point3D center , double angle, double largo, double ancho) : base(center , angle,largo,ancho)
		{
			yInc = 30;
			this.canCullFace=true;
		}

		public Cama(Point3D center,double angle) : this(center , angle, 100, 60){}

		public Cama(Point3D center) : this(center , 0, 100, 60){}
		
		protected override void Particular()
		{
			//Gl.glColor3d(0.26171875,0.55078125,0.69140625);
			Gl.glColor3d(1,1,1);
			Gl.glPushMatrix();
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("COLCHON"));
			GlUtils.PintaOrtoedro(largo,10,ancho,true);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("WOOD1"));
			Glu.GLUquadric q = Glu.gluNewQuadric();
			Glu.gluQuadricTexture(q,Gl.GL_TRUE);
			Gl.glTranslated(-largo + 5,-10,-ancho + 5);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(q,5,2,20,20,20);
			Gl.glRotated(-90,1,0,0);
			Gl.glTranslated(0,0,2*ancho -10);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(q,5,2,20,20,20);
			Gl.glRotated(-90,1,0,0);
			Gl.glTranslated(2*largo -10,0,0);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(q,5,2,20,20,20);
			Gl.glRotated(-90,1,0,0);
			Gl.glTranslated(0,0,-2*ancho +10);
			Gl.glRotated(90,1,0,0);
			Glu.gluCylinder(q,5,2,20,20,20);
			Gl.glRotated(-90,1,0,0);
            Gl.glPopMatrix();
//			yInc = 30;
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
		}
	
//		#region Para pintar el colchon
//		protected double[][] puntos;
//		protected Point3D[] normales;
//		protected double[][] texturas;
//		protected void ponPunto(int i) 
//		{
//			Gl.glNormal3dv(normales[i-1].Normalized.Coords);
//			Gl.glTexCoord2dv(texturas[i-1]);
//			Gl.glVertex3dv(puntos[i-1]);
//		}
//
//		protected Point3D pointNormal(int point, params int[] others) 
//		{
//			Point3D normal = new Point3D(0,0,0);
//			for (int i =0; i<others.Length-1; i++)
//				normal+=this.planeNormal(others[i],point,others[i+1]);
//			return normal.Normalized;
//		}
//		
//		protected void ponPuntos(params int[] p) 
//		{
//			for (int i =0; i < p.Length; i++)
//				ponPunto(p[i]);
//		}
//		protected Point3D planeNormal(int before, int center, int after) 
//		{
//			Point3D v1 = (new Point3D(puntos[before-1]))-new Point3D(puntos[center-1]);
//			Point3D v2 = (new Point3D(puntos[after-1]))-new Point3D(puntos[center-1]);
//			//return ((new Point3D(puntos[before]))-new Point3D(puntos[center])).CrossProduct(((new Point3D(puntos[before]))-new Point3D(puntos[center])));
//			return v1.CrossProduct(v2).Normalized;
//		}
//		
//		protected void pintaColchon(double w, double h, double d) 
//		{
//			double l = 30;
//			double e = 10;
//			double a = 20;
//			double p = 10;
//			#region arreglo de puntos
//			puntos = new double [][]
//			{
//				new double[] {-w,-h,-d},
//				new double[] {w,-h,-d},
//				new double[] {w,h,-d},
//				new double[] {w-l,h,-d},
//				new double[] {-w+l,h,-d},
//				new double[] {-w,h,-d},
//				new double[] {-w+l,h+p,-d+e},
//				new double[] {w-l,h+p,-d+e},
//				new double[] {-w+l,h+p,-d+e+a},
//				new double[] {w-l,h+p,-d+e+a},
//				new double[] {-w,h,-d+e+a+l},
//				new double[] {-w+l,h,-d+e+a+l},
//				new double[] {w-l,h,-d+e+a+l},
//				new double[] {w,h,-d+e+a+l},
//				new double[] {-w,h,d},
//				new double[] {w,h,d},
//				new double[] {-w,-h,d},
//				new double[] {w,-h,d},
//				new double[] {0,h,-d+e+a+l},
//				new double[] {0,h,-d}
//			};
//			#endregion
//
//			#region arreglo de normales
//			normales=new Point3D[] 
//				{
//					pointNormal(1,17,6,2),
//					pointNormal(2,1,3,18),
//					pointNormal(3,4,8,14,2),
//					pointNormal(4,5,7,8,3,1),
//					pointNormal(5,6,7,4,2),
//					pointNormal(6,1,15,7,5),
//					pointNormal(7,6,9,8,4,5),
//					pointNormal(8,7,10,3,4,5),				
//					pointNormal(9,6,11,12,10,8),
//					pointNormal(10,9,13,14,8,7),
//					pointNormal(11,17,15,12,9,6),
//					pointNormal(12,16,13,9,11),
//					pointNormal(13,16,14,10,12),
//					pointNormal(14,2,3,10,13,16),
//					pointNormal(15,14,11,17,18),
//					pointNormal(16,11,15,18,14),
//					pointNormal(17,18,15,1),
//					pointNormal(18,2,16,17),
//					pointNormal(19,16,14,10),
//					pointNormal(20,1,5,8)
//				};
//			#endregion
//
//			double rx=(1-2*w)/2;
//			double ry=(1-2*d)/2;
//			#region	arreglo de texturas
//			texturas = new double[][]
//				{
//					new double[]{0,0},
//					new double[]{0,1},
//					new double[]{1-rx,ry},
//					new double[]{1-l-rx,ry},
//					new double[]{rx+l,ry},
//					new double[]{rx,ry},
//					new double[]{rx+l,ry+e},
//					new double[]{1-l-rx,ry+e},
//					new double[]{rx+l,ry+e+a},
//					new double[]{1-rx-l,ry+e+a},
//					new double[]{rx,ry+e+a+l},
//					new double[]{rx+l,ry+e+a+l},
//					new double[]{1-rx-l,ry+e+a+l},
//					new double[]{1-rx,ry+e+a+l},
//					new double[]{rx,1-ry},
//					new double[]{1-rx,1-ry},
//					new double[]{0,1},
//					new double[]{1,1},
//					new double[]{.5,ry+e+a+l},
//					new double[]{.5,ry},
//			};
//			#endregion
//			pinta();
//		}
//		protected void pinta() 
//		{
//			Gl.glBegin(Gl.GL_TRIANGLE_FAN);
//			ponPuntos(20,1,2,3,4,5,6,1);
//			Gl.glEnd();
//
//			Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
//			ponPuntos(6,5,7,4,8,3);
//			Gl.glEnd();
//
//			Gl.glBegin(Gl.GL_QUAD_STRIP);
//			ponPuntos(6,11,7,9,8,10,3,14);
//			Gl.glEnd();
//
//			Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
//			ponPuntos(11,9,12,10,13,14);
//			Gl.glEnd();
//
//			Gl.glBegin(Gl.GL_TRIANGLE_FAN);
//			ponPuntos(19,14,16,15,11,6,3,14);
//			Gl.glEnd();
//
//			Gl.glBegin(Gl.GL_TRIANGLE_FAN);
//			ponPuntos(11,6,1,17,15);
//			Gl.glEnd();
//
//
//			Gl.glBegin(Gl.GL_QUADS);
//			ponPuntos(17,15,16,18);
//			Gl.glEnd();
//
//			Gl.glBegin(Gl.GL_TRIANGLE_FAN);
//			ponPuntos(14,3,2,18,16);
//			Gl.glEnd();
//		}
////		public override void Render() 
////		{
////			double h = 10;
////			double d = 50;
////			double w = 30;
////			this.pintaColchon(w,h,d);
////		}
//		#endregion

	}
}
