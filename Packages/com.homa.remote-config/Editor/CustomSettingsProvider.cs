using UnityEditor;
using UnityEngine;

namespace Homa.RemoteConfig.Editor
{
    public class ProjectSettingsProvider : SettingsProvider
    {
        private HomaWindow editorWindowInstance;

        public ProjectSettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
            : base(path, scope)
        {
            editorWindowInstance = ScriptableObject.CreateInstance<HomaWindow>();
        }

        public override void OnGUI(string searchContext)
        {
            if (editorWindowInstance != null)
            {
                editorWindowInstance.DrawGUI();
            }
        }

        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            var provider = new ProjectSettingsProvider("Project/RemoteConfig")
            {
                label = "Homa Remote Config",
                keywords = new string[] { "remote", "config", "homa", "settings" }
            };

            return provider;
        }
    }
}