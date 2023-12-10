using Core;
using UnityEngine;

namespace Core
{
    public class BaseComponent : MonoBehaviour
    {
        protected virtual void Start()
        {
            var _globalContainer = GameManager.Instance.GlobalContainer;
            _globalContainer.Register(this);
        }

        protected virtual void OnDestroy()
        {
            var _globalContainer = GameManager.Instance.GlobalContainer;
            _globalContainer.Unregister(this);
        }
    }
}
