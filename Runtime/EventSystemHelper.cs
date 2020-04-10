using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if TextMeshPro
using TMPro;
#endif

namespace HGrandry.Helpers
{
  public static class EventSystemHelper
  {
    public static bool IsFocusOnInputField(this EventSystem eventSystem)
    {
      if (eventSystem == null)
        return false;

      GameObject selectedGo = eventSystem.currentSelectedGameObject;

      bool isFocusOnInputField = selectedGo != null
                                 && (selectedGo.GetComponent<InputField>() != null
#if TextMeshPro
                                      || selectedGo.GetComponent<TMP_InputField>() != null
#endif                             
                                      );

      return isFocusOnInputField;
    }

    public static void UpdateTabNavigation(this EventSystem eventSystem)
    {
      if (Input.GetKeyDown(KeyCode.Tab))
      {
        var currentGo = eventSystem.currentSelectedGameObject;
        if (currentGo == null)
          return;

        var currentControl = currentGo.GetComponent<Selectable>();
        if (currentControl == null)
          return;

        bool isShiftDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        var navigation = currentControl.navigation;
        Selectable target = isShiftDown
            ? navigation.selectOnLeft
                ? navigation.selectOnLeft
                : navigation.selectOnUp
            : navigation.selectOnRight
                ? navigation.selectOnRight
                : navigation.selectOnDown;

        if (target != null)
        {
          target.Select();
        }
      }
    }

  }
}