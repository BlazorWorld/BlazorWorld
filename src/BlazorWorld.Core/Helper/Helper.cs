using Markdig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BlazorWorld.Core.Helper
{
    public static class Helper
    {
        public static TConvert ConvertTo<TConvert>(this object entity) where TConvert : new()
        {
            var convertProperties = TypeDescriptor.GetProperties(typeof(TConvert)).Cast<PropertyDescriptor>();
            var entityProperties = TypeDescriptor.GetProperties(entity).Cast<PropertyDescriptor>();

            var convert = new TConvert();

            foreach (var entityProperty in entityProperties)
            {
                var property = entityProperty;
                var convertProperty = convertProperties.FirstOrDefault(prop => prop.Name == property.Name);
                if (convertProperty != null)
                {
                    Console.WriteLine(property.Name);
                    Debug.WriteLine(property.Name);
                    convertProperty.SetValue(convert, Convert.ChangeType(entityProperty.GetValue(entity), convertProperty.PropertyType));
                }
            }

            return convert;
        }

        public static TConvert[] ConvertTo<TConvert>(this object[] entities) where TConvert : new()
        {
            var convertList = new List<TConvert>();
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    var convert = entity.ConvertTo<TConvert>();
                    convertList.Add(convert);
                }
            }

            return convertList.ToArray();
        }

        public static string Truncate(this string text, int length = 140)
        {
            string output = text;

            if (!String.IsNullOrEmpty(text))
            {
                output = Regex.Replace(output, @"\t|\n|\r", " ");

                // strip tags
                output = Markdown.ToHtml(text);
                output = Regex.Replace(output, @"<[^>]+>|&nbsp;", "").Trim();
                output = Regex.Replace(output, @"{{[^>]+}}|&nbsp;", "").Trim();

                // strip extra whitespace
                output = Regex.Replace(output, @"\s{2,}", " ");

                if (text.Length > length)
                {
                    output = StringExt.Truncate(output, length);
                    output = output + " ...";
                }
            }

            return output;
        }
    }
}
