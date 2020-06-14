using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for MesetaConFregadero.
	/// </summary>
	public class MesetaConFregadero:Plantilla
	{

		protected override void setInitialParams(object[] InitialParams) 
		{
			yInc=0;
			height=100;
			alto=height/100;
		}
		public MesetaConFregadero(Point3D center, double angle): base(center,angle)
		{
			this.customRendering=true;
		}

		double largo = 1.97 , alto, ancho = 0.54 , anchoF = 0.15 , anchoM = 0.05 ;
		double lead=1.7;
		
		protected double distance=0;
		public override void Prepare (Avatar observer) 
		{
			distance = (observer.Origin-this.center-new Point3D(largo*100/2,alto*100,-ancho*100/2)).Norm;
		}

		protected override void customRenderer() 
		{

			Gl.glCallList(this.idObject);

			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("AZULEJO"));
			Gl.glColor3d(0.1,0.15,.1);

			double d = (int)this.distance/50;
			Gl.glBegin(Gl.GL_QUADS);

			//pared
			
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,0);Gl.glVertex3d(0,alto*100,-ancho*100+d);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(12,0);Gl.glVertex3d(largo*100,alto*100,-ancho*100+d);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(12,5);Gl.glVertex3d(largo*100,(alto+0.5)*100,-ancho*100+d);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,5);Gl.glVertex3d(0,(alto+0.5)*100,-ancho*100+d);

			//pared derecha
			
			Gl.glNormal3d(-1,0,0);Gl.glTexCoord2d(0,0);Gl.glVertex3d(largo*100-d,alto*100,-ancho*100);
			Gl.glNormal3d(-1,0,0);Gl.glTexCoord2d(5,0);Gl.glVertex3d(largo*100-d,alto*100,0);
			Gl.glNormal3d(-1,0,0);Gl.glTexCoord2d(5,5);Gl.glVertex3d(largo*100-d,(alto+0.5)*100,0);
			Gl.glNormal3d(-1,0,0);Gl.glTexCoord2d(0,5);Gl.glVertex3d(largo*100-d,(alto+0.5)*100,-ancho*100);

			//pared izquierda

			Gl.glNormal3d(1,0,0);Gl.glTexCoord2d(0,5);Gl.glVertex3d(d,(alto+0.5)*100,-ancho*100);
			Gl.glNormal3d(1,0,0);Gl.glTexCoord2d(5,5);Gl.glVertex3d(d,(alto+0.5)*100,0);
			Gl.glNormal3d(1,0,0);Gl.glTexCoord2d(5,0);Gl.glVertex3d(d,alto*100,0);
			Gl.glNormal3d(1,0,0);Gl.glTexCoord2d(0,0);Gl.glVertex3d(d,alto*100,-ancho*100);

			if (lead>0) 
			{
				Gl.glNormal3d(1,0,0);Gl.glTexCoord2d(0,6);Gl.glVertex3d(d,(alto)*100,0);
				Gl.glNormal3d(1,0,0);Gl.glTexCoord2d(lead*5,6);Gl.glVertex3d(d,(alto)*100,lead*100);
				Gl.glNormal3d(1,0,0);Gl.glTexCoord2d(lead*5,0);Gl.glVertex3d(d,0,lead*100);
				Gl.glNormal3d(1,0,0);Gl.glTexCoord2d(0,0);Gl.glVertex3d(d,0,0);
			}

			Gl.glEnd();
		}
		protected override void Particular() 
	 	{
			double n1 = 1 , n2 = 0.7 , n3 = 0.57 ; 	
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("AZULEJO"));

			Gl.glBegin(Gl.GL_QUADS);

			//lateral izquierdo
			//Gl.glColor3d(0.95,0.95,1);
			Gl.glColor3d(0.1,0.15,.1);
			Gl.glNormal3d(-n3,-n3,n3);Gl.glTexCoord2d(0,0);Gl.glVertex3d(0,0,0);
			Gl.glNormal3d(-n3,n3,n3);Gl.glTexCoord2d(6,0);Gl.glVertex3d(0,alto*100,0);
			Gl.glNormal3d(-n3,n3,-n3);Gl.glTexCoord2d(6,5);Gl.glVertex3d(0,alto*100,-ancho*100);
			Gl.glNormal3d(-n3,-n3,-n3);Gl.glTexCoord2d(0,5);Gl.glVertex3d(0,0,-ancho*100);

			//lateral derecho
			
			Gl.glNormal3d(n3,-n3,n3);Gl.glTexCoord2d(0,0);Gl.glVertex3d(largo*100,0,0);
			Gl.glNormal3d(n3,-n3,-n3);Gl.glTexCoord2d(5,0);Gl.glVertex3d(largo*100,0,-ancho*100);
			Gl.glNormal3d(n3,n3,-n3);Gl.glTexCoord2d(5,6);Gl.glVertex3d(largo*100,alto*100,-ancho*100);
			Gl.glNormal3d(n3,n3,n3);Gl.glTexCoord2d(0,6);Gl.glVertex3d(largo*100,alto*100,0);

			//abajo
			Gl.glNormal3d(-n3,-n3,n3);Gl.glTexCoord2d(0,0);Gl.glVertex3d(0,0,0);
			Gl.glNormal3d(n3,-n3,n3);Gl.glTexCoord2d(1,0);Gl.glVertex3d(largo*100,0,0);
			Gl.glNormal3d(n3,-n3,-n3);Gl.glTexCoord2d(1,1);Gl.glVertex3d(largo*100,0,-ancho*100);
			Gl.glNormal3d(-n3,-n3,-n3);Gl.glTexCoord2d(0,1);Gl.glVertex3d(0,0,-ancho*100);

			//frente izquierda
			
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,0);Gl.glVertex3d(0,0,0);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(1,0);Gl.glVertex3d(anchoF*100,0,0);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(1,6);Gl.glVertex3d(anchoF*100,alto*100,0);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,6);Gl.glVertex3d(0,alto*100,0);

			//frente derecha
			
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,0);Gl.glVertex3d((largo-anchoF)*100,0,0);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(1,0);Gl.glVertex3d(largo*100,0,0);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(1,6);Gl.glVertex3d(largo*100,alto*100,0);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,6);Gl.glVertex3d((largo-anchoF)*100,alto*100,0);

			//frente arriba	
			
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,0);Gl.glVertex3d(anchoF*100,(alto-anchoF)*100,0);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(10,0);Gl.glVertex3d((largo-anchoF)*100,(alto-anchoF)*100,0);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(10,1);Gl.glVertex3d((largo-anchoF)*100,alto*100,0);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,1);Gl.glVertex3d(anchoF*100,alto*100,0);

			//frente abajo
			
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,0);Gl.glVertex3d(anchoF*100,0,0);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(10,0);Gl.glVertex3d((largo-anchoF)*100,0,0);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(10,1);Gl.glVertex3d((largo-anchoF)*100,anchoF*100,0);
			Gl.glNormal3d(0,0,1);Gl.glTexCoord2d(0,1);Gl.glVertex3d(anchoF*100,anchoF*100,0);

			Gl.glEnd();
                  
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("WOOD2"));

			Gl.glBegin(Gl.GL_QUADS);

			//marco izquierdo
			Gl.glColor3d(0.8,0.8,0.8);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,0);Gl.glVertex3d(anchoF*100,anchoF*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(1,0);Gl.glVertex3d((anchoF+anchoM)*100,anchoF*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(1,1);Gl.glVertex3d((anchoF+anchoM)*100,(alto-anchoF)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,1);Gl.glVertex3d(anchoF*100,(alto-anchoF)*100,0);

			//marco derecho
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,0);Gl.glVertex3d((largo-anchoF-anchoM)*100,anchoF*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(1,0);Gl.glVertex3d((largo-anchoF)*100,anchoF*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(1,1);Gl.glVertex3d((largo-anchoF)*100,(alto-anchoF)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,1);Gl.glVertex3d((largo-anchoF-anchoM)*100,(alto-anchoF)*100,0);

			//marco arriba
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,0);Gl.glVertex3d((anchoF+anchoM)*100,(alto-anchoF-anchoM)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,2);Gl.glVertex3d((largo-anchoF-anchoM)*100,(alto-anchoF-anchoM)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(1,2);Gl.glVertex3d((largo-anchoF-anchoM)*100,(alto-anchoF)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(1,0);Gl.glVertex3d((anchoF+anchoM)*100,(alto-anchoF)*100,0);

			//marco abajo
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,0);Gl.glVertex3d((anchoF+anchoM)*100,anchoF*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,2);Gl.glVertex3d((largo-anchoF-anchoM)*100,anchoF*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(1,2);Gl.glVertex3d((largo-anchoF-anchoM)*100,(anchoF+anchoM)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(1,0);Gl.glVertex3d((anchoF+anchoM)*100,(anchoF+anchoM)*100,0);

			//marco medio
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,0);Gl.glVertex3d((largo/2-anchoM/2)*100,(anchoF+anchoM)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(1,0);Gl.glVertex3d((largo/2+anchoM/2)*100,(anchoF+anchoM)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(1,1);Gl.glVertex3d((largo/2+anchoM/2)*100,(alto-anchoF-anchoM)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,1);Gl.glVertex3d((largo/2-anchoM/2)*100,(alto-anchoF-anchoM)*100,0);

			Gl.glColor3d(0.85,0.85,0.85);
			//puerta izquierda
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,0);Gl.glVertex3d((anchoF+anchoM)*100,(anchoF+anchoM)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(2,0);Gl.glVertex3d((largo/2-anchoM/2)*100,(anchoF+anchoM)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(2,1);Gl.glVertex3d((largo/2-anchoM/2)*100,(alto-anchoF-anchoM)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,1);Gl.glVertex3d((anchoF+anchoM)*100,(alto-anchoF-anchoM)*100,0);

			//puerta derecha
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,0);Gl.glVertex3d((largo/2+anchoM/2)*100,(anchoF+anchoM)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(2,0);Gl.glVertex3d((largo-anchoF-anchoM)*100,(anchoF+anchoM)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(2,1);Gl.glVertex3d((largo-anchoF-anchoM)*100,(alto-anchoF-anchoM)*100,0);
			Gl.glNormal3d(0,0,n1);Gl.glTexCoord2d(0,1);Gl.glVertex3d((largo/2+anchoM/2)*100,(alto-anchoF-anchoM)*100,0);

			Gl.glEnd();

			//Knobs

			Gl.glDisable(Gl.GL_TEXTURE_2D);
			Gl.glColor3d(0.7,0.7,0.7);

			//izquierda
			Gl.glBegin(Gl.GL_QUAD_STRIP);

			Gl.glNormal3d(-n3,-n3,n3);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.05)*100,0.03*100);
			Gl.glNormal3d(-n3,n3,n3);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.05)*100,0.03*100);

			Gl.glNormal3d(n3,-n3,n3);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2-0.05)*100,0.03*100);
			Gl.glNormal3d(n3,n3,n3);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2+0.05)*100,0.03*100);

			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2+0.05)*100,0.02*100);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.05)*100,0.02*100);

			Gl.glNormal3d(-n3,-n3,n3);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.05)*100,0.03*100);
			Gl.glNormal3d(-n3,n3,n3);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.05)*100,0.03*100);

			Gl.glEnd();

