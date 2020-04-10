using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HGrandry.Helpers
{
    /// <summary>
    /// Exposes function and keyboard shortcuts to move gameobjects in the hierarchy
    /// </summary>
    class HierarchyHelper : MonoBehaviour
    {
        /// <summary>
        /// CTRL + E to enable / disable a gameobject
        /// </summary>
        [MenuItem("Tools/Hierarchy/Activate-deactivate objects %e")]
        public static void DoDeactivate()
        {
            if (!Selection.gameObjects.Any())
                return;

            Undo.RegisterCompleteObjectUndo(Selection.gameObjects.Cast<Object>().ToArray(), "Activate-deactivate objects");

            foreach (GameObject go in Selection.gameObjects)
            {
                go.SetActive(!go.activeSelf);
            }
        }

        /// <summary>
        /// CTRL + SHIFT + E to enable a game object and disable all its siblings
        /// </summary>
        [MenuItem("Tools/Hierarchy/Activate only selected object %#e")]
        public static void SetActive()
        {
            if (!Selection.gameObjects.Any())
                return;

            var o = Selection.gameObjects.First();
            var parent = o.transform.parent;

            if (parent == null)
                return;

            var toActivate = new List<GameObject> {o};

            // deactivate siblings
            GameObject[] toDeactivate = parent.transform.OfType<Transform>().Select(t => t.gameObject).ToArray();

            // activate parents to make sure the object is visible
            while (parent != null)
            {
                toActivate.Add(parent.gameObject);
                parent = parent.transform.parent;
            }

            Object[] allModifiedObjects = toActivate.Concat(toDeactivate).OfType<Object>().ToArray();
            Undo.RegisterCompleteObjectUndo(allModifiedObjects, "Activate only selected object");

            foreach (GameObject go in toDeactivate)
            {
                go.SetActive(false);
            }

            foreach (GameObject go in toActivate)
            {
                go.SetActive(true);
            }
        }

        /// <summary>
        /// ALT + left to move a gameobject to its parent level
        /// </summary>
        [MenuItem("Tools/Hierarchy/Move to parent level %#A")]
        public static void MoveAsParent()
        {
            var selection = Selection.activeGameObject;
            if (selection == null)
                return;

            if (selection.transform.parent == null)
                return;

            Transform parent = selection.transform.parent;
            var parentIndex = parent.GetSiblingIndex();

            Undo.SetTransformParent(selection.transform, parent.parent, "Move to parent level");
            selection.transform.SetSiblingIndex(parentIndex + 1);
        }

        /// <summary>
        /// ALT + right to move a gameobject in its direct neighbor scope (as child)
        /// </summary>
        [MenuItem("Tools/Hierarchy/Move to child level %#D")]
        public static void MoveAsChild()
        {
            var selection = Selection.activeGameObject;
            if (selection == null)
                return;

            var index = selection.transform.GetSiblingIndex();
            if (index == 0)
                return;

            Transform previous;

            if (selection.transform.parent == null)
            {
                if (selection.scene.name == null)
                    return;

                GameObject[] rootGameObjects = selection.scene.GetRootGameObjects();
                previous = rootGameObjects[index - 1].transform;
            }
            else
            {
                previous = selection.transform.parent.GetChild(index - 1);
                if (previous == null)
                    return;
            }

            Undo.SetTransformParent(selection.transform, previous.transform, "Move to child level");
        }

        /// <summary>
        /// ALT + up to move a gameobject in up in the hierarchy (stay on the same level, only the order changes)
        /// Disclaimer: still some issues with gameobjects at the root level
        /// </summary>
        [MenuItem("Tools/Hierarchy/Move up %#UP")]
        public static void MoveUp()
        {
            var selection = Selection.activeGameObject;
            if (selection == null)
                return;

            var index = selection.transform.GetSiblingIndex();
            if (index == 0)
                return;

            Undo.RegisterCompleteObjectUndo(selection, "Move up");

            try
            {
                selection.transform.SetSiblingIndex(index - 1);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// ALT + down to move a gameobject in down in the hierarchy (stay on the same level, only the order changes)
        /// Disclaimer: still some issues with gameobjects at the root level
        /// </summary>
        [MenuItem("Tools/Hierarchy/Move down %#DOWN")]
        public static void MoveDown()
        {
            var selection = Selection.activeGameObject;
            if (selection == null)
                return;

            var index = selection.transform.GetSiblingIndex();
            try
            {
                selection.transform.SetSiblingIndex(index + 1);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}