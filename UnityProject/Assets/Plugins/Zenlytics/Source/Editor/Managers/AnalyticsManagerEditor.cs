using UnityEditor;

using Zenlytics.Managers;

namespace Zenlytics.Editor.Managers
{

    [CustomEditor(typeof(AnalyticsManager))]
    public class AnalyticsManagerEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            base.OnInspectorGUI();
            EditorGUI.EndDisabledGroup();
        }

    }

}