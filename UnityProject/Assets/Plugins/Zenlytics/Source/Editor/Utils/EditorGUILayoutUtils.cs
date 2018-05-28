using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.CSharp;

using UnityEditor;

using UnityEngine;

namespace Zenlytics.Editor.Utils
{

    public static class EditorGUILayoutUtils
    {

        public static Type DrawTypePopup(SerializedProperty obj, Type[] types)
        {
            string currentValue = obj.stringValue;
            var displayValues = new string[types.Length];
            var realValues = new string[types.Length];
            int index = 0;

            for (int i = 0; i < types.Length; i++)
            {
                if (currentValue == types[i].AssemblyQualifiedName)
                {
                    index = i;
                }

                displayValues[i] = types[i].Name;
                realValues[i] = types[i].AssemblyQualifiedName;
            }

            index = EditorGUILayout.Popup(obj.displayName, index, displayValues);
            obj.stringValue = realValues[index];
            return types[index];
        }

        public static Type DrawSignalArgumentsPopup(SerializedProperty obj, Type signalType)
        {
            Type[] genericArguments = signalType.BaseType.GetGenericArguments();
            var arguments = new Type[genericArguments.Length - 1];
            Array.Copy(genericArguments, 1, arguments, 0, arguments.Length);

            var displayNames = new string[arguments.Length];
            for (int i = 0; i < arguments.Length; i++)
            {
                displayNames[i] = $"[{i}] {arguments[i].FullName.Replace("+", ".")}";
            }

            string displayName = obj.displayName;
            int index = Mathf.Min(obj.intValue, arguments.Length - 1);

            Color color = GUI.color;

            Type currentType = arguments[index];
            if (currentType.IsValueType)
            {
                EditorGUILayout.HelpBox(
                                        $"{currentType.FullName} is a value type. Value types are not supported yet. Please select a reference type.",
                                        MessageType.Error);
                GUI.color = Color.red;
            }

            index = EditorGUILayout.Popup(displayName, index, displayNames);
            obj.intValue = index;

            GUI.color = color;

            return !currentType.IsValueType ? arguments[index] : null;
        }

        public static PropertyInfo DrawPropertyPopup(
            SerializedProperty obj,
            Type type,
            Type[] types,
            BindingFlags flags)
        {
            PropertyInfo[] properties = type.GetProperties(flags);
            var possibleTypes = new List<Type>(types);
            List<PropertyInfo> possibleProperties =
                (from property in properties
                 let propertyType = property.PropertyType
                 from possibleType in possibleTypes
                 where possibleType == propertyType || propertyType.IsSubclassOf(possibleType)
                 select property).ToList();

            int index = 0;
            var displayValues = new string[possibleProperties.Count];

            using (var provider = new CSharpCodeProvider())
            {
                for (int i = 0; i < possibleProperties.Count; i++)
                {
                    var typeRef = new CodeTypeReference(possibleProperties[i].PropertyType);
                    string typeName = provider.GetTypeOutput(typeRef);
                    string propertyName = possibleProperties[i].Name;
                    displayValues[i] = $"{propertyName} : {typeName}";
                    if (obj.stringValue == propertyName)
                    {
                        index = i;
                    }
                }
            }

            if (displayValues.Length == 0)
            {
                var stringBuilder = new StringBuilder();
                using (var provider = new CSharpCodeProvider())
                {
                    for (int i = 0; i < possibleTypes.Count; i++)
                    {
                        var typeRef = new CodeTypeReference(possibleTypes[i]);

                        string typeName = provider.GetTypeOutput(typeRef);

                        if (i == 0)
                        {
                            stringBuilder.AppendFormat("{0}", typeName);
                        }
                        else if (i == possibleTypes.Count - 1)
                        {
                            stringBuilder.AppendFormat(" or {0}", typeName);
                        }
                        else
                        {
                            stringBuilder.AppendFormat(", {0}", typeName);
                        }
                    }
                }

                string message = $"No suitable properties found. Property type must derive from {stringBuilder}.";
                EditorGUILayout.HelpBox(message, MessageType.Error);
                return null;
            }

            index = EditorGUILayout.Popup(obj.displayName, index, displayValues);
            PropertyInfo selectedProperty = possibleProperties[index];
            obj.stringValue = selectedProperty.Name;

            return selectedProperty;
        }

    }

}