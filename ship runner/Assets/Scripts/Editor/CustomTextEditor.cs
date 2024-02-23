using Enums;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEditor.UI;

namespace CustomUi
{
    [CustomEditor(typeof(CustomText))]
    public class CustomTextEditor : TextEditor
    {
        private SerializedProperty uiStateProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            uiStateProperty = serializedObject.FindProperty("uiType");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        
            serializedObject.Update();
            EditorGUILayout.PropertyField(uiStateProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}