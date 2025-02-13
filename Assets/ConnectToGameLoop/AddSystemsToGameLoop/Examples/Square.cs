using UnityEngine;

namespace ConnectToGameLoop.AddSystemsToGameLoop.Examples
{
public class Square : MonoBehaviour, IEarlyFixedUpdate, ILateFixedUpdate
{
   private void OnEnable()
   {
      FixedUpdateManager.RegisterEarlyFixedUpdate(this);
      FixedUpdateManager.RegisterLateFixedUpdate(this);
   }

   private void Update() => Debug.Log("Inside Square's Update");

   private void FixedUpdate() => Debug.Log("Inside Square's Fixed Update");

   private void OnDisable()
   {
      FixedUpdateManager.UnregisterEarlyFixedUpdate(this);
      FixedUpdateManager.UnregisterLateFixedUpdate(this);
   }

   void IEarlyFixedUpdate.EarlyFixedUpdate() => Debug.Log("Inside Square's Early Fixed Update");

   void ILateFixedUpdate.LateFixedUpdate() => Debug.Log("Inside Square's Late Fixed Update");
}
}
