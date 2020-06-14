//#define destechada
using System;
using Tao.OpenGl;
using System.Collections;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Casa2.
	/// </summary>
	public class Casa2: GlObject
	{
		protected WallStrip ws;
		Piso piso;
		Techo techo;
		double x1=521.5;
		double x2=-344;
		double z1=603;
		double z2=-670;
		double height=300;
		double farDistance=1200;

		public Casa2 () 
		{
			ws = new WallStrip(0,270);
			ws.CloseFrom(false);
			ws.CloseTo(false);
			piso = new Piso(x1,z1,x2,z2,10);
			techo = new Techo(x1,z1,x2,z2,20,height-10-20);
//			piso = new Piso(521.5,603,-344,-670,10);
//			techo = new Techo(521.5,603,-344,-670,20,270);
			#region BalconRecibidor
			//Pared exterior de la sala
			//1
			ws.BeginStrip(false,true);
			ws.CloseTo(true);
			ws.Add(491.5,-10,491.5,373,"woden glass woden");
//			ws.Add(491.5,-10,491.5,373,"woden");
			ws.CloseTo(false);

			ws.AddTo(491.5,523,"glass");
			ws.AddTo(296,573,"glass");
			ws.CloseFrom(true);
			ws.AddTo(184,573);
			ws.CloseFrom(false);
			ws.AddTo(184,318);
			ws.EndStrip();
			#endregion
			#region salaCocina - division entre la sala y la cocina
			ws.BeginStrip(false,false);
			ws.Add(178,416,103,416,"reversed door");
			ws.AddTo(0,416);
			ws.AddTo(-103,318);
			ws.AddTo(-103,37);
			ws.AddTo(-103,-37,"passage");
			ws.EndStrip();

//			ws.BeginStrip(false,true);
//			ws.Add(-103,-37,-103,37,"passage");
//			ws.AddTo(-103,318);
//			ws.AddTo(0,416);
//			ws.AddTo(103,416);
//			ws.AddTo(178,416,"door");
//			ws.EndStrip();
			#endregion
			#region outterShell			
			//9
			ws.BeginStrip(false,false);
			ws.Add(-103,-37,-103,-288);
			ws.AddTo(-103,-362,"door");
			ws.EndStrip();

			//8
			ws.BeginStrip(false,false);
			ws.Add(-103,-125,-314,-125);
			//ws.AddTo(-314,162,"woden woden",true);
			ws.AddTo(-230,162);
			ws.AddTo(-103,162);
			ws.EndStrip();

			//7
			ws.BeginStrip(false,true);
			ws.Add(-185,-125,-185,-220);
			ws.EndStrip();
			
			//6
			ws.BeginStrip(false,true);
			ws.Add(-314,-125,-314,-215);
			ws.AddTo(-268,-215);
			ws.EndStrip();
			
			//5
			ws.BeginStrip(false,false);
			ws.Add(-314,-215,-314,-480,"glass woden");
			ws.AddTo(-103,-480);
			ws.AddTo(-103,-362);
			ws.EndStrip();
			
			//4
			ws.BeginStrip(false,true);
			ws.Add(-314,-480,-314,-640);
			ws.AddTo(10,-640);
			ws.AddTo(10,-420);
			ws.AddTo(126,-420);
			ws.AddTo(126,-640); // Idealmente, esta seria otro tipo de pared, "walk in closet" o algo asi.
			ws.EndStrip();
			
			//3
			ws.BeginStrip(false,true);
			ws.Add(10,-640,491.5,-640,"glass woden");
			ws.AddTo(491.5,-325,"woden glass woden");
			ws.AddTo(71,-325);
			ws.EndStrip();
			ws.BeginStrip(false,false);
			ws.Add(88,-325,88,-230,"passage");
			ws.EndStrip();
			ws.BeginStrip(false,false);
			ws.Add(88,-325,86,-420,"passage");
			ws.EndStrip();

			//2
			ws.BeginStrip(false,true);
			ws.Add(491.5,-325,491.5,-10,"woden glass woden");
			ws.AddTo(10,-5);
			ws.AddTo(10,-230);
			ws.AddTo(126,-230);
			ws.AddTo(126,-5); // Idealmente, esta seria otro tipo de pared, "walk in closet" o algo asi.
			ws.EndStrip();
			#endregion
			//Puerta del banno
			ws.BeginStrip(false,false);
			ws.Add(10,-480,-103,-480,"passage");
			ws.EndStrip();
		}
		public override void Split(ArrayList far, ArrayList near) 
		{
			ws.Split(far,near);
			piso.Split(far,near);
#if !destechada
			techo.Split(far,near);
#endif
		}
		public override void Prepare (Avatar observer) 
		{
//			observer.Translate(-center);
			piso.Prepare(observer);
			ws.Prepare(observer);
			techo.Prepare(observer);
//			observer.Translate(center);
		}
		public override void Render() 
		{
//			Gl.glPushMatrix();
//			Gl.glTranslated(center.X,center.Y,center.Z);
			ws.Render();
			piso.Render();
			techo.Render();
//			Gl.glPopMatrix();
		}
		public override void FindTargetsFor(char c, ArrayList result) 
		{
			ws.FindTargetsFor(c,result);
		}
		public override Point3D ColisionNormal(Point3D punto, Point3D direction, double radius) 
		{
			Point3D p2 = punto+direction;
			if (p2.Norm>this.farDistance)
				return p2.Normalized.Scaled(-p2.Norm+farDistance);
			if (p2.X<x2-radius || p2.X > x1+radius || p2.Z<z2-radius || p2.Z>z1+radius ||
				p2.Y>height+radius) return new Point3D(0,0,0);
			return this.ws.ColisionNormal(punto,direction,radius)+this.techo.ColisionNormal(punto,direction,radius);
		}
	}

	class Piso:GlObject 
	{
		double x1;
		double z1; 
		double x2; 
		double z2;
		double height;
		int textura;
		public Piso(double x1, double z1, double x2, double z2, double height) 
		{
			this.x1=Math.Min(x1,x2);
			this.x2=Math.Max(x1,x2);
			this.z1=Math.Min(z1,z2);
			this.z2=Math.Max(z1,z2);
			this.height=height;
			textura = GlUtils.Texture("PISO");
		}
		
		Point3D camara;
		public override void Prepare (Avatar observer)
		{
			//Parche: pondre a las normales siempre apuntando hacia la camara,
			//que es donde esta la fuente de luz.
			this.camara=observer.Origin;
		}
		public override void Render ()
		{
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,textura);
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glTexCoord2d((z2-z1)/100,0);
			Gl.glNormal3dv((camara-(new Point3D(x1,0,z2))).Normalized.Coords);
			Gl.glVertex3d(x1,0,z2);
			Gl.glTexCoord2d((z2-z1)/100,(x2-x1)/100);
			Gl.glNormal3dv((camara-(new Point3D(x2,0,z2))).Normalized.Coords);
			Gl.glVertex3d(x2,0,z2);
			Gl.glTexCoord2d(0,(x2-x1)/100);
			Gl.glNormal3dv((camara-(new Point3D(x2,0,z1))).Normalized.Coords);
			Gl.glVertex3d(x2,0,z1);
			Gl.glTexCoord2d(0,0);
			Gl.glNormal3dv((camara-(new Point3D(x1,0,z1))).Normalized.Coords);
			Gl.glVertex3d(x1,0,z1);
			Gl.glEnd();
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);

			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glColor3d(.5,.5,.5);
			Gl.glNormal3dv((camara-(new Point3D(x1,-height,z1))).Normalized.Coords);
			Gl.glVertex3d(x1,-height,z1);
			Gl.glNormal3dv((camara-(new Point3D(x1,0,z1))).Normalized.Coords);
			Gl.glVertex3d(x1,0,z1);

			//Gl.glNormal3d(1,0,-1);
			Gl.glNormal3dv((camara-(new Point3D(x2,-height,z1))).Normalized.Coords);
			Gl.glVertex3d(x2,-height,z1);
			Gl.glNormal3dv((camara-(new Point3D(x2,0,z1))).Normalized.Coords);
			Gl.glVertex3d(x2,0,z1);

			//Gl.glNormal3d(1,0,1);
			Gl.glNormal3dv((camara-(new Point3D(x2,-height,z2))).Normalized.Coords);
			Gl.glVertex3d(x2,-height,z2);
			Gl.glNormal3dv((camara-(new Point3D(x2,0,z2))).Normalized.Coords);
			Gl.glVertex3d(x2,0,z2);

			//Gl.glNormal3d(-1,0,1);
			Gl.glNormal3dv((camara-(new Point3D(x1,-height,z2))).Normalized.Coords);
			Gl.glVertex3d(x1,-height,z2);
			Gl.glNormal3dv((camara-(new Point3D(x1,0,z2))).Normalized.Coords);
			Gl.glVertex3d(x1,0,z2);

			//Gl.glNormal3d(-1,0,-1);
			Gl.glNormal3dv((camara-(new Point3D(x1,0,z1))).Normalized.Coords);
			Gl.glVertex3d(x1,-height,z1);
			Gl.glNormal3dv((camara-(new Point3D(x1,0,z1))).Normalized.Coords);
			Gl.glVertex3d(x1,0,z1);

			Gl.glEnd();
		}

	}


	class Techo:GlObject 
	{
		double x1;
		double z1; 
		double x2; 
		double z2;
		double height;
		int texturaIn;
		int texturaOut;
		double bottom;
		public Techo(double x1, double z1, double x2, double z2, double height,double bottom) 
		{
			this.x1=Math.Min(x1,x2);
			this.x2=Math.Max(x1,x2);
			this.z1=Math.Min(z1,z2);
			this.z2=Math.Max(z1,z2);
			this.height=height;
			this.bottom=bottom;
			texturaIn = GlUtils.Texture("TECHOIN");
			texturaOut = GlUtils.Texture("TECHOOUT");
		}
		
		Point3D camara;
		bool pintaArriba=true;
		bool pintaAbajo=true;
		public override void Prepare (Avatar observer)
		{
			//Parche: pondre a las normales siempre apuntando hacia la camara,
			//que es donde esta la fuente de luz.
			this.camara=observer.Origin;
			pintaAbajo = camara.Y<bottom+height/2;
			pintaArriba = camara.Y>bottom+height/2;
		}
		public override void Render ()
		{
			if (pintaAbajo) 
			{
			#region techoabajo
				Gl.glBindTexture(Gl.GL_TEXTURE_2D,texturaIn);
				Gl.glBegin(Gl.GL_QUADS);
				Gl.glTexCoord2d(0,0);
				Gl.glNormal3dv((camara-(new Point3D(x1,bottom,z1))).Normalized.Coords);
				Gl.glVertex3d(x1,bottom,z1);
				Gl.glTexCoord2d(0,(x2-x1)/100);
				Gl.glNormal3dv((camara-(new Point3D(x2,bottom,z1))).Normalized.Coords);
				Gl.glVertex3d(x2,bottom,z1);		
				Gl.glTexCoord2d((z2-z1)/100,(x2-x1)/100);
				Gl.glNormal3dv((camara-(new Point3D(x2,bottom,z2))).Normalized.Coords);
				Gl.glVertex3d(x2,bottom,z2);			
				Gl.glTexCoord2d((z2-z1)/100,0);
				Gl.glNormal3dv((camara-(new Point3D(x1,bottom,z2))).Normalized.Coords);
				Gl.glVertex3d(x1,bottom,z2);			
				Gl.glEnd();
				Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
			#endregion
			}
			#region techolados
			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glNormal3dv((camara-(new Point3D(x1,bottom,z1))).Normalized.Coords);
			Gl.glVertex3d(x1,bottom,z1);
			Gl.glNormal3dv((camara-(new Point3D(x1,bottom+height,z1))).Normalized.Coords);
			Gl.glVertex3d(x1,bottom+height,z1);

			//Gl.glNormal3d(1,0,-1);
			Gl.glNormal3dv((camara-(new Point3D(x2,bottom,z1))).Normalized.Coords);
			Gl.glVertex3d(x2,bottom,z1);
			Gl.glNormal3dv((camara-(new Point3D(x2,bottom+height,z1))).Normalized.Coords);
			Gl.glVertex3d(x2,bottom+height,z1);

			//Gl.glNormal3d(1,0,1);
			Gl.glNormal3dv((camara-(new Point3D(x2,bottom,z2))).Normalized.Coords);
			Gl.glVertex3d(x2,bottom,z2);
			Gl.glNormal3dv((camara-(new Point3D(x2,bottom+height,z2))).Normalized.Coords);
			Gl.glVertex3d(x2,bottom+height,z2);

			//Gl.glNormal3d(-1,0,1);
			Gl.glNormal3dv((camara-(new Point3D(x1,bottom,z2))).Normalized.Coords);
			Gl.glVertex3d(x1,bottom,z2);
			Gl.glNormal3dv((camara-(new Point3D(x1,bottom+height,z2))).Normalized.Coords);
			Gl.glVertex3d(x1,bottom+height,z2);

			//Gl.glNormal3d(-1,0,-1);
			Gl.glNormal3dv((camara-(new Point3D(x1,bottom,z1))).Normalized.Coords);
			Gl.glVertex3d(x1,bottom,z1);
			Gl.glNormal3dv((camara-(new Point3D(x1,bottom,z1))).Normalized.Coords);
			Gl.glVertex3d(x1,bottom+height,z1);

			Gl.glEnd();
			#endregion
			if (pintaArriba) 
			{
			#region techoarriba
				Gl.glBindTexture(Gl.GL_TEXTURE_2D,texturaOut);
				Gl.glBegin(Gl.GL_QUADS);
				Gl.glTexCoord2d((z2-z1)/100,0);
				Gl.glNormal3dv((camara-(new Point3D(x1,bottom+height,z2))).Normalized.Coords);
				Gl.glVertex3d(x1,bottom+height,z2);
				Gl.glTexCoord2d((z2-z1)/100,(x2-x1)/100);
				Gl.glNormal3dv((camara-(new Point3D(x2,bottom+height,z2))).Normalized.Coords);
				Gl.glVertex3d(x2,bottom+height,z2);
				Gl.glTexCoord2d(0,(x2-x1)/100);
				Gl.glNormal3dv((camara-(new Point3D(x2,bottom+height,z1))).Normalized.Coords);
				Gl.glVertex3d(x2,bottom+height,z1);
				Gl.glTexCoord2d(0,0);
				Gl.glNormal3dv((camara-(new Point3D(x1,bottom+height,z1))).Normalized.Coords);
				Gl.glVertex3d(x1,bottom+height,z1);
				Gl.glEnd();
				Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
			#endregion
			}
		}
		public override Point3D ColisionNormal(Point3D punto, Point3D direccion, double radio) 
		{
			Point3D destino=punto+direccion;
			if (destino.X<x1-radio || destino.X>x2+radio) return new Point3D(0,0,0);
			if (destino.Z<z1-radio || destino.Z>z2+radio) return new Point3D(0,0,0);
			if ((destino.Y<this.bottom-radio && punto.Y<this.bottom-radio) || 
				(destino.Y>this.bottom+radio && punto.Y>this.bottom+radio)) return new Point3D(0,0,0);
			//return new Point3D(0,-direccion.Y/Math.Abs(direccion.Y)*,0);
			if (direccion.Y>0 && punto.Y<bottom)
				return new Point3D(0,-destino.Y+bottom-radio,0);
			if (direccion.Y<0 && punto.Y>bottom) 
				return new Point3D(0,bottom+radio-destino.Y,0);
			return new Point3D(0,0,0);
		}
	}
}
