using System;
using Silk.NET.Windowing;
using Silk.NET.Maths;
using Silk.NET.Input;
using FrostRaven.InputSystem;
using FrostRaven.LevelsManagement;

namespace FrostRaven.Core
{
    public abstract class Game : IDisposable
    {
        private int _windowWidth;
        private int _windowHeight;
        private string _title;

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
            //Input system
            IInputContext context = _window.CreateInput();
            foreach(IKeyboard keyboard in context.Keyboards)
            {
                keyboard.KeyDown += Input.OnWindowKeyDown;
                keyboard.KeyUp += Input.OnWindowKeyUp;
            }

            //GameTime.cs
            unsafe
            {
#pragma warning disable CS8500
                fixed (IWindow* p_window = &_window)
                {
                    GameTime.Run(p_window);
                }
#pragma warning restore CS8500
            }


            LevelsManager.Init();
            OnBegin();
        }

        private void OnGameUpdate(double dt) //dt - delta time
        {
            LevelsManager.CallCurrentLevelOnUpdate((float)dt);
            Input.ClearUpInput();
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

        public void Dispose()
        {
            GameTime.Finish();
            if(_window != null)
                _window.Dispose();
            LevelsManager.Finish();
            GC.SuppressFinalize(this);
        }

        protected virtual void OnWindowResized() { }
        protected virtual void OnWindowClosing() { }
        protected virtual void OnBegin() { }

        //Window settings
        protected void SetVSync(bool state) => _window.VSync = state;
        protected void SetWindowState(WindowSettings.WindowState state) => _window.WindowState = (WindowState)state;
        protected void SetWindowStyle(WindowSettings.WindowBorder border) => _window.WindowBorder = (WindowBorder)border;
        protected void SetWindowTitle(string title) => _window.Title = _title = Console.Title = title;
        protected void SetWindowSize(uint width, uint height)
        {
            _windowWidth = (int)width;
            _windowHeight = (int)height;
            _window.Size = new Vector2D<int>((int)width, (int)height);
        }

        //Window getters
        protected bool GetVSync() => _window.VSync;
        protected WindowSettings.WindowState GetWindowState() => (WindowSettings.WindowState)_window.WindowState;
        protected WindowSettings.WindowBorder GetWindowBorder() => (WindowSettings.WindowBorder)_window.WindowBorder;
        protected string GetWindowTitle() => _title;
        protected (int Width, int Height) GetWindowSize() => (_windowWidth, _windowHeight);

    }
}
