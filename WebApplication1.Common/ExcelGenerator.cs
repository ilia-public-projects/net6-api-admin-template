using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models.Attributes;
using WebApplication1.Services;

namespace WebApplication1.Common
{
    public class ExcelGenerator : IExcelGenerator
    {
        private ExcelPackage excelPackage;
        private ExcelWorksheet excelWorksheet;
        private string worksheetName;

        public bool showTimeOnDates;
        public async Task<byte[]> GenerateAsync<T>(IEnumerable<T> data, string sheetName = "", bool showTimeOnDates = false) where T : class
        {
            worksheetName = string.IsNullOrEmpty(sheetName) ? "Data" : sheetName;
            this.showTimeOnDates = showTimeOnDates;

            using (excelPackage = new ExcelPackage())
            {
                PopulateExcelSheet(data);
                byte[] result = await excelPackage.GetAsByteArrayAsync();
                return result;
            }
        }

        private void PopulateExcelSheet<T>(IEnumerable<T> data) where T : class
        {
            excelWorksheet = excelPackage.Workbook.Worksheets.Add(worksheetName);
            SetColumnNames<T>();

            int row = 1;
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var item in data)
            {
                row++;
                int column = 0;
                foreach (PropertyInfo property in properties)
                {
                    if (!HideColumn(property))
                    {
                        column++;

                        // format all date fields
                        object propertyValue = property.GetValue(item, null);
                        if (propertyValue is DateTime)
                        {
                            if (showTimeOnDates == false)
                            {
                                propertyValue = Convert.ToDateTime(propertyValue).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                propertyValue = Convert.ToDateTime(propertyValue).ToString("dd/MM/yyyy HH:mm:ss");
                            }
                        }
                        excelWorksheet.SetValue(row, column, propertyValue);
                    }
                }
            }

            excelWorksheet.Cells[excelWorksheet.Dimension.Address].AutoFitColumns();
        }
        private void SetColumnNames<T>()
        {
            int column = 1;

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!HideColumn(property))
                {
                    excelWorksheet.SetValue(1, column, GetColumnDisplayNameOrDefault(property));
                    column++;
                }
            }
        }

        private string GetColumnDisplayNameOrDefault(PropertyInfo property)
        {
            string result = property.Name;
            object attribute = property.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
            if (attribute != null)
            {
                result = ((DisplayAttribute)attribute).Name;
            }

            return result;
        }

        private bool HideColumn(PropertyInfo property)
        {
            object attribute = property.GetCustomAttributes(typeof(HideInExcelAttribute), false).FirstOrDefault();
            return attribute != null;
        }
    }
}
