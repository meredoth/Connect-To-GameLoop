using UnityEngine;
using UnityEngine.InputSystem;

namespace ConnectToGameLoop.AwaitableFixedUpdate
{
public class Square : MonoBehaviour
{

    private NonMonoBehavior _example;
    private bool _shouldRun = true;

    private void Start()
    {
        _example = new NonMonoBehavior();
        _example.FixedUpdate(ShouldRun);
    }
    
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            _shouldRun = false;
    }

    private void FixedUpdate() => Debug.Log("Square Fixed Update");

    private bool ShouldRun() => _shouldRun;
}
}
