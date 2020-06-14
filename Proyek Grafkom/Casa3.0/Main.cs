//#define testingObjects
using System;
using Tao.OpenGl;
using Platform.Windows;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace TareaGL
{
	/// <summary>
	/// Summary description for Main.
	/// </summary>
	public class MainClass
	{

		protected bool running=true;

		protected int Width=1024;
		protected int Height=768;

		protected Glut.PassiveMotionCallback PassiveMotionFunc = null;
		protected Avatar observer;
		public MainClass(string[] args) 
		{
			GlControl ViewPort = new GlControl(Width,Height);
			GlObjectList world = new GlObjectList();
			world.Add(new LightSource());
		#if !testingObjects
			world.Add(new TranslatedObject(new Point3D(0,-10,0),new SkyBox()));
			GlObjectList casa = new GlObjectList();
			casa.Add(new Casa2());

			casa.Add(new Librero(new Point3D(210,0,-318),0));
			casa.Add(new Librero(new Point3D(210,Librero.Height+.3,-316),-3));
			casa.Add(new Lamp(new Point3D(60,270,190),70));
			//casa.Add(new Refrigerador(new Point3D(-145,0,120),180));
			//Plantilla obj = new Mesita(new Point3D(80,0,40),90);
			//casa.Add(obj);

			//Cushion
			casa.Add(new Cojin(new Point3D(200,0,325),120));


			//casa.Add(new Cama(new Point3D(370,0,-250),0,100,70));
			//casa.Add(new Cama(new Point3D(370,0,-480),0,100,60));
			//casa.Add(new Cama(new Point3D(-210,0,-420),0,90,40));
			casa.Add(new Estante(new Point3D(-210,170,-108)));
			Plantilla obj = new Mesa(new Point3D(380,0,200));
			casa.Add(obj);
			obj = new EstanteHorizontal(new Point3D(100,0,50));
			//System.Windows.Forms.MessageBox.Show(origin.X+" "+origin.Y+" "+origin.Z);
			casa.Add(obj);
			
			//Picture
			//obj = new Cuadro(new Point3D(200,0,325));
			//casa.Add(obj);

			//casa.Add(new Silla(new Point3D(440,0,160)));
			//casa.Add(new Silla(new Point3D(440,0,240)));
			//casa.Add(new Silla(new Point3D(330,0,160),180));
			//casa.Add(new Silla(new Point3D(330,0,240),180));
			//casa.Add(new Silla(new Point3D(385,0,110),90));
			//casa.Add(new Silla(new Point3D(385,0,290),-90));
			casa.Add(new Plato(new Point3D(380,obj.Height+.2,200)));
			casa.Add(new Vaso(new Point3D(380,obj.Height+.2,230)));
			//casa.Add(new MesitaDeNoche(new Point3D(455,0,-140),-90));
			//casa.Add(new MesitaDeNoche(new Point3D(455,0,-380),-90));
			//casa.Add(new MesitaDeNoche(new Point3D(455,0,-580),-90));
			//casa.Add(new MesitaDeNoche(new Point3D(-280,0,-340),90));
			obj = new EstanteHorizontal(new Point3D(330,0,-70),180);
			casa.Add(obj);

			//casa.Add(new Butaca(new Point3D(-30,0,200),90,2));
			//casa.Add(new Butaca(new Point3D(200,0,270),250,1));
			//casa.Add(new Butaca(new Point3D(200,0,110),-70,1));

			//casa.Add(new MesetaConFregadero(new Point3D(-307,0,-64),0));			

			casa.Add(new Clock(new Point3D(485,200,325),270));
			GlObject c = new TranslatedObject(new Point3D(0,0,-70),casa);
			world.Add(c);
		#endif

			observer=new Avatar(ViewPort, world);
			Glut.glutDisplayFunc(new Glut.DisplayCallback(observer.Look));
			Glut.glutIdleFunc(new Glut.IdleCallback(observer.Look));
			Glut.glutMainLoop();
		}

		public static void Main(string[] args) 
		{
			new MainClass(args);
		}


	}




}



