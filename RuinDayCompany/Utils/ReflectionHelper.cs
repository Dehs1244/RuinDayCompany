using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Utils
{
    internal static class ReflectionHelper
    {
        public static object InvokePrivateMethod<T>(T instance, string methodName, params object[] parameters)
        {
            var method = typeof(T).GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return method?.Invoke(instance, parameters);
        }
    }
}
