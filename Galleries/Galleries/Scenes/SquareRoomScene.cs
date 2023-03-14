using MOROPO.FCBC.Galleries.Scenes.Contract;
using Urho;

namespace MOROPO.FCBC.Galleries.Scenes
{
    internal class SquareRoomScene : AbstractRoomScene
    {
        private const float FLOOR_TILE_SIZE = 35.0f;
        private const float FLOOR_TILE_SPACING = 0.0f;
        private const float WALL_WEIGTH = 5f;
        private const int FLOOR_TILE_COUNT = 20;
        private const float WALL_VINTAGE_WEIGTH = 1.0f;
        private const float WALL_VINTAGE_HEIGHT = 5.0f;

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

            mainCameraNode.Position = new Vector3(0.0f, WALL_HEIGHT * 4 / 7, 0.0f);

            mainCamera = mainCameraNode.CreateComponent<Camera>();

            SetupLights();

            SetupFloor();

            SetupRoof();

            SetupWalls();

            SetupMoveMarks();

            SetupArtworkFrames(Providers.ArtworkModelProvider.Instance.GetImageRawModels());

            SetupIdentyFrames();

            SetupVintages();
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

        private void SetupVintages()
        {
            var material = new Material();

            material.SetTexture(TextureUnit.Diffuse, Application.Current.ResourceCache.GetTexture2D("Textures/Wood_023_basecolor.jpg"));
            material.SetTexture(TextureUnit.Normal, Application.Current.ResourceCache.GetTexture2D("Textures/Wood_023_normal.jpg"));
            material.SetTexture(TextureUnit.Specular, Application.Current.ResourceCache.GetTexture2D("Textures/Wood_023_roughness.jpg"));
            material.SetTechnique(0u, CoreAssets.Techniques.DiffNormalSpec);
            //material.SetUVTransform(Vector2.Zero, 1f, new Vector2(45f, 45f));

            var centerWallDistance = FLOOR_FULL_SIZE / 2 - WALL_VINTAGE_WEIGTH / 2;

            var northWallVintageNode = CreateChild();

            northWallVintageNode.Position = new Vector3(0.0f, WALL_VINTAGE_HEIGHT / 2, centerWallDistance);
            northWallVintageNode.Scale = new Vector3(FLOOR_FULL_SIZE, WALL_VINTAGE_HEIGHT, WALL_VINTAGE_WEIGTH);

            var northWallVintageComponent = northWallVintageNode.CreateComponent<StaticModel>();

            northWallVintageComponent.Model = CoreAssets.Models.Box;
            northWallVintageComponent.Material = CoreAssets.Materials.DefaultGrey;

            var westWallVintageNode = CreateChild();

            westWallVintageNode.Position = new Vector3(-1 * centerWallDistance, WALL_VINTAGE_HEIGHT / 2, 0.0f);
            westWallVintageNode.Scale = new Vector3(WALL_VINTAGE_WEIGTH, WALL_VINTAGE_HEIGHT, FLOOR_FULL_SIZE - WALL_VINTAGE_WEIGTH * 2);

            var westWallVintageComponent = westWallVintageNode.CreateComponent<StaticModel>();

            westWallVintageComponent.Model = CoreAssets.Models.Box;
            westWallVintageComponent.Material = CoreAssets.Materials.DefaultGrey;

            var eastWallVintageNode = CreateChild();

            eastWallVintageNode.Position = new Vector3(centerWallDistance, WALL_VINTAGE_HEIGHT / 2, 0.0f);
            eastWallVintageNode.Scale = new Vector3(WALL_VINTAGE_WEIGTH, WALL_VINTAGE_HEIGHT, FLOOR_FULL_SIZE - WALL_VINTAGE_WEIGTH * 2);

            var eastWallVintageComponent = eastWallVintageNode.CreateComponent<StaticModel>();

            eastWallVintageComponent.Model = CoreAssets.Models.Box;
            eastWallVintageComponent.Material = CoreAssets.Materials.DefaultGrey;

            var leftSouthWallVintageNode = CreateChild();

            leftSouthWallVintageNode.Position = new Vector3(FLOOR_FULL_SIZE * 3 / 12 + FLOOR_FULL_SIZE / 24 - WALL_VINTAGE_WEIGTH / 2, WALL_VINTAGE_HEIGHT / 2, -1 * centerWallDistance);
            leftSouthWallVintageNode.Scale = new Vector3(FLOOR_FULL_SIZE * 5 / 12 + WALL_VINTAGE_WEIGTH, WALL_VINTAGE_HEIGHT, WALL_VINTAGE_WEIGTH);

            var leftSouthWallVintageComponent = leftSouthWallVintageNode.CreateComponent<StaticModel>();

            leftSouthWallVintageComponent.Model = CoreAssets.Models.Box;
            leftSouthWallVintageComponent.Material = CoreAssets.Materials.DefaultGrey;

            var rightSouthWallVintageNode = CreateChild();

            rightSouthWallVintageNode.Position = new Vector3(-1 * FLOOR_FULL_SIZE * 3 / 12 - FLOOR_FULL_SIZE / 24 + WALL_VINTAGE_WEIGTH / 2, WALL_VINTAGE_HEIGHT / 2, -1 * centerWallDistance);
            rightSouthWallVintageNode.Scale = new Vector3(FLOOR_FULL_SIZE * 5 / 12 + WALL_VINTAGE_WEIGTH, WALL_VINTAGE_HEIGHT, WALL_VINTAGE_WEIGTH);

            var rightSouthWallVintageComponent = rightSouthWallVintageNode.CreateComponent<StaticModel>();

            rightSouthWallVintageComponent.Model = CoreAssets.Models.Box;
            rightSouthWallVintageComponent.Material = CoreAssets.Materials.DefaultGrey;

            var leftSouthWallVerticalVintageNode = CreateChild();

            leftSouthWallVerticalVintageNode.Position = new Vector3(FLOOR_FULL_SIZE * 1 / 12 + 1.5f, WALL_HEIGHT * 4 / 10 + WALL_VINTAGE_HEIGHT * 2 / 3 + 0.5f, -1 * centerWallDistance);
            leftSouthWallVerticalVintageNode.Scale = new Vector3(WALL_VINTAGE_HEIGHT, WALL_HEIGHT * 8 / 10 - WALL_VINTAGE_HEIGHT * 2 / 3 + 1.0f, WALL_VINTAGE_WEIGTH);

            var leftSouthWallVerticalVintageComponent = leftSouthWallVerticalVintageNode.CreateComponent<StaticModel>();

            leftSouthWallVerticalVintageComponent.Model = CoreAssets.Models.Box;
            leftSouthWallVerticalVintageComponent.Material = CoreAssets.Materials.DefaultGrey;

            var rightSouthWallVerticalVintageNode = CreateChild();

            rightSouthWallVerticalVintageNode.Position = new Vector3(-1 * FLOOR_FULL_SIZE * 1 / 12 - 1.5f, WALL_HEIGHT * 4 / 10 + WALL_VINTAGE_HEIGHT * 2 / 3 + 0.5f, -1 * centerWallDistance);
            rightSouthWallVerticalVintageNode.Scale = new Vector3(WALL_VINTAGE_HEIGHT, WALL_HEIGHT * 8 / 10 - WALL_VINTAGE_HEIGHT * 2 / 3 + 1.0f, WALL_VINTAGE_WEIGTH);

            var rightSouthWallVerticalVintageComponent = rightSouthWallVerticalVintageNode.CreateComponent<StaticModel>();

            rightSouthWallVerticalVintageComponent.Model = CoreAssets.Models.Box;
            rightSouthWallVerticalVintageComponent.Material = CoreAssets.Materials.DefaultGrey;

            var middleSouthWallVintageNode = CreateChild();

            middleSouthWallVintageNode.Position = new Vector3(0.0f, WALL_HEIGHT * 8 / 10 + 0.25f, -1 * centerWallDistance);
            middleSouthWallVintageNode.Scale = new Vector3(FLOOR_FULL_SIZE * 2 / 12 - 2.0f, WALL_VINTAGE_HEIGHT, WALL_VINTAGE_WEIGTH);

            var middleSouthWallVintageComponent = middleSouthWallVintageNode.CreateComponent<StaticModel>();

            middleSouthWallVintageComponent.Model = CoreAssets.Models.Box;
            middleSouthWallVintageComponent.Material = CoreAssets.Materials.DefaultGrey;
        }
        
