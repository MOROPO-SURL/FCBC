using MOROPO.FCBC.Galleries.Scenes.Contract;
using Urho;

namespace MOROPO.FCBC.Galleries.Scenes
{
    internal class SquareRoomScene : AbstractRoomScene
    {
        private const float FLOOR_TILE_SIZE = 40.0f;
        private const float FLOOR_TILE_SPACING = 0.25f;
        private const float WALL_WEIGTH = 5f;
        private const int FLOOR_TILE_COUNT = 20;

        private static readonly float FLOOR_FULL_SIZE = (FLOOR_TILE_COUNT + 1) * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING);
        private static readonly float WALL_HEIGHT = 4f * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING);

        private readonly Node mainCameraNode;
        private readonly Camera mainCamera;

        private bool isTouchBegin;
        private float touchBeginX;
        private float touchBeginY;

        public SquareRoomScene(Presenters.VirtualGalleryRoomPresenter presenter) : base(presenter)
        {
            isTouchBegin = false;

            mainCameraNode = CreateChild("Camera");

            mainCameraNode.Position = new Vector3(0.0f, WALL_HEIGHT / 2, 0.0f);

            mainCamera = mainCameraNode.CreateComponent<Camera>();

            SetupLights();

            SetupFloor();

            SetupRoof();

            SetupWalls();

            SetupMoveMarks();

            SetupArtworkFrames(Providers.ArtworkModelProvider.Instance.GetImageRawModels());

            SetupIdentyFrames();



            //var doorNode = CreateChild();

            //doorNode.Position = new Vector3(0.0f, 0.0f, 30.0f);
            //doorNode.Scale = new Vector3(30.0f, 50.0f, 15.0f);

            //var doorComponent = doorNode.CreateComponent<StaticModel>();

            //doorComponent.Model = resourceCache.GetModel("Models/Door.fbx");
        }

        public override Camera MainCamera => mainCamera;

        public override void SceneInput_TouchBegin(TouchBeginEventArgs eventArgs)
        {
            touchBeginX = eventArgs.X;
            touchBeginY = eventArgs.Y;

            isTouchBegin = true;
        }

        public override void SceneInput_TouchEnd(TouchEndEventArgs eventArgs)
        {
            if (isTouchBegin)
            {
                if (touchBeginX != eventArgs.X || touchBeginY != eventArgs.Y)
                {
                    isTouchBegin = false;

                    return;
                }
            }

            isTouchBegin = false;

            var cameraRay = mainCamera.GetScreenRay((float)eventArgs.X / Application.Current.Graphics.Width, (float)eventArgs.Y / Application.Current.Graphics.Height);

            var result = octree.RaycastSingle(cameraRay);

            if (result != null)
            {
                var movementFloorTile = result.Value.Drawable as MoveMark;

                if (movementFloorTile != null)
                {
                    mainCameraNode.RunActions(new Urho.Actions.EaseElastic(new Urho.Actions.MoveTo(1f, new Vector3(movementFloorTile.Node.Position.X, mainCameraNode.Position.Y, movementFloorTile.Node.Position.Z))));
                }

                var artWorkFrame = result.Value.Drawable as ArtworkFrame;

                if (artWorkFrame != null)
                {
                    presenter.PresentArtworkDetailById(artWorkFrame.ArtworkId);
                }
            }
        }

        private void SetupLights()
        {
            var mainLightNode = CreateChild();

            mainLightNode.SetDirection(new Vector3(-0.3f, 1.0f, -0.3f));

            var mainLightComponent = mainLightNode.CreateComponent<Light>();

            mainLightComponent.LightType = LightType.Directional;
            mainLightComponent.Color = Color.White;
            mainLightComponent.Brightness = 0.75f;

            var pivotPositive = FLOOR_TILE_COUNT / 2;
            var pivotNegative = pivotPositive * -1;

            for (int y = pivotNegative; y <= pivotPositive; ++y)
            {
                for (int x = pivotNegative; x <= pivotPositive; ++x)
                {
                    if (x == 0 && y == 0 || System.Math.Abs(x) % 4 == 0 && System.Math.Abs(y) % 4 == 0)
                    {
                        var roofPointLightNode = CreateChild();

                        roofPointLightNode.Position = new Vector3(x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT - 5.0f, y * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));

                        var roofPointLightComponent = roofPointLightNode.CreateComponent<Light>();

                        roofPointLightComponent.LightType = LightType.Point;
                        roofPointLightComponent.CastShadows = true;
                        roofPointLightComponent.Brightness = 0.75f;
                        roofPointLightComponent.Color = Color.White;
                        roofPointLightComponent.Range = 275.0f;
                        roofPointLightComponent.Radius = 180;

                        var roofLightLampNode = CreateChild();

                        roofLightLampNode.Position = new Vector3(roofPointLightNode.Position.X, WALL_HEIGHT - 2.0f, roofPointLightNode.Position.Z);
                        roofLightLampNode.Scale = new Vector3(FLOOR_TILE_SIZE / 4, 2.0f, FLOOR_TILE_SIZE / 4);

                        var roofLightLampComponent = roofLightLampNode.CreateComponent<StaticModel>();

                        roofLightLampComponent.Model = CoreAssets.Models.Cylinder;
                        roofLightLampComponent.Material = Material.FromColor(Color.White);
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

            var texture2D = Application.Current.ResourceCache.GetTexture2D("Textures/Roof.jpg");

            var material = new Material();

            material.SetTexture(TextureUnit.Diffuse, texture2D);
            material.SetTechnique(0u, CoreAssets.Techniques.Diff);
            material.SetUVTransform(Vector2.Zero, 0f, new Vector2(5f, 5f));

            roofComponent.Model = CoreAssets.Models.Box;
            roofComponent.Material = material;
        }

        private void SetupFloor()
        {
            var floorNode = CreateChild("Floor");

            floorNode.Position = new Vector3(0.0f, 0.0f, 0.0f);
            floorNode.Scale = new Vector3(FLOOR_FULL_SIZE, 1.0f, FLOOR_FULL_SIZE);

            var floorComponent = floorNode.CreateComponent<StaticModel>();

            var texture2D = Application.Current.ResourceCache.GetTexture2D("Textures/Floor.jpg");

            var material = new Material();

            material.SetTexture(TextureUnit.Diffuse, texture2D);
            material.SetTechnique(0u, CoreAssets.Techniques.Diff);
            material.SetUVTransform(Vector2.Zero, 0f, new Vector2(5f, 5f));

            floorComponent.Model = CoreAssets.Models.Plane;
            floorComponent.Material = material;
        }

        private void SetupMoveMarks()
        {
            var pivotPositive = FLOOR_TILE_COUNT / 2;
            var pivotNegative = pivotPositive * -1;

            for (int y = pivotNegative; y <= pivotPositive; ++y)
            {
                for (int x = pivotNegative; x <= pivotPositive; ++x)
                {
                    if (x == 0 && y == 0 ||
                        System.Math.Abs(x) == 6 && System.Math.Abs(y) == 6 ||
                        x == 0 && System.Math.Abs(y) == 6 ||
                        System.Math.Abs(x) == 6 && y == 0)
                    {
                        var moveMarkNode = CreateChild();

                        moveMarkNode.Position = new Vector3(x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), 0.25f, y * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));
                        moveMarkNode.Scale = new Vector3(FLOOR_TILE_SIZE / 2, 0.5f, FLOOR_TILE_SIZE / 2);

                        var movementMarkComponent = moveMarkNode.CreateComponent<MoveMark>();

                        movementMarkComponent.Model = CoreAssets.Models.Cylinder;
                        movementMarkComponent.Material = Material.FromColor(Color.Green);
                    }
                }
            }
        }

        private void SetupWalls()
        {
            var texture2D = Application.Current.ResourceCache.GetTexture2D("Textures/Wall.jpg");

            var material = new Material();

            material.SetTexture(TextureUnit.Diffuse, texture2D);
            material.SetTechnique(0u, CoreAssets.Techniques.Diff);
            material.SetUVTransform(Vector2.Zero, 0f, new Vector2(5f, 5f));

            var centerWallDistance = FLOOR_FULL_SIZE / 2 + WALL_WEIGTH / 2;

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

        private void SetupIdentyFrames()
        {
            var identityFrameWight = 2.0f;

            var identityFrameNode = CreateChild();

            identityFrameNode.Position = new Vector3(0.0f, WALL_HEIGHT / 2, FLOOR_FULL_SIZE / 2 - identityFrameWight);
            identityFrameNode.Scale = new Vector3(60.0f, 120.0f, identityFrameWight);

            var identityFrameComponent = identityFrameNode.CreateComponent<StaticModel>();

            identityFrameComponent.Model = CoreAssets.Models.Box;
            identityFrameComponent.Material = Material.FromImage("Textures/Identity_Primary_FCBC.jpg");
        }

        private void SetupArtworkFrames(Models.Raw.ImageRawModel[] imageRawModels)
        {
            var pivotPositive = FLOOR_TILE_COUNT / 2;
            var pivotNegative = pivotPositive * -1;

            int index = 0;

            // North Wall
            for (int x = pivotNegative; x <= pivotPositive; ++x)
            {
                if (x == 0 || x % 4 != 0) continue;

                var item = imageRawModels[index++];

                var pictureFrameNode = CreateChild();

                var frameWidth = 100f;
                var frameHeight = frameWidth;

                var zoom = (item.Width + item.Height) / 2 / frameWidth;

                if (item.Width != item.Height)
                {
                    frameWidth = item.Width / zoom;

                    frameHeight = item.Height / zoom;
                }

                pictureFrameNode.Position = new Vector3(x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT / 2, FLOOR_FULL_SIZE / 2 - 0.5f);
                pictureFrameNode.Scale = new Vector3(frameWidth, frameHeight, 1.0f);

                var boxObject = pictureFrameNode.CreateComponent<ArtworkFrame>();

                var urhoResourceImage = new Urho.Resources.Image();

                using (var urhoBuffer = new MemoryBuffer(item.Data))
                    urhoResourceImage.Load(urhoBuffer);

                boxObject.Model = CoreAssets.Models.Box;
                boxObject.Material = Material.FromImage(urhoResourceImage);
            }

            // South Wall
            for (int x = pivotNegative; x <= pivotPositive; ++x)
            {
                if (x == 0 || x % 4 != 0) continue;

                var item = imageRawModels[index++];

                var pictureFrameNode = CreateChild();

                var frameWidth = 100f;
                var frameHeight = frameWidth;

                var zoom = (item.Width + item.Height) / 2 / frameWidth;

                if (item.Width != item.Height)
                {
                    frameWidth = item.Width / zoom;

                    frameHeight = item.Height / zoom;
                }

                pictureFrameNode.Position = new Vector3(-1 * x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT / 2, -1 * FLOOR_FULL_SIZE / 2 + 0.5f);
                pictureFrameNode.Scale = new Vector3(frameWidth, frameHeight, 1.0f);

                var boxObject = pictureFrameNode.CreateComponent<ArtworkFrame>();

                var urhoResourceImage = new Urho.Resources.Image();

                using (var urhoBuffer = new MemoryBuffer(item.Data))
                    urhoResourceImage.Load(urhoBuffer);

                boxObject.Model = CoreAssets.Models.Box;
                boxObject.Material = Material.FromImage(urhoResourceImage);
            }

            // East Wall
            for (int x = pivotNegative; x <= pivotPositive; ++x)
            {
                if (x % 4 != 0) continue;

                var item = imageRawModels[index++];

                var pictureFrameNode = CreateChild();

                var frameWidth = 100f;
                var frameHeight = frameWidth;

                var zoom = (item.Width + item.Height) / 2 / frameWidth;

                if (item.Width != item.Height)
                {
                    frameWidth = item.Width / zoom;

                    frameHeight = item.Height / zoom;
                }

                pictureFrameNode.Position = new Vector3(FLOOR_FULL_SIZE / 2 - 0.5f, WALL_HEIGHT / 2, x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));
                pictureFrameNode.Scale = new Vector3(1.0f, frameHeight, frameWidth);

                var boxObject = pictureFrameNode.CreateComponent<ArtworkFrame>();

                var urhoResourceImage = new Urho.Resources.Image();

                using (var urhoBuffer = new MemoryBuffer(item.Data))
                    urhoResourceImage.Load(urhoBuffer);

                boxObject.Model = CoreAssets.Models.Box;
                boxObject.Material = Material.FromImage(urhoResourceImage);
            }

            // West Wall
            for (int x = pivotNegative; x <= pivotPositive; ++x)
            {
                if (x % 4 != 0) continue;

                var item = imageRawModels[index++];

                var pictureFrameNode = CreateChild();

                var frameWidth = 100f;
                var frameHeight = frameWidth;

                var zoom = (item.Width + item.Height) / 2 / frameWidth;

                if (item.Width != item.Height)
                {
                    frameWidth = item.Width / zoom;

                    frameHeight = item.Height / zoom;
                }

                pictureFrameNode.Position = new Vector3(-1 * FLOOR_FULL_SIZE / 2 + 0.5f, WALL_HEIGHT / 2, -1 * x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));
                pictureFrameNode.Scale = new Vector3(1.0f, frameHeight, frameWidth);

                var boxObject = pictureFrameNode.CreateComponent<ArtworkFrame>();

                var urhoResourceImage = new Urho.Resources.Image();

                using (var urhoBuffer = new MemoryBuffer(item.Data))
                    urhoResourceImage.Load(urhoBuffer);

                boxObject.Model = CoreAssets.Models.Box;
                boxObject.Material = Material.FromImage(urhoResourceImage);
            }
        }
    }

    internal class MoveMark : StaticModel
    {
    }

    internal class ArtworkFrame : StaticModel
    {
        public ArtworkFrame()
        {
            ArtworkId = System.Guid.NewGuid();
        }

        public System.Guid ArtworkId { get; private set; }
    }
}
