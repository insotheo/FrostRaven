using System;
using Silk.NET.Windowing;
using Silk.NET.Maths;

namespace FrostRaven.Core
{
    public abstract class Game : IDisposable
    {
        private readonly int _windowWidth;
        private readonly int _windowHeight;
        private readonly string _title;

        private IWindow _window;
        private WindowOptions _windowOptions = WindowOptions.Default;

        protected Game(uint width, uint height, string title, bool isVSyncEnabled)
        {
            Console.Title = title;

            _windowHeight = (int)height;
            _windowWidth = (int)width;
            _title = title;

            //configuring window
            _windowOptions.Size = new Vector2D<int>(_windowWidth, _windowHeight);
            _windowOptions.Title = title;
            _windowOptions.VSync = isVSyncEnabled;

            _window = Window.Create(_windowOptions);

            _window.Load += OnGameLoad;
            _window.Update += OnGameUpdate;
            _window.Render += OnGameRender;
        }

        private void OnGameLoad()
        {
            //TODO: Input system
        }

        private void OnGameUpdate(double dt) //dt - delta time
        {

        }

        private void OnGameRender(double dt) //dt - delta time
        {
            
        }

        public void Run()
        {
            if(_window == null)
            {
                return;
            }
            _window.Run();
        }

        protected (int Width, int Height) GetWindowSize()
        {
            return (_windowWidth, _windowHeight);
        }

        public void Dispose()
        {
            if(_window != null)
                _window.Dispose();
        }

    }
}
