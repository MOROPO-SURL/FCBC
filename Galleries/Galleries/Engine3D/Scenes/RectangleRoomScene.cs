using Urho;

namespace MOROPO.FCBC.Galleries.Engine3D.Scenes
{
    internal class RectangleRoomScene : Contract.AbstractRoomScene
    {
		private const float FLOOR_TILE_SIZE = 40.0f;
		private const float FLOOR_TILE_SPACING = 0.25f;
		private const float WALL_WEIGTH = 4f;

		private Node mainCameraNode;
		private Camera mainCamera;

        public RectangleRoomScene(Urho.Resources.ResourceCache resourceCache) : base(resourceCache)
		{
			// Create a Zone component for ambient lighting & fog control
			//var zoneNode = CreateChild("Zone");

			//var zone = zoneNode.CreateComponent<Zone>();

			//zone.SetBoundingBox(new BoundingBox(-1000.0f, 1000.0f));
			//zone.AmbientColor = new Color(0.1f, 0.1f, 0.1f);
			//zone.FogStart = 100.0f;
			//zone.FogEnd = 300.0f;

			SetupLights();

			SetupFloor();

			SetupWalls();

			// Pictures
			var pictureFrameNode = CreateChild("PictureFrame");

			pictureFrameNode.Position = new Vector3(0.0f, 40.0f, AbsoluteWallDistance - 0.5f);
			pictureFrameNode.Scale = new Vector3(80.0f, 36.0f, 1.0f);
			
			var boxObject = pictureFrameNode.CreateComponent<StaticModel>();
			
			boxObject.Model = CoreAssets.Models.Box;
			boxObject.Material = Material.FromColor(Color.Red);

			// Camera
			mainCameraNode = CreateChild(name: "camera");

			mainCameraNode.Position = new Vector3(0.0f, 60.0f, 0.0f);

			mainCamera = mainCameraNode.CreateComponent<Camera>();
		}

		private float AbsoluteWallDistance => 5.5f * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING);

        public override Camera MainCamera => mainCamera;

        public override void SceneInput_TouchEnd(TouchEndEventArgs eventArgs)
        {
            var cameraRay = mainCamera.GetScreenRay((float)eventArgs.X / Application.Current.Graphics.Width, (float)eventArgs.Y / Application.Current.Graphics.Height);

			var result = octree.RaycastSingle(cameraRay);

			if (result != null)
            {
                var movementFloorTile = result.Value.Drawable as MovementFloorTile;

				if (movementFloorTile != null)
                {
					mainCameraNode.Position = new Vector3(movementFloorTile.Node.Position.X, mainCameraNode.Position.Y, movementFloorTile.Node.Position.Z);
                }
            }
        }

        private void SetupLights()
        {
			var centerRoofLightNode = CreateChild("CenterRoofLight");

			centerRoofLightNode.SetDirection(new Vector3(0.5f, -1.0f, 0.5f));

			var centerRoofLight = centerRoofLightNode.CreateComponent<Light>();

			centerRoofLight.LightType = LightType.Directional;
			centerRoofLight.Color = Color.White;
			centerRoofLight.SpecularIntensity = 1.0f;

			var northWestRoofLightNode = CreateChild("CenterRoofLight");

			northWestRoofLightNode.SetDirection(new Vector3(-3f * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), -1.0f, -3f * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING)));

			var northWestRoofLight = northWestRoofLightNode.CreateComponent<Light>();

			northWestRoofLight.LightType = LightType.Directional;
			northWestRoofLight.Color = Color.White;
			northWestRoofLight.SpecularIntensity = 1.0f;
		}

		private void SetupFloor()
        {
			for (int y = -5; y <= 5; ++y)
			{
				for (int x = -5; x <= 5; ++x)
				{
					var floorNode = CreateChild("FloorTile");

					floorNode.Position = new Vector3(x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), -1 * FLOOR_TILE_SPACING, y * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));
					floorNode.Scale = new Vector3(FLOOR_TILE_SIZE, 1.0f, FLOOR_TILE_SIZE);

					if ((x == 0 && y == 0) || (System.Math.Abs(x) == 3 && System.Math.Abs(y) == 3) || (System.Math.Abs(x) == 5 && System.Math.Abs(y) == 5))
					{
						var movementFloorTile = floorNode.CreateComponent<MovementFloorTile>();

                        movementFloorTile.Model = CoreAssets.Models.Box;
                        movementFloorTile.Material = Material.FromColor(Color.Green);
					}
					else
					{
                        var floorTile = floorNode.CreateComponent<StaticModel>();

                        floorTile.Model = CoreAssets.Models.Box;
                        floorTile.Material = CoreAssets.Materials.DefaultGrey;
					}
				}
			}
		}

		private void SetupWalls()
        {
			var centerWallDistance = AbsoluteWallDistance + (WALL_WEIGTH / 2);
			var centerWallHeight = 3f * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING);

            var northWallNode = CreateChild("NorthWall");

			northWallNode.Position = new Vector3(0f, centerWallHeight / 2, centerWallDistance);
			northWallNode.Scale = new Vector3(11f * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), centerWallHeight, WALL_WEIGTH);

			var northWallObject = northWallNode.CreateComponent<StaticModel>();

			northWallObject.Model = CoreAssets.Models.Box;
			northWallObject.Material = CoreAssets.Materials.DefaultGrey;

			var southWallNode = CreateChild("SouthWall");

			southWallNode.Position = new Vector3(0f, centerWallHeight / 2, -1 * centerWallDistance);
			southWallNode.Scale = new Vector3(11f * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), centerWallHeight, WALL_WEIGTH);

			var southWallObject = southWallNode.CreateComponent<StaticModel>();

			southWallObject.Model = CoreAssets.Models.Box;
			southWallObject.Material = CoreAssets.Materials.DefaultGrey;

			var westWallNode = CreateChild("WestWall");

			westWallNode.Position = new Vector3(-1 * centerWallDistance, centerWallHeight / 2, 0f);
			westWallNode.Scale = new Vector3(WALL_WEIGTH, centerWallHeight, 11f * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));

			var westWallObject = westWallNode.CreateComponent<StaticModel>();

			westWallObject.Model = CoreAssets.Models.Box;
			westWallObject.Material = CoreAssets.Materials.DefaultGrey;

			var eastWallNode = CreateChild("EastWall");

			eastWallNode.Position = new Vector3(centerWallDistance, centerWallHeight / 2, 0f);
			eastWallNode.Scale = new Vector3(WALL_WEIGTH, centerWallHeight, 11f * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));

			var eastWallObject = eastWallNode.CreateComponent<StaticModel>();

			eastWallObject.Model = CoreAssets.Models.Box;
			eastWallObject.Material = CoreAssets.Materials.DefaultGrey;
		}
    }

	internal class MovementFloorTile : StaticModel
    {
    }
}
