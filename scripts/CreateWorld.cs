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
						var circleShape = new CircleShape2D();
						circleShape.Radius = @object.width / 2;
						var collisionShape = new CollisionShape2D();
						collisionShape.Shape = circleShape;
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
						var playerScene = ResourceLoader.Load<PackedScene>("res://objects/player.tscn");
						var player = playerScene.Instantiate() as Node2D;
						AddChild(player);
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
		}
	}

	public override void _Process(double delta)
	{
	}

}
