namespace Library.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The data table extension.
    /// </summary>
    public static class DataTableExtension
    {
        #region Public Methods and Operators

        /// <summary>
        /// The to data table.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <typeparam name="TItem">
        /// Type of item.
        /// </typeparam>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable ToDataTable<TItem>(this IList<TItem> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(TItem));
            var table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            var values = new object[props.Count];
            foreach (TItem item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }

                table.Rows.Add(values);
            }

            return table;
        }

        /// <summary>
        /// Converts datatable to list<T> dynamically
        /// </summary>
        /// <typeparam name="T">Class name</typeparam>
        /// <param name="dataTable">data table to convert</param>
        /// <returns>List<T></returns>
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, Convert.ToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, Convert.ToInt32(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, Convert.ToInt64(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, Convert.ToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                    (classObj, dataRow[dtField.Name] == null ? string.Empty : dataRow[dtField.Name].ToString(), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, Convert.ToString(dataRow[dtField.Name]), null);
                            }
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        public static T ToObject<T>(this DataTable dataTable) where T : new()
        {
            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            var dataRow = dataTable.AsEnumerable().ToList()[0];
            var classObj = new T();

            foreach (var dtField in dtlFieldNames)
            {
                PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                var field = objFieldNames.Find(x => x.Name == dtField.Name);

                if (field != null)
                {

                    if (propertyInfos.PropertyType == typeof(DateTime))
                    {
                        propertyInfos.SetValue
                        (classObj, Convert.ToDateTime(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(int))
                    {
                        propertyInfos.SetValue
                        (classObj, Convert.ToInt32(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(long))
                    {
                        propertyInfos.SetValue
                        (classObj, Convert.ToInt64(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(decimal))
                    {
                        propertyInfos.SetValue
                        (classObj, Convert.ToDecimal(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(String))
                    {
                        if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                                (classObj, dataRow[dtField.Name] == null ? string.Empty : dataRow[dtField.Name].ToString(), null);
                        }
                        else
                        {
                            propertyInfos.SetValue
                            (classObj, Convert.ToString(dataRow[dtField.Name]), null);
                        }
                    }
                }
            }
            return classObj;
        }
        #endregion
    }
}