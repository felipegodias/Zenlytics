using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEditor;

using Zenlytics.Editor.Fields;
using Zenlytics.Editor.Utils;
using Zenlytics.Events;

namespace Zenlytics.Editor.Events
{

    [CustomEditor(typeof(AnalyticsEvent), true)]
    public class AnalyticsEventEditor : UnityEditor.Editor
    {

        private SerializedProperty m_SignalName;
        private List<SerializedProperty> m_Fields;

        private void OnEnable()
        {
            m_SignalName = serializedObject.FindProperty("m_SignalToListen");
            m_Fields = new List<SerializedProperty>();

            var regex = new Regex("Analytics.*Field");
            SerializedProperty ii = serializedObject.GetIterator();
            while (ii.Next(true))
            {
                if (!regex.IsMatch(ii.type))
                {
                    continue;
                }

                SerializedProperty field = ii.Copy();
                m_Fields.Add(field);
            }
        }

        public override void OnInspectorGUI()
        {
            Type signalType = EditorGUILayoutUtils.DrawTypePopup(m_SignalName, SignalEditorUtils.SignalTypes);
            foreach (SerializedProperty serializedProperty in m_Fields)
            {
                AnalyticsFieldEditor.DrawAnalyticsField(serializedProperty, signalType);
            }

            serializedObject.ApplyModifiedProperties();
        }

    }

}