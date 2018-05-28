using UnityEditor;

using Zenlytics.Managers;

namespace Zenlytics.Editor.Importers
{

    public static class ImportHandler
    {

        private static AnalyticsManager s_AnalyticsManager;

        public static AnalyticsManager AnalyticsManager
        {
            get
            {
                if (s_AnalyticsManager != null)
                {
                    return s_AnalyticsManager;
                }

                string scriptName = typeof(AnalyticsManager).Name;

                string[] guids = AssetDatabase.FindAssets($"{scriptName}");
                if (guids == null || guids.Length == 0)
                {
                    return null;
                }


                foreach (string guid in guids)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    s_AnalyticsManager = AssetDatabase.LoadAssetAtPath<AnalyticsManager>(assetPath);
                    if (s_AnalyticsManager != null)
                    {
                        break;
                    }
                }

                return s_AnalyticsManager;
            }
        }

    }

}