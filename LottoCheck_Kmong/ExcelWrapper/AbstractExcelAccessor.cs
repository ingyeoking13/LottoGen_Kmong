﻿using LottoCheck_Kmong.Services;
using OfficeOpenXml;
using System;
using System.IO;

namespace LottoCheck_Kmong.ExcelWrapper
{
    public class AbstractExcelAccessor
    {
        protected FileInfo excelFile;
        protected ExcelPackage package;

        public AbstractExcelAccessor(FileInfo excelFile)
        {
            this.excelFile = excelFile;
            if (!File.Exists(excelFile.Name))
            {
                ExceptionDialogService.getInstance().showMessageAndAllert("File 이 존재하지 않습니다." + Environment.NewLine + $"{excelFile.Name} does not exist. please make it ");
            };
            package = new ExcelPackage(excelFile);
        }

        protected ExcelWorksheets getWorkSheet(ExcelPackage package)
        {
            return package.Workbook.Worksheets;
        }

        public void Dispose()
        {
            package.Save();
        }
    }
}