//			Gl.glColor3d(0.1,0.15,.1);
			Gl.glBegin(Gl.GL_QUADS);

			Gl.glNormal3d(-n3,n3,n3);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.05)*100,0.03*100);
			Gl.glNormal3d(n3,n3,n3);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2+0.05)*100,0.03*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2+0.05)*100,0.02*100);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.05)*100,0.02*100);

			Gl.glNormal3d(-n3,-n3,n3);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.05)*100,0.03*100);
			Gl.glNormal3d(n3,-n3,n3);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2-0.05)*100,0.03*100);
			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.05)*100,0.02*100);

			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUAD_STRIP);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.04)*100,0.02*100);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.05)*100,0.02*100);

			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2+0.04)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2+0.05)*100,0.02*100);

			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2+0.04)*100,0);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2+0.05)*100,0);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.04)*100,0);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.05)*100,0);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.04)*100,0.02*100);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.05)*100,0.02*100);

			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUADS);

			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.05)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2+0.05)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2+0.05)*100,0);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.05)*100,0);
				
			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.04)*100,0.02*100);
			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2+0.04)*100,0.02*100);
			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2+0.04)*100,0);
			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2+0.04)*100,0);

			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUAD_STRIP);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.04)*100,0.02*100);

			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2-0.04)*100,0.02*100);

			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2-0.05)*100,0);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2-0.04)*100,0);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.05)*100,0);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.04)*100,0);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.04)*100,0.02*100);

			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUADS);

			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.04)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2-0.04)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2-0.04)*100,0);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.04)*100,0);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.04)*100,(alto/2-0.05)*100,0);
			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2-anchoM/2-0.05)*100,(alto/2-0.05)*100,0);

			Gl.glEnd();

			//derecha
			Gl.glBegin(Gl.GL_QUAD_STRIP);

			Gl.glNormal3d(-n3,-n3,n3);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.05)*100,0.03*100);
			Gl.glNormal3d(-n3,n3,n3);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.05)*100,0.03*100);

			Gl.glNormal3d(n3,-n3,n3);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2-0.05)*100,0.03*100);
			Gl.glNormal3d(n3,n3,n3);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2+0.05)*100,0.03*100);

			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2+0.05)*100,0.02*100);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.05)*100,0.02*100);

			Gl.glNormal3d(-n3,-n3,n3);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.05)*100,0.03*100);
			Gl.glNormal3d(-n3,n3,n3);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.05)*100,0.03*100);

			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUADS);

			Gl.glNormal3d(-n3,n3,n3);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.05)*100,0.03*100);
			Gl.glNormal3d(n3,n3,n3);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2+0.05)*100,0.03*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2+0.05)*100,0.02*100);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.05)*100,0.02*100);

			Gl.glNormal3d(-n3,-n3,n3);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.05)*100,0.03*100);
			Gl.glNormal3d(n3,-n3,n3);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2-0.05)*100,0.03*100);
			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.05)*100,0.02*100);

			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUAD_STRIP);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.04)*100,0.02*100);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.05)*100,0.02*100);

			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2+0.04)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2+0.05)*100,0.02*100);

			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2+0.04)*100,0);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2+0.05)*100,0);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.04)*100,0);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.05)*100,0);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.04)*100,0.02*100);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.05)*100,0.02*100);

			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUADS);

			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.05)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2+0.05)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2+0.05)*100,0);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.05)*100,0);
				
			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.04)*100,0.02*100);
			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2+0.04)*100,0.02*100);
			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2+0.04)*100,0);
			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2+0.04)*100,0);

			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUAD_STRIP);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.04)*100,0.02*100);

			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2-0.04)*100,0.02*100);

			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2-0.05)*100,0);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2-0.04)*100,0);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.05)*100,0);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.04)*100,0);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.04)*100,0.02*100);

			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUADS);

			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.04)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2-0.04)*100,0.02*100);
			Gl.glNormal3d(n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2-0.04)*100,0);
			Gl.glNormal3d(-n2,n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.04)*100,0);

			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2-0.05)*100,0.02*100);
			Gl.glNormal3d(n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.05)*100,(alto/2-0.05)*100,0);
			Gl.glNormal3d(-n2,-n2,0);Gl.glVertex3d((largo/2+anchoM/2+0.04)*100,(alto/2-0.05)*100,0);

			Gl.glEnd();
			
			Gl.glColor3d(0.1,0.15,.1);
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("AZULEJO"));

			Gl.glBegin(Gl.GL_QUADS);
			

			// meseta derecha
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(0,0);Gl.glVertex3d(0,alto*100,0);
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(6,0);Gl.glVertex3d(largo/2*100,alto*100,0);
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(6,5);Gl.glVertex3d(largo/2*100,alto*100,-ancho*100);
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(0,5);Gl.glVertex3d(0,alto*100,-ancho*100);


			// meseta izquierda
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(0,0);Gl.glVertex3d((largo-anchoF)*100,alto*100,0);
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(1,0);Gl.glVertex3d(largo*100,alto*100,0);
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(1,5);Gl.glVertex3d(largo*100,alto*100,-ancho*100);
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(0,5);Gl.glVertex3d((largo-anchoF)*100,alto*100,-ancho*100);

			// meseta delante
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(0,0);Gl.glVertex3d(largo/2*100,alto*100,0);
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(5,0);Gl.glVertex3d((largo-anchoF)*100,alto*100,0);
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(5,1);Gl.glVertex3d((largo-anchoF)*100,alto*100,-0.1*100);
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(0,1);Gl.glVertex3d(largo/2*100,alto*100,-0.1*100);

			// meseta detras
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(0,0);Gl.glVertex3d(largo/2*100,alto*100,(-ancho+0.1)*100);
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(5,0);Gl.glVertex3d((largo-anchoF)*100,alto*100,(-ancho+0.1)*100);
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(5,1);Gl.glVertex3d((largo-anchoF)*100,alto*100,-ancho*100);
			Gl.glNormal3d(0,1,0);Gl.glTexCoord2d(0,1);Gl.glVertex3d(largo/2*100,alto*100,-ancho*100);

			Gl.glEnd();

			Gl.glColor3d(1,1,1);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,GlUtils.Texture("ALUMINIO"));
				
			Gl.glBegin(Gl.GL_QUADS);

			//fregadero izquierda
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(0,0);Gl.glVertex3d(largo/2*100,alto*100,-0.1*100);
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(1,0);Gl.glVertex3d((largo/2+0.05)*100,alto*100,-0.1*100);
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(1,1);Gl.glVertex3d((largo/2+0.05)*100,alto*100,(-ancho+0.1)*100);
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(0,1);Gl.glVertex3d(largo/2*100,alto*100,(-ancho+0.1)*100);

			//fregadero derecha
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(0,0);Gl.glVertex3d((largo-anchoF-0.05)*100,alto*100,-0.1*100);
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(1,0);Gl.glVertex3d((largo-anchoF)*100,alto*100,-0.1*100);
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(1,1);Gl.glVertex3d((largo-anchoF)*100,alto*100,(-ancho+0.1)*100);
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(0,1);Gl.glVertex3d((largo-anchoF-0.05)*100,alto*100,(-ancho+0.1)*100);

			//fregadero detras
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(0,0);Gl.glVertex3d((largo/2+0.05)*100,alto*100,(-ancho+0.1+0.05)*100);
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(1,0);Gl.glVertex3d((largo-anchoF-0.05)*100,alto*100,(-ancho+0.1+0.05)*100);
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(1,1);Gl.glVertex3d((largo-anchoF-0.05)*100,alto*100,(-ancho+0.1)*100);
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(0,1);Gl.glVertex3d((largo/2+0.05)*100,alto*100,(-ancho+0.1)*100);

			//fregadero delante
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(0,0);Gl.glVertex3d((largo/2+0.05)*100,alto*100,-0.1*100);
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(1,0);Gl.glVertex3d((largo-anchoF-0.05)*100,alto*100,-0.1*100);
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(1,1);Gl.glVertex3d((largo-anchoF-0.05)*100,alto*100,(-0.1-0.05)*100);
			Gl.glNormal3d(0,n1,0);Gl.glTexCoord2d(0,1);Gl.glVertex3d((largo/2+0.05)*100,alto*100,(-0.1-0.05)*100);

			Gl.glEnd();

			//fregadero
			Gl.glColor3d(0.95,0.95,0.95);
			Gl.glBegin(Gl.GL_QUAD_STRIP);

			Gl.glNormal3d(n3,n3,-n3);Gl.glTexCoord2d(0,0);Gl.glVertex3d((largo/2+0.05)*100,(alto-anchoF)*100,(-0.1-0.05)*100);
			Gl.glNormal3d(n3,n3,-n3);Gl.glTexCoord2d(0,1);Gl.glVertex3d((largo/2+0.05)*100,alto*100,(-0.1-0.05)*100);
				
			Gl.glNormal3d(-n3,n3,-n3);Gl.glTexCoord2d(0.4,0);Gl.glVertex3d((largo-anchoF-0.05)*100,(alto-anchoF)*100,(-0.1-0.05)*100);
			Gl.glNormal3d(-n3,n3,-n3);Gl.glTexCoord2d(0.4,1);Gl.glVertex3d((largo-anchoF-0.05)*100,alto*100,(-0.1-0.05)*100);

			Gl.glNormal3d(-n3,n3,n3);Gl.glTexCoord2d(0.5,0);Gl.glVertex3d((largo-anchoF-0.05)*100,(alto-anchoF)*100,(-ancho+0.1+0.05)*100);
			Gl.glNormal3d(-n3,n3,n3);Gl.glTexCoord2d(0.5,1);Gl.glVertex3d((largo-anchoF-0.05)*100,alto*100,(-ancho+0.1+0.05)*100);

			Gl.glNormal3d(n3,n3,n3);Gl.glTexCoord2d(0.9,0);Gl.glVertex3d((largo/2+0.05)*100,(alto-anchoF)*100,(-ancho+0.1+0.05)*100);
			Gl.glNormal3d(n3,n3,n3);Gl.glTexCoord2d(0.9,1);Gl.glVertex3d((largo/2+0.05)*100,alto*100,(-ancho+0.1+0.05)*100);

			Gl.glNormal3d(n3,n3,-n3);Gl.glTexCoord2d(1,0);Gl.glVertex3d((largo/2+0.05)*100,(alto-anchoF)*100,(-0.1-0.05)*100);
			Gl.glNormal3d(n3,n3,-n3);Gl.glTexCoord2d(1,1);Gl.glVertex3d((largo/2+0.05)*100,alto*100,(-0.1-0.05)*100);
				
			Gl.glEnd();

			//fondo del fregadero
			Gl.glColor3d(0.9,0.9,0.9);
			Gl.glBegin(Gl.GL_QUADS);

			Gl.glNormal3d(n3,n3,-n3);Gl.glTexCoord2d(0,0);Gl.glVertex3d((largo/2+0.05)*100,(alto-anchoF)*100,(-0.1-0.05)*100);
			Gl.glNormal3d(-n3,n3,-n3);Gl.glTexCoord2d(5,0);Gl.glVertex3d((largo-anchoF-0.05)*100,(alto-anchoF)*100,(-0.1-0.05)*100);
			Gl.glNormal3d(-n3,n3,n3);Gl.glTexCoord2d(5,1);Gl.glVertex3d((largo-anchoF-0.05)*100,(alto-anchoF)*100,(-ancho+0.1+0.05)*100);
			Gl.glNormal3d(n3,n3,n3);Gl.glTexCoord2d(0,1);Gl.glVertex3d((largo/2+0.05)*100,(alto-anchoF)*100,(-ancho+0.1+0.05)*100);

			Gl.glEnd();
			Gl.glPushMatrix();
			double tph = 10;
			Gl.glTranslated(largo*100/4,Height+tph*.8,-ancho*100/2);
			Gl.glRotated(30,0,1,0);
			Glut.glutSolidTeapot(10);
			Gl.glPopMatrix();

		}
	}
}
