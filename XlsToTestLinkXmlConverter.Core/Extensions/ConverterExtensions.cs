using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XlsToTestLinkXmlConverter.Core.Enums;

namespace XlsToTestLinkXmlConverter.Core.Extensions
{
    static class ConverterExtensions
    {
        public static T ToEnumInt<T>(this object str)
        {
            if (str != null && Enum.IsDefined(typeof(T), str))
                return (T)Enum.Parse(typeof(T), str.ToString());
            if (typeof(T) == typeof(Importance))
                return (T)Enum.ToObject(typeof(T), Importance.P3);
            return (T)Enum.ToObject(typeof(T), 1);
        }
    }
}
