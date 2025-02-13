using System.Text;
using UnityEngine;
using UnityEngine.LowLevel;

namespace ConnectToGameLoop.AddSystemsToGameLoop
{
public static class DefaultGameLoop
{
   //[RuntimeInitializeOnLoadMethod]
   private static void Init()
   {
      StringBuilder sb = new();
      ShowPlayerLoop(PlayerLoop.GetDefaultPlayerLoop(), sb, 0);
      Debug.Log(sb);
   }

   public static void ShowPlayerLoop(PlayerLoopSystem playerLoopSystem, StringBuilder text, int inline)
   {
      if (playerLoopSystem.type != null)
      {
         for (var i = 0; i < inline; i++)
         {
            text.Append("\t");
         }
         text.AppendLine(playerLoopSystem.type.Name);
      }

      if (playerLoopSystem.subSystemList != null)
      {
         inline++;
         foreach (var s in playerLoopSystem.subSystemList)
         {
            ShowPlayerLoop(s, text, inline);
         }
      }
   }
}
}
