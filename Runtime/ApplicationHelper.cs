using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HGrandry.Helpers
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
  public static class ApplicationHelper
  {
    private static bool _isPlaying;

    // Editor only: true if the engine is stopping
    public static bool IsStopping { get; private set; }

    public static bool IsStarting { get; private set; }

#if UNITY_EDITOR

        // Work around an editor crash when unloading scenes while the player is stopping.

        static ApplicationHelper()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            // the application is stopping if the playMode changed as Application.isPlaying is still true)
            var wasPlaying = _isPlaying;
            _isPlaying = Application.isPlaying;
            IsStopping = wasPlaying && _isPlaying; // this is true just before the engine stops in the editor.
            IsStarting = !wasPlaying && _isPlaying;
        }
#endif
  }
}