using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLinkXmlConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path to xlsx file with test cases:");
            string filePath = Console.ReadLine();
            
            Console.ReadKey();
        }
    }
}
