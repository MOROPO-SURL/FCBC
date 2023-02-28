using Urho;

namespace MOROPO.FCBC.Galleries.Presenters
{
    internal class VirtualGalleryRoomPresenter : Application
    {
        protected const float TOUCH_SENSITIVITY = 2;

        private float yaw { get; set; }
        private float pitch { get; set; }

        private Scenes.Contract.AbstractRoomScene scene;

        public VirtualGalleryRoomPresenter(ApplicationOptions options) : base(options)
        {
        }

        protected override void Start()
        {
            base.Start();

            scene = new Scenes.SquareRoomScene(this);

            #region Interaction initialize

            Input.Enabled = true;
            
            BindSceneToucEvents();

            #endregion

            // Viewport
            var viewport = new Viewport(Context, scene, scene.MainCamera, null);

            viewport.RenderPath.Append(CoreAssets.PostProcess.FXAA3);

            Renderer.HDRRendering = true;

            Renderer.SetViewport(0, viewport);
        }

        protected override void OnUpdate(float timeStep)
        {
            MoveCameraByTouches(timeStep);

            base.OnUpdate(timeStep);
        }

        public void PresentArtworkDetailById(System.Guid artWorkId)
        {
            UnbindSceneToucEvents();

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Aviso", string.Format("Mediante esta acción se mostrará una ventana flotante detallando la obra con el identifcador {0}.", artWorkId), "Aceptar").ContinueWith(_ => 
                {
                    BindSceneToucEvents();
                });
            });
        }

        private void BindSceneToucEvents()
        {
            Input.TouchBegin += scene.SceneInput_TouchBegin;
            Input.TouchEnd += scene.SceneInput_TouchEnd;
        }

        private void UnbindSceneToucEvents()
        {
            Input.TouchBegin -= scene.SceneInput_TouchBegin;
            Input.TouchEnd -= scene.SceneInput_TouchEnd;
        }

        private void MoveCameraByTouches(float timeStep)
        {
            if (scene.MainCamera == null) return;

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
    }
}
