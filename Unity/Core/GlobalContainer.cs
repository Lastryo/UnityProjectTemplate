using System;
using System.Collections.Generic;
using Core.Interfaces;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "GlobalContainer", menuName = "UnityProjectTemplate/GlobalContainer", order = 0)]
    public class GlobalContainer : ScriptableObject
    {
        [NonSerialized]
        private List<IUpdatable> updatables = new List<IUpdatable>();
        [NonSerialized]
        private List<IFixedUpdatable> fixedUpdatables = new List<IFixedUpdatable>();
        [NonSerialized]
        private List<ILateUpdatable> lateUpdatables = new List<ILateUpdatable>();

        public void Register(UnityEngine.Object obj)
        {
            if (obj is IUpdatable updatable)
                updatables.Add(updatable);
            if (obj is IFixedUpdatable fixedUpdatable)
                fixedUpdatables.Add(fixedUpdatable);
            if (obj is ILateUpdatable lateUpdatable)
                lateUpdatables.Add(lateUpdatable);
        }

        public void Unregister(UnityEngine.Object obj)
        {
            if (obj is IUpdatable updatable)
                updatables.Remove(updatable);
            if (obj is IFixedUpdatable fixedUpdatable)
                fixedUpdatables.Remove(fixedUpdatable);
            if (obj is ILateUpdatable lateUpdatable)
                lateUpdatables.Remove(lateUpdatable);
        }

        public void Update()
        {
            foreach (var updatable in updatables)
                updatable.OnUpdate();
        }

        public void FixedUpdate()
        {
            foreach (var updatable in fixedUpdatables)
                updatable.OnFixedUpdate();
        }

        public void LateUpdate()
        {
            foreach (var updatable in lateUpdatables)
                updatable.OnLateUpdate();
        }
    }
}