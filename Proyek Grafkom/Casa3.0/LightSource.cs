using System;
using Tao.OpenGl;
using System.Collections;

namespace TareaGL
{
	/// <summary>
	/// Summary description for LightSource.
	/// </summary>
	public class LightSource : GlObject, InteractiveObject
	{
		public LightSource()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		Random r = new Random();
		Point3D position=new Point3D(50,350,150);
		public override void Prepare (Avatar observer) 
		{
			this.position=observer.Origin-observer.Direction.Normalized.Scaled(50);
		}
		public override void Render() 
		{
			Gl.glDisable(Gl.GL_LIGHT0);
			float[] ambience = {.3f, .3f, .3f, 1f};		// The color of the light in the world
			float[] diffuse = {1.0f, 1f, 1f, 1.0f};
			Gl.glLightfv( Gl.GL_LIGHT0, Gl.GL_AMBIENT,  ambience );		// Set our ambience values (Default color without direct light)
			Gl.glLightfv( Gl.GL_LIGHT0, Gl.GL_DIFFUSE,  diffuse );		// Set our diffuse color (The light color)
			//			Gl.glLightfv( Gl.GL_LIGHT0, Gl.GL_SPECULAR,  new float[]{1f, 1f, 1f, 1f} );		// Set our ambience values (Default color without direct light)
			Gl.glLightfv( Gl.GL_LIGHT0, Gl.GL_POSITION,new float[]{(float)position.X,(float)position.Y,(float)position.Z,1});
//			Gl.glLightfv( Gl.GL_LIGHT0, Gl.GL_POSITION,new float[]{0,0,0,1});
			if (on) Gl.glEnable(  Gl.GL_LIGHT0   );
			else Gl.glDisable(  Gl.GL_LIGHT0   );
		}

		public double DistanceTo(Point3D other) 
		{
			return (position-other).Norm;
		}
		public bool HasActionFor(char c) 
		{
			return c=='l';
		}
		protected bool on = true;
		public void Act (char c) 
		{
			if (this.HasActionFor(c))
				on = ! on;
		}
	}
}
