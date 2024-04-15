using MOROPO.FCBC.Galleries.Scenes.Contract;
using System.Linq;
using Urho;

namespace MOROPO.FCBC.Galleries.Scenes
{
    [Preserve]
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

            mainCamera.FarClip = FLOOR_FULL_SIZE * 2;

            SetupLights();

            SetupFloor();

            SetupRoof();

            SetupWalls();

            SetupSeatPositions();

            SetupArtworkFrames(Providers.IsolateExposureDataProvider.Instance.GetExhibits());

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

                    return;
                }

                var artWorkFrame = result.Value.Drawable as ArtWorkFrame;

                if (artWorkFrame != null)
                {
                    presenter.PresentArtworkDetailFor(artWorkFrame.ArtWork);

                    return;
                }

                var artWorkStripe = result.Value.Drawable as ArtWorkStripe;

                if (artWorkStripe != null)
                {
                    presenter.PresentArtworkDetailFor(artWorkStripe.ArtWork);

                    return;
                }
            }
        }

        private void SetupVintages()
        {
            var material = new Material();

            material.SetTexture(TextureUnit.Diffuse, Application.Current.ResourceCache.GetTexture2D("Textures/Wood_024_basecolor.jpg"));
            material.SetTexture(TextureUnit.Normal, Application.Current.ResourceCache.GetTexture2D("Textures/Wood_024_normal.jpg"));
            material.SetTexture(TextureUnit.Specular, Application.Current.ResourceCache.GetTexture2D("Textures/Wood_024_roughness.jpg"));
            material.SetTechnique(0u, CoreAssets.Techniques.DiffNormalSpec);

            var centerWallDistance = FLOOR_FULL_SIZE / 2 - WALL_VINTAGE_WEIGTH / 2;

            var northWallVintageNode = CreateChild();

            northWallVintageNode.Position = new Vector3(0.0f, WALL_VINTAGE_HEIGHT / 2, centerWallDistance);
            northWallVintageNode.Scale = new Vector3(FLOOR_FULL_SIZE, WALL_VINTAGE_HEIGHT, WALL_VINTAGE_WEIGTH);

            var northWallVintageComponent = northWallVintageNode.CreateComponent<StaticModel>();

            northWallVintageComponent.Model = CoreAssets.Models.Box;
            northWallVintageComponent.Material = material.Clone();

            var westWallVintageNode = CreateChild();

            westWallVintageNode.Position = new Vector3(-1 * centerWallDistance, WALL_VINTAGE_HEIGHT / 2, 0.0f);
            westWallVintageNode.Scale = new Vector3(WALL_VINTAGE_WEIGTH, WALL_VINTAGE_HEIGHT, FLOOR_FULL_SIZE - WALL_VINTAGE_WEIGTH * 2);

            var westWallVintageComponent = westWallVintageNode.CreateComponent<StaticModel>();

            westWallVintageComponent.Model = CoreAssets.Models.Box;
            westWallVintageComponent.Material = material.Clone();

            var eastWallVintageNode = CreateChild();

            eastWallVintageNode.Position = new Vector3(centerWallDistance, WALL_VINTAGE_HEIGHT / 2, 0.0f);
            eastWallVintageNode.Scale = new Vector3(WALL_VINTAGE_WEIGTH, WALL_VINTAGE_HEIGHT, FLOOR_FULL_SIZE - WALL_VINTAGE_WEIGTH * 2);

            var eastWallVintageComponent = eastWallVintageNode.CreateComponent<StaticModel>();

            eastWallVintageComponent.Model = CoreAssets.Models.Box;
            eastWallVintageComponent.Material = material.Clone();

            var leftSouthWallVintageNode = CreateChild();

            leftSouthWallVintageNode.Position = new Vector3(FLOOR_FULL_SIZE * 3 / 12 + FLOOR_FULL_SIZE / 24 - WALL_VINTAGE_WEIGTH / 2, WALL_VINTAGE_HEIGHT / 2, -1 * centerWallDistance);
            leftSouthWallVintageNode.Scale = new Vector3(FLOOR_FULL_SIZE * 5 / 12 + WALL_VINTAGE_WEIGTH, WALL_VINTAGE_HEIGHT, WALL_VINTAGE_WEIGTH);

            var leftSouthWallVintageComponent = leftSouthWallVintageNode.CreateComponent<StaticModel>();

            leftSouthWallVintageComponent.Model = CoreAssets.Models.Box;
            leftSouthWallVintageComponent.Material = material.Clone();

            var rightSouthWallVintageNode = CreateChild();

            rightSouthWallVintageNode.Position = new Vector3(-1 * FLOOR_FULL_SIZE * 3 / 12 - FLOOR_FULL_SIZE / 24 + WALL_VINTAGE_WEIGTH / 2, WALL_VINTAGE_HEIGHT / 2, -1 * centerWallDistance);
            rightSouthWallVintageNode.Scale = new Vector3(FLOOR_FULL_SIZE * 5 / 12 + WALL_VINTAGE_WEIGTH, WALL_VINTAGE_HEIGHT, WALL_VINTAGE_WEIGTH);

            var rightSouthWallVintageComponent = rightSouthWallVintageNode.CreateComponent<StaticModel>();

            rightSouthWallVintageComponent.Model = CoreAssets.Models.Box;
            rightSouthWallVintageComponent.Material = material.Clone();

            var leftOutsideDoorVintageNode = CreateChild();

            leftOutsideDoorVintageNode.Position = new Vector3(FLOOR_FULL_SIZE * 1 / 12 + 1.5f, WALL_HEIGHT * 4 / 10 + WALL_VINTAGE_HEIGHT, -1 * centerWallDistance);
            leftOutsideDoorVintageNode.Scale = new Vector3(WALL_VINTAGE_HEIGHT, WALL_HEIGHT * 8 / 10, WALL_VINTAGE_WEIGTH);

            var leftOutsideDoorVintageComponent = leftOutsideDoorVintageNode.CreateComponent<StaticModel>();

            leftOutsideDoorVintageComponent.Model = CoreAssets.Models.Box;
            leftOutsideDoorVintageComponent.Material = material.Clone();

            var rightOutsideDoorVintageNode = CreateChild();

            rightOutsideDoorVintageNode.Position = new Vector3(-1 * FLOOR_FULL_SIZE * 1 / 12 - 1.5f, WALL_HEIGHT * 4 / 10 + WALL_VINTAGE_HEIGHT, -1 * centerWallDistance);
            rightOutsideDoorVintageNode.Scale = new Vector3(WALL_VINTAGE_HEIGHT, WALL_HEIGHT * 8 / 10, WALL_VINTAGE_WEIGTH);

            var rightOutsideDoorVintageComponent = rightOutsideDoorVintageNode.CreateComponent<StaticModel>();

            rightOutsideDoorVintageComponent.Model = CoreAssets.Models.Box;
            rightOutsideDoorVintageComponent.Material = material.Clone();

            var middleOutsideDoorVintageNode = CreateChild();

            middleOutsideDoorVintageNode.Position = new Vector3(0.0f, WALL_HEIGHT * 8 / 10 + WALL_VINTAGE_HEIGHT / 2 - WALL_VINTAGE_WEIGTH / 2, -1 * centerWallDistance);
            middleOutsideDoorVintageNode.Scale = new Vector3(FLOOR_FULL_SIZE * 2 / 12 - 2.0f, WALL_VINTAGE_HEIGHT + WALL_VINTAGE_WEIGTH, WALL_VINTAGE_WEIGTH);

            var middleOutsideDoorVintageComponent = middleOutsideDoorVintageNode.CreateComponent<StaticModel>();

            middleOutsideDoorVintageComponent.Model = CoreAssets.Models.Box;
            middleOutsideDoorVintageComponent.Material = material.Clone();

            var middleInsideDoorVintageNode = CreateChild();

            middleInsideDoorVintageNode.Position = new Vector3(0.0f, WALL_HEIGHT * 8f / 10f - WALL_VINTAGE_WEIGTH / 2f, -1f * centerWallDistance - WALL_WEIGTH / 2f - 0.5f);
            middleInsideDoorVintageNode.Scale = new Vector3(FLOOR_FULL_SIZE * 2 / 12 - 2.0f, WALL_VINTAGE_WEIGTH, WALL_WEIGTH);

            var middleInsideDoorVintageComponent = middleInsideDoorVintageNode.CreateComponent<StaticModel>();

            middleInsideDoorVintageComponent.Model = CoreAssets.Models.Box;
            middleInsideDoorVintageComponent.Material = material.Clone();

            var rightInsideDoorVintageNode = CreateChild();

            rightInsideDoorVintageNode.Position = new Vector3(-1 * FLOOR_FULL_SIZE * 1 / 12 + WALL_VINTAGE_WEIGTH / 2, WALL_HEIGHT * 4 / 10, -1 * centerWallDistance - WALL_WEIGTH / 2f - WALL_VINTAGE_WEIGTH / 2);
            rightInsideDoorVintageNode.Scale = new Vector3(WALL_VINTAGE_WEIGTH, WALL_HEIGHT * 8 / 10, WALL_VINTAGE_HEIGHT);

            var rightInsideDoorVintageComponent = rightInsideDoorVintageNode.CreateComponent<StaticModel>();

            rightInsideDoorVintageComponent.Model = CoreAssets.Models.Box;
            rightInsideDoorVintageComponent.Material = material.Clone();

            var leftInsideDoorVintageNode = CreateChild();

            leftInsideDoorVintageNode.Position = new Vector3(FLOOR_FULL_SIZE * 1 / 12 - WALL_VINTAGE_WEIGTH / 2, WALL_HEIGHT * 4 / 10, -1 * centerWallDistance - WALL_WEIGTH / 2f - WALL_VINTAGE_WEIGTH / 2);
            leftInsideDoorVintageNode.Scale = new Vector3(WALL_VINTAGE_WEIGTH, WALL_HEIGHT * 8 / 10, WALL_VINTAGE_HEIGHT);

            var leftInsideDoorVintageComponent = leftInsideDoorVintageNode.CreateComponent<StaticModel>();

            leftInsideDoorVintageComponent.Model = CoreAssets.Models.Box;
            leftInsideDoorVintageComponent.Material = material.Clone();
        }
        
        private void SetupLights()
        {
            #region Ambient Lights

            var verticalMovement = -0.15f;
            var wallBrightness = 0.30f;
            var specularIntensity = 7;

            #region Roof Ambient Light
            var roofLightNode = CreateChild();

            roofLightNode.SetDirection(new Vector3(0.0f, 1.0f, 0.0f));

            var roofLightComponent = roofLightNode.CreateComponent<Light>();

            roofLightComponent.LightType = LightType.Directional;
            roofLightComponent.UsePhysicalValues = false;
            roofLightComponent.CastShadows = false;
            roofLightComponent.Brightness = 0.35f;
            roofLightComponent.Color = Color.White;
            roofLightComponent.SpecularIntensity = specularIntensity;
            #endregion

            #region North Ambient Light
            var floorNorthWallLightNode = CreateChild();

            floorNorthWallLightNode.SetDirection(new Vector3(0.0f, verticalMovement, 1.0f));

            var floorNorthWallLightComponent = floorNorthWallLightNode.CreateComponent<Light>();

            floorNorthWallLightComponent.LightType = LightType.Directional;
            floorNorthWallLightComponent.Color = Color.White;
            floorNorthWallLightComponent.Brightness = wallBrightness;
            floorNorthWallLightComponent.SpecularIntensity = specularIntensity;
            #endregion

            #region West
            var floorWestWallLightNode = CreateChild();

            floorWestWallLightNode.SetDirection(new Vector3(1.0f, verticalMovement, 0.0f));

            var floorWestWallLightComponent = floorWestWallLightNode.CreateComponent<Light>();

            floorWestWallLightComponent.LightType = LightType.Directional;
            floorWestWallLightComponent.Color = Color.White;
            floorWestWallLightComponent.Brightness = wallBrightness;
            floorWestWallLightComponent.SpecularIntensity = specularIntensity;
            #endregion

            #region East
            var floorEastWallLightNode = CreateChild();

            floorEastWallLightNode.SetDirection(new Vector3(-1.0f, verticalMovement, 0.0f));

            var floorEastWallLightComponent = floorEastWallLightNode.CreateComponent<Light>();

            floorEastWallLightComponent.LightType = LightType.Directional;
            floorEastWallLightComponent.Color = Color.White;
            floorEastWallLightComponent.Brightness = wallBrightness;
            floorEastWallLightComponent.SpecularIntensity = specularIntensity;
            #endregion

            #region South
            var floorSouthWallLightNode = CreateChild();

            floorSouthWallLightNode.SetDirection(new Vector3(0.0f, verticalMovement, -1.0f));

            var floorSouthWallLightComponent = floorSouthWallLightNode.CreateComponent<Light>();

            floorSouthWallLightComponent.LightType = LightType.Directional;
            floorSouthWallLightComponent.Color = Color.White;
            floorSouthWallLightComponent.Brightness = wallBrightness;
            floorSouthWallLightComponent.SpecularIntensity = specularIntensity;
            #endregion

            #endregion

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
                        roofPointLightComponent.UsePhysicalValues = false;
                        roofPointLightComponent.CastShadows = true;
                        roofPointLightComponent.Brightness = 0.75f;
                        roofPointLightComponent.Color = Color.White;
                        roofPointLightComponent.Range = 200.0f;
                        roofPointLightComponent.SpecularIntensity = 10;

                        var roofLightLampNode = CreateChild();

                        roofLightLampNode.Position = new Vector3(roofPointLightNode.Position.X - x / 4 * 1.5f, WALL_HEIGHT - 0.5f, roofPointLightNode.Position.Z - y / 4 * 1.5f);
                        roofLightLampNode.Scale = new Vector3(FLOOR_TILE_SIZE / 3, 2.0f, FLOOR_TILE_SIZE / 3);

                        var roofLightLampComponent = roofLightLampNode.CreateComponent<StaticModel>();

                        roofLightLampComponent.Model = CoreAssets.Models.Cylinder;
                        roofLightLampComponent.Material = Material.FromColor(Color.White);

                        roofLightLampComponent.Material.SetTechnique(0, CoreAssets.Techniques.NoTextureUnlit);
                    }
                }
            }

            for (int x = pivotNegative + 1; x <= pivotPositive; ++x)
            {
                if (x == 0 || System.Math.Abs(x) % 4 == 0)
                {
                    var roofPointLightNode = CreateChild();

                    roofPointLightNode.Position = new Vector3(x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT - 20.0f, -13 * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));

                    var roofPointLightComponent = roofPointLightNode.CreateComponent<Light>();

                    roofPointLightComponent.LightType = LightType.Point;
                    roofPointLightComponent.UsePhysicalValues = false;
                    roofPointLightComponent.CastShadows = true;
                    roofPointLightComponent.Brightness = 0.65f;
                    roofPointLightComponent.Color = Color.White;
                    roofPointLightComponent.Range = 200.0f;
                    roofPointLightComponent.SpecularIntensity = 3;

                    var roofLightLampNode = CreateChild();

                    roofLightLampNode.Position = new Vector3(roofPointLightNode.Position.X - x / 4 * 1.5f, WALL_HEIGHT - 0.5f, roofPointLightNode.Position.Z + 13 / 4 * 1.5f);
                    roofLightLampNode.Scale = new Vector3(FLOOR_TILE_SIZE / 4, 1.0f, FLOOR_TILE_SIZE / 4);

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

            var material = new Material();

            material.SetTexture(TextureUnit.Diffuse, Application.Current.ResourceCache.GetTexture2D("Textures/Plaster_Rough_001_COLOR.jpg"));
            material.SetTexture(TextureUnit.Normal, Application.Current.ResourceCache.GetTexture2D("Textures/Plaster_Rough_001_NORM.jpg"));
            material.SetTexture(TextureUnit.Specular, Application.Current.ResourceCache.GetTexture2D("Textures/Plaster_Rough_001_ROUGH.jpg"));
            material.SetTechnique(0u, CoreAssets.Techniques.DiffNormalSpec);
            material.SetUVTransform(Vector2.Zero, 0f, new Vector2(roofNode.Scale.X / roofNode.Scale.Y * 3.0f, 3.0f));

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

            material.SetTexture(TextureUnit.Diffuse, Application.Current.ResourceCache.GetTexture2D("Textures/Marble_Carrara_003_COLOR.jpg"));
            material.SetTexture(TextureUnit.Normal, Application.Current.ResourceCache.GetTexture2D("Textures/Marble_Carrara_003_NORM.jpg"));
            material.SetTexture(TextureUnit.Specular, Application.Current.ResourceCache.GetTexture2D("Textures/Marble_Carrara_003_ROUGH.jpg"));
            material.SetTechnique(0u, CoreAssets.Techniques.DiffNormalSpec);
            material.SetUVTransform(Vector2.Zero, 0f, new Vector2(7.5f, 7.5f));

            floorComponent.Model = CoreAssets.Models.Box;
            floorComponent.Material = material;
        }

        private void SetupSeatPositions()
        {
            var seatUniformScale = 15.0f;

            var material = Application.Current.ResourceCache.GetMaterial("Materials/Leather_Beige.xml");
            var model = Application.Current.ResourceCache.GetModel("Models/Leather_Pouf_Sofa.mdl");

            var pivotPositive = FLOOR_TILE_COUNT / 2;
            var pivotNegative = pivotPositive * -1;

            var flootSegmentSize = FLOOR_TILE_SIZE + FLOOR_TILE_SPACING;

            for (int y = pivotNegative; y <= pivotPositive; y += 2)
            {
                for (int x = pivotNegative; x <= pivotPositive; x += 2)
                {
                    if (System.Math.Abs(x) == 6 && System.Math.Abs(y) == 6 ||
                        x == 0 && System.Math.Abs(y) == 6 ||
                        System.Math.Abs(x) == 6 && y == 0)
                    {
                        var centerSeatNode = CreateChild();

                        centerSeatNode.Position = new Vector3(x * flootSegmentSize, 3.36f, y * flootSegmentSize);
                        
                        centerSeatNode.SetScale(seatUniformScale);

                        var centerSeatComponent = centerSeatNode.CreateComponent<MoveMark>();

                        centerSeatComponent.Model = model;
                        centerSeatComponent.Material = material;
                        centerSeatComponent.CastShadows = true;

                        if (x == 0 || y == 0)
                        {
                            var leftSeatMiddleNode = CreateChild();

                            leftSeatMiddleNode.Position = new Vector3(x == 0 ? -1 * flootSegmentSize : x * flootSegmentSize, 3.36f, y == 0 ? -1 * flootSegmentSize : y * flootSegmentSize);

                            leftSeatMiddleNode.SetScale(seatUniformScale);

                            var leftSeatMiddleComponent = leftSeatMiddleNode.CreateComponent<MoveMark>();

                            leftSeatMiddleComponent.Model = model;
                            leftSeatMiddleComponent.Material = material;
                            leftSeatMiddleComponent.CastShadows = true;

                            var rightMidddleSeatNode = CreateChild();

                            rightMidddleSeatNode.Position = new Vector3(x == 0 ? flootSegmentSize : x * flootSegmentSize, 3.36f, y == 0 ? flootSegmentSize : y * flootSegmentSize);

                            rightMidddleSeatNode.SetScale(seatUniformScale);

                            var rightSeatMiddleComponent = rightMidddleSeatNode.CreateComponent<MoveMark>();

                            rightSeatMiddleComponent.Model = model;
                            rightSeatMiddleComponent.Material = material;
                            rightSeatMiddleComponent.CastShadows = true;
                        }

                        if (x != 0 && y != 0)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                var pivot1X = i == 0 ? x : x + (x > 0 ? -1 : 1);
                                var pivot1Y = i == 1 ? y : y + (y > 0 ? -1 : 1);

                                var pivot2X = i == 0 ? x : x + (x > 0 ? -2 : 2);
                                var pivot2Y = i == 1 ? y : y + (y > 0 ? -2 : 2);

                                if (i == 0)
                                {
                                    var leftSeat1CornerNode = CreateChild();

                                    leftSeat1CornerNode.Position = new Vector3(pivot1X * flootSegmentSize, 3.36f, pivot1Y * flootSegmentSize);

                                    leftSeat1CornerNode.SetScale(seatUniformScale);

                                    var leftSeat1CornetComponent = leftSeat1CornerNode.CreateComponent<MoveMark>();

                                    leftSeat1CornetComponent.Model = model;
                                    leftSeat1CornetComponent.Material = material;
                                    leftSeat1CornetComponent.CastShadows = true;

                                    var leftSeat2CornerNode = CreateChild();

                                    leftSeat2CornerNode.Position = new Vector3(pivot2X * flootSegmentSize, 3.36f, pivot2Y * flootSegmentSize);

                                    leftSeat2CornerNode.SetScale(seatUniformScale);

                                    var leftSeat2CornetComponent = leftSeat2CornerNode.CreateComponent<MoveMark>();

                                    leftSeat2CornetComponent.Model = model;
                                    leftSeat2CornetComponent.Material = material;
                                    leftSeat2CornetComponent.CastShadows = true;
                                }
                                else if (i == 1)
                                {
                                    var rightSeat1CornerNode = CreateChild();

                                    rightSeat1CornerNode.Position = new Vector3(pivot1X * flootSegmentSize, 3.36f, pivot1Y * flootSegmentSize);

                                    rightSeat1CornerNode.SetScale(seatUniformScale);

                                    var rightSeat1CornerComponent = rightSeat1CornerNode.CreateComponent<MoveMark>();

                                    rightSeat1CornerComponent.Model = model;
                                    rightSeat1CornerComponent.Material = material;
                                    rightSeat1CornerComponent.CastShadows = true;

                                    var rightSeat2CornerNode = CreateChild();

                                    rightSeat2CornerNode.Position = new Vector3(pivot2X * flootSegmentSize, 3.36f, pivot2Y * flootSegmentSize);

                                    rightSeat2CornerNode.SetScale(seatUniformScale);

                                    var rightSeat2CornerComponent = rightSeat2CornerNode.CreateComponent<MoveMark>();

                                    rightSeat2CornerComponent.Model = model;
                                    rightSeat2CornerComponent.Material = material;
                                    rightSeat2CornerComponent.CastShadows = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SetupWalls()
        {
            var materialUVTransformScale = 15.0f;

            var material = new Material();

            material.SetTexture(TextureUnit.Diffuse, Application.Current.ResourceCache.GetTexture2D("Textures/Plaster_Rough_001_COLOR.jpg"));
            material.SetTexture(TextureUnit.Normal, Application.Current.ResourceCache.GetTexture2D("Textures/Plaster_Rough_001_NORM.jpg"));
            material.SetTexture(TextureUnit.Specular, Application.Current.ResourceCache.GetTexture2D("Textures/Plaster_Rough_001_ROUGH.jpg"));
            material.SetTechnique(0u, CoreAssets.Techniques.DiffNormalSpec);

            var centerWallDistance = FLOOR_FULL_SIZE / 2 + WALL_WEIGTH / 2;

            var northWallNode = CreateChild();

            northWallNode.Position = new Vector3(0f, WALL_HEIGHT / 2, centerWallDistance);
            northWallNode.Scale = new Vector3(FLOOR_FULL_SIZE, WALL_HEIGHT, WALL_WEIGTH);

            var northWallObject = northWallNode.CreateComponent<StaticModel>();

            northWallObject.Model = CoreAssets.Models.Box;
            northWallObject.Material = material.Clone();
            northWallObject.Material.SetUVTransform(Vector2.Zero, 0f, new Vector2(northWallNode.Scale.X / northWallNode.Scale.Y * materialUVTransformScale, materialUVTransformScale));

            var westWallNode = CreateChild();

            westWallNode.Position = new Vector3(-1 * centerWallDistance, WALL_HEIGHT / 2, 0f);
            westWallNode.Scale = new Vector3(WALL_WEIGTH, WALL_HEIGHT, FLOOR_FULL_SIZE);

            var westWallObject = westWallNode.CreateComponent<StaticModel>();

            westWallObject.Model = CoreAssets.Models.Box;
            westWallObject.Material = material.Clone();
            westWallObject.Material.SetUVTransform(Vector2.Zero, 0f, new Vector2(westWallNode.Scale.Z / westWallNode.Scale.Y * materialUVTransformScale, materialUVTransformScale));

            var eastWallNode = CreateChild();

            eastWallNode.Position = new Vector3(centerWallDistance, WALL_HEIGHT / 2, 0f);
            eastWallNode.Scale = new Vector3(WALL_WEIGTH, WALL_HEIGHT, FLOOR_FULL_SIZE);

            var eastWallObject = eastWallNode.CreateComponent<StaticModel>();

            eastWallObject.Model = CoreAssets.Models.Box;
            eastWallObject.Material = material.Clone();
            eastWallObject.Material.SetUVTransform(Vector2.Zero, 0f, new Vector2(eastWallNode.Scale.Z / eastWallNode.Scale.Y * materialUVTransformScale, materialUVTransformScale));

            var leftSouthWallNode = CreateChild();

            leftSouthWallNode.Position = new Vector3(FLOOR_FULL_SIZE * 3 / 12 + FLOOR_FULL_SIZE / 24, WALL_HEIGHT / 2, -1 * centerWallDistance);
            leftSouthWallNode.Scale = new Vector3(FLOOR_FULL_SIZE * 5 / 12, WALL_HEIGHT, WALL_WEIGTH);

            var leftSouthWallObject = leftSouthWallNode.CreateComponent<StaticModel>();

            leftSouthWallObject.Model = CoreAssets.Models.Box;
            leftSouthWallObject.Material = material.Clone();
            leftSouthWallObject.Material.SetUVTransform(Vector2.Zero, 0f, new Vector2(leftSouthWallNode.Scale.X / leftSouthWallNode.Scale.Y * materialUVTransformScale, materialUVTransformScale));

            var rightSouthWallNode = CreateChild();

            rightSouthWallNode.Position = new Vector3(-1 * FLOOR_FULL_SIZE * 3 / 12 - FLOOR_FULL_SIZE / 24, WALL_HEIGHT / 2, -1 * centerWallDistance);
            rightSouthWallNode.Scale = new Vector3(FLOOR_FULL_SIZE * 5 / 12, WALL_HEIGHT, WALL_WEIGTH);

            var rightSouthWallObject = rightSouthWallNode.CreateComponent<StaticModel>();

            rightSouthWallObject.Model = CoreAssets.Models.Box;
            rightSouthWallObject.Material = material.Clone();
            rightSouthWallObject.Material.SetUVTransform(Vector2.Zero, 0f, new Vector2(rightSouthWallNode.Scale.X / rightSouthWallNode.Scale.Y * materialUVTransformScale, materialUVTransformScale));

            var middleSouthWallNode = CreateChild();

            middleSouthWallNode.Position = new Vector3(0.0f, WALL_HEIGHT * 9 / 10, -1 * centerWallDistance);
            middleSouthWallNode.Scale = new Vector3(FLOOR_FULL_SIZE * 2 / 12, WALL_HEIGHT / 5, WALL_WEIGTH);

            var middleSouthWallObject = middleSouthWallNode.CreateComponent<StaticModel>();

            middleSouthWallObject.Model = CoreAssets.Models.Box;
            middleSouthWallObject.Material = material.Clone();
            middleSouthWallObject.Material.SetUVTransform(Vector2.Zero, 0f, new Vector2(middleSouthWallNode.Scale.X / middleSouthWallNode.Scale.Y * materialUVTransformScale, materialUVTransformScale));

            var hallWallNode = CreateChild();

            hallWallNode.Position = new Vector3(0.0f, WALL_HEIGHT / 2, -1 * centerWallDistance - FLOOR_FULL_SIZE / 4);
            hallWallNode.Scale = new Vector3(FLOOR_FULL_SIZE, WALL_HEIGHT, WALL_WEIGTH);

            var hallWallObject = hallWallNode.CreateComponent<StaticModel>();

            hallWallObject.Model = CoreAssets.Models.Box;
            hallWallObject.Material = new Material();
            hallWallObject.Material.SetTexture(TextureUnit.Diffuse, Application.Current.ResourceCache.GetTexture2D("Textures/Geometric_wallpaper_basecolor.jpg"));
            hallWallObject.Material.SetTexture(TextureUnit.Normal, Application.Current.ResourceCache.GetTexture2D("Textures/Geometric_wallpaper_normal.jpg"));
            hallWallObject.Material.SetTexture(TextureUnit.Specular, Application.Current.ResourceCache.GetTexture2D("Textures/Geometric_wallpaper_roughness.jpg"));
            hallWallObject.Material.SetTechnique(0u, CoreAssets.Techniques.DiffNormalSpec);
            hallWallObject.Material.SetUVTransform(Vector2.Zero, 0f, new Vector2(hallWallNode.Scale.X / hallWallNode.Scale.Y * 1.5f, 1.5f));
        }

        private void SetupIdentyFrames()
        {
            var identityFrameWeight = 2.0f;

            var identityFrameNode = CreateChild();

            identityFrameNode.Position = new Vector3(0.0f, WALL_HEIGHT / 2, FLOOR_FULL_SIZE / 2 - identityFrameWeight);
            identityFrameNode.Scale = new Vector3(46.0f, 98.0f, identityFrameWeight);

            var identityFrameComponent = identityFrameNode.CreateComponent<StaticModel>();

            identityFrameComponent.Model = CoreAssets.Models.Box;
            identityFrameComponent.Material = Material.FromColor(Color.White);

            var identitySpriteNode = CreateChild();

            identitySpriteNode.Position = new Vector3(identityFrameNode.Position.X, identityFrameNode.Position.Y, identityFrameNode.Position.Z - identityFrameWeight / 2 - 1.0f);

            identitySpriteNode.SetScale(4.0f);

            var identitySpriteComponent = identitySpriteNode.CreateComponent<Urho.Urho2D.StaticSprite2D>();

            identitySpriteComponent.Sprite = Application.Current.ResourceCache.GetSprite2D("Sprites/Identity_Primary_FCBC.jpg");
        }

        private void SetupArtworkFrames(Models.View.ExhibitViewModel[] exhibits)
        {
            var pivotPositive = FLOOR_TILE_COUNT / 2;
            var pivotNegative = pivotPositive * -1;

            var referenceFrameWidth = 80f;

            var pictureFrameComponentMaterial = new Material();

            pictureFrameComponentMaterial.SetTexture(TextureUnit.Diffuse, Application.Current.ResourceCache.GetTexture2D("Textures/Wood_024_basecolor.jpg"));
            pictureFrameComponentMaterial.SetTexture(TextureUnit.Normal, Application.Current.ResourceCache.GetTexture2D("Textures/Wood_024_normal.jpg"));
            pictureFrameComponentMaterial.SetTexture(TextureUnit.Specular, Application.Current.ResourceCache.GetTexture2D("Textures/Wood_024_roughness.jpg"));
            pictureFrameComponentMaterial.SetTechnique(0u, CoreAssets.Techniques.DiffNormalSpec);

            int index = 1;

            // North Wall
            for (int x = pivotNegative; x <= pivotPositive; ++x)
            {
                if (x == 0 || x % 4 != 0) continue;

                var exhibit = exhibits.FirstOrDefault(item => item.Index == index);

                index += 1;

                if (exhibit == null) continue;

                var frameWidth = referenceFrameWidth;
                var frameHeight = frameWidth;

                var artWork = exhibit.ArtWorks[0];

                var zoom = (artWork.FeatureImage.Width + artWork.FeatureImage.Height) / 2 / frameWidth;

                if (artWork.FeatureImage.Width != artWork.FeatureImage.Height)
                {
                    frameWidth = artWork.FeatureImage.Width / zoom;

                    frameHeight = artWork.FeatureImage.Height / zoom;
                }

                var pictureFrameNode = CreateChild();

                pictureFrameNode.Position = new Vector3(x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT / 2, FLOOR_FULL_SIZE / 2 - 0.5f);
                pictureFrameNode.Scale = new Vector3(frameWidth, frameHeight, 1.0f);

                var pictureFrameComponent = pictureFrameNode.CreateComponent<ArtWorkFrame>();

                pictureFrameComponent.ArtWork = artWork;

                var artWorkSprite = new Urho.Urho2D.Sprite2D();

                using (var urhoBuffer = new MemoryBuffer(artWork.FeatureImage.Data))
                    artWorkSprite.Load(urhoBuffer);

                pictureFrameComponent.Model = CoreAssets.Models.Box;

                if (Providers.IsolateExposureDataProvider.Instance.FrameBaseColor == null)
                {
                    pictureFrameComponent.Material = pictureFrameComponentMaterial;
                }
                else
                {
                    pictureFrameComponent.Material = Material.FromColor(Providers.IsolateExposureDataProvider.Instance.FrameBaseColor.Value);
                }

                var pictureSpriteNode = CreateChild();

                pictureSpriteNode.Position = new Vector3(pictureFrameNode.Position.X, pictureFrameNode.Position.Y, pictureFrameNode.Position.Z - 1.0f);

                var pictureSpriteComponent = pictureSpriteNode.CreateComponent<ArtWorkStripe>();

                pictureSpriteComponent.ArtWork = artWork;
                pictureSpriteComponent.Sprite = artWorkSprite;

                pictureSpriteNode.SetScale(frameWidth / pictureSpriteComponent.WorldBoundingBox.Size.X * 0.96f);

                var interactiveIndicatorSpriteNode = CreateChild();

                interactiveIndicatorSpriteNode.Position = new Vector3(pictureFrameNode.Position.X + pictureSpriteComponent.WorldBoundingBox.Size.X * 0.5f + 5.0f, pictureFrameNode.Position.Y - pictureSpriteComponent.WorldBoundingBox.Size.Y * 0.5f - 5.0f, pictureSpriteNode.Position.Z - 1.0f);

                interactiveIndicatorSpriteNode.SetScale(7.0f);

                var interactiveIndicatorSpriteComponent = interactiveIndicatorSpriteNode.CreateComponent<Urho.Urho2D.StaticSprite2D>();

                interactiveIndicatorSpriteComponent.Sprite = Application.Current.ResourceCache.GetSprite2D("Sprites/Touch_Call_Action_Light.png");
            }

            // South Wall
            for (int x = pivotNegative; x <= pivotPositive; ++x)
            {
                if (x == 0 || x % 4 != 0) continue;

                var exhibit = exhibits.FirstOrDefault(item => item.Index == index);

                index += 1;

                if (exhibit == null) continue;

                var pictureFrameNode = CreateChild();

                var frameWidth = referenceFrameWidth;
                var frameHeight = frameWidth;

                var artWork = exhibit.ArtWorks[0];

                var zoom = (artWork.FeatureImage.Width + artWork.FeatureImage.Height) / 2 / frameWidth;

                if (artWork.FeatureImage.Width != artWork.FeatureImage.Height)
                {
                    frameWidth = artWork.FeatureImage.Width / zoom;

                    frameHeight = artWork.FeatureImage.Height / zoom;
                }

                pictureFrameNode.Position = new Vector3(-1 * x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING), WALL_HEIGHT / 2, -1 * FLOOR_FULL_SIZE / 2 + 0.5f);
                pictureFrameNode.Scale = new Vector3(frameWidth, frameHeight, 1.0f);

                var pictureFrameComponent = pictureFrameNode.CreateComponent<ArtWorkFrame>();

                pictureFrameComponent.ArtWork = artWork;

                var artWorkSprite = new Urho.Urho2D.Sprite2D();

                using (var urhoBuffer = new MemoryBuffer(artWork.FeatureImage.Data))
                    artWorkSprite.Load(urhoBuffer);

                pictureFrameComponent.Model = CoreAssets.Models.Box;

                if (Providers.IsolateExposureDataProvider.Instance.FrameBaseColor == null)
                {
                    pictureFrameComponent.Material = pictureFrameComponentMaterial;
                }
                else
                {
                    pictureFrameComponent.Material = Material.FromColor(Providers.IsolateExposureDataProvider.Instance.FrameBaseColor.Value);
                }

                var pictureSpriteNode = CreateChild();

                pictureSpriteNode.Position = new Vector3(pictureFrameNode.Position.X, pictureFrameNode.Position.Y, pictureFrameNode.Position.Z + 1.0f);
                pictureSpriteNode.Rotation = new Quaternion(0.0f, 1.0f, 0.0f, 0.0f);

                var pictureSpriteComponent = pictureSpriteNode.CreateComponent<ArtWorkStripe>();

                pictureSpriteComponent.ArtWork = artWork;
                pictureSpriteComponent.Sprite = artWorkSprite;

                pictureSpriteNode.SetScale(frameWidth / pictureSpriteComponent.WorldBoundingBox.Size.X * 0.96f);

                var interactiveIndicatorSpriteNode = CreateChild();

                interactiveIndicatorSpriteNode.Position = new Vector3(pictureFrameNode.Position.X - pictureSpriteComponent.WorldBoundingBox.Size.X * 0.5f - 5.0f, pictureFrameNode.Position.Y - pictureSpriteComponent.WorldBoundingBox.Size.Y * 0.5f - 5.0f, pictureSpriteNode.Position.Z - 1.0f);
                interactiveIndicatorSpriteNode.Rotation = new Quaternion(0.0f, 1.0f, 0.0f, 0.0f);

                interactiveIndicatorSpriteNode.SetScale(7.0f);

                var interactiveIndicatorSpriteComponent = interactiveIndicatorSpriteNode.CreateComponent<Urho.Urho2D.StaticSprite2D>();

                interactiveIndicatorSpriteComponent.Sprite = Application.Current.ResourceCache.GetSprite2D("Sprites/Touch_Call_Action_Light.png");
            }

            // East Wall
            for (int x = pivotNegative; x <= pivotPositive; ++x)
            {
                if (x % 4 != 0) continue;

                var exhibit = exhibits.FirstOrDefault(item => item.Index == index);

                index += 1;

                if (exhibit == null) continue;

                var pictureFrameNode = CreateChild();

                var frameWidth = referenceFrameWidth;
                var frameHeight = frameWidth;

                var artWork = exhibit.ArtWorks[0];

                var zoom = (artWork.FeatureImage.Width + artWork.FeatureImage.Height) / 2 / frameWidth;

                if (artWork.FeatureImage.Width != artWork.FeatureImage.Height)
                {
                    frameWidth = artWork.FeatureImage.Width / zoom;

                    frameHeight = artWork.FeatureImage.Height / zoom;
                }

                pictureFrameNode.Position = new Vector3(FLOOR_FULL_SIZE / 2 - 0.5f, WALL_HEIGHT / 2, x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));
                pictureFrameNode.Scale = new Vector3(1.0f, frameHeight, frameWidth);

                var pictureFrameComponent = pictureFrameNode.CreateComponent<ArtWorkFrame>();

                pictureFrameComponent.ArtWork = artWork;

                var artWorkSprite = new Urho.Urho2D.Sprite2D();

                using (var urhoBuffer = new MemoryBuffer(artWork.FeatureImage.Data))
                    artWorkSprite.Load(urhoBuffer);

                pictureFrameComponent.Model = CoreAssets.Models.Box;

                if (Providers.IsolateExposureDataProvider.Instance.FrameBaseColor == null)
                {
                    pictureFrameComponent.Material = pictureFrameComponentMaterial;
                }
                else
                {
                    pictureFrameComponent.Material = Material.FromColor(Providers.IsolateExposureDataProvider.Instance.FrameBaseColor.Value);
                }

                var pictureSpriteNode = CreateChild();

                pictureSpriteNode.Position = new Vector3(pictureFrameNode.Position.X - 1.0f, pictureFrameNode.Position.Y, pictureFrameNode.Position.Z);
                pictureSpriteNode.Rotation = new Quaternion(0, 0.7071068f, 0, 0.7071068f);

                var pictureSpriteComponent = pictureSpriteNode.CreateComponent<ArtWorkStripe>();

                pictureSpriteComponent.ArtWork = artWork;
                pictureSpriteComponent.Sprite = artWorkSprite;

                pictureSpriteNode.SetScale(frameWidth / pictureSpriteComponent.WorldBoundingBox.Size.Z * 0.96f);

                var interactiveIndicatorSpriteNode = CreateChild();

                interactiveIndicatorSpriteNode.Position = new Vector3(pictureFrameNode.Position.X - 1.0f, pictureFrameNode.Position.Y - pictureSpriteComponent.WorldBoundingBox.Size.Y * 0.5f - 5.0f, pictureSpriteNode.Position.Z - pictureSpriteComponent.WorldBoundingBox.Size.Z * 0.5f - 5.0f);
                interactiveIndicatorSpriteNode.Rotation = new Quaternion(0, 0.7071068f, 0, 0.7071068f);

                interactiveIndicatorSpriteNode.SetScale(7.0f);

                var interactiveIndicatorSpriteComponent = interactiveIndicatorSpriteNode.CreateComponent<Urho.Urho2D.StaticSprite2D>();

                interactiveIndicatorSpriteComponent.Sprite = Application.Current.ResourceCache.GetSprite2D("Sprites/Touch_Call_Action_Light.png");
            }

            // West Wall
            for (int x = pivotNegative; x <= pivotPositive; ++x)
            {
                if (x % 4 != 0) continue;

                var exhibit = exhibits.FirstOrDefault(item => item.Index == index);

                index += 1;

                if (exhibit == null) continue;

                var pictureFrameNode = CreateChild();

                var frameWidth = referenceFrameWidth;
                var frameHeight = frameWidth;

                var artWork = exhibit.ArtWorks[0];

                var zoom = (artWork.FeatureImage.Width + artWork.FeatureImage.Height) / 2 / frameWidth;

                if (artWork.FeatureImage.Width != artWork.FeatureImage.Height)
                {
                    frameWidth = artWork.FeatureImage.Width / zoom;

                    frameHeight = artWork.FeatureImage.Height / zoom;
                }

                pictureFrameNode.Position = new Vector3(-1 * FLOOR_FULL_SIZE / 2 + 0.5f, WALL_HEIGHT / 2, -1 * x * (FLOOR_TILE_SIZE + FLOOR_TILE_SPACING));
                pictureFrameNode.Scale = new Vector3(1.0f, frameHeight, frameWidth);

                var pictureFrameComponent = pictureFrameNode.CreateComponent<ArtWorkFrame>();

                pictureFrameComponent.ArtWork = artWork;

                var artWorkSprite = new Urho.Urho2D.Sprite2D();

                using (var urhoBuffer = new MemoryBuffer(artWork.FeatureImage.Data))
                    artWorkSprite.Load(urhoBuffer);

                pictureFrameComponent.Model = CoreAssets.Models.Box;

                if (Providers.IsolateExposureDataProvider.Instance.FrameBaseColor == null)
                {
                    pictureFrameComponent.Material = pictureFrameComponentMaterial;
                }
                else
                {
                    pictureFrameComponent.Material = Material.FromColor(Providers.IsolateExposureDataProvider.Instance.FrameBaseColor.Value);
                }

                var pictureSpriteNode = CreateChild();

                pictureSpriteNode.Position = new Vector3(pictureFrameNode.Position.X + 1.0f, pictureFrameNode.Position.Y, pictureFrameNode.Position.Z);
                pictureSpriteNode.Rotation = new Quaternion(0, 0.7071068f, 0, -0.7071068f);

                var pictureSpriteComponent = pictureSpriteNode.CreateComponent<ArtWorkStripe>();

                pictureSpriteComponent.ArtWork = artWork;
                pictureSpriteComponent.Sprite = artWorkSprite;

                pictureSpriteNode.SetScale(frameWidth / pictureSpriteComponent.WorldBoundingBox.Size.Z * 0.96f);

                var interactiveIndicatorSpriteNode = CreateChild();

                interactiveIndicatorSpriteNode.Position = new Vector3(pictureFrameNode.Position.X + 1.0f, pictureFrameNode.Position.Y - pictureSpriteComponent.WorldBoundingBox.Size.Y * 0.5f - 5.0f, pictureSpriteNode.Position.Z + pictureSpriteComponent.WorldBoundingBox.Size.Z * 0.5f + 5.0f);
                interactiveIndicatorSpriteNode.Rotation = new Quaternion(0, 0.7071068f, 0, -0.7071068f);

                interactiveIndicatorSpriteNode.SetScale(7.0f);

                var interactiveIndicatorSpriteComponent = interactiveIndicatorSpriteNode.CreateComponent<Urho.Urho2D.StaticSprite2D>();

                interactiveIndicatorSpriteComponent.Sprite = Application.Current.ResourceCache.GetSprite2D("Sprites/Touch_Call_Action_Light.png");
            }
        }
    }

    internal class MoveMark : StaticModel
    {
    }

    internal class ArtWorkFrame : StaticModel
    {
        public Models.View.ArtWorkViewModel ArtWork { get; set; }
    }

    internal class ArtWorkStripe : Urho.Urho2D.StaticSprite2D
    {
        public Models.View.ArtWorkViewModel ArtWork { get; set; }
    }
}
