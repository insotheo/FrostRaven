using FrostRaven.Core;

namespace SandBox
{
    internal class MyGame : Game
    {
        public MyGame() : base(width: 800, height: 600,
            title: "SandBox game", isVSyncEnabled: true)
        {
            Log.Info(GetWindowSize());
        }
    }
}
