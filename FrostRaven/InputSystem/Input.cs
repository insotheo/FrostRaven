using Silk.NET.Input;
using System.Collections.Generic;

namespace FrostRaven.InputSystem
{
    public static class Input
    {
        private static HashSet<KeyCode> _downKeys = new HashSet<KeyCode>();
        private static HashSet<KeyCode> _upKeys = new HashSet<KeyCode>();

        internal static void OnWindowKeyDown(IKeyboard keyboard, Key key, int code)
        {
            KeyCode keyCode = (KeyCode)key;
            _downKeys.Add(keyCode);
            _upKeys.Remove(keyCode);
        }

        internal static void OnWindowKeyUp(IKeyboard keyboard, Key key, int arg3)
        {
            KeyCode keyCode = (KeyCode)key;
            _downKeys.Remove(keyCode);
            _upKeys.Add(keyCode);
        }

        internal static void ClearUpInput()
        {
            _upKeys.Clear();
        }

        public static bool IsKeyDown(KeyCode key) => _downKeys.Contains(key);
        public static bool IsAnyKeyDown() => _downKeys.Count > 0;
        public static bool IsKeyUp(KeyCode key) => _upKeys.Contains(key);
        public static bool IsAnyKeyUp() => _upKeys.Count > 0;
    }
}
