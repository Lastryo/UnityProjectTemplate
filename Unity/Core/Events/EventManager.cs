using Core.Interfaces;

namespace Core.Events
{
    public class EventManager : BaseComponent, IUpdatable
    {
        public static EventManager Instance;
        internal EventHolder EventHolder;

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);

            Instance = this;
            DontDestroyOnLoad(Instance);

            EventHolder = new EventHolder();
        }

        public void OnUpdate()
        {
            var list = EventHolder.DispatchedEvents;
            if (list.Count == 0)
            {
                return;
            }

            EventHolder.DispatchedEvents = EventHolder.ExecutingEvents;
            EventHolder.ExecutingEvents = list;

            foreach (var evt in list)
            {
                evt.Invoke();
            }

            list.Clear();
        }
    }
}
