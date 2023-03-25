using Urho;
using Urho.Actions;

namespace MOROPO.FCBC.Galleries.Presenters
{
    internal class VirtualGalleryRoomPresenter : Application
    {
        protected const float TOUCH_SENSITIVITY = 2;
        protected const float MOVEMENT_SIZE = 2.0f;
        protected const float MOVEMENT_SPEED = 0.25f;

        private float yaw;
        private float pitch;
        private bool isEventHandlingBind;

        private System.DateTime? showcaseStartAt;

        private Scenes.Contract.AbstractRoomScene scene;

        public VirtualGalleryRoomPresenter(ApplicationOptions options) : base(options)
        {
        }

        protected override void Start()
        {
            base.Start();

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

            scene.MainCamera.Node.RunActionsAsync(new RepeatForever(new RotateBy(duration: 1.0f, deltaAngleX: 0.0f, deltaAngleY: 3.0f, deltaAngleZ: 0.0f)));

            showcaseStartAt = System.DateTime.Now;
        }

        protected override void OnUpdate(float timeStep)
        {
            MoveCameraByTouches(timeStep);

            base.OnUpdate(timeStep);
        }

        public void PresentArtworkDetailById(System.Guid artWorkId)
        {
            UnbindSceneEvents();

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Aviso", string.Format("Mediante esta acción se mostrará una ventana flotante detallando la obra con el identifcador {0}.", artWorkId), "Aceptar").ContinueWith(_ => 
                {
                    BindSceneEvents();
                });
            });
        }

        private void BindSceneEvents()
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
                cameraNode.RunActions(new MoveTo(MOVEMENT_SPEED, new Vector3(cameraNode.Position.X, cameraNode.Position.Y + 2f, cameraNode.Position.Z)));
            }
            else if (obj.Key == Key.Z)
            {
                cameraNode.RunActions(new MoveTo(MOVEMENT_SPEED, new Vector3(cameraNode.Position.X, cameraNode.Position.Y - 2f, cameraNode.Position.Z)));
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
