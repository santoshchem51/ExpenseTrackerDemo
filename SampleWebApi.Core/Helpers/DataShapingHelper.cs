using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi.Data.Helpers
{
    public static class DataShapingHelper<T> where T : class
    {
        public static object CreateDataShapedObject(T inputObject , List<string> fieldList)
        {
            if(!fieldList.Any())
            {
                return inputObject;
            }

            ExpandoObject returnObject = new ExpandoObject();
            foreach (var field in fieldList)
            {
                var fieldValue = inputObject.GetType()
                    .GetProperty(field, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    .GetValue(inputObject, null);

                ((IDictionary<string, object>)returnObject).Add(field, fieldValue);
            }

            return returnObject;
        }
    }
}
