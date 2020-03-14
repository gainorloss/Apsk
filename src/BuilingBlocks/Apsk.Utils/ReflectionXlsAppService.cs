using Apsk.Annotations;
using Apsk.Utils.Abstractions;
using Apsk.Utils.Annotations;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Apsk
{
    [Component(ComponentLifeTimeScope.Singleton)]
    public class ReflectionXlsAppService
        : IXlsAppService
    {
        private static IDictionary<Type, KeyValuePair<XlsColumnAttribute[], XlsSheetAttribute>> PropertyInfoMapContainer
      = new Dictionary<Type, KeyValuePair<XlsColumnAttribute[], XlsSheetAttribute>>();

        /// <summary>
        /// 提取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public IEnumerable<T> Extract<T>(string fileName)
            where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
                throw new ArgumentException("message", nameof(fileName));

            var xlsSetting = TryGetOrCreateXlsSetting<T>();

            if (xlsSetting.Key == null || !xlsSetting.Key.Any()) return null;

            var items = new List<T>();

            IWorkbook wb = null;
            if (Path.GetExtension(fileName).Equals(".xls"))
                wb = new HSSFWorkbook(File.OpenRead(fileName));
            else
                wb = new XSSFWorkbook(fileName);

            ISheet sheet = wb.GetSheetAt(0);

            var firstRow = sheet.GetRow(0);
            var firstCellNum = firstRow.FirstCellNum;
            var lastCellNum = firstRow.LastCellNum;

            for (int i = firstCellNum; i < lastCellNum; i++)
            {
                var cell = firstRow.GetCell(i).ToString();
                foreach (var displayAttr in xlsSetting.Key)
                {
                    if (displayAttr.Name.Equals(cell.ToString(),
                        StringComparison.OrdinalIgnoreCase))
                        displayAttr.Order = i;
                }
            }

            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                var t = new T();
                var row = sheet.GetRow(i);
                for (int j = firstCellNum; j < lastCellNum; j++)
                {
                    var cell = row.GetCell(j);
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;

                    foreach (var displayAttr in xlsSetting.Key)
                    {
                        if (displayAttr.Order == j)
                        {
                            var val = Convert.ChangeType(cell.ToString().Trim(), displayAttr.Property.PropertyType);
                            displayAttr.Property.SetValue(t, val);
                        }
                    }
                }
                items.Add(t);
            }
            return items;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        public void Save<T>(string fileName, IEnumerable<T> data)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("message", nameof(fileName));

            var xlsSetting = TryGetOrCreateXlsSetting<T>();

            var dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook wb = null;
                if (Path.GetExtension(fileName).Equals(".xls"))
                    wb = new HSSFWorkbook();
                else
                    wb = new XSSFWorkbook();

                var sheet = wb.CreateSheet(xlsSetting.Value.SheetName);

                var headerStyle = wb.CreateCellStyle();
                var headerFont = wb.CreateFont();
                headerFont.IsBold = true;
                headerStyle.SetFont(headerFont);

                #region header
                var headerRow = sheet.CreateRow(0);
                var headerIdx = 0;

                var cellStyles = new Dictionary<string, ICellStyle>();
                foreach (var xlsCol in xlsSetting.Key)
                {
                    var cell = headerRow.CreateCell(headerIdx);
                    cell.CellStyle = headerStyle;
                    cell.SetCellValue(xlsCol.Name);
                    headerIdx++;

                    var cellStyle = wb.CreateCellStyle();
                    var cellFont = wb.CreateFont();
                    cellFont.IsBold = xlsCol.IsBold;
                    cellFont.Color = xlsCol.Forground;
                    cellStyle.SetFont(cellFont);

                    cellStyles.Add(xlsCol.ShortName, cellStyle);
                }
                #endregion

                var rowIdx_ = 0;
                foreach (var item in data)
                {
                    var row = sheet.CreateRow(rowIdx_ + 1);
                    var colIdx = 0;
                    foreach (var attr in xlsSetting.Key)
                    {
                        var cell = row.CreateCell(colIdx);

                        cell.CellStyle = cellStyles[attr.ShortName];

                        if (!string.IsNullOrWhiteSpace(attr.Format))
                        {
                            var format = string.Format(attr.Format, attr.Property.GetValue(item));
                            cell.SetCellValue(format);
                        }
                        else
                        {
                            cell.SetCellValue(attr.Property.GetValue(item)?.ToString());
                        }
                        colIdx++;
                    }
                    rowIdx_++;
                }
                wb.Write(fs);
            }
        }

        private static KeyValuePair<XlsColumnAttribute[], XlsSheetAttribute> TryGetOrCreateXlsSetting<T>()
        {
            var type = typeof(T);

            if (!PropertyInfoMapContainer.TryGetValue(type, out var xlsColsOfT))
            {
                var xlsSheet = new XlsSheetAttribute(type);

                var xlsSheetAttrs = type.GetCustomAttributes(typeof(XlsSheetAttribute), false);
                if (xlsSheetAttrs.Any())
                {
                    var xlsSheetAttr = xlsSheetAttrs.FirstOrDefault() as XlsSheetAttribute;
                    xlsSheet.SheetName = xlsSheetAttr.SheetName;
                }

                var xlsCols = new List<XlsColumnAttribute>();
                foreach (var p in type.GetProperties())
                {
                    var attrs = p.GetCustomAttributes(typeof(XlsColumnAttribute), false);
                    if (attrs != null && attrs.Any())
                    {
                        var attr = attrs.FirstOrDefault() as XlsColumnAttribute;
                        attr.Property = p;
                        attr.ShortName = p.Name;
                        xlsCols.Add(attr);
                    }
                }

                var val = new KeyValuePair<XlsColumnAttribute[], XlsSheetAttribute>(xlsCols.ToArray(), xlsSheet);
                PropertyInfoMapContainer.Add(type, val);
                xlsColsOfT = val;
            }
            return xlsColsOfT;
        }
    }
}

