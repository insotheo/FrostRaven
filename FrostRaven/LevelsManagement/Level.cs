using FrostRaven.Core;
using System;
using System.Collections.Generic;

namespace FrostRaven.LevelsManagement
{
    public class Level : IDisposable
    {
        private List<GamePawn> _gamePawns;

        public Level()
        {
            _gamePawns = new List<GamePawn>();
        }

        internal void OnLevelBegin()
        {
            OnBegin();
            if(_gamePawns.Count > 0)
            {
                for(int i = 0; i < _gamePawns.Count; i++)
                {
                    _gamePawns[i].OnPawnBegin();
                }
            }
        }

        internal void OnLevelUpdate(float dt)
        {
            OnUpdate(dt);
            if (_gamePawns.Count > 0)
            {
                for (int i = 0; i < _gamePawns.Count; i++)
                {
                    _gamePawns[i].OnPawnUpdate(dt);
                }
            }
        }

        public void Dispose()
        {
            _gamePawns.Clear();
        }

        protected virtual void OnBegin() { }
        protected virtual void OnUpdate(float dt) { }

        protected void AddGamePawn(GamePawn gamePawn) => _gamePawns.Add(gamePawn);
        protected void RemoveGamePawn(GamePawn gamePawn) => _gamePawns.Remove(gamePawn);
        protected void RemoveGamePawnAt(int index) => _gamePawns.RemoveAt(index);
        protected int GetIndexOfGamePawn(GamePawn gamePawn) => _gamePawns.IndexOf(gamePawn);
        protected GamePawn GetGamePawnAtIndex(int index) => _gamePawns[index];
        protected bool DoesGamePawnExist(GamePawn gamePawn) => _gamePawns.Contains(gamePawn); 
        protected void ClearGamePawns() => _gamePawns.Clear();
        protected int GetGamePawnsAmount() => _gamePawns.Count;
    }
}
