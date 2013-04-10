using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace Qt.Fixtures
{
    public class EntityLoader<T>
    {
        private string _fileName { get; set; }
        public static string directoryName = "Data";

        public EntityLoader(string fileName)
        {
            _fileName = directoryName + "\\" + fileName;
        }

        public List<T> LoadEntities()
        {
            using (var csvDataFile = new TextFieldParser(_fileName))
            {
                var objectArray = new List<T>();

                csvDataFile.TextFieldType = FieldType.Delimited;
                csvDataFile.SetDelimiters(",");
                var header = csvDataFile.ReadFields();
                while (!csvDataFile.EndOfData)
                {
                    string[] fieldArray;
                    try
                    {
                        fieldArray = csvDataFile.ReadFields();
                    }
                    catch (MalformedLineException ex)
                    {
                        throw ex;
                    }
                    var instance = LoadObject(header, fieldArray);
                    objectArray.Add(instance);
                }
                return objectArray;
            }
        }

        /// <summary>
        /// Instantiates an object populating properties in propertyArray
        /// </summary>
        /// <param name="header"></param>
        /// <param name="propertyArray">Properties stored in the same order as header</param>
        /// <returns></returns>
        private T LoadObject(string[] header, string[] propertyArray)
        {
            var instance = Activator.CreateInstance<T>();
            int i = 0;
            foreach (var propertyName in header)
            {
                var propertyInfo = instance.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                propertyInfo.SetValue(instance, Convert.ChangeType(propertyArray[i], propertyInfo.PropertyType), null);
                i++;
            }
            return instance;
        }
    }
}
