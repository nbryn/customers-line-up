using System;
using System.Linq;
using System.Collections.Generic;

namespace CLup.Util
{
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}