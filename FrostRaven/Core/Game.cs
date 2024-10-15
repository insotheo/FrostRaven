using System;
using Silk.NET.Windowing;
using Silk.NET.Maths;

namespace FrostRaven.Core
{
    public abstract class Game : IDisposable
    {
        private int _windowWidth;
        private int _windowHeight;
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

            _window.Load += OnGameBegin;
            _window.Update += OnGameUpdate;
            _window.Render += OnGameRender;
            _window.Resize += OnGameWindowResized;
            _window.Closing += OnGameClosing;
        }

        private void OnGameWindowResized(Vector2D<int> newSize)
        {
            _windowWidth = newSize.X;
            _windowHeight = newSize.Y;
            OnWindowResized();
        }

        private void OnGameBegin()
        {
            //TODO: Input system
            OnBegin();
        }

        private void OnGameUpdate(double dt) //dt - delta time
        {

        }

        private void OnGameRender(double dt) //dt - delta time
        {
            
        }

        private void OnGameClosing() => OnWindowClosing();

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

        protected virtual void OnWindowResized() { }
        protected virtual void OnWindowClosing() { }
        protected virtual void OnBegin() { }
    }
}
