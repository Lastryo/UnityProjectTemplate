using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] private GlobalUpdate globalUpdate;
        public GlobalUpdate GlobalUpdate => globalUpdate;

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);

            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        private void Update()
        {
            GlobalUpdate.Update();
        }

        private void FixedUpdate()
        {
            GlobalUpdate.FixedUpdate();
        }

        private void LateUpdate()
        {
            GlobalUpdate.LateUpdate();
        }
    }
}
