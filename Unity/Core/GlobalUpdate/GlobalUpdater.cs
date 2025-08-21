using System;
using System.Buffers;

[Serializable]
public class GlobalUpdater
{
    private readonly UpdaterCollection<IUpdatable> updatables = new UpdaterCollection<IUpdatable>();
    private readonly UpdaterCollection<IFixedUpdatable> fixedUpdatables = new UpdaterCollection<IFixedUpdatable>();
    private readonly UpdaterCollection<ILateUpdatable> lateUpdatables = new UpdaterCollection<ILateUpdatable>();

    private class UpdaterCollection<T> where T : class
    {
        private const int InitialCapacity = 16; // Начальная ёмкость массива
        private T[] array; // Арендованный массив из пула
        private int count; // Текущее количество элементов

        public UpdaterCollection()
        {
            array = ArrayPool<T>.Shared.Rent(InitialCapacity);
            count = 0;
        }

        public void Add(T item)
        {
            if (count >= array.Length)
            {
                // Удваиваем размер массива, если он заполнен
                var newArray = ArrayPool<T>.Shared.Rent(array.Length * 2);
                Array.Copy(array, newArray, count);
                ArrayPool<T>.Shared.Return(array);
                array = newArray;
            }
            array[count] = item;
            count++;
        }

        public void Remove(T item)
        {
            int index = array.AsSpan(0, count).IndexOf(item);
            if (index >= 0)
            {
                // Swap-and-pop: заменяем удаляемый элемент последним
                count--;
                array[index] = array[count];
                array[count] = null;
            }
        }

        public void Update(Action<T> updateAction)
        {
            foreach (var item in array.AsSpan(0, count))
                updateAction(item);
        }

        public void Dispose()
        {
            if (array.Length > 0)
                ArrayPool<T>.Shared.Return(array);
            array = Array.Empty<T>();
            count = 0;
        }
    }

    public void Register(object obj)
    {
        // Регистрируем объект во всех подходящих коллекциях
        if (obj is IUpdatable updatable)
            updatables.Add(updatable);
        if (obj is IFixedUpdatable fixedUpdatable)
            fixedUpdatables.Add(fixedUpdatable);
        if (obj is ILateUpdatable lateUpdatable)
            lateUpdatables.Add(lateUpdatable);
    }

    public void Unregister(object obj)
    {
        // Удаляем объект из всех подходящих коллекций
        if (obj is IUpdatable updatable)
            updatables.Remove(updatable);
        if (obj is IFixedUpdatable fixedUpdatable)
            fixedUpdatables.Remove(fixedUpdatable);
        if (obj is ILateUpdatable lateUpdatable)
            lateUpdatables.Remove(lateUpdatable);
    }

    public void Update()
    {
        updatables.Update(item => item.OnUpdate());
    }

    public void FixedUpdate()
    {
        fixedUpdatables.Update(item => item.OnFixedUpdate());
    }

    public void LateUpdate()
    {
        lateUpdatables.Update(item => item.OnLateUpdate());
    }

    public void Dispose()
    {
        // Освобождаем все арендованные массивы
        updatables.Dispose();
        fixedUpdatables.Dispose();
        lateUpdatables.Dispose();
    }
}
