using UnityEngine;

namespace HGrandry.Helpers
{
    /// <summary>
    /// For guide objects that need to stay alive in edit mode but should be hidden at runtime.
    /// Also, set the tag "EditorOnly" on your GameObject so it actually doesn't end up in the build at all.
    /// </summary>
    public sealed class HideOnAwake : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}