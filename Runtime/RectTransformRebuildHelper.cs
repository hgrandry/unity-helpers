using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HGrandry.Helpers
{
    public static class RectTransformRebuildHelper
    {
        public static void RebuildBottomToTop(this RectTransform rt)
        {
            List<List<RectTransform>> children = GetChildrenSortedByLevel(rt);

            for (int i = children.Count - 1; i >= 0; i--)
            {
                List<RectTransform> childrenList = children[i];
                foreach (RectTransform child in childrenList)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(child);
                    Canvas.ForceUpdateCanvases();
                }
            }
        }

        private static List<List<RectTransform>> GetChildrenSortedByLevel(RectTransform root)
        {
            var list = new List<List<RectTransform>>();

            void GetChildrenSortedByLevel(Component scope, int level)
            {
                if (scope.transform.childCount == 0)
                    return;

                if (list.Count <= level)
                    list.Add(new List<RectTransform>());

                for (int i = 0; i < scope.transform.childCount; i++)
                {
                    var child = scope.transform.GetChild(i).GetComponent<RectTransform>();
                    list[level].Add(child);
                    GetChildrenSortedByLevel(child, level + 1);
                }
            }

            GetChildrenSortedByLevel(root, 0);

            return list;
        }
    }
}