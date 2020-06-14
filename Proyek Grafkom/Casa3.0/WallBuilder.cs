using System;

namespace TareaGL
{
	/// <summary>
	/// Summary description for WallBuilder.
	/// </summary>
	public class WallBuilder
	{
		public WallBuilder()
		{
		}

		public static Wall BuildWall(Point3D from, Point3D to, double bottom, double height, string type) 
		{
			Wall res;
			switch (type) 
			{
				case "glass":
					res = new GlassWall(from,to,bottom,height);
					break;
				case "windowed":
					res = new WindowedWall(from,to,bottom,height);
					break;
				case "woden":
					res = new WindowedWall(from,to,bottom,height,new WodenWindow());
					break;
				case "woden glass":
					res = new WindowedWall(from,to,bottom,height,new WindowArray(new WodenWindow(),new GlassWindow()));
					break;
				case "woden glass woden":
					res = new WindowedWall(from,to,bottom,height,new WindowArray(new WodenWindow(),new GlassWindow(),new WodenWindow()));
					break;
				case "glass woden":
					res = new WindowedWall(from,to,bottom,height,new WindowArray(new GlassWindow(),new WodenWindow()));
					break;
				case "woden woden":
					res = new WindowedWall(from,to,bottom,height,new WindowArray(new WodenWindow(),new WodenWindow()));
					break;
				case "door":
					res = new DoorWall(from,to,bottom,height);
					break;
				case "reversed door":
					res = new DoorWall(from,to,bottom,height,false,true,true);
					break;
				case "closed door":
					res = new DoorWall(from,to,bottom,height,false,false,false);
					break;
				case "closed reversed door":
					res = new DoorWall(from,to,bottom,height,false,true,false);
					break;
				case "passage":
					res = new DoorWall(from,to,bottom,height,true,false,true);
					break;
				default:
					res= new SolidWall(from,to,bottom,height);
					break;
			}
			return res;
		}
	}
}
