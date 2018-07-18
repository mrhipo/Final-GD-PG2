using System;
using System.Collections.Generic;
using System.Linq;

public class EventRegistry{

    private readonly Dictionary<Type, List<Invoker>> handlers = new Dictionary<Type, List<Invoker>>();
    private readonly ObjectPool<Invoker> invokerPool = new ObjectPool<Invoker>();

    public void Clear()
    {
        handlers.Clear();
        invokerPool.Clear();
    }

    public void AddEventHandler(Type eventType, Action handler)
    {
        var invoker = invokerPool.Get<ParameterlessInvoker>();

        invoker.Handler = handler;

        RegisterInvoker(eventType, invoker);
    }

    public void AddEventHandler(Type eventType, Action<GameEvent> handler)
    {
        var invoker = invokerPool.Get<SpecificInvoker<GameEvent>>();

        invoker.Handler = handler;

        RegisterInvoker(eventType, invoker);
    }

    public void AddEventHandler<T>(Action handler) where T : GameEvent
    {
        AddEventHandler(typeof(T), handler);
    }

    public void AddEventHandler<T>(Action<T> handler) where T : GameEvent
    {
        var invoker = invokerPool.Get<SpecificInvoker<T>>();

        invoker.Handler = handler;

        RegisterInvoker(typeof(T), invoker);
    }

    public List<Invoker> RemoveEventHandler(Type eventType, object handler)
    {
        List<Invoker> handlerList = null;

        if (handlers.TryGetValue(eventType, out handlerList))
        {
            var index = handlerList.FindIndex(i => (i != null) && i.Target.Equals(handler));
            if (index >= 0)
            {
                invokerPool.Release(handlerList[index]);
                handlerList[index] = null;
            }
            else
            {
                handlerList = null;
            }
        }

        return handlerList;
    }

    public void Dispatch<T>(T gameEvent) where T : GameEvent
    {
        List<Invoker> handlerList = null;

        if (handlers.TryGetValue(typeof(T), out handlerList))
        {
            for (int handlerIndex = 0, maxIndex = handlerList.Count; handlerIndex < maxIndex; handlerIndex++)
            {
                if (handlerList[handlerIndex] != null)
                {
                    handlerList[handlerIndex].Invoke(gameEvent);
                }
            }
        }
    }

    private void RegisterInvoker(Type eventType, Invoker invoker)
    {
        if (!handlers.ContainsKey(eventType))
        {
            handlers.Add(eventType, new List<Invoker>());
        }

        var handlerList = handlers[eventType];

        if (!handlerList.Any(i => (i != null) && i.Target.Equals(invoker.Target)))
        {
            handlerList.Add(invoker);
        }
        else
        {
            invokerPool.Release(invoker);
        }
    }
    
}

public abstract class BaseObjectPool<KeyType, BaseType>
    {
        public delegate BaseType Allocator(KeyType key);

        private readonly Dictionary<KeyType, Queue<BaseType>> items = new Dictionary<KeyType, Queue<BaseType>>();

        abstract public BaseType DefaultAllocator(KeyType key);
        abstract public void Release(BaseType item);

        public int Count(KeyType key)
        {
            return items.ContainsKey(key) ? items[key].Count : 0;
        }

        public void Allocate(KeyType key, int count)
        {
            Allocator allocator = DefaultAllocator;
            Allocate(key, count, allocator);
        }

        public void Allocate(KeyType key, int count, Allocator allocator)
        {
            for (var index = Count(key); index < count; index++)
            {
                Release(allocator(key));
            }
        }

        virtual public void Clear()
        {
            items.Clear();
        }

        virtual public BaseType Get(KeyType key)
        {
            Allocator allocator = DefaultAllocator;
            return Get(key, allocator);
        }

        public BaseType Get(KeyType key, Allocator allocator)
        {
            Queue<BaseType> instances;

            if (items.TryGetValue(key, out instances) && (instances.Count > 0))
            {
                return instances.Dequeue();
            }

            return allocator(key);
        }

        protected void ReturnToPool(KeyType key, BaseType item)
        {
            if (!items.ContainsKey(key))
            {
                items.Add(key, new Queue<BaseType>());
            }

            if (!items[key].Contains(item))
            {
                items[key].Enqueue(item);
            }
        }
    }
    
    public class ObjectPool<T> : BaseObjectPool<Type, T>
    {
        public int Count<DerivedType>() where DerivedType : T
        {
            return Count(typeof(DerivedType));
        }

        public void Allocate<DerivedType>(int count) where DerivedType : T
        {
            Allocate(typeof(DerivedType), count);
        }

        public void Allocate<DerivedType>(int count, Allocator allocator) where DerivedType : T
        {
            Allocate(typeof(DerivedType), count, allocator);
        }

        public T Get()
        {
            return Get(typeof(T));
        }

        public U Get<U>() where U : T
        {
            return Get<U>(typeof(U));
        }

        private DerivedType Get<DerivedType>(Type key) where DerivedType : T
        {
            Func<Type, DerivedType> allocator = DefaultAllocator<DerivedType>;
            return Get<DerivedType>(key, allocator);
        }

        private DerivedType Get<DerivedType>(Type key, System.Func<Type, DerivedType> allocator) where DerivedType : T
        {
            Allocator wrapper = (k) => (T)allocator(k);
            return (DerivedType)Get(key, wrapper);
        }

        public override T DefaultAllocator(Type key)
        {
            return (T)Activator.CreateInstance(key);
        }

        private DerivedType DefaultAllocator<DerivedType>(Type key)
        {
            return (DerivedType)Activator.CreateInstance(key);
        }

        public override void Release(T item)
        {
            ReturnToPool(item.GetType(), item);
        }
    }
    
    public interface Invoker
    {
        object Target { get; }

        void Invoke(GameEvent gameEvent);
    }

    public class SpecificInvoker<T> : Invoker where T : GameEvent
    {
        public object Target { get { return Handler; } }
        public System.Action<T> Handler { get; set; }

        public void Invoke(GameEvent gameEvent)
        {
            Handler.Invoke((T)gameEvent);
        }
    }

    public class ParameterlessInvoker : Invoker
    {
        public object Target { get { return Handler; } }
        public System.Action Handler { get; set; }

#if UNITY_EDITOR
        public object TargetObject { get { return Handler.Target; } }
#endif

        public void Invoke(GameEvent gameEvent)
        {
            Handler.Invoke();
        }
    }

