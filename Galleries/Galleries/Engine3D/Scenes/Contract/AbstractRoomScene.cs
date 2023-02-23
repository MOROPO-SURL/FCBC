using Urho;

namespace MOROPO.FCBC.Galleries.Engine3D.Scenes.Contract
{
    internal abstract class AbstractRoomScene : Scene
    {
        protected Octree octree;

        public AbstractRoomScene(Urho.Resources.ResourceCache resourceCache)
        {
            octree = CreateComponent<Octree>();
        }

        public abstract Camera MainCamera { get; }

        public abstract void SceneInput_TouchEnd(TouchEndEventArgs eventArgs);
    }
}
