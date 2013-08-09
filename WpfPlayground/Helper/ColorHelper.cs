﻿using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;

namespace WpfPlayground.Helper
{
    public class ColorHelper
    {
        /// <summary>
        /// Returns a list containing all known colors, each as a KeyValuePair with the name
        /// as the key and the Color as the value.
        /// </summary>
        /// <see cref="http://www.blogs.intuidev.com/post/2010/02/05/ColorHelper.aspx"/>
        public static List<KeyValuePair<string, Color>> GetKnownColors()
        {
            var result = new List<KeyValuePair<string, Color>>();
            var colorType = typeof(Colors);
            var propertyInfos = colorType.GetProperties(BindingFlags.Public | BindingFlags.Static);

            foreach (var propertyInfo in propertyInfos)
                result.Add(new KeyValuePair<string, Color>(propertyInfo.Name, (Color)propertyInfo.GetValue(null, null)));
            return result;
        }
    }
}