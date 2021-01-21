using System.Collections.Generic;
using System.Xml.Linq;
using Tests.Common.TranslationTessInner.Models;

namespace Tests.Common.TranslationTessInner
{
    /// <summary>
    /// 
    /// </summary>
    public static class TranslationHelper
    {
        /// <summary>
        /// Разбор файла, так как ни одна библиотека не смогла в разбор файлов ресурсов. Дно какое-то
        /// </summary>
        /// <param name="path">Путь к файлу с ресурсами</param>
        /// <returns>Набор ключей-значений для проверки</returns>
        public static List<Element> ParseFile(string path)
        {
            List<Element> result = new List<Element>();
            XDocument xdoc = XDocument.Load(path);

            foreach (XElement dataElement in xdoc.Element("root")?.Elements("data")!)
            {
                var name = dataElement.Attribute("name")!.Value;
                var value = dataElement.Element("value")!.Value;
                Element el = new Element
                {
                    Name = name,
                    Value = value
                };
                if (!result.Exists(_ => _.Name == name))
                    result.Add(el);
            }

            return result;
        }
    }
}
