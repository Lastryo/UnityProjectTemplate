using UnityEngine;

namespace Core
{
    public class GlobalManager : MonoBehaviour
    {
        public static GlobalManager Instance;
        public GlobalUpdater GlobalUpdater { get; private set; } = new GlobalUpdater();

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);

            Instance = this;
            DontDestroyOnLoad(Instance);
        }

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
