using UnityEngine;
using UnityEditor;


    //todo: use BeginChangeChecks to enable or disable save and clear buttons.  Alternatively, see if unity built-in "Apply-Undo" will work here.

namespace Homa.RemoteConfig.Editor
{

    [CustomEditor(typeof(ReconSettings))]
    public class ReconSettingsEditor : UnityEditor.Editor
    {
        private Vector2 scrollPosition;

        private string _json;
        private ReconSettings _settings;

        //Props
        private SerializedProperty tokenProp;
        private SerializedProperty manifestNameProp;
        private SerializedProperty androidBundleIdProp;
        private SerializedProperty iosBundleIdProp;
        private SerializedProperty packagesProp;


        void OnEnable()
        {
           
        }

        public override void OnInspectorGUI()
        {
            if (_settings == null)
            {
                _settings = (ReconSettings)target;
                RefreshJson(); // Call this here after ensuring _settings is not null
            }

            // Initialize SerializedProperties here to ensure the target is fully loaded
            if (tokenProp == null) tokenProp = serializedObject.FindProperty("token");
            if (manifestNameProp == null) manifestNameProp = serializedObject.FindProperty("manifestName");
            if (androidBundleIdProp == null) androidBundleIdProp = serializedObject.FindProperty("androidBundleId");
            if (iosBundleIdProp == null) iosBundleIdProp = serializedObject.FindProperty("iosBundleId");
            if (packagesProp == null) packagesProp = serializedObject.FindProperty("packages");

            serializedObject.Update(); // Update the SerializedObject
            // Draw the simple fields
            if (tokenProp != null) EditorGUILayout.PropertyField(tokenProp);
            if (manifestNameProp != null) EditorGUILayout.PropertyField(manifestNameProp);
            if (androidBundleIdProp != null) EditorGUILayout.PropertyField(androidBundleIdProp);
            if (iosBundleIdProp != null) EditorGUILayout.PropertyField(iosBundleIdProp);
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            if (packagesProp != null)
            {
                EditorGUILayout.PropertyField(packagesProp, new GUIContent("Packages"), true);
            }

            serializedObject.ApplyModifiedProperties(); // Apply changes to the SerializedObject


            GUI.enabled = HasChanged();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save"))
            {
                var success = _settings.SaveToCache();
                RefreshJson();
            }
            
            if (GUILayout.Button("Clear Unsaved Changes"))
            {
                var success = _settings.RestoreFromCache();
                RefreshJson();
            }
            GUI.enabled =true;
            GUILayout.EndHorizontal();

            //Add JsonViewer
            GUILayout.Space(20);
            ShowJsonViewer();
            EditorGUILayout.EndScrollView();

        }

        private void ShowJsonViewer()
        {
            GUI.enabled = true;
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("JSON Representation:");
            if (GUILayout.Button("refresh", GUILayout.MaxWidth(50)))
            {
                RefreshJson();
            }
            GUILayout.EndHorizontal();
            GUI.enabled = false;
            EditorGUILayout.TextArea(_json, GUILayout.MinHeight(600));
            GUI.enabled = true;

        }

        public void RefreshJson()
        {
            _json = _settings.CachedJson();
        }
        
        private bool HasChanged()
        {
            return true;
            //todo
        }

    }
}