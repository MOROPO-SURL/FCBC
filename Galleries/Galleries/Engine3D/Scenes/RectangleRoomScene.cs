using Urho;

namespace MOROPO.FCBC.Galleries.Engine3D.Scenes
{
    internal class RectangleRoomScene : Contract.AbstractRoomScene
    {
        public RectangleRoomScene(Urho.Resources.ResourceCache resourceCache) : base(resourceCache)
        {
            // Create a Zone component for ambient lighting & fog control
			var zoneNode = CreateChild("Zone");
			
			var zone = zoneNode.CreateComponent<Zone>();
			
			zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
			zone.AmbientColor = new Color(0.1f, 0.1f, 0.1f);
			zone.FogStart = 100.0f;
			zone.FogEnd = 300.0f;

			// Create a directional light without shadows
			var lightNode = CreateChild("DirectionalLight");
			
			lightNode.SetDirection(new Vector3(0.5f, -1.0f, 0.5f));
			
			var light = lightNode.CreateComponent<Light>();
			
			light.LightType = LightType.Directional;
			light.Color = Color.Red;
			light.SpecularIntensity = 1.0f;

			// Create a "floor" consisting of several tiles
			for (int y = -5; y <= 5; ++y)
			{
				for (int x = -5; x <= 5; ++x)
				{
					var floorNode = CreateChild("FloorTile");
					
					floorNode.Position = new Vector3(x * 20.5f, -0.5f, y * 20.5f);
					floorNode.Scale = new Vector3(20.0f, 1.0f, 20.0f);
					
					var floorObject = floorNode.CreateComponent<StaticModel>();
					
					floorObject.Model = CoreAssets.Models.Box;
					floorObject.Material = CoreAssets.Materials.DefaultGrey;
				}
			}
		}
    }
}
