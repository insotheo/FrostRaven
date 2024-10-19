using FrostRaven.Core;
using FrostRaven.InputSystem;
using FrostRaven.LevelsManagement;

namespace SandBox
{
    public class TestLevel : Level
    {
        protected override void OnBegin()
        {
            Log.Trace("Hello from TestLevel!");
        }

        protected override void OnUpdate(float dt)
        {
            if (Input.IsKeyUp(KeyCode.Space))
            {
                Log.Info(dt);
            }
            if (Input.IsKeyUp(KeyCode.Escape))
            {
                GameInfo.QuitGame();
            }
        }
    }
}
