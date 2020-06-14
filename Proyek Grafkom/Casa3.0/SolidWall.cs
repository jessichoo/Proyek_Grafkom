#define halfwalls
//#define useCulFace
using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for SolidWall.
	/// </summary>
	public class SolidWall : Wall
	{
		protected double[] wallColor=new double[]{1f,1f,1f};

		protected int texture;
		protected bool showFront=true;
		protected bool showBack=true;
		protected bool showFrom=true;
		protected bool showTo=true;
		protected bool showTop=true;

		public SolidWall(Point3D from, Point3D to, double bottom, double height):base(from,to,bottom,height)
		{
			this.calcPoints();
			texture = GlUtils.Texture("WALL");
			width=(to-from).Norm;
		}
		protected bool closeUp=false;
		public virtual void CloseUp(bool close) 
		{
			this.closeUp=close;
		}
		public override Point3D before 
		{
			set 
			{
//				this.closeFrom = (value==null);
				if (value==null)
					this.dirFrom = this.planeRot(to-from,90).Normalized;
				else 
				{
					Point3D p1 = this.planeRot(from-value,90).Normalized;
					Point3D p2 = this.planeRot(to-from,90).Normalized;
					this.dirFrom=(p1+p2).Normalized;
					this.dirFrom=this.dirFrom.Scaled(1/(p2.ScalarProduct(dirFrom)));
				}
				this.calcPoints();
			}
		}
		public override Point3D after
		{
			set 
			{
//				this.closeTo = (value==null);
				if (value==null)
					this.dirTo = this.planeRot(to-from,90).Normalized;
				else 
				{
					Point3D p1 = this.planeRot(value-to,90).Normalized;
					Point3D p2 = this.planeRot(to-from,90).Normalized;
					this.dirTo=(p1+p2).Normalized;
					this.dirTo=this.dirTo.Scaled(1/(p2.ScalarProduct(dirTo)));
				}
				this.calcPoints();
			}
		}

		public override void Prepare(Avatar observer) 
		{
#if halfwalls && ! useCulFace
			showFront=true;
			showBack=true;
			showFrom=true;
			showTo=true;
			showTop=true;
			Point3D position = (observer.Origin-from).Normalized;
			Point3D direction = (to-from).Normalized;
			if (direction.ScalarProduct(position)>0)
				this.showFrom=false;
			if ((observer.Origin-to).ScalarProduct(-direction)>0)
				this.showTo=false;
			double rot = this.planeRot(direction,90).Normalized.ScalarProduct(position.Normalized);
			if (rot>0)
				this.showBack=false;
			if (-rot>0)
				this.showFront=false;
			if (observer.Origin.Y<this.bottom+this.height)
				this.showTop=false;
#endif
		}
		

		protected Point3D frontDownLeft;
		protected Point3D frontDownRight;
		protected Point3D frontUpLeft;
		protected Point3D frontUpRight;
		protected Point3D backDownLeft;
		protected Point3D backDownRight;
		protected Point3D backUpLeft;
		protected Point3D backUpRight;
		protected void calcPoints() 
		{
			this.frontDownLeft = from+dirFrom.Scaled(Deep*baseDeep)+new Point3D(0,this.bottom,0);
			this.frontDownRight = to+dirTo.Scaled(Deep*baseDeep)+new Point3D(0,this.bottom,0);
			this.frontUpRight = to+dirTo.Scaled(Deep*baseDeep)+new Point3D(0,this.bottom+this.height,0);
			this.frontUpLeft = from+dirFrom.Scaled(Deep*baseDeep)+new Point3D(0,this.bottom+this.height,0);

			this.backDownRight = to-dirTo.Scaled(Deep*baseDeep)+new Point3D(0,this.bottom,0);
			this.backDownLeft  = from-dirFrom.Scaled(Deep*baseDeep)+new Point3D(0,this.bottom,0);
			this.backUpLeft = from-dirFrom.Scaled(Deep*baseDeep)+new Point3D(0,this.bottom+this.height,0);
			this.backUpRight = to-dirTo.Scaled(Deep*baseDeep)+new Point3D(0,this.bottom+this.height,0);
		}
		public override void Render() 
		{
#if halfwalls && useCulFace
			//Gl.glEnable(Gl.GL_CULL_FACE);
#endif
			Gl.glColor3d(1,1,1);
			//Gl.glColor3dv(this.wallColor);;
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,texture);
			Gl.glBegin(Gl.GL_QUADS);
			if (showFront) 
			{
				Gl.glNormal3dv(this.frontSideNormal.Coords);
				//Gl.glNormal3dv((this.frontSideNormal+new Point3D(0,1,0)).Normalized.Coords);
				Gl.glTexCoord2d(0,0);
				Gl.glVertex3dv(this.frontDownLeft.Coords);
				Gl.glTexCoord2d(width/100,0);
				Gl.glVertex3dv(this.frontDownRight.Coords);
				//Gl.glNormal3dv((this.frontSideNormal+new Point3D(0,-1,0)).Normalized.Coords);
				Gl.glTexCoord2d(width/100,height/100);
				Gl.glVertex3dv(this.frontUpRight.Coords);
				Gl.glTexCoord2d(0,height/100);
				Gl.glVertex3dv(this.frontUpLeft.Coords);
			}
			if (showBack) 
			{
				Gl.glNormal3dv((-this.frontSideNormal).Coords);
				//Gl.glNormal3dv((-this.frontSideNormal+new Point3D(0,1,0)).Normalized.Coords);
				Gl.glTexCoord2d(width/100,0);
				Gl.glVertex3dv(this.backDownRight.Coords);
				Gl.glTexCoord2d(0,0);
				Gl.glVertex3dv(this.backDownLeft.Coords);
				//Gl.glNormal3dv((-this.frontSideNormal+new Point3D(0,-1,0)).Normalized.Coords);
				Gl.glTexCoord2d(0,height/100);
				Gl.glVertex3dv(this.backUpLeft.Coords);
				Gl.glTexCoord2d(width/100,height/100);
				Gl.glVertex3dv(this.backUpRight.Coords);
			}

			if (showTo && this.closeTo) 
			{
				Gl.glNormal3dv(this.endSideNormal.Coords);
				Gl.glTexCoord2d(height/100,0);
				Gl.glVertex3dv(this.backUpRight.Coords);
				Gl.glTexCoord2d(height/100,Deep*baseDeep*2/100);
				Gl.glVertex3dv(this.frontUpRight.Coords);
				Gl.glTexCoord2d(0,Deep*baseDeep*2/100);
				Gl.glVertex3dv(this.frontDownRight.Coords);
				Gl.glTexCoord2d(0,0);
				Gl.glVertex3dv(this.backDownRight.Coords);
			}
			
			if (showFrom && this.closeFrom) 
			{
				Gl.glNormal3dv((-this.endSideNormal).Coords);
				Gl.glTexCoord2d(0,Deep*baseDeep*2/100);
				Gl.glVertex3dv(this.backDownLeft.Coords);
				Gl.glTexCoord2d(height/100,Deep*baseDeep*2/100);
				Gl.glVertex3dv(this.frontDownLeft.Coords);
				Gl.glTexCoord2d(height/100,0);
				Gl.glVertex3dv(this.frontUpLeft.Coords);
				Gl.glTexCoord2d(0,0);
				Gl.glVertex3dv(this.backUpLeft.Coords);
			}
			Gl.glEnd();
			if (closeUp && showTop) 
			{
				Gl.glBegin(Gl.GL_POLYGON);
				Gl.glNormal3d(0,1,0);
				Gl.glTexCoord2d(width/100,0);
				Gl.glVertex3dv(this.frontUpLeft.Coords);
				Gl.glTexCoord2d(width/100,height/100);
				Gl.glVertex3dv(this.frontUpRight.Coords);
				Gl.glTexCoord2d(0,height/100);
				Gl.glVertex3dv(this.backUpRight.Coords);
				Gl.glTexCoord2d(0,0);
				Gl.glVertex3dv(this.backUpLeft.Coords);
				Gl.glEnd();
			}
			Gl.glBindTexture(Gl.GL_TEXTURE_2D,0);
#if halfwalls && useCulFace
			//Gl.glDisable(Gl.GL_CULL_FACE);
#endif
		}

	}
}
