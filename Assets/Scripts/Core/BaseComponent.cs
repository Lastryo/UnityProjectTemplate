using Core;
using UnityEngine;

namespace Core
{
    public class BaseComponent : MonoBehaviour
    {
        protected virtual void Start()
        {
            var _globalUpdate = GameManager.Instance.GlobalUpdate;
            _globalUpdate.Register(this);
        }

        private void OnDestroy()
        {
            var _globalUpdate = GameManager.Instance.GlobalUpdate;
            _globalUpdate.Unregister(this);
        }
    }
}
