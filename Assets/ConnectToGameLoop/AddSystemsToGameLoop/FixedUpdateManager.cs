using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace ConnectToGameLoop.AddSystemsToGameLoop
{
public static class FixedUpdateManager
{
   private static readonly HashSet<IEarlyFixedUpdate> _EarlyFixedUpdates = new();
   private static readonly HashSet<ILateFixedUpdate> _LateFixedUpdates = new();

   public static void RegisterEarlyFixedUpdate(IEarlyFixedUpdate earlyUpdate) => _EarlyFixedUpdates.Add(earlyUpdate);

   public static void RegisterLateFixedUpdate(ILateFixedUpdate superLateUpdate) => _LateFixedUpdates.Add(superLateUpdate);

   public static void UnregisterEarlyFixedUpdate(IEarlyFixedUpdate earlyUpdate) => _EarlyFixedUpdates.Remove(earlyUpdate);

   public static void UnregisterLateFixedUpdate(ILateFixedUpdate superLateUpdate) => _LateFixedUpdates.Remove(superLateUpdate);

   [RuntimeInitializeOnLoadMethod]
   private static void Init()
   {
      var defaultSystems = PlayerLoop.GetDefaultPlayerLoop();

      var myEarlyFixedUpdate = new PlayerLoopSystem
      {
         subSystemList = null,
         updateDelegate = OnEarlyFixedUpdate,
         type = typeof(EarlyFixedUpdate)
      };

      var myLateFixedUpdate = new PlayerLoopSystem
      {
         subSystemList = null,
         updateDelegate = OnLateFixedUpdate,
         type = typeof(LateFixedUpdate)
      };

      PlayerLoopSystem newPlayerLoop = new()
      {
         loopConditionFunction = defaultSystems.loopConditionFunction,
         type = defaultSystems.type,
         updateDelegate = defaultSystems.updateDelegate,
         updateFunction = defaultSystems.updateFunction
      };

      List<PlayerLoopSystem> newSubSystemList = new();

      foreach (var subSystem in defaultSystems.subSystemList)
      {
         if (subSystem.type != typeof(FixedUpdate))
         {
            newSubSystemList.Add(subSystem);
         }
         else
         {
            var newSubSystem = CreateNewSubsystem(subSystem);
            newSubSystemList.Add(newSubSystem);
         }
      }

      newPlayerLoop.subSystemList = newSubSystemList.ToArray();
      PlayerLoop.SetPlayerLoop(newPlayerLoop);

      return;

      PlayerLoopSystem CreateNewSubsystem(PlayerLoopSystem subSystem)
      {
         PlayerLoopSystem newSubSystem = new()
         {
            loopConditionFunction = subSystem.loopConditionFunction,
            type = subSystem.type,
            updateDelegate = subSystem.updateDelegate,
            updateFunction = subSystem.updateFunction
         };
            
         List<PlayerLoopSystem> newSystemSubSystemList = new();

         foreach (var newSystemSubsystem in subSystem.subSystemList)
         {
            if (newSystemSubsystem.type != typeof(FixedUpdate.ScriptRunBehaviourFixedUpdate))
            {
               newSystemSubSystemList.Add(newSystemSubsystem);
            }
            else
            {
               newSystemSubSystemList.Add(myEarlyFixedUpdate);
               newSystemSubSystemList.Add(newSystemSubsystem);
               newSystemSubSystemList.Add(myLateFixedUpdate);
            }
         }

         newSubSystem.subSystemList = newSystemSubSystemList.ToArray();
         return newSubSystem;
      }
   }

   private static void OnEarlyFixedUpdate()
   {
      using var e = _EarlyFixedUpdates.GetEnumerator();
      while (e.MoveNext())
      {
         e.Current?.EarlyFixedUpdate();
      }
   }

   private static void OnLateFixedUpdate()
   {
      using var e = _LateFixedUpdates.GetEnumerator();
      while (e.MoveNext())
      {
         e.Current?.LateFixedUpdate();
      }
   }
}
}
