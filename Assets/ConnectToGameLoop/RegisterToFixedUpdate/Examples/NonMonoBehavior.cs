using UnityEngine;

namespace ConnectToGameLoop.RegisterToFixedUpdate.Examples
{
public class NonMonoBehavior : IEarlyFixedUpdate,ILateFixedUpdate
{
   public NonMonoBehavior()
   {
      FixedUpdateManager.Instance.RegisterEarlyFixedUpdate(this);
      FixedUpdateManager.Instance.RegisterLateFixedUpdate(this);
   }
   
   void IEarlyFixedUpdate.EarlyFixedUpdate() => Debug.Log("Inside NonMonoBehavior Early Fixed Update");
   void ILateFixedUpdate.LateFixedUpdate() => Debug.Log("Inside NonMonoBehavior Late Fixed Update");

   // Could also implement IDisposable for unregistering from FixedUpdateManager
   public void UnregisterFromFixedUpdateManager()
   {
      FixedUpdateManager.Instance.UnregisterEarlyFixedUpdate(this);
      FixedUpdateManager.Instance.UnregisterLateFixedUpdate(this);
   }
}
}
