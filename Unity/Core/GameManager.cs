using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] private GlobalContainer globalContainer;
        public GlobalContainer GlobalContainer => globalContainer;

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);

            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        private void Update()
        {
            globalContainer.Update();
        }

        private void FixedUpdate()
        {
            globalContainer.FixedUpdate();
        }

        private void LateUpdate()
        {
            globalContainer.LateUpdate();
        }
    }
}
