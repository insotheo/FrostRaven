using FrostRaven.Core;

namespace SandBox
{
    internal class MyGame : Game
    {
        public MyGame() : base(width: 800, height: 600,
            title: "SandBox game", isVSyncEnabled: true)
        {

        }

        protected override void OnBegin()
        {
            Log.Info("Game is running!");
        }

        protected override void OnWindowResized()
        {
            Log.Trace(GetWindowSize());
        }

        protected override void OnWindowClosing()
        {
            Log.Warn("Goodbye!");
        }

    }
}
