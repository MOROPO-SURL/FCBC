using Urho;

namespace MOROPO.FCBC.Galleries.Engine3D.Scenes
{
    internal class RectangleRoomScene : Contract.AbstractRoomScene
    {
		private const float FLOOR_TILE_SIZE = 40.0f;
		private const float FLOOR_TILE_SPACING = 0.25f;
		private const float WALL_WEIGTH = 4f;
		private const int FLOOR_TILE_COUNT = 20;

		private static readonly float FLOOR_FULL_SIZE = (FLOOR_TILE_COUNT + 1) * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING);
		private static readonly float WALL_HEIGHT = 4f * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING);

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

			SetupMoveMarks();

			SetupArtworkFrames();
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
			var pivotPositive = FLOOR_TILE_COUNT / 2;
			var pivotNegative = pivotPositive * -1;

			for (int y = pivotNegative; y <= pivotPositive; ++y)
			{
				for (int x = pivotNegative; x <= pivotPositive; ++x)
				{
					if ((x == 0 && y == 0) ||
						(System.Math.Abs(x) == 4 && System.Math.Abs(y) == 4) ||
						(System.Math.Abs(x) == 8 && System.Math.Abs(y) == 8) ||
						(x == 0 && System.Math.Abs(y) == 8) ||
						(System.Math.Abs(x) == 8 && y == 0))
					{
						var roofLightNode = CreateChild();

						roofLightNode.Position = new Vector3(x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT - 5.0f, y * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));

						var roofLightComponent = roofLightNode.CreateComponent<Light>();

						roofLightComponent.LightType = LightType.Point;
						roofLightComponent.CastShadows = true;
						roofLightComponent.Brightness = 0.75f;
						roofLightComponent.Color = Color.White;
						roofLightComponent.Range = 350.0f;
					}
				}
			}
		}

		private void SetupRoof()
        {
			var roofNode = CreateChild("Roof");

			roofNode.Position = new Vector3(0f, WALL_HEIGHT, 0f);
			roofNode.Scale = new Vector3(FLOOR_FULL_SIZE, WALL_WEIGTH, FLOOR_FULL_SIZE);

			var roofComponent = roofNode.CreateComponent<StaticModel>();

			roofComponent.Model = CoreAssets.Models.Box;
			roofComponent.Material = Material.FromImage("Textures/WhiteWallGrungePlaster.jpg");
		}

		private void SetupFloor()
        {
			var floorNode = CreateChild("Floor");

			floorNode.Position = new Vector3(0.0f, 0.0f, 0.0f);
			floorNode.Scale = new Vector3(FLOOR_FULL_SIZE, 1.0f, FLOOR_FULL_SIZE);

			var floorComponent = floorNode.CreateComponent<StaticModel>();

			floorComponent.Model = CoreAssets.Models.Plane;
			floorComponent.Material = Material.FromImage("Textures/ListoneWoodFloor.jpg");
		}

		private void SetupMoveMarks()
        {
			var pivotPositive = FLOOR_TILE_COUNT / 2;
			var pivotNegative = pivotPositive * -1;

			for (int y = pivotNegative; y <= pivotPositive; ++y)
			{
				for (int x = pivotNegative; x <= pivotPositive; ++x)
				{
					if ((x == 0 && y == 0) ||
						(System.Math.Abs(x) == 6 && System.Math.Abs(y) == 6) ||
						(x == 0 && System.Math.Abs(y) == 6) ||
						(System.Math.Abs(x) == 6 && y == 0))
					{
						var moveMarkNode = CreateChild();

						moveMarkNode.Position = new Vector3(x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), 0.0f, y * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));
						moveMarkNode.Scale = new Vector3(FLOOR_TILE_SIZE, 1.0f, FLOOR_TILE_SIZE);

						var movementMarkComponent = moveMarkNode.CreateComponent<MovementFloorTile>();

						movementMarkComponent.Model = CoreAssets.Models.Box;
						movementMarkComponent.Material = Material.FromColor(Color.Green);
					}
				}
			}
		}

		private void SetupWalls()
        {
			var material = Material.FromImage("Textures/DarkGrungeWall.jpg");

			var centerWallDistance = (FLOOR_FULL_SIZE / 2) + (WALL_WEIGTH / 2);

            var northWallNode = CreateChild("NorthWall");

			northWallNode.Position = new Vector3(0f, WALL_HEIGHT / 2, centerWallDistance);
			northWallNode.Scale = new Vector3(FLOOR_FULL_SIZE, WALL_HEIGHT, WALL_WEIGTH);

			var northWallObject = northWallNode.CreateComponent<StaticModel>();

			northWallObject.Model = CoreAssets.Models.Box;
			northWallObject.Material = material;

			var southWallNode = CreateChild("SouthWall");

			southWallNode.Position = new Vector3(0f, WALL_HEIGHT / 2, -1 * centerWallDistance);
			southWallNode.Scale = new Vector3(FLOOR_FULL_SIZE, WALL_HEIGHT, WALL_WEIGTH);

			var southWallObject = southWallNode.CreateComponent<StaticModel>();

			southWallObject.Model = CoreAssets.Models.Box;
			southWallObject.Material = material;

			var westWallNode = CreateChild("WestWall");

			westWallNode.Position = new Vector3(-1 * centerWallDistance, WALL_HEIGHT / 2, 0f);
			westWallNode.Scale = new Vector3(WALL_WEIGTH, WALL_HEIGHT, FLOOR_FULL_SIZE);

			var westWallObject = westWallNode.CreateComponent<StaticModel>();

			westWallObject.Model = CoreAssets.Models.Box;
			westWallObject.Material = material;

			var eastWallNode = CreateChild("EastWall");

			eastWallNode.Position = new Vector3(centerWallDistance, WALL_HEIGHT / 2, 0f);
			eastWallNode.Scale = new Vector3(WALL_WEIGTH, WALL_HEIGHT, FLOOR_FULL_SIZE);

			var eastWallObject = eastWallNode.CreateComponent<StaticModel>();

			eastWallObject.Model = CoreAssets.Models.Box;
			eastWallObject.Material = material;
		}

		private void SetupArtworkFrames()
        {
			var material = Material.FromImage("Textures/PlayboWood.jpg");

			var pivotPositive = FLOOR_TILE_COUNT / 2;
			var pivotNegative = pivotPositive * -1;

			// North Wall
			for (int x = pivotNegative; x <= pivotPositive; ++x)
			{
				if (x % 4 != 0) continue;

				var pictureFrameNode = CreateChild();

				pictureFrameNode.Position = new Vector3(x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT / 2, FLOOR_FULL_SIZE / 2 - 0.5f);
				pictureFrameNode.Scale = new Vector3(100.0f, 75.0f, 1.0f);

				var boxObject = pictureFrameNode.CreateComponent<StaticModel>();

				boxObject.Model = CoreAssets.Models.Box;
				boxObject.Material = material;
			}

			// South Wall
			for (int x = pivotNegative; x <= pivotPositive; ++x)
			{
				if (x % 4 != 0) continue;

				var pictureFrameNode = CreateChild();

				pictureFrameNode.Position = new Vector3(-1 * x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT / 2, -1 * FLOOR_FULL_SIZE / 2 + 0.5f);
				pictureFrameNode.Scale = new Vector3(100.0f, 75.0f, 1.0f);

				var boxObject = pictureFrameNode.CreateComponent<StaticModel>();

				boxObject.Model = CoreAssets.Models.Box;
				boxObject.Material = material;
			}

			// East Wall
			for (int x = pivotNegative; x <= pivotPositive; ++x)
			{
				if (x % 4 != 0) continue;

				var pictureFrameNode = CreateChild();

				pictureFrameNode.Position = new Vector3(FLOOR_FULL_SIZE / 2 - 0.5f, WALL_HEIGHT / 2, x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));
				pictureFrameNode.Scale = new Vector3(1.0f, 75.0f, 100.0f);

				var boxObject = pictureFrameNode.CreateComponent<StaticModel>();

				boxObject.Model = CoreAssets.Models.Box;
				boxObject.Material = material;
			}

			// West Wall
			for (int x = pivotNegative; x <= pivotPositive; ++x)
			{
				if (x % 4 != 0) continue;

				var pictureFrameNode = CreateChild();

				pictureFrameNode.Position = new Vector3(-1 * FLOOR_FULL_SIZE / 2 + 0.5f, WALL_HEIGHT / 2, -1 * x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING) );
				pictureFrameNode.Scale = new Vector3(1.0f, 75.0f, 100.0f);

				var boxObject = pictureFrameNode.CreateComponent<StaticModel>();

				boxObject.Model = CoreAssets.Models.Box;
				boxObject.Material = material;
			}
		}
    }

	internal class MovementFloorTile : StaticModel
    {
    }
}
