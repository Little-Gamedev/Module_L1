using Assets._Project.Develop.Runtime.Gameplay.Sequence;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(SequenceMode mode)
        {
            Mode = mode;
        }

        public SequenceMode Mode { get; }
    }
}