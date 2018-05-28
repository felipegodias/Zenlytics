using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using Zenlytics.Adapters;
using Zenlytics.Managers;

namespace Zenlytics.Editor.Importers
{

    public class AdaptersImportHandler : AssetPostprocessor
    {

        private static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            var adaptersToInsert = new HashSet<AnalyticsAdapter>();

            foreach (string str in importedAssets)
            {
                AnalyticsAdapter analyticsAdapter =
                    !str.EndsWith(".asset") ? null : AssetDatabase.LoadAssetAtPath<AnalyticsAdapter>(str);

                if (analyticsAdapter == null)
                {
                    continue;
                }

                adaptersToInsert.Add(analyticsAdapter);
            }

            bool checkForMissingEvents = deletedAssets.Any(str => str.EndsWith(".asset"));

            if (adaptersToInsert.Count == 0 && !checkForMissingEvents)
            {
                return;
            }

            var serializedObject = new SerializedObject(ImportHandler.AnalyticsManager);
            SerializedProperty events = serializedObject.FindProperty("m_AnalyticsAdapters");
            for (int i = 0; i < events.arraySize; i++)
            {
                SerializedProperty evt = events.GetArrayElementAtIndex(i);
                var analyticsAdapter = evt.objectReferenceValue as AnalyticsAdapter;
                if (analyticsAdapter != null && !adaptersToInsert.Contains(analyticsAdapter))
                {
                    adaptersToInsert.Add(analyticsAdapter);
                }
            }

            events.ClearArray();
            events.arraySize = adaptersToInsert.Count;

            int j = 0;

            IOrderedEnumerable<AnalyticsAdapter> orderedSet = adaptersToInsert.OrderBy(evt => evt.name);
            foreach (AnalyticsAdapter analyticsAdapter in orderedSet)
            {
                SerializedProperty evt = events.GetArrayElementAtIndex(j);
                evt.objectReferenceValue = analyticsAdapter;
                j++;
            }

            serializedObject.ApplyModifiedProperties();
        }

    }

}