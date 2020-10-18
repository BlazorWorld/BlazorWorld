using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public static string Snippet(this string text)
        {
            string output = text;

            if (!String.IsNullOrEmpty(text))
            {
                output = Regex.Replace(output, @"\t|\n|\r", " ");

                // strip tags
                // output = Markdown.ToHtml(output);
                output = Regex.Replace(output, @"<[^>]+>|&nbsp;", "").Trim();
                output = Regex.Replace(output, @"{{[^>]+}}|&nbsp;", "").Trim();

                // strip extra whitespace
                output = Regex.Replace(output, @"\s{2,}", " ");

                if (text.Length > 140)
                {
                    output = output.Truncate(140);
                    output = output + " ...";
                }
            }

            return output;
        }
    }
}
