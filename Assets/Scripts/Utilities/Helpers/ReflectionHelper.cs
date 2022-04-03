using System;
using System.Reflection;

namespace UnityCleanArchitecture.Utilities.Helpers
{
    public static class ReflectionHelper
    {
        private static readonly BindingFlags _bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

        public static T GetInstanceField<T>(object instance, string fieldName)
        {
            var type = instance.GetType();
            return GetInstanceField<T>(type, instance, fieldName);
        }

        public static T GetInstanceField<T>(Type type, object instance, string fieldName)
        {
            var field = type.GetField(fieldName, _bindFlags);
            return (T)field.GetValue(instance);
        }

        public static void SetInstanceField(object instance, string fieldName, object newValue)
        {
            var type = instance.GetType();
            SetInstanceField(type, instance, fieldName, newValue);
        }

        public static void SetInstanceField(Type type, object instance, string fieldName, object newValue)
        {
            var field = type.GetField(fieldName, _bindFlags);
            field.SetValue(instance, newValue);
        }

        public static object InvokeInstanceMethod(object instance, string methodName, params object[] parameters)
        {
            var type = instance.GetType();
            return InvokeInstanceMethod(type, instance, methodName, parameters);
        }

        public static object InvokeInstanceMethod(Type type, object instance, string methodName, params object[] parameters)
        {
            var field = type.GetMethod(methodName, _bindFlags);
            return field.Invoke(instance, parameters);
        }
    }
}