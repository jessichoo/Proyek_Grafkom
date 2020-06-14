//#define showCenterPoint
using System;
using Tao.OpenGl;
using System.Collections;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Puerta.
	/// </summary>
	public class Puerta : GlObject, InteractiveObject
	{
		protected int idPuerta = -1;
		protected static int textureIndex=-1;
		public double Apertura = 0;
		public bool SoloMarco=false;
		public bool IsOpened 
		{
			get 
			{
				return SoloMarco || this.aperturaActual==this.Apertura && this.Apertura>0; 
			}
		}
		public Puerta():this(new Point3D(0,0,0),0)
		{
		}
		protected Point3D position;
		protected double angle;
		protected int texturaBisagra;
		protected int texturaPuerta;
		protected int texturaKnob;
		protected bool rev = true;
		public bool reversed 
		{
			get 
			{
				return rev;
			}
			set 
			{
				rev=value;
				Gl.glNewList(idPuerta,Gl.GL_COMPILE);
				this.pintaPuerta();
				Gl.glEndList();
			}
		}
		public Puerta(Point3D position, double angle)
		{
			textureIndex=GlUtils.Texture("ROSEGOLD");
			texturaBisagra=GlUtils.Texture("BISAGRA");
			texturaPuerta=GlUtils.Texture("PUERTA");
			texturaKnob=GlUtils.Texture("ROSEGOLD");
			this.position=position;
			this.angle=angle;
			if (idPuerta==-1) 
			{
				idPuerta=Gl.glGenLists(2);
				Gl.glNewList(idPuerta,Gl.GL_COMPILE);
				this.pintaPuerta();
				Gl.glEndList();
				Gl.glNewList(idPuerta+1,Gl.GL_COMPILE);
				this.pintaBisagra();
				Gl.glEndList();
			}
		}

		public Point3D Location { get {return position;} set {position=value;}}
		public double Angle {get { return angle;} set {angle=value;}}
		protected bool far = false;
		public override void Prepare (Avatar observer) 
		{
			double elapsed = observer.CurrentTime.Subtract(this.inicio).TotalSeconds;
			if (this.opening && this.aperturaActual!=this.Apertura)
				this.aperturaActual=Math.Min(this.Apertura,this.velocidad*elapsed);
			if (this.closing && this.aperturaActual!=0)
				this.aperturaActual=Math.Max(0,this.Apertura-this.velocidad*elapsed);
			far = this.DistanceTo(observer.Origin)>500;
		}
		protected double width=74;
		protected double height=200;
		protected double deep=10;
		protected double border=4;
		public double Height { get { return height;} }
		public double Width { 
			get { return width;}
			set 
			{
				width=value;
			}
		}
		public override void Render () 
		{
			//Gl.glEnable(Gl.GL_CULL_FACE);
			#region Una esfera flotando en el centro
#if showCenterPoint
			Glu.GLUquadric qua = Glu.gluNewQuadric();
			Point3D center;
			center = position+(new Point3D(width*Math.Cos(Angle*Math.PI/180),height,width*Math.Sin(Angle*Math.PI/180))).Scaled(.5);
			Gl.glPushMatrix();
			Console.WriteLine(center.ToString());
			Gl.glTranslated(center.X,center.Y,center.Z);
			Glu.gluSphere(qua,5,10,10);
			Gl.glPopMatrix();
			Glu.gluDeleteQuadric(qua);
#endif
			#endregion

			Gl.glPushMatrix();
			Gl.glTranslated(position.X,position.Y,position.Z);
			Gl.glRotated(angle,0,-1,0);

			Gl.glPushMatrix();
			Gl.glTranslated(width/2,height/2,0);
			#region Marco
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,textureIndex);
			Gl.glColor3d(0.8,0.8,0.8);
			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,1);Gl.glVertex3d(-width/2,height/2,deep/2);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,0);Gl.glVertex3d(-width/2,-height/2,deep/2);
				
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0.25,1);Gl.glVertex3d(-width/2+border,height/2,deep/2);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0.25,0);Gl.glVertex3d(-width/2+border,-height/2,deep/2);
				
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0.5,1);Gl.glVertex3d(-width/2+border,height/2,-deep/2);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0.5,0);Gl.glVertex3d(-width/2+border,-height/2,-deep/2);
				
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(.75,1);Gl.glVertex3d(-width/2,height/2,-deep/2);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(.75,0);Gl.glVertex3d(-width/2,-height/2,-deep/2);
			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0,1);Gl.glVertex3d(width/2,height/2,-deep/2);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0,0);Gl.glVertex3d(width/2,-height/2,-deep/2);
				
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0.25,1);Gl.glVertex3d(width/2-border,height/2,-deep/2);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0.25,0);Gl.glVertex3d(width/2-border,-height/2,-deep/2);
				
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0.5,1);Gl.glVertex3d(width/2-border,height/2,deep/2);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0.5,0);Gl.glVertex3d(width/2-border,-height/2,deep/2);
				
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(.75,1);Gl.glVertex3d(width/2,height/2,deep/2);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(.75,0);Gl.glVertex3d(width/2,-height/2,deep/2);
			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUAD_STRIP);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0,0);Gl.glVertex3d(-width/2+border,height/2,-deep/2);
			Gl.glNormal3d(0,0,-1);Gl.glTexCoord2d(0,1);Gl.glVertex3d(width/2-border,height/2,-deep/2);
				
			Gl.glNormal3d(0,-0.7,-0.7);Gl.glTexCoord2d(0.25,0);Gl.glVertex3d(-width/2+border,height/2-border,-deep/2);
			Gl.glNormal3d(0,-0.7,-0.7);Gl.glTexCoord2d(0.25,1);Gl.glVertex3d(width/2-border,height/2-border,-deep/2);
				
			Gl.glNormal3d(0,-0.7,0.7);Gl.glTexCoord2d(0.5,0);Gl.glVertex3d(-width/2+border,height/2-border,deep/2);
			Gl.glNormal3d(0,-0.7,0.7);Gl.glTexCoord2d(0.5,1);Gl.glVertex3d(width/2-border,height/2-border,deep/2);
				
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(.75,0);Gl.glVertex3d(-width/2+border,height/2,deep/2);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(.75,1);Gl.glVertex3d(width/2-border,height/2,deep/2);
			Gl.glEnd();
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);
			#endregion
			Gl.glPopMatrix();
			if (!SoloMarco) 
			{
				Gl.glPushMatrix();
				if (!reversed)
					Gl.glTranslated(border+.5,0,-deep/2+.5);
				else 
					Gl.glTranslated(border+.5,0,deep/2-.5);
//				pintaPuerta();
				Gl.glCallList(this.idPuerta+1);
				Gl.glPushMatrix();
				Gl.glColor3d(1,1,1);
				Gl.glRotated(reversed?-this.aperturaActual:this.aperturaActual,0,1,0);
				//if (far) Gl.glLightModeli(Gl.GL_LIGHT_MODEL_TWO_SIDE,1);
				Gl.glCallList(idPuerta);
				//if (far) Gl.glLightModeli(Gl.GL_LIGHT_MODEL_TWO_SIDE,0);
				Gl.glPopMatrix();
				Gl.glPopMatrix();
			}
			Gl.glPopMatrix();
			//Gl.glDisable(Gl.GL_CULL_FACE);
		}
		protected void pintaBisagra() 
		{
			#region bisagras
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,this.texturaBisagra);
			Gl.glColor3d(1,1,1);
			Glu.GLUquadric q = Glu.gluNewQuadric();
			Glu.gluQuadricTexture(q,Gl.GL_TRUE);
			double h = (height-border)/3;
			double m = (height-border)/2-4;
			Gl.glPushMatrix();
			Gl.glTranslated(0,m,0);
			Gl.glRotated(90,-1,0,0);
			Glu.gluCylinder(q,1,1,8,10,10);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
			Gl.glTranslated(0,m-h,0);
			Gl.glRotated(90,-1,0,0);
			Glu.gluCylinder(q,1,1,8,10,10);
			Gl.glPopMatrix();
			Gl.glPushMatrix();
			Gl.glTranslated(0,m+h,0);
			Gl.glRotated(90,-1,0,0);
			Glu.gluCylinder(q,1,1,8,10,10);
			Gl.glPopMatrix();
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
			Glu.gluDeleteQuadric(q);
			#endregion
		}
		protected void pintaPuerta() 
		{
			#region puerta
//			Gl.glPushMatrix();
//			Gl.glColor3d(1,1,1);
//			Gl.glRotated(reversed?-this.aperturaActual:this.aperturaActual,0,1,0);
			double halfdeep = 2*deep/6;
			if (!reversed) 
			{
				Gl.glTranslated(width/2-border-.5,(height-border)/2,halfdeep);
				Gl.glTranslated(.5,0,.5);
			} 
			else 
			{
				Gl.glTranslated(width/2-border-.5,(height-border)/2,-halfdeep);
				Gl.glTranslated(.5,0,-.5);
			}
			pintaCaja(width/2-border-.5,(height-border)/2,halfdeep);
			Gl.glPushMatrix();
			Gl.glTranslated(width/2-border-10,0,halfdeep);
			doorKnob();
			Gl.glPopMatrix();
			Gl.glPushMatrix();
			Gl.glScaled(1,1,-1);
			Gl.glTranslated(width/2-border-10,0,halfdeep);
			doorKnob();
			Gl.glPopMatrix();
			#endregion
		}
		protected void pintaCaja(double x, double y, double z) 
		{
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,this.texturaPuerta);
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glNormal3d(0,0,1);
			Gl.glTexCoord2d(0,0);
			Gl.glVertex3d(-x,y,z);
			Gl.glTexCoord2d(0,y*2/100);
			Gl.glVertex3d(-x,-y,z);
			Gl.glTexCoord2d(x*2/100,y*2/100);
			Gl.glVertex3d(x,-y,z);
			Gl.glTexCoord2d(x*2/100,0);
			Gl.glVertex3d(x,y,z);

			Gl.glNormal3d(1,0,0);
			Gl.glTexCoord2d(0,0);
			Gl.glVertex3d(x,y,z);
			Gl.glTexCoord2d(0,y*2/100);	
			Gl.glVertex3d(x,-y,z);
			Gl.glTexCoord2d(z*2/100,y*2/100);
			Gl.glVertex3d(x,-y,-z);
			Gl.glTexCoord2d(z*2/100,0);
			Gl.glVertex3d(x,y,-z);

			Gl.glNormal3d(0,0,-1);
			Gl.glTexCoord2d(0,0);
			Gl.glVertex3d(x,y,-z);
			Gl.glTexCoord2d(0,y*2/100);
			Gl.glVertex3d(x,-y,-z);
			Gl.glTexCoord2d(x*2/100,y*2/100);
			Gl.glVertex3d(-x,-y,-z);
			Gl.glTexCoord2d(x*2/100,0);
			Gl.glVertex3d(-x,y,-z);

			Gl.glNormal3d(-1,0,0);
			Gl.glTexCoord2d(0,0);
			Gl.glVertex3d(-x,y,-z);
			Gl.glTexCoord2d(0,y*2/100);
			Gl.glVertex3d(-x,-y,-z);
			Gl.glTexCoord2d(z*2/100,y*2/100);
			Gl.glVertex3d(-x,-y,z);
			Gl.glTexCoord2d(z*2/100,0);
			Gl.glVertex3d(-x,y,z);

			Gl.glNormal3d(0,1,0);
			Gl.glVertex3d(-x,y,z);
			Gl.glVertex3d(x,y,z);
			Gl.glVertex3d(x,y,-z);
			Gl.glVertex3d(-x,y,-z);

