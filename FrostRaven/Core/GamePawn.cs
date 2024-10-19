using System;

namespace FrostRaven.Core
{
    public class GamePawn : IDisposable
    {

        internal void OnPawnBegin() => OnBegin();
        internal void OnPawnUpdate(float dt) => OnUpdate(dt);

        protected virtual void OnBegin() { }
        protected virtual void OnUpdate(float dt) { }

        public void Dispose()
        {

        }
    }
}
