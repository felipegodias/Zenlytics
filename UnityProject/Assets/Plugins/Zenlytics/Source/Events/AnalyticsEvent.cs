using System;
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

        private IAnalyticsManager m_AnalyticsManager;

        private InjectContext m_InjectContext;
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

        public void Initialize(IAnalyticsManager analyticsManager)
        {
            m_AnalyticsManager = analyticsManager;

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

            m_InjectContext = new InjectContext(null, contractType, null, true);
        }

        public void Inject(DiContainer diContainer)
        {
            if (diContainer == null)
            {
                return;
            }

            m_InjectContext.Container = diContainer;
            m_SignalInstance = diContainer.Resolve(m_InjectContext);
        }

        public void Listen()
        {
            if (m_SignalInstance == null)
            {
                return;
            }

            object[] args =
            {
                m_OnSignalFired
            };

            m_ListenMethodInfo.Invoke(m_SignalInstance, args);
        }

        public void Unlisten()
        {
            if (m_SignalInstance == null)
            {
                return;
            }

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
            HandleOnSignalFired();
        }

        // Do not remove, this is called by reflection.
        private void OnSignalFired(object a)
        {
            a = GetObjectOf(a);
            HandleOnSignalFired(a);
        }

        // Do not remove, this is called by reflection.
        private void OnSignalFired(object a, object b)
        {
            a = GetObjectOf(a);
            b = GetObjectOf(b);
            HandleOnSignalFired(a, b);
        }

        // Do not remove, this is called by reflection.
        private void OnSignalFired(object a, object b, object c)
        {
            a = GetObjectOf(a);
            b = GetObjectOf(b);
            c = GetObjectOf(c);
            HandleOnSignalFired(a, b, c);
        }

        // Do not remove, this is called by reflection.
        private void OnSignalFired(object a, object b, object c, object d)
        {
            a = GetObjectOf(a);
            b = GetObjectOf(b);
            c = GetObjectOf(c);
            d = GetObjectOf(d);
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