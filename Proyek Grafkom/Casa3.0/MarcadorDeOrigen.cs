using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for MarcadorDeOrigen.
	/// </summary>
	public class MarcadorDeOrigen:GlObject
	{
		public MarcadorDeOrigen()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public override void Render() 
		{
			Gl.glColor3d(1,0,0);
			Glu.GLUquadric q = Glu.gluNewQuadric();
			Glu.gluSphere(q,2,20,20);
		}
	}
}
