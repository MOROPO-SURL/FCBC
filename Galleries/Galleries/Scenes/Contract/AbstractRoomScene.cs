using Urho;

namespace MOROPO.FCBC.Galleries.Scenes.Contract
{
    internal abstract class AbstractRoomScene : Scene
    {
        protected Presenters.VirtualGalleryRoomPresenter presenter;

        protected Octree octree;

        public AbstractRoomScene(Presenters.VirtualGalleryRoomPresenter presenter)
        {
            this.presenter = presenter;

            octree = CreateComponent<Octree>();
        }

        public abstract Camera MainCamera { get; }

        public abstract void SceneInput_TouchBegin(MouseButtonDownEventArgs eventArgs);

        public abstract void SceneInput_TouchEnd(MouseButtonUpEventArgs eventArgs);
    }
}
