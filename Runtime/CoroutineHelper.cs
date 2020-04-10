using System;
using System.Collections;
using UnityEngine;

namespace HGrandry.Helpers
{
    public static class CoroutineHelper
    {
        public static Coroutine ExecuteAfterFixeUpdate(this MonoBehaviour monoBehaviour, Action action)
        {
            return monoBehaviour.StartCoroutine(ExecuteAfter(new WaitForFixedUpdate(), action));
        }

        public static Coroutine ExecuteAfterEndOfFrame(this MonoBehaviour monoBehaviour, Action action)
        {
            return monoBehaviour.StartCoroutine(ExecuteAfter(new WaitForEndOfFrame(), action));
        }

        public static Coroutine ExecuteAfterSeconds(this MonoBehaviour monoBehaviour, float seconds, Action action)
        {
            return monoBehaviour.StartCoroutine(ExecuteAfter(new WaitForSeconds(seconds), action));
        }

        private static IEnumerator ExecuteAfter(YieldInstruction yieldInstruction, Action action)
        {
            yield return yieldInstruction;
            action();
        }
    }
}