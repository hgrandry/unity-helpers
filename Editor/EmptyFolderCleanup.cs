using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HGrandry.Helpers
{
    public static class CleanupEmptyFolder
    {
        [MenuItem("Tools/Delete empty folders")]
        public static void DeleteEmptyFolders()
        {
            string root = Application.dataPath;
            string[] dirs = Directory.GetDirectories(root);
            foreach (var dir in dirs)
            {
                DeleteEmptyFolders(dir);
            }

            AssetDatabase.Refresh();
        }

        private static void DeleteEmptyFolders(string dir)
        {
            string[] dirs = Directory.GetDirectories(dir);
            foreach (var subDir in dirs)
            {
                DeleteEmptyFolders(subDir);
            }

            dirs = Directory.GetDirectories(dir);
            string[] files = Directory.GetFiles(dir);

            if (!dirs.Any() && !files.Any())
                Directory.Delete(dir);
        }
    }
}