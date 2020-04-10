using UnityEngine;

namespace HGrandry.Helpers.Data
{
    public static class PlayerPrefsUtil
    {
        public static bool GetBool(string key)
        {
            return !PlayerPrefs.GetString(key, "").ToLower().Equals("false");
        }

        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetString(key, value.ToString());
        }
    }
}