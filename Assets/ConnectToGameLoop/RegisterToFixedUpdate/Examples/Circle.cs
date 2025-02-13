using UnityEngine;

namespace ConnectToGameLoop.RegisterToFixedUpdate.Examples
{
public class Circle : MonoBehaviour
{
   private NonMonoBehavior _example;
    
   private void Start() => _example = new NonMonoBehavior();
   private void OnDestroy() => _example.UnregisterFromFixedUpdateManager();
}
}
