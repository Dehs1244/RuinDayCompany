using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RuinDayCompany.Tests.Utils
{
    public static class TestableObjectFactory
    {
        public static T CreateTestObject<T>()
            where T : MonoBehaviour => 
            (T)FormatterServices.GetUninitializedObject(typeof(T));
    }
}
