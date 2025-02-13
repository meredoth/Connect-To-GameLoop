using System;
using UnityEngine;

namespace ConnectToGameLoop.AwaitableFixedUpdate
{
public class NonMonoBehavior
{
    public async Awaitable FixedUpdate(Func<bool> predicate)
    {
        while (predicate())
        {
            Debug.Log("Fixed update from NonMonobehavior");
            await Awaitable.FixedUpdateAsync();
        }
    }
}
}
