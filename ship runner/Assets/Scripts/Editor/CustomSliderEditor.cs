using UnityEditor;
using UnityEditor.UI;

namespace CustomUi
{
    [CustomEditor(typeof(CustomSlider))]
    public class CustomSliderEditor : SliderEditor
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