//			Gl.glNormal3d(0,-1,0);
//			Gl.glVertex3d(-x,-y,z);
//			Gl.glVertex3d(x,-y,z);
//			Gl.glVertex3d(x,-y,-z);
//			Gl.glVertex3d(-x,-y,-z);
			Gl.glEnd();
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,this.texturaPuerta);
		}
		protected void doorKnob() 
		{
			int cullFace;
			Gl.glGetBooleanv(Gl.GL_CULL_FACE,out cullFace);
			Gl.glDisable(Gl.GL_CULL_FACE);

			Glu.GLUquadric q = Glu.gluNewQuadric();
			//Gl.glColor3d(1,1,1);
			Gl.glColor3d(1,1,0);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,this.texturaKnob);
			Glu.gluQuadricTexture(q,Gl.GL_TRUE);
			Glu.gluQuadricNormals(q,Gl.GL_SMOOTH);
			Gl.glPushMatrix();
			Glu.gluCylinder(q,5,1,1,15,3);
			Glu.gluCylinder(q,1,1,4,10,1);
			Gl.glPushMatrix();
			Gl.glTranslated(0,0,5);
			Gl.glScaled(1,1,.5);
			Glu.gluSphere(q,4,10,10);
			Gl.glPopMatrix();
			Gl.glPopMatrix();
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
			Glu.gluDeleteQuadric(q);

			if (cullFace!=Gl.GL_FALSE) 
				Gl.glEnable(Gl.GL_CULL_FACE); 
		}

		public double DistanceTo(Point3D other) 
		{
			return (other - position - (new Point3D(width*Math.Cos(Angle*Math.PI/180),height,width*Math.Sin(Angle*Math.PI/180))).Scaled(.5)).Norm;
		}
		public bool HasActionFor(char c) 
		{
			return (c==' ' && !this.SoloMarco);
		}

		protected DateTime inicio;
		protected double aperturaActual;
		protected double velocidad=60; //Grados por segundo
		protected bool opening=false;
		protected bool closing=false;

		public void Act (char c) 
		{
			double elapsed = DateTime.Now.Subtract(this.inicio).TotalSeconds;
			if (elapsed < this.Apertura/velocidad) return;
			if (HasActionFor(c) && this.Apertura!=0) 
			{
				this.inicio=DateTime.Now;
				if (this.aperturaActual==this.aperturaActual) //Hay que cerrar
				{
					closing=true;
					opening=false;
				}
				if (this.aperturaActual==0) //Hay que abrir
				{
					closing=false;
					opening=true;
				}
			}
			if (HasActionFor(c) && this.Apertura==0) 
			{
				GlUtils.Beep();
			}
		}
		public override void FindTargetsFor(char c, ArrayList result) 
		{
			if (this.HasActionFor(c))
				result.Add(this);
		}
	}
}
