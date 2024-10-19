using System.Collections.Generic;

namespace FrostRaven.LevelsManagement
{
    public static class LevelsManager
    {
        private static Dictionary<string, Level> _levels;
        private static string _currentLevelName;

        internal static void Init()
        {
            _levels = new Dictionary<string, Level>();
            _levels.Add("FROST_RAVEN_EMPTY_ZERO_LEVEL", new Level());
            _currentLevelName = "FROST_RAVEN_EMPTY_ZERO_LEVEL";
            RunLevel(_currentLevelName);
        }

        internal static Level GetCurrentLevel() => _levels[_currentLevelName];
        internal static string GetCurrentLevelName() => _currentLevelName;
        internal static void CallCurrentLevelOnBegin() => _levels[_currentLevelName].OnLevelBegin();
        internal static void CallCurrentLevelOnUpdate(float dt) => _levels[_currentLevelName].OnLevelUpdate(dt);

        internal static void Finish()
        {
            _levels.Clear();
            _currentLevelName = string.Empty;
        }

        public static void AddLevel(string levelName, Level level) => _levels.Add(levelName, level);
        public static void RemoveLevel(string levelName) => _levels.Remove(levelName);
        public static bool DoesLevelExist(string levelName) => _levels.ContainsKey(levelName);
        public static void RunLevel(string levelName)
        {
            if (!_levels.ContainsKey(levelName))
            {
                return;
            }
            _currentLevelName = levelName;
            _levels[_currentLevelName].OnLevelBegin();
        }
    }
}
