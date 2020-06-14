using System;
using Tao.OpenGl;

namespace TareaGL
{
	/// <summary>
	/// Summary description for GlControl.
	/// </summary>
	public class GlControl
	{
		protected int height;
		protected int width;
		public int Height {get {return height;}}
		public int Width {get {return width;}}
		public GlControl(int Width, int Height)
		{
			//
			// TODO: Add constructor logic here
			//
			this.width=Width;
			this.height=Height;
			Glut.glutInit();
			Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE| Glut.GLUT_RGB|Glut.GLUT_DEPTH);
			Glut.glutInitWindowSize(Width,Height);
			Glut.glutInitWindowPosition(0,0);
			Glut.glutCreateWindow("Tarea OpenGL");
//			Glut.glutReshapeFunc(new Glut.ReshapeCallback(this.myReshape));
			Init();
		}

		protected void Init() 
		{
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Glu.gluPerspective(90,1,1,8000);
			Gl.glEnable(Gl.GL_DEPTH_TEST);
			Gl.glDepthFunc(Gl.GL_LEQUAL);
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			Gl.glEnable(Gl.GL_FRONT_AND_BACK);
			Gl.glEnable(Gl.GL_NORMALIZE);
//			float[] ambience = {.1f, .1f, .1f, 1f};		// The color of the light in the world
//			float[] diffuse = {1.0f, 1f, 1f, 1.0f};			// The color of the positioned light
			Gl.glShadeModel(Gl.GL_SMOOTH);
			Gl.glLightModeli(Gl.GL_LIGHT_MODEL_LOCAL_VIEWER,1);
//			Gl.glLightModeli(Gl.GL_LIGHT_MODEL_TWO_SIDE,1);

			Gl.glEnable(Gl.GL_CULL_FACE);
			Gl.glCullFace(Gl.GL_BACK);

			Gl.glEnable(  Gl.GL_LIGHTING );
			Gl.glEnable(Gl.GL_COLOR_MATERIAL);
			//Gl.glEnable(Gl.GL_BLEND);
			Gl.glBlendFunc(Gl.GL_SRC_ALPHA,Gl.GL_ONE_MINUS_SRC_ALPHA);
//			Glut.glutFullScreen();

		}

//		protected void myReshape(int w, int h)
//		{
//			Gl.glViewport(0, 0, w, h);
//			Gl.glMatrixMode(Gl.GL_PROJECTION);
//			Gl.glLoadIdentity();
//			if (w <= h) 
//				Gl.glOrtho (-1.5, 1.5, -1.5*(float)h/(float)w, 
//					1.5*(float)h/(float)w, -10.0, 10.0);
//			else 
//				Gl.glOrtho (-1.5*(float)w/(float)h, 
//					1.5*(float)w/(float)h, -1.5, 1.5, -10.0, 10.0);
//			Gl.glMatrixMode(Gl.GL_MODELVIEW);
//			Gl.glLoadIdentity();
//		}

	}
}
