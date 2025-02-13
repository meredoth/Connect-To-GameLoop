using UnityEngine;

namespace ConnectToGameLoop.AddSystemsToGameLoop.Examples
{
public class NonMonoBehavior : IEarlyFixedUpdate, ILateFixedUpdate
{
   public NonMonoBehavior()
   {
      FixedUpdateManager.RegisterEarlyFixedUpdate(this);
      FixedUpdateManager.RegisterLateFixedUpdate(this);
   }
   
   void IEarlyFixedUpdate.EarlyFixedUpdate() => Debug.Log("Inside NonMonoBehavior Early Fixed Update");
   void ILateFixedUpdate.LateFixedUpdate() => Debug.Log("Inside NonMonoBehavior Late Fixed Update");

   // Could also implement IDisposable for unregistering from FixedUpdateManager
   public void UnregisterFromFixedUpdateManager()
   {
      FixedUpdateManager.UnregisterEarlyFixedUpdate(this);
      FixedUpdateManager.UnregisterLateFixedUpdate(this);
   }
}
}
