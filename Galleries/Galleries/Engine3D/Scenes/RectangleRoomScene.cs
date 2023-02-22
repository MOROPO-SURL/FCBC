using Urho;

namespace MOROPO.FCBC.Galleries.Engine3D.Scenes
{
    internal class RectangleRoomScene : Contract.AbstractRoomScene
    {
        public RectangleRoomScene(Urho.Resources.ResourceCache resourceCache) : base(resourceCache)
		{
			var floorTileSize = 40.0f;
			var floorTileSpacing = 0.25f;
			
			// Create a Zone component for ambient lighting & fog control
			var zoneNode = CreateChild("Zone");
			
			//var zone = zoneNode.CreateComponent<Zone>();
			
			//zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
			//zone.AmbientColor = new Color(0.1f, 0.1f, 0.1f);
			//zone.FogStart = 100.0f;
			//zone.FogEnd = 300.0f;

			// Create a directional light without shadows
			var centerRoofLightNode = CreateChild("CenterRoofLight");
			
			centerRoofLightNode.SetDirection(new Vector3(0.5f, -1.0f, 0.5f));
			
			var centerRoofLight = centerRoofLightNode.CreateComponent<Light>();
			
			centerRoofLight.LightType = LightType.Directional;
			centerRoofLight.Color = Color.White;
			centerRoofLight.SpecularIntensity = 1.0f;

			var northWestRoofLightNode = CreateChild("CenterRoofLight");

			northWestRoofLightNode.SetDirection(new Vector3(-3f * (floorTileSize + floorTileSpacing), -1.0f, -3f * (floorTileSize + floorTileSpacing)));

			var northWestRoofLight = northWestRoofLightNode.CreateComponent<Light>();

			northWestRoofLight.LightType = LightType.Directional;
			northWestRoofLight.Color = Color.White;
			northWestRoofLight.SpecularIntensity = 1.0f;

			// Create a "floor" consisting of several tiles
			for (int y = -5; y <= 5; ++y)
			{
				for (int x = -5; x <= 5; ++x)
				{
					var floorNode = CreateChild("FloorTile");
					
					floorNode.Position = new Vector3(x * (floorTileSize + floorTileSpacing), -1 * floorTileSpacing, y * (floorTileSize + floorTileSpacing));
					floorNode.Scale = new Vector3(floorTileSize, 1.0f, floorTileSize);
					
					var floorObject = floorNode.CreateComponent<StaticModel>();
					
					floorObject.Model = CoreAssets.Models.Box;
					floorObject.Material = CoreAssets.Materials.DefaultGrey;
				}
			}

			//var testNode = CreateChild("FloorTile");

			//testNode.Position = new Vector3(-1 * (floorTileSize + floorTileSpacing), 120 * floorTileSpacing, 3 * (floorTileSize + floorTileSpacing));
			//testNode.Scale = new Vector3(floorTileSize, 15.0f, floorTileSize);

			//var testObject = testNode.CreateComponent<StaticModel>();

			//testObject.Model = CoreAssets.Models.Box;
			//testObject.Material = CoreAssets.Materials.DefaultGrey;

            var northWallNode = CreateChild("NorthWall");

            northWallNode.Position = new Vector3(0f, 0f, 5.5f * (floorTileSize + floorTileSpacing));
            northWallNode.Scale = new Vector3(11f * (floorTileSize + floorTileSpacing), 4f * (floorTileSize + floorTileSpacing), 2f);

            var northWallObject = northWallNode.CreateComponent<StaticModel>();

            northWallObject.Model = CoreAssets.Models.Box;
            northWallObject.Material = CoreAssets.Materials.DefaultGrey;

			var southWallNode = CreateChild("SouthWall");

			southWallNode.Position = new Vector3(0f, 0f, -5.5f * (floorTileSize + floorTileSpacing));
			southWallNode.Scale = new Vector3(11f * (floorTileSize + floorTileSpacing), 4f * (floorTileSize + floorTileSpacing), 2f);

			var southWallObject = southWallNode.CreateComponent<StaticModel>();

			southWallObject.Model = CoreAssets.Models.Box;
			southWallObject.Material = CoreAssets.Materials.DefaultGrey;

			var westWallNode = CreateChild("WestWall");

			westWallNode.Position = new Vector3(-5.5f * (floorTileSize + floorTileSpacing), 0f, 0f);
			westWallNode.Scale = new Vector3(2f, 4f * (floorTileSize + floorTileSpacing), 11f * (floorTileSize + floorTileSpacing));

			var westWallObject = westWallNode.CreateComponent<StaticModel>();

			westWallObject.Model = CoreAssets.Models.Box;
			westWallObject.Material = CoreAssets.Materials.DefaultGrey;

			var eastWallNode = CreateChild("EastWall");

			eastWallNode.Position = new Vector3(5.5f * (floorTileSize + floorTileSpacing), 0f, 0f);
			eastWallNode.Scale = new Vector3(2f, 4f * (floorTileSize + floorTileSpacing), 11f * (floorTileSize + floorTileSpacing));

			var eastWallObject = eastWallNode.CreateComponent<StaticModel>();

			eastWallObject.Model = CoreAssets.Models.Box;
			eastWallObject.Material = CoreAssets.Materials.DefaultGrey;
		}
    }
}
