namespace MOROPO.FCBC.Galleries.Engine3D
{
    internal class VirtualGalleryRoomPresenter : Urho.Application
    {
        protected const float TOUCH_SENSITIVITY = 2;
        
        private float yaw { get; set; }
        private float pitch { get; set; }

        private Scenes.Contract.AbstractRoomScene scene;

        public VirtualGalleryRoomPresenter(Urho.ApplicationOptions options) : base(options)
        {
        }

        protected override void Start()
        {
            base.Start();

            #region Interaction initialize
            
            if (Platform == Urho.Platforms.Android || Platform == Urho.Platforms.iOS || Options.TouchEmulation)
            {
                //InitTouchInput();
            }

            Input.Enabled = true;
            Input.KeyDown += Input_KeyDown;

            #endregion

            scene = new Scenes.RectangleRoomScene(ResourceCache);

            // Viewport
            Renderer.SetViewport(0, new Urho.Viewport(Context, scene, scene.MainCamera, null));
        }

        protected override void OnUpdate(float timeStep)
        {
            MoveCameraByTouches(timeStep);

            base.OnUpdate(timeStep);
        }

        private void Input_KeyDown(Urho.KeyDownEventArgs eventArgs)
        {
            // TODO Implement expected behavior!!
        }

        protected void MoveCameraByTouches(float timeStep)
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

                    scene.MainCamera.Node.Rotation = new Urho.Quaternion(pitch, yaw, 0);
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
