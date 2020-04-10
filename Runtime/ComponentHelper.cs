using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HGrandry.Helpers
{
    public static class ComponentHelper
    {
        public static T GetOrCreateComponent<T>(this MonoBehaviour component) where T : Component
        {
            var comp = component.gameObject.GetComponent<T>();
            if (comp == null)
            {
                comp = component.gameObject.AddComponent<T>();
            }

            return comp;
        }

        public static T GetOrCreateComponent<T>(this GameObject go) where T : Component
        {
            var comp = go.GetComponent<T>();
            if (comp == null)
            {
                comp = go.AddComponent<T>();
            }

            return comp;
        }

        public static string GetHierarchyPath(this Component component)
        {
            if (component)
            {
                string path = component.name;
                while (component && component.transform.parent)
                {
                    component = component.transform.parent;
                    path = component.name + "/" + path;
                }

                return path;
            }

            return string.Empty;
        }

        public static T[] GetAll<T>(this MonoBehaviour comp)
        {
            var scene = comp.gameObject.scene;
            if (scene.name == null || !scene.isLoaded)
                return new T[0];

            var all = new List<MonoBehaviour>();

            foreach (GameObject rootGameObject in scene.GetRootGameObjects())
            {
                rootGameObject.GetComponentsInChildren(true, all);
            }

            var result = all.OfType<T>().ToArray();
            return result;
        }
    }
}