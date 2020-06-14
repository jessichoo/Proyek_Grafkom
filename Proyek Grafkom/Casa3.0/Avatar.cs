using System;
using Tao.OpenGl;
using Platform.Windows;
using System.Collections;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Avatar.
	/// </summary>
	public class Avatar
	{
		protected bool ghostMode=false;
		public double StrafeSpeed=300; /* Velocidad en unidades por segundo */
        protected GlControl ViewPort;
		protected Point3D origin = new Point3D(103,100,700);
		protected Point3D center = new Point3D(179,100,318);
		protected Point3D up = new Point3D(0,1,0);
		protected Camera camera;

		public Camera Camera{ get { return camera;}}

		public Point3D Origin { get { return camera.Origin; }}
		public Point3D Direction { get { return camera.Direction; }}
		public Point3D Up { get {return camera.Up;}}

		protected GlObject World;
		public Avatar(GlControl ViewPort,GlObject World)
		{
			camera = new Camera(origin,center,up);
			this.World=World;
			this.ViewPort=ViewPort;
			Glut.glutKeyboardFunc(new Glut.KeyboardCallback(keymove));
			Glut.glutSpecialFunc(new Glut.SpecialCallback(specialKeyMove));
			Glut.glutSpecialUpFunc(new Glut.SpecialUpCallback(specialKeyUp));
			Glut.glutMouseFunc(new Glut.MouseCallback(this.MouseFunc));
		}
		public void Look() 
		{
			Update();
			RenderScene();
		}

		public void RenderScene() 
		{
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT|Gl.GL_DEPTH_BUFFER_BIT);
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();
			camera.Look();
			World.Render();
			Glut.glutSwapBuffers();
		}

		void keymove (byte b, int i, int j) 
		{
			char c = (char) b;
			switch (c) 
			{
				case (char)27: toggleUpdate(); break;
				case 'g': this.ghostMode=!ghostMode;break;
				case '+': this.StrafeSpeed*=1.5; break;
				case '-': this.StrafeSpeed/=1.5; break;
				default: 
					findTargetObject(c);
					break;
			}
		}
		protected ArrayList targets = new ArrayList();
		protected char lastChar;
		protected bool lastCharValid=false;
		protected void findTargetObject(char c) 
		{
			if (lastChar!=c || !lastCharValid) 
			{
				lastChar=c;
				lastCharValid=true;
				targets.Clear();
				this.World.FindTargetsFor(c,targets);
			}
			double minDist = double.MaxValue;
			InteractiveObject closer = null;
			foreach (InteractiveObject o in targets)
			{
				double d = o.DistanceTo(this.camera.Origin+this.camera.Direction.Normalized.Scaled(50));
				if (o.HasActionFor(c)&&d<minDist) 
				{
					minDist=d;
					closer=o;
				}
			}
			if (minDist<=130)
				closer.Act(c);
		}
		protected bool lightEnabled=true;

		protected bool PressedKeyDown=false;
		protected bool PressedKeyUp=false;
		protected bool PressedKeyLeft=false;
		protected bool PressedKeyRight=false;
		protected bool PressedKeyForward=false;
		protected bool PressedKeyBackward=false;
		void specialKeyMove (int b, int i, int j) 
		{
			switch (b) 
			{
				case Glut.GLUT_KEY_DOWN:
					PressedKeyDown=true;
					break;
				case Glut.GLUT_KEY_UP:
					PressedKeyUp=true;
					break;
				case Glut.GLUT_KEY_LEFT:
					PressedKeyLeft=true;
					break;
				case Glut.GLUT_KEY_RIGHT:
					PressedKeyRight=true;
					break;
				case Glut.GLUT_KEY_PAGE_DOWN:
					PressedKeyForward=true;
					break;
				case Glut.GLUT_KEY_PAGE_UP:
					PressedKeyBackward=true;
					break;
			}
		}
		void specialKeyUp (int b, int i, int j) 
		{
			switch (b) 
			{
				case Glut.GLUT_KEY_DOWN:
					PressedKeyDown=false;
					break;
				case Glut.GLUT_KEY_UP:
					PressedKeyUp=false;
					break;
				case Glut.GLUT_KEY_LEFT:
					PressedKeyLeft=false;
					break;
				case Glut.GLUT_KEY_RIGHT:
					PressedKeyRight=false;
					break;
				case Glut.GLUT_KEY_PAGE_DOWN:
					PressedKeyForward=false;
					break;
				case Glut.GLUT_KEY_PAGE_UP:
					PressedKeyBackward=false;
					break;
			}
			Console.WriteLine("x: " + origin.X + "y: " + origin.Y);
			
		}


		protected int mousex=-1;
		protected int mousey=-1;

		private bool started =false;

		protected bool lButtonDown=false;
		protected bool rButtonDown=false;
		protected Point3D strafeDir = new Point3D(0,0,0);
		protected void MouseFunc(int button, int state, int x, int y) 
		{

			switch (button) 
			{
				case Glut.GLUT_LEFT_BUTTON: 
					lButtonDown=state==Glut.GLUT_DOWN;
					this. PressedKeyBackward=state==Glut.GLUT_DOWN;
					break;
				case Glut.GLUT_RIGHT_BUTTON: 
					rButtonDown=state==Glut.GLUT_DOWN;
					this.PressedKeyForward=state==Glut.GLUT_DOWN;
					break;
				case Glut.GLUT_MIDDLE_BUTTON: 
					if (state==Glut.GLUT_DOWN) toggleUpdate();
					break;
			}
		}

		protected void toggleUpdate() 
		{
			updating=!updating;	
			if (updating)
				WinApi.SetCursorPos(ViewPort.Width>>1,ViewPort.Height>>1);
		}
		bool updating=true;
		
		public DateTime CurrentTime;

		protected void Update() 
		{
			TimeSpan elapsed = DateTime.Now.Subtract(CurrentTime);
			CurrentTime = DateTime.Now;
			World.Prepare(this);
			if (!updating)return;
			double sx=0;
			double sy=0;
			double sz=0;
            
			sx+=PressedKeyRight?1:0;
			sx+=PressedKeyLeft?-1:0;
			sy+=PressedKeyUp?1:0;
			sy+=PressedKeyDown?-1:0;
			sz+=PressedKeyBackward?1:0;
			sz+=PressedKeyForward?-1:0;

			this.strafeDir=new Point3D(sx,sy,sz);
			int xcenter = this.ViewPort.Width>>1;
			int ycenter = this.ViewPort.Height>>1;
			if (started) 
			{
				WinApi.Point p = WinApi.GetCursorPos();
				camera.Pan((-p.x+xcenter)/10f,(-p.y+ycenter)/10f);
			} 
			else started=true;
			Point3D moveDir = camera.StrafeDir(this.strafeDir.Scaled(elapsed.TotalSeconds*this.StrafeSpeed));
			Point3D ColisionNormal=(ghostMode)?new Point3D(0,0,0):World.ColisionNormal(camera.Origin,moveDir,30);
			if (ColisionNormal.Norm>0) 
			{
				moveDir+=ColisionNormal;
				ColisionNormal=World.ColisionNormal(camera.Origin,moveDir,30);
			}
			if (ColisionNormal.Norm<.5 && moveDir.Norm>1) //No me movere si la distancia a moverme es muy pequenna
				camera.Translate(moveDir);
			WinApi.SetCursorPos(xcenter,ycenter);
		}
	}
}
