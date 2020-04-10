using UnityEngine;

namespace HGrandry.Helpers
{
    public class AwakeChildren : MonoBehaviour
    {
        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}