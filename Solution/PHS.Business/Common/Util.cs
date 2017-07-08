using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Common
{
    public class Util
    {
        #region Copy values

        public static void CopyNonNullProperty(object objFrom, object objTo)
        {
            List<Type> typeList = new List<Type>() { typeof(byte), typeof(byte?), typeof(sbyte),  typeof(sbyte?), typeof(int), typeof(int?), typeof(uint), typeof(uint?),
                typeof(short), typeof(short?), typeof(ushort), typeof(ushort?), typeof(long), typeof(long?), typeof(ulong), typeof(ulong?), typeof(float), typeof(float?),
                typeof(double), typeof(double?), typeof(char), typeof(char?), typeof(bool), typeof(bool?), typeof(string), typeof(decimal), typeof(decimal?),
                typeof(DateTime), typeof(DateTime?), typeof(Enum), typeof(Guid), typeof(Guid?), typeof(IntPtr), typeof(IntPtr?), typeof(TimeSpan), typeof(TimeSpan?),
                typeof(UIntPtr), typeof(UIntPtr?)  };
            if (objFrom == null || objTo == null) return;
            PropertyInfo[] allProps = objFrom.GetType().GetProperties();
            PropertyInfo toProp;
            foreach (PropertyInfo fromProp in allProps)
            {
                if (typeList.Contains(fromProp.PropertyType))
                {
                    toProp = objTo.GetType().GetProperty(fromProp.Name);
                    if (toProp == null) continue; //not here
                    if (!toProp.CanWrite) continue; //only if writeable
                    if (fromProp.GetValue(objFrom, null) == null) continue;
                    toProp.SetValue(objTo, fromProp.GetValue(objFrom, null), null);
                }
            }


        }
        #endregion
    }
}
