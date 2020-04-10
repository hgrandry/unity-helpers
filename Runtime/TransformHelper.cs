using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HGrandry.Helpers
{
    public static class TransformHelper
    {
        public static void Clear(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform t = transform.GetChild(i);
                Object.Destroy(t.gameObject);
            }
        }

        public static void Clear(this Transform transform, Predicate<Transform> condition)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform child = transform.GetChild(i);
                if (condition(child))
                    Object.Destroy(child.gameObject);
            }
        }

        public static IEnumerable<Transform> AllChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                yield return child;

                foreach (Transform t in AllChildren(child))
                {
                    yield return t;
                }
            }
        }

        public static IEnumerable<GameObject> ChildrenGameObjects(this Transform transform)
        {
            foreach (object child in transform)
            {
                yield return ((Transform) child).gameObject;
            }
        }

        public static IEnumerable<GameObject> FindChildrenWithTag(this Transform t, string tag)
        {
            foreach (Transform child in t.OfType<Transform>())
            {
                if (child.gameObject.CompareTag(tag))
                    yield return child.gameObject;

                foreach (GameObject go in FindChildrenWithTag(child, tag))
                {
                    yield return go;
                }
            }
        }
    }
}