using UnityEngine;

namespace ConnectToGameLoop.RegisterToFixedUpdate.Examples
{
public class Square : MonoBehaviour, IEarlyFixedUpdate, ILateFixedUpdate
{
   private void OnEnable()
   {
      FixedUpdateManager.Instance.RegisterEarlyFixedUpdate(this);
      FixedUpdateManager.Instance.RegisterLateFixedUpdate(this);
   }

   private void Update() => Debug.Log("Inside Square's Update");

   // This would be wrong in this example, all Monobehaviors should NOT have a FixedUpdate
   // Instead they should only use FixedUpdateManager fixed update methods by registering to it
   // private void FixedUpdate() => Debug.Log("Inside Square's Fixed Update");

   private void OnDisable()
   {
      if(gameObject.scene.isLoaded)
      {
         FixedUpdateManager.Instance.UnregisterEarlyFixedUpdate(this);
         FixedUpdateManager.Instance.UnregisterLateFixedUpdate(this);
      }
   }

   void IEarlyFixedUpdate.EarlyFixedUpdate() => Debug.Log("Inside Square's Early Fixed Update");

   void ILateFixedUpdate.LateFixedUpdate() => Debug.Log("Inside Square's Late Fixed Update");
}
}
