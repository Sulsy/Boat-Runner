using UnityEditor;
using UnityEditor.UI;

namespace CustomUi
{
    [CustomEditor(typeof(CustomButton))]
    public class CustomButtonEditor : ButtonEditor
    {
        private SerializedProperty uiStateProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            uiStateProperty = serializedObject.FindProperty("buttonType");
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