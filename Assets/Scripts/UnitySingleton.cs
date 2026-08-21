using System;
using Unity.Scripting.LifecycleManagement;
using UnityEngine;

public abstract partial class UnitySingleton<T> : UnitySingletonBase where T : UnitySingleton<T>
{
    #region static

    [AutoStaticsCleanup]
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T[] instances = FindObjectsByType<T>();
                if (instances is null || instances.Length == 0)
                {
                    throw new Exception($"No instance of singleton {typeof(T)} found.");
                }

                if (instances.Length > 1)
                {
                    Debug.LogWarning($"Multiple instances of singleton {typeof(T)} found. Using the first instance.");
                }

                _instance = instances[0];
                _instance.Initialize();
            }

            return _instance;
        }
    }

    public static bool IsInstanceCreated => _instance;

    #endregion

    public bool IsValidInstance => IsInstanceCreated && _instance == this;

    #region unity event

    protected sealed override void Awake()
    {
        if (!IsInstanceCreated)
        {
            _instance = (T)this;
            _instance.Initialize();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected sealed override void OnDestroy() => Dispose();

    protected sealed override void Start()
    {
        if (IsValidInstance)
        {
            OnStart();
        }
    }

    #endregion

    public void Initialize() => OnInitialize();

    public void Dispose()
    {
        if (IsValidInstance)
        {
            OnDispose();

            _instance = null;
        }
    }

    protected virtual void OnInitialize()
    {
    }

    protected virtual void OnDispose()
    {
    }

    protected virtual void OnStart()
    {
    }
}

public abstract class UnitySingletonBase : MonoBehaviour
{
    protected virtual void Awake()
    {
    }

    protected virtual void OnDestroy()
    {
    }

    protected virtual void Start()
    {
    }
}