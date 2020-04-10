using UnityEngine;

namespace HGrandry.Helpers
{
    public enum AnchorPresets
    {
        TopLeft,
        TopCenter,
        TopRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottomCenter,
        BottomRight,

        VerticalStretchLeft,
        VerticalStretchRight,
        VerticalStretchCenter,

        HorizontalStretchTop,
        HorizontalStretchMiddle,
        HorizontalStretchBottom,

        StretchAll
    }

    public enum PivotPresets
    {
        TopLeft,
        TopCenter,
        TopRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottomCenter,
        BottomRight,
    }

    public static class RectTransformHelper
    {
        public static void Apply(this RectTransform rectTransform, RectTransform value)
        {
            rectTransform.pivot = value.pivot;
            rectTransform.anchoredPosition = value.anchoredPosition;
            rectTransform.anchorMin = value.anchorMin;
            rectTransform.anchorMax = value.anchorMax;
            rectTransform.sizeDelta = value.sizeDelta;
            rectTransform.localScale = value.localScale;
        }

        public static void Stretch(this RectTransform rect)
        {
            rect.pivot = new Vector2(.5f, .5f);
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        public static void SetAnchors(this RectTransform rect, AnchorPresets align)
        {
            SetAnchors(rect, align, Vector2.zero, Vector2.zero);
        }

        public static void SetAnchors(this RectTransform rect, AnchorPresets align, Vector2 offsetMin, Vector2 offsetMax)
        {
            GetAnchors(align, out Vector2 anchorMin, out Vector2 anchorMax);
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = offsetMin;
            rect.offsetMax = offsetMax;
        }

        public static void SetPivot(this RectTransform source, PivotPresets preset)
        {
            source.pivot = GetPivot(preset);
        }

        public static Vector2 GetPivot(PivotPresets preset)
        {
            Vector2 pivot = Vector2.zero;
            switch (preset)
            {
                case (PivotPresets.TopLeft):
                {
                    pivot = new Vector2(0, 1);
                    break;
                }
                case (PivotPresets.TopCenter):
                {
                    pivot = new Vector2(0.5f, 1);
                    break;
                }
                case (PivotPresets.TopRight):
                {
                    pivot = new Vector2(1, 1);
                    break;
                }

                case (PivotPresets.MiddleLeft):
                {
                    pivot = new Vector2(0, 0.5f);
                    break;
                }
                case (PivotPresets.MiddleCenter):
                {
                    pivot = new Vector2(0.5f, 0.5f);
                    break;
                }
                case (PivotPresets.MiddleRight):
                {
                    pivot = new Vector2(1, 0.5f);
                    break;
                }

                case (PivotPresets.BottomLeft):
                {
                    pivot = new Vector2(0, 0);
                    break;
                }
                case (PivotPresets.BottomCenter):
                {
                    pivot = new Vector2(0.5f, 0);
                    break;
                }
                case (PivotPresets.BottomRight):
                {
                    pivot = new Vector2(1, 0);
                    break;
                }
            }

            return pivot;
        }

        public static void GetAnchors(AnchorPresets align, out Vector2 anchorMin, out Vector2 anchorMax)
        {
            switch (align)
            {
                case (AnchorPresets.TopLeft):
                {
                    anchorMin = new Vector2(0, 1);
                    anchorMax = new Vector2(0, 1);
                    break;
                }
                case (AnchorPresets.TopCenter):
                {
                    anchorMin = new Vector2(0.5f, 1);
                    anchorMax = new Vector2(0.5f, 1);
                    break;
                }
                case (AnchorPresets.TopRight):
                {
                    anchorMin = new Vector2(1, 1);
                    anchorMax = new Vector2(1, 1);
                    break;
                }

                case (AnchorPresets.MiddleLeft):
                {
                    anchorMin = new Vector2(0, 0.5f);
                    anchorMax = new Vector2(0, 0.5f);
                    break;
                }
                case (AnchorPresets.MiddleCenter):
                {
                    anchorMin = new Vector2(0.5f, 0.5f);
                    anchorMax = new Vector2(0.5f, 0.5f);
                    break;
                }
                case (AnchorPresets.MiddleRight):
                {
                    anchorMin = new Vector2(1, 0.5f);
                    anchorMax = new Vector2(1, 0.5f);
                    break;
                }

                case (AnchorPresets.BottomLeft):
                {
                    anchorMin = new Vector2(0, 0);
                    anchorMax = new Vector2(0, 0);
                    break;
                }
                case (AnchorPresets.BottomCenter):
                {
                    anchorMin = new Vector2(0.5f, 0);
                    anchorMax = new Vector2(0.5f, 0);
                    break;
                }
                case (AnchorPresets.BottomRight):
                {
                    anchorMin = new Vector2(1, 0);
                    anchorMax = new Vector2(1, 0);
                    break;
                }

                case (AnchorPresets.HorizontalStretchTop):
                {
                    anchorMin = new Vector2(0, 1);
                    anchorMax = new Vector2(1, 1);
                    break;
                }
                case (AnchorPresets.HorizontalStretchMiddle):
                {
                    anchorMin = new Vector2(0, 0.5f);
                    anchorMax = new Vector2(1, 0.5f);
                    break;
                }
                case (AnchorPresets.HorizontalStretchBottom):
                {
                    anchorMin = new Vector2(0, 0);
                    anchorMax = new Vector2(1, 0);
                    break;
                }

                case (AnchorPresets.VerticalStretchLeft):
                {
                    anchorMin = new Vector2(0, 0);
                    anchorMax = new Vector2(0, 1);
                    break;
                }
                case (AnchorPresets.VerticalStretchCenter):
                {
                    anchorMin = new Vector2(0.5f, 0);
                    anchorMax = new Vector2(0.5f, 1);
                    break;
                }
                case (AnchorPresets.VerticalStretchRight):
                {
                    anchorMin = new Vector2(1, 0);
                    anchorMax = new Vector2(1, 1);
                    break;
                }

                case (AnchorPresets.StretchAll):
                {
                    anchorMin = new Vector2(0, 0);
                    anchorMax = new Vector2(1, 1);
                    break;
                }

                default:
                    anchorMin = Vector2.zero;
                    anchorMax = Vector2.one;
                    break;
            }
        }
    }
}