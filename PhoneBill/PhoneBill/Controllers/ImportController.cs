
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace PhoneBill.Controllers
{
    public class ImportController : Controller
    {
        // GET: Import
        public ActionResult Index()
        {

            //var fileBytes = Server.MapPath("~/App_Data/system/" + file_Name);
            //string fileName = file_Name;
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

            //string pathToExcelFile = ""+ @"D:\Code\Blog Projects\BlogSandbox\ArtistAlbums.xlsx";
            //string pathToExcelFile = "" 
            //    + @"C:\Users\Jalal\Documents\Visual Studio 2015\Projects\PhoneBill\PhoneBill\App_Data\Excel\Book1.xlsx";
            //string pathToExcelFile = Server.MapPath("~/App_Data/Excel/Book1.xlsx");
            //string sheetName = "Sheet1";

            //var excelFile = new ExcelQueryFactory(pathToExcelFile);
            //var artistAlbums = from a in excelFile.Worksheet(sheetName) select a;

            //foreach (var a in artistAlbums)
            //{
            //    string artistInfo = "Artist Name: {0}; Album: {1}";
            //    Console.WriteLine(string.Format(artistInfo, a["Name"], a["Title"]));
            //}
            return View();
        }
    }
}