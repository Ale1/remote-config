using UnityEditor;
using UnityEngine;

namespace Homa.RemoteConfig.Editor
{
    public class HomaWindow : EditorWindow
    {
        //Embedded editors
        private UnityEditor.Editor remoteReconEditor;
        private UnityEditor.Editor localReconEditor;
        
        //scrolling
        private Vector2 _scrollPosition;
        
        //styles //todo: move to a style bank?
        private GUIStyle titleStyle;

        [MenuItem("Tools/ReconTool")]
        public static void ShowWindow()
        {
            GetWindow<HomaWindow>("Remote Config tool");
        }

        private void OnEnable()
        {
            titleStyle = new GUIStyle
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal =
                {
                    textColor = EditorGUIUtility.isProSkin ? Color.white : Color.gray
                }
            };
        }

        void OnGUI()
        {
            DrawGUI(); //gui logic is moved to a different method so SettingsProvider can access it.
        }

        public void DrawGUI()
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            ShowRemoteEmbeddedEditor();
            EditorGUILayout.Space(10);
            ShowLocalEmbeddedEditor();
            GUILayout.Space(15);
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
        }

        private void ShowRemoteEmbeddedEditor()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            EditorGUILayout.LabelField(DataManager.RemoteRecon.name, titleStyle); // todo: to pretty name
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Fetch Remote"))
            {
                DataManager.SyncRemoteReconWithServer(RefreshJsons);
            }

            if (GUILayout.Button("Upload To Server"))
            {
                EditorUtility.DisplayDialog("Not Available",
                    "Not possible to upload to server in this demo, since there is no endpoint for this", "Okey Dokey");
            }

            GUILayout.EndHorizontal();

            GUI.enabled = false;
            GUILayout.Space(10);
            UnityEditor.Editor.CreateCachedEditor(DataManager.RemoteRecon, typeof(ReconSettingsEditor), ref remoteReconEditor);
            remoteReconEditor.OnInspectorGUI();
            GUILayout.EndVertical();
            GUI.enabled = true;
        }

        private void ShowLocalEmbeddedEditor()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            EditorGUILayout.LabelField(DataManager.LocalRecon.name, titleStyle);
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Apply Remote unto Local"))
            {
                DataManager.ApplyRemoteUntoLocal();
                RefreshJsons();
            }

            if (GUILayout.Button("Apply Local unto Remote"))
            {
                DataManager.ApplyLocalUntoRemote();
                RefreshJsons();
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            UnityEditor.Editor.CreateCachedEditor(DataManager.LocalRecon, typeof(ReconSettingsEditor), ref localReconEditor);
            localReconEditor.OnInspectorGUI();
            GUILayout.EndVertical();
        }

        public void RefreshJsons()
        {
            if (remoteReconEditor is ReconSettingsEditor remoteEditor)
            {
                remoteEditor.RefreshJson(); 
            }

            if (localReconEditor is ReconSettingsEditor localEditor)
            {
                localEditor.RefreshJson(); 
            }
        }

    }
}