        private void SetupLights()
        {
            var floorLightNode = CreateChild();

            floorLightNode.SetDirection(new Vector3(0.0f, -1.0f, 0.0f));

            var floorLightComponent = floorLightNode.CreateComponent<Light>();

            floorLightComponent.LightType = LightType.Directional;
            floorLightComponent.Brightness = 0.3f;

            var roofLightNode = CreateChild();

            roofLightNode.SetDirection(new Vector3(0.0f, 1.0f, 0.0f));

            var roofLightComponent = roofLightNode.CreateComponent<Light>();

            roofLightComponent.LightType = LightType.Directional;
            roofLightComponent.Brightness = 0.3f;

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
                        roofPointLightComponent.CastShadows = false;
                        roofPointLightComponent.Brightness = 0.5f;
                        roofPointLightComponent.Color = Color.White;
                        roofPointLightComponent.Range = 280.0f;
                        roofPointLightComponent.Radius = 5;

                        var roofLightLampNode = CreateChild();

                        roofLightLampNode.Position = new Vector3(roofPointLightNode.Position.X, WALL_HEIGHT - 2.0f, roofPointLightNode.Position.Z);
                        roofLightLampNode.Scale = new Vector3(FLOOR_TILE_SIZE / 4, 2.0f, FLOOR_TILE_SIZE / 4);

                        var roofLightLampComponent = roofLightLampNode.CreateComponent<StaticModel>();

                        roofLightLampComponent.Model = CoreAssets.Models.Cylinder;
                        roofLightLampComponent.Material = Material.FromColor(Color.White);
                    }
                }
            }

            for (int x = pivotNegative + 1; x <= pivotPositive; ++x)
            {
                if (x == 0 || System.Math.Abs(x) % 4 == 0)
                {
                    var roofPointLightNode = CreateChild();

                    roofPointLightNode.Position = new Vector3(x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT - 5.0f, -13 * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));

                    var roofPointLightComponent = roofPointLightNode.CreateComponent<Light>();

                    roofPointLightComponent.LightType = LightType.Point;
                    roofPointLightComponent.CastShadows = false;
                    roofPointLightComponent.Brightness = 0.25f;
                    roofPointLightComponent.Color = Color.White;
                    roofPointLightComponent.Range = 375.0f;
                    roofPointLightComponent.Radius = 5;

                    var roofLightLampNode = CreateChild();

                    roofLightLampNode.Position = new Vector3(roofPointLightNode.Position.X, WALL_HEIGHT - 2.0f, roofPointLightNode.Position.Z);
                    roofLightLampNode.Scale = new Vector3(FLOOR_TILE_SIZE / 4, 2.0f, FLOOR_TILE_SIZE / 4);

                    var roofLightLampComponent = roofLightLampNode.CreateComponent<StaticModel>();

                    roofLightLampComponent.Model = CoreAssets.Models.Cylinder;
                    roofLightLampComponent.Material = Material.FromColor(Color.White);
                }
            }
        }

        private void SetupRoof()
        {
            var roofNode = CreateChild();

            roofNode.Position = new Vector3(0f, WALL_HEIGHT, 0f);
            roofNode.Scale = new Vector3(FLOOR_FULL_SIZE + FLOOR_FULL_SIZE / 2 + WALL_WEIGTH, 0.1f, FLOOR_FULL_SIZE + FLOOR_FULL_SIZE / 2 + WALL_WEIGTH);

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
            var floorNode = CreateChild();

            floorNode.Position = new Vector3(0.0f, 0.0f, 0.0f);
            floorNode.Scale = new Vector3(FLOOR_FULL_SIZE + FLOOR_FULL_SIZE / 2 + WALL_WEIGTH, 0.1f, FLOOR_FULL_SIZE + FLOOR_FULL_SIZE / 2 + WALL_WEIGTH);

            var floorComponent = floorNode.CreateComponent<StaticModel>();

            var material = new Material();

            material.SetTexture(TextureUnit.Diffuse, Application.Current.ResourceCache.GetTexture2D("Textures/Wood_Floor_007_COLOR.jpg"));
            material.SetTexture(TextureUnit.Normal, Application.Current.ResourceCache.GetTexture2D("Textures/Wood_Floor_007_NORM.jpg"));
            material.SetTexture(TextureUnit.Specular, Application.Current.ResourceCache.GetTexture2D("Textures/Wood_Floor_007_ROUGH.jpg"));
            material.SetTechnique(0u, CoreAssets.Techniques.DiffNormalSpec);
            material.SetUVTransform(Vector2.Zero, 1f, new Vector2(6.5f, 6.5f));

            floorComponent.Model = CoreAssets.Models.Box;
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

            var wallMaterial = new Material();

            wallMaterial.SetTexture(TextureUnit.Diffuse, texture2D);
            wallMaterial.SetTechnique(0u, CoreAssets.Techniques.Diff);
            wallMaterial.SetUVTransform(Vector2.Zero, 0f, new Vector2(5f, 5f));

            var centerWallDistance = FLOOR_FULL_SIZE / 2 + WALL_WEIGTH / 2;

            var northWallNode = CreateChild();

            northWallNode.Position = new Vector3(0f, WALL_HEIGHT / 2, centerWallDistance);
            northWallNode.Scale = new Vector3(FLOOR_FULL_SIZE, WALL_HEIGHT, WALL_WEIGTH);

            var northWallObject = northWallNode.CreateComponent<StaticModel>();

            northWallObject.Model = CoreAssets.Models.Box;
            northWallObject.Material = wallMaterial;

            var westWallNode = CreateChild();

            westWallNode.Position = new Vector3(-1 * centerWallDistance, WALL_HEIGHT / 2, 0f);
            westWallNode.Scale = new Vector3(WALL_WEIGTH, WALL_HEIGHT, FLOOR_FULL_SIZE);

            var westWallObject = westWallNode.CreateComponent<StaticModel>();

            westWallObject.Model = CoreAssets.Models.Box;
            westWallObject.Material = wallMaterial;

            var eastWallNode = CreateChild();

            eastWallNode.Position = new Vector3(centerWallDistance, WALL_HEIGHT / 2, 0f);
            eastWallNode.Scale = new Vector3(WALL_WEIGTH, WALL_HEIGHT, FLOOR_FULL_SIZE);

            var eastWallObject = eastWallNode.CreateComponent<StaticModel>();

            eastWallObject.Model = CoreAssets.Models.Box;
            eastWallObject.Material = wallMaterial;

            var leftSouthWallNode = CreateChild();

            leftSouthWallNode.Position = new Vector3(FLOOR_FULL_SIZE * 3 / 12 + FLOOR_FULL_SIZE / 24, WALL_HEIGHT / 2, -1 * centerWallDistance);
            leftSouthWallNode.Scale = new Vector3(FLOOR_FULL_SIZE * 5 / 12, WALL_HEIGHT, WALL_WEIGTH);

            var leftSouthWallObject = leftSouthWallNode.CreateComponent<StaticModel>();

            leftSouthWallObject.Model = CoreAssets.Models.Box;
            leftSouthWallObject.Material = wallMaterial;

            var rightSouthWallNode = CreateChild();

            rightSouthWallNode.Position = new Vector3(-1 * FLOOR_FULL_SIZE * 3 / 12 - FLOOR_FULL_SIZE / 24, WALL_HEIGHT / 2, -1 * centerWallDistance);
            rightSouthWallNode.Scale = new Vector3(FLOOR_FULL_SIZE * 5 / 12, WALL_HEIGHT, WALL_WEIGTH);

            var rightSouthWallObject = rightSouthWallNode.CreateComponent<StaticModel>();

            rightSouthWallObject.Model = CoreAssets.Models.Box;
            rightSouthWallObject.Material = wallMaterial;

            var middleSouthWallNode = CreateChild();

            middleSouthWallNode.Position = new Vector3(0.0f, WALL_HEIGHT * 9 / 10, -1 * centerWallDistance);
            middleSouthWallNode.Scale = new Vector3(FLOOR_FULL_SIZE * 2 / 12, WALL_HEIGHT / 5, WALL_WEIGTH);

            var middleSouthWallObject = middleSouthWallNode.CreateComponent<StaticModel>();

            middleSouthWallObject.Model = CoreAssets.Models.Box;
            middleSouthWallObject.Material = wallMaterial;

            var hallWallNode = CreateChild();

            hallWallNode.Position = new Vector3(0.0f, WALL_HEIGHT / 2, -1 * centerWallDistance - FLOOR_FULL_SIZE / 5);
            hallWallNode.Scale = new Vector3(FLOOR_FULL_SIZE, WALL_HEIGHT, WALL_WEIGTH);

            var hallWallObject = hallWallNode.CreateComponent<StaticModel>();

            hallWallObject.Model = CoreAssets.Models.Box;
            hallWallObject.Material = wallMaterial;
        }

        private void SetupIdentyFrames()
        {
            var identityFrameWight = 2.0f;

            var identityFrameNode = CreateChild();

            identityFrameNode.Position = new Vector3(0.0f, WALL_HEIGHT / 2, FLOOR_FULL_SIZE / 2 - identityFrameWight);
            identityFrameNode.Scale = new Vector3(48.0f, 96.0f, identityFrameWight);

            var identityFrameComponent = identityFrameNode.CreateComponent<StaticModel>();

            identityFrameComponent.Model = CoreAssets.Models.Box;
            identityFrameComponent.Material = Material.FromImage("Textures/Identity_Primary_FCBC.jpg");
        }

        private void SetupArtworkFrames(Models.Raw.ImageRawModel[] imageRawModels)
        {
            var pivotPositive = FLOOR_TILE_COUNT / 2;
            var pivotNegative = pivotPositive * -1;

            var referenceFrameWidth = 80f;

            int index = 0;

            // North Wall
            for (int x = pivotNegative; x <= pivotPositive; ++x)
            {
                if (x == 0 || x % 4 != 0) continue;

                var item = imageRawModels[index++];

                var pictureFrameNode = CreateChild();

                var frameWidth = referenceFrameWidth;
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

                var frameWidth = referenceFrameWidth;
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

                var frameWidth = referenceFrameWidth;
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

                var frameWidth = referenceFrameWidth;
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
