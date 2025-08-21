using UnityEngine;

namespace Core
{
    public interface ISingleton<T> where T : Object
    {
        static T Instance { get; }
    }

    public class Singleton<T> : MonoBehaviour, ISingleton<T> where T : Object
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
                Destroy(Instance);

            Instance = this as T;
            DontDestroyOnLoad(Instance);
        }
    }
}
