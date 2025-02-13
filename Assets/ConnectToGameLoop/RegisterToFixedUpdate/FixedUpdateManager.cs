using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ConnectToGameLoop.RegisterToFixedUpdate
{
public static class FixedUpdateManager 
{
    private static FixedUpdateGameObject _Instance;
    public static FixedUpdateGameObject Instance
    {
        get
        {
            if (!_Instance && SceneManager.GetActiveScene().isLoaded)
            {
                _Instance = new GameObject().AddComponent<FixedUpdateGameObject>();
                _Instance.name = _Instance.GetType().ToString();
                Object.DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    public class FixedUpdateGameObject : MonoBehaviour
    {
        private readonly HashSet<IEarlyFixedUpdate> _earlyFixedUpdates = new();
        private readonly HashSet<ILateFixedUpdate> _lateFixedUpdates = new();

        private void FixedUpdate()
        {
            OnEarlyFixedUpdate();
            OnLateFixedUpdate();
        }
        
        public void RegisterEarlyFixedUpdate(IEarlyFixedUpdate earlyUpdate) => _earlyFixedUpdates.Add(earlyUpdate);

        public void RegisterLateFixedUpdate(ILateFixedUpdate superLateUpdate) => _lateFixedUpdates.Add(superLateUpdate);

        public void UnregisterEarlyFixedUpdate(IEarlyFixedUpdate earlyUpdate) => _earlyFixedUpdates.Remove(earlyUpdate);

        public void UnregisterLateFixedUpdate(ILateFixedUpdate superLateUpdate) => _lateFixedUpdates.Remove(superLateUpdate);
    
        private void OnEarlyFixedUpdate()
        {
            using var e = _earlyFixedUpdates.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current?.EarlyFixedUpdate();
            }
        }

        private void OnLateFixedUpdate()
        {
            using var e = _lateFixedUpdates.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current?.LateFixedUpdate();
            }
        }
    }
}
}
