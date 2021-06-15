using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AIMLChatBot.Conversion
{
    public static class StringConversion
    {
        public static string ReturnSafeString(dynamic value)
        {
            string result = String.Empty;
            try
            {

                if (value != null)
                {
                    var convertible = value as IConvertible;
                    result = (convertible == null) ? Convert.ToString(value) : Convert.ChangeType(value, typeof(string));
                }
                else
                {
                    result = String.Empty;
                }
            }
            catch (Exception)
            {
                result = default(string);
            }

            return result.ToString();
        }
    }
}
