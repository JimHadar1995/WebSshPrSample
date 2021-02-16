using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Tests.Common.TranslationTessInner.Models;

namespace Tests.Common.TranslationTessInner
{
    /// <summary>
    /// 
    /// </summary>
    public class TestTranslationBase
    {
        protected string ResourceManagerLibraryName;
        protected readonly string _resourcesBasePath;

        public TestTranslationBase(string resourceManagerLibraryName, string resourcesBasePath)
        {
            ResourceManagerLibraryName = resourceManagerLibraryName;
            _resourcesBasePath = resourcesBasePath;
        }

        public void RunTest()
        {
            List<Translations> translations = new List<Translations>();
            var fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var index = fullPath!.IndexOf(_resourcesBasePath, StringComparison.Ordinal);
            var sourcePath = fullPath.Substring(0, index + _resourcesBasePath.Length);
            var certificatesPath = Path.Combine(sourcePath, ResourceManagerLibraryName);

            Dictionary<string, List<Element>> resources = new Dictionary<string, List<Element>>();

            // получаем все ресурсы
            foreach (string file in Directory.EnumerateFiles(certificatesPath, "*.ru.resx",
                SearchOption.AllDirectories))
            {
                FileInfo fl = new FileInfo(file);
                resources.Add(fl.Name.Replace(".ru.resx", string.Empty), TranslationHelper.ParseFile(file));
            }


            var assemblyPath = Directory.EnumerateFiles(certificatesPath, $"{ResourceManagerLibraryName}.dll", SearchOption.AllDirectories).FirstOrDefault();
            if (assemblyPath is null)
            {
                Assert.Fail("Не найдена библиотека с константами");
            }

            var assembly = Assembly.LoadFile(assemblyPath!);
            var allTypes = assembly.GetTypes()?.Where(_ => _.Name.EndsWith("Constants"));
            foreach (Type type in allTypes)
            {
                Translations tr = new Translations { FileName = type.FullName?.Split('.').Last() };

                if (tr.FileName is null)
                {
                    Assert.Fail($"Не удалось вычленить имя файла из сборки {type.Name}");
                }

                resources.TryGetValue(tr.FileName, out var els);

                if (els is null)
                {
                    Assert.Fail($"Не удалось получить список констант для файла {tr.FileName}, не сопоставить с ресурсами");
                }

                tr.Resources = els;
                var fields = type.GetRuntimeFields();
                foreach (FieldInfo field in fields)
                {
                    if (field.FieldType == typeof(String))
                    {
                        var res = field.GetValue(null)!.ToString();
                        tr.Values.Add(res);
                    }
                }
                translations.Add(tr);
            }

            List<string> results = new List<string>();
            foreach (Translations translation in translations)
            {
                foreach (string s in translation.Values)
                {
                    if (translation.Resources.Find(_ => _.Name == s) is null)
                    {
                        results.Add($"Не найдено сопоставление для константы \"{s}\" в наборе ресурсов \"{translation.FileName}\"");
                    }
                }

                if (translation.Resources.Count > translation.Values.Count)
                    foreach (Element element in translation.Resources)
                    {
                        if (translation.Values.Find(_ => _.Equals(element.Name)) is null)
                        {
                            results.Add($"Отсутствует сопоставление для ресурса \"{element.Name}\" в файле констант \"{translation.FileName}\"");
                        }
                    }
            }


            if (results.Any())
            {
                Assert.Multiple(() =>
                {
                    foreach (string result in results)
                    {
                        Assert.Fail(result);
                    }
                });
            }
        }
    }

}
