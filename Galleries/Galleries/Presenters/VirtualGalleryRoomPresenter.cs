using MOROPO.FCBC.Galleries.Models;
using Urho;
using Urho.Actions;

namespace MOROPO.FCBC.Galleries.Presenters
{
    [Preserve]
    internal class VirtualGalleryRoomPresenter : Application
    {
        protected const float TOUCH_SENSITIVITY = 2;
        protected const float MOVEMENT_SIZE = 10.0f;
        protected const float MOVEMENT_SPEED = 0.25f;

        private float yaw;
        private float pitch;
        private bool isEventHandlingBind;

        private System.DateTime? showcaseStartAt;

        private Scenes.Contract.AbstractRoomScene scene;

        public VirtualGalleryRoomPresenter(ApplicationOptions options) : base(options)
        {
            UnhandledException += VirtualGalleryRoomPresenter_UnhandledException;
        }

        public Models.Page.ExposureRoomPageModel ExposureRoomPageModel { get; set; }

        protected override void Start()
        {
            base.Start();

            Log.LogLevel = LogLevel.Warning;
            Log.LogMessage += e => System.Diagnostics.Debug.WriteLine($"[Urho] {e.Message}"); ;

            scene = new Scenes.SquareRoomScene(this);

            Input.Enabled = true;

            BindSceneEvents();

            // Viewport
            var viewport = new Viewport(Context, scene, scene.MainCamera, null);

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.UWP)
            {
                viewport.RenderPath.Append(CoreAssets.PostProcess.FXAA3);
                viewport.RenderPath.Append(CoreAssets.PostProcess.FXAA2);
            }

            Renderer.HDRRendering = true;

            Renderer.SetViewport(0, viewport);

            // TODO Decide if return this behavior!!
            //scene.MainCamera.Node.RunActionsAsync(new RepeatForever(new RotateBy(duration: 1.0f, deltaAngleX: 0.0f, deltaAngleY: 3.0f, deltaAngleZ: 0.0f)));

            //showcaseStartAt = System.DateTime.Now;

            //try
            //{
            //    var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            //    System.Diagnostics.Debug.WriteLine(path);

            //    scene.SaveXml(System.IO.Path.Combine(path, "SquareRoomScene.xml"));
            //}
            //catch (System.Exception _)
            //{
            //}
        }

        public void LoadSceneState(VirtualGalleryPresentationStateModel model)
        {
            yaw = model.Yaw;
            pitch = model.Pitch;

            scene.MainCamera.Node.Position = model.CameraPosition;
            scene.MainCamera.Node.Rotation = model.CameraRotation;
        }

        public VirtualGalleryPresentationStateModel BuildSceneState()
        {
            return new VirtualGalleryPresentationStateModel
            {
                Yaw = yaw,
                Pitch = pitch,
                CameraPosition = scene.MainCamera.Node.Position,
                CameraRotation = scene.MainCamera.Node.Rotation
            };
        }

        protected override void Stop()
        {
            UnbindSceneEvents();

            base.Stop();
        }

        protected override void OnUpdate(float timeStep)
        {
            MoveCameraByTouches(timeStep);

            base.OnUpdate(timeStep);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            ExposureRoomPageModel = null;

            UnhandledException -= VirtualGalleryRoomPresenter_UnhandledException;
        }

