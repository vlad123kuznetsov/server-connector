using System.Collections;
using System.Collections.Generic;
using Models.State;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;


public class AppContext : MVCSContext
{
    public AppContext (MonoBehaviour view) : base(view)
    {
        _instance = this;
    }

    public AppContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
        _instance = this;
    }

    private static AppContext _instance;

    public static T Get<T>()
    {
        return _instance.injectionBinder.GetInstance<T>();
    }
		
    // Unbind the default EventCommandBinder and rebind the SignalCommandBinder
    protected override void addCoreComponents()
    {
        base.addCoreComponents();
        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }
		
    // Override Start so that we can fire the StartSignal 
    public override IContext Start()
    {
        base.Start();

       // var startSignal= injectionBinder.GetInstance<StartSignal>();
        //startSignal.Dispatch();
        
        return this;
    }
		
    protected override void mapBindings()
    {
        injectionBinder.Bind<IDataLoaderService>().To<DataLoaderService>().ToSingleton();
        injectionBinder.Bind<GameState>().ToSingleton();
        injectionBinder.Bind<UserState>().ToSingleton();
        injectionBinder.Bind<Serializer>().ToSingleton();
    }
}


