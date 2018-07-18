using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class GlobalEvent
{
    
    public static GlobalEvent Instance
    {
        get; private set;
    }
    
    private int dispatchesInProgress;

    private readonly ObjectPool<GameEvent> eventPool = new ObjectPool<GameEvent>();
    private readonly HashSet<List<Invoker>> handlerListsToClean = new HashSet<List<Invoker>>();

    public EventRegistry globalEvent;

    public GlobalEvent()
    {
        globalEvent = new EventRegistry();
        Instance = this;
    }

    public void AddEventHandler<T>(Action handler) where T : GameEvent
    {
        globalEvent.AddEventHandler<T>(handler);
    }

    public void AddEventHandler<T>(Action<T> handler) where T : GameEvent
    {
        globalEvent.AddEventHandler<T>(handler);
    }

    public void RemoveEventHandler<T>(Action handler) where T : GameEvent
    {
        RemoveEventHandlers(typeof(T), handler);
    }

    public void RemoveEventHandler<T>(Action<T> handler) where T : GameEvent
    {
        RemoveEventHandlers(typeof(T), handler);
    }

    public void Dispatch<T>(T gameEvent) where T : GameEvent
    {
        //UnityEngine.Debug.Log(gameEvent);
        dispatchesInProgress++;

        globalEvent.Dispatch<T>(gameEvent);

        dispatchesInProgress--;
        CleanModifiedHandlerLists();
    }

    public void DispatchPooled<T>(T gameEvent) where T : GameEvent
    {
        Dispatch(gameEvent);
        AddPooledEvent(gameEvent);
    }

    public T GetPooledEvent<T>() where T : GameEvent
    {
        return eventPool.Get<T>();
    }

    public void AddPooledEvent<T>(T gameEvent) where T : GameEvent
    {
        eventPool.Release(gameEvent);
    }

    private void RemoveEventHandlers(Type eventType, object handler)
    {
        var listToClean = globalEvent.RemoveEventHandler(eventType, handler);

        if (listToClean != null)
        {
            handlerListsToClean.Add(listToClean);
        }
    }

    private void CleanModifiedHandlerLists()
    {
        if ((dispatchesInProgress == 0) && (handlerListsToClean.Count > 0))
        {
            foreach (var handlerList in handlerListsToClean)
            {
                handlerList.RemoveAll(h => (h == null));
            }

            handlerListsToClean.Clear();
        }
    }
    
}

public interface GameEvent{}
