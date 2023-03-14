using Godot;
using System;

public partial class CreateWorld : Node
{
	MapData mapData = MapData.Load("map/world_map.json");

	public override void _Ready()
	{
		foreach (Layer layer in mapData.layers)
		{
			if (layer.type == "objectgroup")
			{
				foreach (TiledObject @object in layer.objects)
				{
					var node = new Node2D();
					node.Position = new Vector2(@object.x, @object.y);
					AddChild(node);

					var staticBody = new StaticBody2D();
					staticBody.Position = new Vector2(@object.width / 2, @object.height / 2);
					node.AddChild(staticBody);

					if (@object.ellipse)
					{
						var ellipseShape = new CapsuleShape2D();
						ellipseShape.Height = @object.height / 2;
						ellipseShape.Radius = @object.width / 2;
						var collisionShape = new CollisionShape2D();
						collisionShape.Shape = ellipseShape;
						staticBody.AddChild(collisionShape);
					}
					else if (@object.polygon != null)
					{
						var collisionPolygon = new CollisionPolygon2D();
						var points = new Vector2[@object.polygon.Count];
						for (int i = 0; i < @object.polygon.Count; i++)
						{
							points[i] = new Vector2(@object.polygon[i].x, @object.polygon[i].y);
						}
						collisionPolygon.Polygon = points;
						staticBody.AddChild(collisionPolygon);
					}
					else if (@object.point)
					{
						var nodeScene = ResourceLoader.Load<PackedScene>("res://" + @object.@class + ".tscn");
						var inst = nodeScene.Instantiate() as Node2D;
						AddChild(inst);
						inst.Position = new Vector2(@object.x, @object.y);
					}
					else
					{
						var collisionShape = new CollisionShape2D();
						var rectangleShape = new RectangleShape2D();
						rectangleShape.Size = new Vector2(@object.width, @object.height);
						collisionShape.Shape = rectangleShape;
						staticBody.AddChild(collisionShape);
					}
				}
			}
			else if (layer.type == "tilelayer")
			{
				int tileWidth = mapData.tilewidth;
				int xx = 0;
				int yy = 0;
				foreach (int tileId in layer.data)
				{
					if (xx >= mapData.width)
					{
						xx = 0;
						yy += 1;
					}
					xx += 1;

					if (tileId == 0)
					{
						continue;
					}
					var inst = new Node2D();
					AddChild(inst);
					inst.Position = new Vector2(
						xx * tileWidth - (tileWidth / 2),
						yy * tileWidth + (tileWidth / 2)
					);

					//var sprite = inst.GetNode<Sprite2D>("Sprite2D");
					//sprite.Frame = tileId - 1;

					var sprite = new Sprite2D();
					var texture = ResourceLoader.Load<Texture>("res://" + mapData.tilesets[0].image);
					sprite.Texture = (Texture2D)texture;
					sprite.Hframes = mapData.tilesets[0].imagewidth / 32;
					sprite.Vframes = mapData.tilesets[0].imageheight / 32;
					sprite.Frame = tileId - 1;


					inst.AddChild(sprite);


				}
			}
		}
	}

	public override void _Process(double delta)
	{
	}

}
