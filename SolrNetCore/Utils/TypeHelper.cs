
using System;
using System.ComponentModel;

namespace SolrNetCore.Utils {
    /// <summary>
    /// <see cref="Type"/>-related helper functions
    /// </summary>
    public static class TypeHelper {
        /// <summary>
        /// Returns the underlying type from a nullable type.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>
        /// From http://davidhayden.com/blog/dave/archive/2006/11/26/IsTypeNullableTypeConverter.aspx
        /// </remarks>
        public static Type GetUnderlyingNullableType(Type t) {
            if (!IsNullableType(t))
                return t;
            var nc = new NullableConverter(t);
            return nc.UnderlyingType;
        }

        /// <summary>
        /// Returns true if the argument is a nullable type
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        /// <remarks>
        /// From http://davidhayden.com/blog/dave/archive/2006/11/26/IsTypeNullableTypeConverter.aspx
        /// </remarks>
        public static bool IsNullableType(Type theType) {
            return theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof (Nullable<>));
        }

        public static bool IsGenericAssignableFrom(Type t, Type other) {
            if (other.GetGenericArguments().Length != t.GetGenericArguments().Length)
                return false;
            var genericT = t.MakeGenericType(other.GetGenericArguments());
            return genericT.IsAssignableFrom(other);
        }
    }
}