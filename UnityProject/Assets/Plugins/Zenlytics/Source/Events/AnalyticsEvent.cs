﻿using System;
using System.Linq;
using System.Reflection;

using UnityEngine;

using Zenject;

using Zenlytics.Managers;

namespace Zenlytics.Events
{

    public abstract class AnalyticsEvent : ScriptableObject
    {

        private const string kListenMethodName = "Listen";
        private const string kUnlistenMethodName = "Unlisten";
        private const string kOnSignalFiredMethodName = "OnSignalFired";

        [SerializeField]
        private string m_SignalToListen;

        [Inject]
        private IAnalyticsManager m_AnalyticsManager;

        private object m_SignalInstance;
        private MethodInfo m_ListenMethodInfo;
        private MethodInfo m_UnlistenMethodInfo;
        private Delegate m_OnSignalFired;

        protected IAnalyticsManager AnalyticsManager
        {
            get
            {
                return m_AnalyticsManager;
            }
        }

        public void Initialize()
        {
            Type contractType = Type.GetType(m_SignalToListen);

            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

            m_ListenMethodInfo = contractType.GetMethod(kListenMethodName, bindingFlags);
            m_UnlistenMethodInfo = contractType.GetMethod(kUnlistenMethodName, bindingFlags);
            Type delegateType = m_ListenMethodInfo.GetParameters()[0].ParameterType;

            int argsCount = delegateType.GetGenericArguments().Length;

            Type analyticsEventType = typeof(AnalyticsEvent);

            bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            MethodInfo[] possibleMethods = analyticsEventType.GetMethods(bindingFlags);

            Func<MethodInfo, bool> predicate = m => m.Name == kOnSignalFiredMethodName &&
                                                    m.GetParameters().Length == argsCount;

            MethodInfo onSignalFiredMethodInfo = possibleMethods.FirstOrDefault(predicate);

            m_OnSignalFired = Delegate.CreateDelegate(delegateType, this, onSignalFiredMethodInfo);

            m_SignalInstance = ProjectContext.Instance.Container.Resolve(contractType);
        }

        public void Listen()
        {
            object[] args =
            {
                m_OnSignalFired
            };

            m_ListenMethodInfo.Invoke(m_SignalInstance, args);
        }

        public void Unlisten()
        {
            object[] args =
            {
                m_OnSignalFired
            };

            m_UnlistenMethodInfo.Invoke(m_SignalInstance, args);
        }

        protected abstract void HandleOnSignalFired(params object[] args);

        // Do not remove, this is called by reflection.
        private void OnSignalFired()
        {
            Debug.LogFormat("AnalyticsEvent.OnSignalFired()");
            HandleOnSignalFired();
        }

        // Do not remove, this is called by reflection.
        private void OnSignalFired(object a)
        {
            a = GetObjectOf(a);
            Debug.LogFormat("AnalyticsEvent.OnSignalFired(a:{0})", a);
            HandleOnSignalFired(a);
        }

        // Do not remove, this is called by reflection.
        private void OnSignalFired(object a, object b)
        {
            a = GetObjectOf(a);
            b = GetObjectOf(b);
            Debug.LogFormat("AnalyticsEvent.OnSignalFired(a:{0}, b:{1})", a, b);
            HandleOnSignalFired(a, b);
        }

        // Do not remove, this is called by reflection.
        private void OnSignalFired(object a, object b, object c)
        {
            a = GetObjectOf(a);
            b = GetObjectOf(b);
            c = GetObjectOf(c);
            Debug.LogFormat("AnalyticsEvent.OnSignalFired(a:{0}, b:{1}, c:{2})", a, b, c);
            HandleOnSignalFired(a, b, c);
        }

        // Do not remove, this is called by reflection.
        private void OnSignalFired(object a, object b, object c, object d)
        {
            a = GetObjectOf(a);
            b = GetObjectOf(b);
            c = GetObjectOf(c);
            d = GetObjectOf(d);
            Debug.LogFormat("AnalyticsEvent.OnSignalFired(a:{0}, b:{1}, c:{2}, d:{3})", a, b, c, d);
            HandleOnSignalFired(a, b, c, d);
        }

        private object GetObjectOf(object obj)
        {
            try
            {
                // In order to avoid a crazy C# issue when trying to handle with structs we have to do this.
                // I know, I'm not proud of it either, but what can I do? :(
                return !obj.Equals(null) ? obj : null;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

    }

}