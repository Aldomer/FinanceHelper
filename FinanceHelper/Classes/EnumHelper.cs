using System;
using System.ComponentModel;
using System.Reflection;

namespace FinanceHelper.Classes
{ 
    public static class EnumHelper
    {
        public static TEnum ParseEnum<TEnum>(string item) where TEnum : struct
        {
            TEnum tenumResult;
            ParseEnum<TEnum>(item, out tenumResult);
            return tenumResult;
        }

        /// <summary>
        /// Used where code wants the string converted to an enum and a bool saying if it was able to convert
        /// </summary>
        public static bool ParseEnum<TEnum>(string item, out TEnum tenumResult) where TEnum : struct
        {
            bool isDescriptionEnumParsed = false;
            bool isItemEnumParsed = Enum.TryParse(item, true, out tenumResult);

            if (!isItemEnumParsed)
            {
                Type enumType = typeof(TEnum);
                foreach (TEnum enumObj in Enum.GetValues(enumType))
                {
                    FieldInfo fi = enumType.GetField(enumObj.ToString());
                    DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attributes.Length > 0 && attributes[0].Description.Equals(item))
                    {
                        tenumResult = enumObj;
                        isDescriptionEnumParsed = true;
                        break;
                    }
                }
            }

            bool isEnumParsed = (isItemEnumParsed || isDescriptionEnumParsed);
            if (!isEnumParsed)
                tenumResult = default(TEnum);

            return isEnumParsed;
        }

        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            try
            {
                FieldInfo fi = type.GetField(value.ToString());
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                return attributes.Length > 0 ? attributes[0].Description : value.ToString();
            }
            catch (Exception e)
            {
                string errorMessage = String.Format("The extension method GetDescription was unable to match enum - value: '{0}' type: '{1}' in the class 'EnumUtility'", value, type);
                throw new Exception(errorMessage, e);
            }
        }
    }
}
