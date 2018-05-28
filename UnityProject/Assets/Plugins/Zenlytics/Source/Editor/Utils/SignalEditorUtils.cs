using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Zenject;

namespace Zenlytics.Editor.Utils
{

    public static class SignalEditorUtils
    {

        private static Type[] s_SignalTypes;

        private static Dictionary<Type, List<PropertyInfo>> s_SignalProperties;

        public static Type[] SignalTypes
        {
            get
            {
                if (s_SignalTypes == null)
                {
                    LoadSignalNamesAndTypes();
                }

                return s_SignalTypes;
            }
        }

        private static void LoadSignalNamesAndTypes()
        {
            var signalNames = new List<string>();
            var signalTypes = new List<Type>();
            AppDomain appDomain = AppDomain.CurrentDomain;
            Assembly[] assemblies = appDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type[] assemblyTypes = assembly.GetTypes();
                foreach (Type type in assemblyTypes)
                {
                    bool isSignalBase = type.IsSubclassOf(typeof(SignalBase));
                    bool isZenjectSignal = type.FullName.Contains("Zenject.Signal");
                    if (isSignalBase && !isZenjectSignal)
                    {
                        signalNames.Add(type.FullName);
                        signalTypes.Add(type);
                    }
                }
            }

            s_SignalTypes = signalTypes.ToArray();
        }

    }

}