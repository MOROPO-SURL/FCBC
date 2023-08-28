using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using Urho;
using Urho.Actions;

namespace MOROPO.FCBC.Galleries.Presenters
{
    public class VirtualGalleryRoomPresenter : Application
    {
        protected const float TOUCH_SENSITIVITY = 0.25f;
        protected const float MOVEMENT_SIZE = 10.0f;
        protected const float MOVEMENT_SPEED = 0.25f;

        private float yaw;
        private float pitch;
        private bool isEventHandlingBind;

        private System.DateTime? showcaseStartAt;

        private Scenes.Contract.AbstractRoomScene scene;

        [Preserve]
        public VirtualGalleryRoomPresenter(ApplicationOptions options) : base(options)
        {
        }

        public Models.Page.ExposureRoomPageModel ExposureRoomPageModel { get; set; }

        protected override void Start()
        {
            base.Start();

            Graphics.WindowTitle = "UrhoSharp Sample";

            Log.LogLevel = LogLevel.Warning;
            Log.LogMessage += e => System.Diagnostics.Debug.WriteLine($"[Urho] {e.Message}"); ;

            scene = new Scenes.SquareRoomScene(this);

            Input.Enabled = true;

            UI.Cursor = new Urho.Gui.Cursor();

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
        }

        protected override void OnUpdate(float timeStep)
        {
            MoveCameraByTouches(timeStep);

            base.OnUpdate(timeStep);
        }

        protected override void Stop()
        {
            UnbindSceneEvents();

            scene.Clear();
            scene.Remove();
            scene.Dispose();

            scene = null;

            base.Stop();
        }

        public void PresentArtworkDetailFor(Models.View.ArtWorkViewModel model)
        {
            if (!isEventHandlingBind) return;

            UnbindSceneEvents();

            ExposureRoomPageModel.TargetArtWork = model;
        }

        public void BindSceneEvents()
        {
            Input.MouseButtonDown += scene.SceneInput_TouchBegin;
            Input.MouseButtonUp += scene.SceneInput_TouchEnd;

#if DEBUG
            Input.KeyUp += Input_KeyUp;
#endif

            isEventHandlingBind = true;
        }

        private void UnbindSceneEvents()
        {
            Input.MouseButtonDown -= scene.SceneInput_TouchBegin;
            Input.MouseButtonUp -= scene.SceneInput_TouchEnd;

#if DEBUG
            Input.KeyUp -= Input_KeyUp;
#endif

            isEventHandlingBind = false;
        }

        private void MoveCameraByTouches(float timeStep)
        {
            if (!isEventHandlingBind || !Input.GetMouseButtonDown(MouseButton.Right)) return;

            if (showcaseStartAt != null)
            {
                scene.MainCamera.Node.RemoveAllActions();

                var elapsedTime = System.DateTime.Now - showcaseStartAt;

                var move = elapsedTime.Value.Milliseconds / 1000.0f * 3.0f;

                scene.MainCamera.Node.RunActionsAsync(new RotateTo(duration: 1.0f, deltaAngleX: 0.0f, deltaAngleY: 0.0f, deltaAngleZ: 0.0f));

                showcaseStartAt = null;

                return;
            }

            var mouseMove = Input.MouseMove;

            yaw += TOUCH_SENSITIVITY * mouseMove.X;

            pitch += TOUCH_SENSITIVITY * mouseMove.Y;

            pitch = MathHelper.Clamp(pitch, -90.0f, 90.0f);

            scene.MainCamera.Node.Rotation = new Quaternion(pitch, yaw, 0);
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