        private void VirtualGalleryRoomPresenter_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
        }

        public void PresentArtworkDetailFor(Models.View.ArtWorkViewModel model)
        {
            if (!isEventHandlingBind) return;

            UnbindSceneEvents();

            ExposureRoomPageModel.TargetArtWork = model;
        }

        public void BindSceneEvents()
        {
            Input.TouchBegin += scene.SceneInput_TouchBegin;
            Input.TouchEnd += scene.SceneInput_TouchEnd;

#if DEBUG
            Input.KeyUp += Input_KeyUp;
#endif

            isEventHandlingBind = true;
        }

        private void UnbindSceneEvents()
        {
            Input.TouchBegin -= scene.SceneInput_TouchBegin;
            Input.TouchEnd -= scene.SceneInput_TouchEnd;

#if DEBUG
            Input.KeyUp -= Input_KeyUp;
#endif

            isEventHandlingBind = false;
        }

        private void MoveCameraByTouches(float timeStep)
        {
            if (!isEventHandlingBind || scene.MainCamera == null || Input.NumTouches <= 0) return;

            if (showcaseStartAt != null)
            {
                scene.MainCamera.Node.RemoveAllActions();

                var elapsedTime = System.DateTime.Now - showcaseStartAt;

                var move = elapsedTime.Value.Milliseconds / 1000.0f * 3.0f;

                scene.MainCamera.Node.RunActionsAsync(new RotateTo(duration: 1.0f, deltaAngleX: 0.0f, deltaAngleY: 0.0f, deltaAngleZ: 0.0f));

                showcaseStartAt = null;

                return;
            }

            for (uint i = 0, num = Input.NumTouches; i < num; ++i)
            {
                var state = Input.GetTouch(i);

                if (state.TouchedElement != null) continue;

                if (!state.Delta.IsEmpty)
                {
                    yaw += TOUCH_SENSITIVITY * scene.MainCamera.Fov / Graphics.Height * state.Delta.X;

                    pitch += TOUCH_SENSITIVITY * scene.MainCamera.Fov / Graphics.Height * state.Delta.Y;

                    scene.MainCamera.Node.Rotation = new Quaternion(pitch, yaw, 0);
                }
                else
                {
                    var cursor = UI.Cursor;

                    if (cursor != null && cursor.Visible)
                    {
                        cursor.Position = state.Position;
                    }
                }
            }
        }

        internal void Input_KeyUp(KeyUpEventArgs obj)
        {
            if (showcaseStartAt != null)
            {
                scene.MainCamera.Node.RemoveAllActions();

                var elapsedTime = System.DateTime.Now - showcaseStartAt;

                var move = elapsedTime.Value.Milliseconds / 1000.0f * 3.0f;

                scene.MainCamera.Node.RunActionsAsync(new RotateTo(duration: 1.0f, deltaAngleX: 0.0f, deltaAngleY: 0.0f, deltaAngleZ: 0.0f));

                showcaseStartAt = null;

                return;
            }

            var cameraNode = scene.MainCamera.Node;

            if (obj.Key == Key.I)
            {
                scene.MainCamera.Zoom += 0.05f;
            }
            else if (obj.Key == Key.O)
            {
                scene.MainCamera.Zoom -= 0.05f;
            }
            else if (obj.Key == Key.Q)
            {
                cameraNode.RunActions(new MoveTo(MOVEMENT_SPEED, new Vector3(cameraNode.Position.X, cameraNode.Position.Y + MOVEMENT_SIZE / 2, cameraNode.Position.Z)));
            }
            else if (obj.Key == Key.Z)
            {
                cameraNode.RunActions(new MoveTo(MOVEMENT_SPEED, new Vector3(cameraNode.Position.X, cameraNode.Position.Y - MOVEMENT_SIZE / 2, cameraNode.Position.Z)));
            }
            else if (obj.Key == Key.W)
            {
                cameraNode.RunActions(new MoveTo(MOVEMENT_SPEED, new Vector3(cameraNode.Position.X, cameraNode.Position.Y, cameraNode.Position.Z + MOVEMENT_SIZE)));
            }
            else if (obj.Key == Key.S)
            {
                cameraNode.RunActions(new MoveTo(MOVEMENT_SPEED, new Vector3(cameraNode.Position.X, cameraNode.Position.Y, cameraNode.Position.Z - MOVEMENT_SIZE)));
            }
            else if (obj.Key == Key.A)
            {
                cameraNode.RunActions(new MoveTo(MOVEMENT_SPEED, new Vector3(cameraNode.Position.X - MOVEMENT_SIZE, cameraNode.Position.Y, cameraNode.Position.Z)));
            }
            else if (obj.Key == Key.D)
            {
                cameraNode.RunActions(new MoveTo(MOVEMENT_SPEED, new Vector3(cameraNode.Position.X + MOVEMENT_SIZE, cameraNode.Position.Y, cameraNode.Position.Z)));
            }
        }
    }
}
