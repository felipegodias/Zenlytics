using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using Zenlytics.Events;
using Zenlytics.Managers;

namespace Zenlytics.Editor.Importers
{

    public class EventsImportHandler : AssetPostprocessor
    {

        private static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            var eventsToInsert = new HashSet<AnalyticsEvent>();

            foreach (string str in importedAssets)
            {
                AnalyticsEvent analyticsEvent =
                    !str.EndsWith(".asset") ? null : AssetDatabase.LoadAssetAtPath<AnalyticsEvent>(str);

                if (analyticsEvent == null)
                {
                    continue;
                }

                eventsToInsert.Add(analyticsEvent);
            }

            bool checkForMissingEvents = deletedAssets.Any(str => str.EndsWith(".asset"));

            if (eventsToInsert.Count == 0 && !checkForMissingEvents)
            {
                return;
            }

            var serializedObject = new SerializedObject(ImportHandler.AnalyticsManager);
            SerializedProperty events = serializedObject.FindProperty("m_AnalyticsEvents");
            for (int i = 0; i < events.arraySize; i++)
            {
                SerializedProperty evt = events.GetArrayElementAtIndex(i);
                var analyticsEvent = evt.objectReferenceValue as AnalyticsEvent;
                if (analyticsEvent != null && !eventsToInsert.Contains(analyticsEvent))
                {
                    eventsToInsert.Add(analyticsEvent);
                }
            }

            events.ClearArray();
            events.arraySize = eventsToInsert.Count;

            int j = 0;

            IOrderedEnumerable<AnalyticsEvent> orderedSet = eventsToInsert.OrderBy(evt => evt.name);
            foreach (AnalyticsEvent analyticsEvent in orderedSet)
            {
                SerializedProperty evt = events.GetArrayElementAtIndex(j);
                evt.objectReferenceValue = analyticsEvent;
                j++;
            }

            serializedObject.ApplyModifiedProperties();
        }

    }

}