using FrostRaven.Core;
using FrostRaven.LevelsManagement;

namespace SandBox
{
    internal class MyGame : Game
    {
        public MyGame() : base(width: 800, height: 600,
            title: "SandBox game", isVSyncEnabled: true)
        {
            SetWindowSize(1280, 720);
        }

        protected override void OnBegin()
        {
            Log.Info("Game is running!");
            LevelsManager.AddLevel("TEST_LEVEL", new TestLevel());
            LevelsManager.RunLevel("TEST_LEVEL");
        }

        protected override void OnWindowResized()
        {
            Log.Trace(GetWindowSize());
        }

        protected override void OnWindowClosing()
        {
            Log.Info($"You played for {GameInfo.GetElapsedTime()} seconds!");
            Log.Warn("Goodbye!");
        }

    }
}
