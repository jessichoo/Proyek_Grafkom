//#define drawCenter
using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Plantilla.
	/// </summary>
	public abstract class Plantilla : GlObject
	{
		protected int idObject = -1;
		protected Point3D center;
		protected double angle;
		protected double height=0;
		public virtual double Height {get { return height;}}
		protected double yInc = double.MinValue;
		protected bool customRendering = false;
		public virtual double YInc {get { return (yInc==double.MinValue)?Height/2:yInc;}}

		protected virtual void setInitialParams(object[] InitialParams) {;}
		public Plantilla(Point3D center , double angle, params object[] InitialParams)
		{
			this.angle = angle;
			this.center=center;
			this.setInitialParams(InitialParams);
			if (idObject==-1)
				creaObject();
		}

		protected virtual void creaObject() 
		{
			idObject = Gl.glGenLists(1);
			Gl.glNewList(idObject, Gl.GL_COMPILE);
			Particular();
			Gl.glEndList();
		}

		protected bool canCullFace=false;
		private int cullFace=Gl.GL_FALSE;
		public override void Render() 
		{
			if (!canCullFace) 
			{
				Gl.glGetBooleanv(Gl.GL_CULL_FACE,out cullFace);
				Gl.glDisable(Gl.GL_CULL_FACE);
			}
			#region Pintar una gran esfera en el 0,0 del objeto
#if drawCenter
			Gl.glPushMatrix();
			Gl.glTranslated(center.X,center.Y,center.Z);
			Glu.GLUquadric q = Glu.gluNewQuadric();
			Gl.glColor3d(1,0,0);
			Glu.gluSphere(q,2,5,5);
			Glu.gluDeleteQuadric(q);
			Gl.glPopMatrix();
#endif
			#endregion
			Gl.glPushMatrix();
			Gl.glTranslated(center.X,center.Y+YInc,center.Z);
			Gl.glRotated(angle,0,1,0);
			if (this.customRendering)
				this.customRenderer();
			else
				Gl.glCallList(idObject);
			Gl.glPopMatrix();
			if (!canCullFace && cullFace!=Gl.GL_FALSE) 
			{
                Gl.glEnable(Gl.GL_CULL_FACE);                				
			}
		}

		protected abstract void Particular();
		protected virtual void customRenderer() {;}

	}
}
