using UnityEditor;
using UnityEngine;

namespace Core
{
    public class GlobalManager : Singleton<GlobalManager>
    {
        public GlobalUpdater GlobalUpdater { get; private set; } = new GlobalUpdater();

        private void Update()
        {
            GlobalUpdater.Update();
        }

        private void FixedUpdate()
        {
            GlobalUpdater.FixedUpdate();
        }

        private void LateUpdate()
        {
            GlobalUpdater.LateUpdate();
        }
    }
}
