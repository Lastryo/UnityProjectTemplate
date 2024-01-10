using Core;
using UnityEngine;

namespace Core
{
    public class BaseComponent : MonoBehaviour
    {
        protected virtual void Start()
        {
            var _globalUpdater = GlobalManager.Instance.GlobalUpdater;
            _globalUpdater.Register(this);
        }

        protected virtual void OnDestroy()
        {
            var _globalUpdater = GlobalManager.Instance.GlobalUpdater;
            _globalUpdater.Unregister(this);
        }
    }
}
