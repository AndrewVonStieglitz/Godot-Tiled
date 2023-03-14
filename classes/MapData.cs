using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class MapData
{
	public int compressionlevel { get; set; }
	public int height { get; set; }
	public bool infinite { get; set; }
	public List<Layer> layers { get; set; }
	public int nextlayerid { get; set; }
	public int nextobjectid { get; set; }
	public string orientation { get; set; }
	public string renderorder { get; set; }
	public string tiledversion { get; set; }
	public int tileheight { get; set; }
	public int tilewidth { get; set; }
	public string type { get; set; }
	public string version { get; set; }
	public int width { get; set; }
	public List<Tileset> tilesets { get; set; }

	public static MapData Load(string jsonPath)
	{
		using (StreamReader reader = new StreamReader(jsonPath))
		{
			string jsonString = reader.ReadToEnd();
			return JsonConvert.DeserializeObject<MapData>(jsonString);
		}
	}
}

public class Layer
{
	public string draworder { get; set; }
	public int id { get; set; }
	public string name { get; set; }
	public List<TiledObject> objects { get; set; }
	public List<int> data { get; set; }
	public float opacity { get; set; }
	public string type { get; set; }
	public bool visible { get; set; }
	public int x { get; set; }
	public int y { get; set; }
}

public class TiledObject
{
	public string @class { get; set; }
	public bool ellipse { get; set; }
	public int height { get; set; }
	public int id { get; set; }
	public string name { get; set; }
	public List<Point> polygon { get; set; }
	public bool point { get; set; }
	public int rotation { get; set; }
	public bool visible { get; set; }
	public int width { get; set; }
	public int x { get; set; }
	public int y { get; set; }
}

public class Point
{
	public int x { get; set; }
	public int y { get; set; }
}

public class Tileset
{
	public int columns { get; set; }
	public int firstgid { get; set; }
	public string image { get; set; }
	public int imageheight { get; set; }
	public int imagewidth { get; set; }
	public int margin { get; set; }
	public string name { get; set; }
	public int spacing { get; set; }
	public int tilecount { get; set; }
	public int tileheight { get; set; }
	public int tilewidth { get; set; }
}