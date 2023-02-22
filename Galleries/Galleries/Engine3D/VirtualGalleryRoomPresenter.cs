namespace MOROPO.FCBC.Galleries.Engine3D
{
    internal class VirtualGalleryRoomPresenter : Urho.Application
    {
        protected const float TOUCH_SENSITIVITY = 2;
        
        private float yaw { get; set; }
        private float pitch { get; set; }
        private Urho.Node cameraNode; 

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

            var scene = new Scenes.RectangleRoomScene(ResourceCache);

            // Camera
            cameraNode = scene.CreateChild(name: "camera");
            
            cameraNode.Position = new Urho.Vector3(10, 105, 10) / 1.75f;
            cameraNode.Rotation = new Urho.Quaternion(-0.121f, 0.878f, -0.305f, -0.35f);

            var camera = cameraNode.CreateComponent<Urho.Camera>();

            // Viewport
            Renderer.SetViewport(0, new Urho.Viewport(Context, scene, camera, null));
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
            if (cameraNode == null) return;

            for (uint i = 0, num = Input.NumTouches; i < num; ++i)
            {
                var state = Input.GetTouch(i);
                
                if (state.TouchedElement != null) continue;

                if (!state.Delta.IsEmpty)
                {
                    var camera = cameraNode.GetComponent<Urho.Camera>();
                    
                    if (camera == null) return;

                    yaw += TOUCH_SENSITIVITY * camera.Fov / Graphics.Height * state.Delta.X;
                    
                    pitch += TOUCH_SENSITIVITY * camera.Fov / Graphics.Height * state.Delta.Y;
                    
                    cameraNode.Rotation = new Urho.Quaternion(pitch, yaw, 0);
                }
                else
                {
                    var cursor = UI.Cursor;

                    if (cursor != null && cursor.Visible)
                    {
                        cursor.Position = state.Position;

                        System.Diagnostics.Debug.WriteLine("Cursor to: {0}", cursor.Position);
                    }
                }
            }
        }
    }
}
