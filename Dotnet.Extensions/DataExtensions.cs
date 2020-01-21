using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Dotnet.Extensions
{
    public static class DataExtensions
    {

/// <summary>
/// Get value from row and return value as cast type
/// </summary>
/// <typeparam name="TReturnType"></typeparam>
/// <param name="row"></param>
/// <param name="columnName"></param>
/// <param name="defaultValue"></param>
/// <returns></returns>
        public static TReturnType ConvertTo<TReturnType>(this DataRow row, string columnName, TReturnType defaultValue = default)
        {
            if (row == null)
            {
                throw new NullReferenceException("The DataRow was null; could not convert value to specified type.");
            }

            var value = row[columnName];
            if (value == DBNull.Value || value==null)
            {
                return defaultValue;
            }

            return (TReturnType)value;
        }

        /// <summary>
        /// Convert IEnumerable data into DataTable. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> input) where T : class
        {
            DataTable dt = new DataTable();
            Type type = typeof(T);
            PropertyInfo[] propertyInfoArray = type.GetProperties();
            FieldInfo[] fieldInfoArray = type.GetFields();

            for(var i=0;i<propertyInfoArray.Length;i++)
            {
                PropertyInfo propertyInfo = propertyInfoArray[i];
                dt.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType);
            }


            for (var i = 0; i < fieldInfoArray.Length; i++)
            {
                FieldInfo fieldInfo = fieldInfoArray[i];
                dt.Columns.Add(fieldInfo.Name, fieldInfo.FieldType);
            }


            foreach (T item in input)
            {
                DataRow dr = dt.NewRow();
                foreach (DataColumn column in dt.Columns)
                {
                        var property = type.GetProperty(column.ColumnName);
                        if (property != null)
                        {
                            dr[column] = property.GetValue(item);
                        }
                        else
                        {
                            var field = type.GetField(column.ColumnName);
                            if (field != null)
                            {
                                dr[column] = field.GetValue(item);
                            }
                        }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }


    }
}
