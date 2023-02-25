using Urho;

namespace MOROPO.FCBC.Galleries.Engine3D.Scenes
{
    internal class RectangleRoomScene : Contract.AbstractRoomScene
    {
		private const float FLOOR_TILE_SIZE = 40.0f;
		private const float FLOOR_TILE_SPACING = 0.25f;
		private const float WALL_WEIGTH = 4f;
		private const int FLOOR_TILE_COUNT = 12;

		private static readonly float FLOOR_FULL_SIZE = (FLOOR_TILE_COUNT + 1) * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING);
		private static readonly float WALL_HEIGHT = 3f * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING);

		private readonly Node mainCameraNode;
		private readonly Camera mainCamera;

        public RectangleRoomScene(Urho.Resources.ResourceCache resourceCache) : base(resourceCache)
		{
			// Camera
			mainCameraNode = CreateChild("Camera");

			mainCameraNode.Position = new Vector3(0.0f, WALL_HEIGHT / 2, 0.0f);

			mainCamera = mainCameraNode.CreateComponent<Camera>();

			SetupLights();

			SetupFloor();

            SetupRoof();

            SetupWalls();

			// Pictures
			var pictureFrameNode = CreateChild("PictureFrame");

			pictureFrameNode.Position = new Vector3(0.0f, WALL_HEIGHT / 2, FLOOR_FULL_SIZE / 2 - 0.5f);
			pictureFrameNode.Scale = new Vector3(80.0f, 60.0f, 1.0f);
			
			var boxObject = pictureFrameNode.CreateComponent<StaticModel>();
			
			boxObject.Model = CoreAssets.Models.Box;
			boxObject.Material = Material.FromColor(Color.Red);
		}

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
			var brightness = 1.0f;
			var range = 275.0f;

			var centerRoofLightNode = CreateChild("CenterRoofLight");

            centerRoofLightNode.Position = new Vector3(0.0f, WALL_HEIGHT - 5.0f, 0.0f);

            var centerRoofLight = centerRoofLightNode.CreateComponent<Light>();

			centerRoofLight.LightType = LightType.Point;
            centerRoofLight.CastShadows = true;
            centerRoofLight.Brightness = brightness;
            centerRoofLight.Color = Color.White;
            centerRoofLight.Range = range;

			var leftUpRoofLightNode = CreateChild("LeftUpRoofLight");

			leftUpRoofLightNode.Position = new Vector3(-3 * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT - 5.0f, 3 * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));

			var leftUpRoofLight = leftUpRoofLightNode.CreateComponent<Light>();

			leftUpRoofLight.LightType = LightType.Point;
			leftUpRoofLight.CastShadows = true;
			leftUpRoofLight.Brightness = brightness;
			leftUpRoofLight.Color = Color.White;
			leftUpRoofLight.Range = range;

			var leftBottomRoofLightNode = CreateChild("LeftBottomRoofLight");

			leftBottomRoofLightNode.Position = new Vector3(-3 * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT - 5.0f, -3 * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));

			var leftBottomRoofLight = leftBottomRoofLightNode.CreateComponent<Light>();

			leftBottomRoofLight.LightType = LightType.Point;
			leftBottomRoofLight.CastShadows = true;
			leftBottomRoofLight.Brightness = brightness;
			leftBottomRoofLight.Color = Color.White;
			leftBottomRoofLight.Range = range;

			var rightUpRoofLightNode = CreateChild("LeftUpRoofLight");

			rightUpRoofLightNode.Position = new Vector3(3 * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT - 5.0f, 3 * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));

			var rightUpRoofLight = rightUpRoofLightNode.CreateComponent<Light>();

			rightUpRoofLight.LightType = LightType.Point;
			rightUpRoofLight.CastShadows = true;
			rightUpRoofLight.Brightness = brightness;
			rightUpRoofLight.Color = Color.White;
			rightUpRoofLight.Range = range;

			var rightBottomRoofLightNode = CreateChild("LeftBottomRoofLight");

			rightBottomRoofLightNode.Position = new Vector3(3 * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT - 5.0f, -3 * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));

			var rightBottomRoofLight = rightBottomRoofLightNode.CreateComponent<Light>();

			rightBottomRoofLight.LightType = LightType.Point;
			rightBottomRoofLight.CastShadows = true;
			rightBottomRoofLight.Brightness = brightness;
			rightBottomRoofLight.Color = Color.White;
			rightBottomRoofLight.Range = range;
		}

		private void SetupRoof()
        {
			var roofPlaneNode = CreateChild("RoofPlane");

			roofPlaneNode.Position = new Vector3(0f, WALL_HEIGHT, 0f);
			roofPlaneNode.Scale = new Vector3(FLOOR_FULL_SIZE, WALL_WEIGTH, FLOOR_FULL_SIZE);

			var roofPlaneComponent = roofPlaneNode.CreateComponent<StaticModel>();

			roofPlaneComponent.Model = CoreAssets.Models.Box;
			roofPlaneComponent.Material = Material.FromColor(Color.White);
		}

		private void SetupFloor()
        {
			var pivotPositive = FLOOR_TILE_COUNT / 2;
			var pivotNegative = pivotPositive * -1;

			for (int y = pivotNegative; y <= pivotPositive; ++y)
			{
				for (int x = pivotNegative; x <= pivotPositive; ++x)
				{
					var floorNode = CreateChild("FloorTile");

					floorNode.Position = new Vector3(x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), -1 * FLOOR_TILE_SPACING, y * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));
					floorNode.Scale = new Vector3(FLOOR_TILE_SIZE, 1.0f, FLOOR_TILE_SIZE);

					if ((x == 0 && y == 0) || (System.Math.Abs(x) == 3 && System.Math.Abs(y) == 3) || (System.Math.Abs(x) == 6 && System.Math.Abs(y) == 6))
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
			var centerWallDistance = (FLOOR_FULL_SIZE / 2) + (WALL_WEIGTH / 2);
			var centerWallHeight = 3f * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING);

            var northWallNode = CreateChild("NorthWall");

			northWallNode.Position = new Vector3(0f, centerWallHeight / 2, centerWallDistance);
			northWallNode.Scale = new Vector3(FLOOR_FULL_SIZE, centerWallHeight, WALL_WEIGTH);

			var northWallObject = northWallNode.CreateComponent<StaticModel>();

			northWallObject.Model = CoreAssets.Models.Box;
			northWallObject.Material = CoreAssets.Materials.DefaultGrey;

			var southWallNode = CreateChild("SouthWall");

			southWallNode.Position = new Vector3(0f, centerWallHeight / 2, -1 * centerWallDistance);
			southWallNode.Scale = new Vector3(FLOOR_FULL_SIZE, centerWallHeight, WALL_WEIGTH);

			var southWallObject = southWallNode.CreateComponent<StaticModel>();

			southWallObject.Model = CoreAssets.Models.Box;
			southWallObject.Material = CoreAssets.Materials.DefaultGrey;

			var westWallNode = CreateChild("WestWall");

			westWallNode.Position = new Vector3(-1 * centerWallDistance, centerWallHeight / 2, 0f);
			westWallNode.Scale = new Vector3(WALL_WEIGTH, centerWallHeight, FLOOR_FULL_SIZE);

			var westWallObject = westWallNode.CreateComponent<StaticModel>();

			westWallObject.Model = CoreAssets.Models.Box;
			westWallObject.Material = CoreAssets.Materials.DefaultGrey;

			var eastWallNode = CreateChild("EastWall");

			eastWallNode.Position = new Vector3(centerWallDistance, centerWallHeight / 2, 0f);
			eastWallNode.Scale = new Vector3(WALL_WEIGTH, centerWallHeight, FLOOR_FULL_SIZE);

			var eastWallObject = eastWallNode.CreateComponent<StaticModel>();

			eastWallObject.Model = CoreAssets.Models.Box;
			eastWallObject.Material = CoreAssets.Materials.DefaultGrey;
		}
    }

	internal class MovementFloorTile : StaticModel
    {
    }
}
