using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HGrandry.Helpers
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    [CanEditMultipleObjects]
    public class InspectorButtonDrawer : Editor
    {
        private System.Type _targetType;
        private List<MethodInfo> _buttons = new List<MethodInfo>();

        private void OnEnable()
        {
            if (serializedObject.targetObject == null)
                return;

            _targetType = serializedObject.targetObject.GetType();
            _buttons = _targetType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(m => m.GetCustomAttributes(typeof(InspectorButtonAttribute), true).Length > 0)
                .ToList();
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            SetInspectorButtons();
        }

        private void SetInspectorButtons()
        {
            if (_buttons.Count <= 0)
                return;
            foreach (MethodInfo methodInfo in _buttons)
            {
                var buttonName = ((InspectorButtonAttribute[]) methodInfo.GetCustomAttributes(typeof(InspectorButtonAttribute), true)).First().Name;
                if (!GUILayout.Button(buttonName ?? methodInfo.Name))
                    continue;

                foreach (Object t in targets)
                {
                    methodInfo.Invoke(t, new object[0]);
                }
            }
        }
    }
}