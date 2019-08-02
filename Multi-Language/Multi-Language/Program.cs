using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;

namespace Multi_Language
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../Documents/多语言配置信息.xlsx";
            string outputPath = "../../../Ouput/";
            ReadTextConfig(path, outputPath);

            List<string> datas = ReadStyle(path);
            WriteStyle(outputPath + "Style", datas);
            Console.ReadKey();
        }

        private static void ReadTextConfig(string path, string outputPath)
        {
            List<TextData> datas = new List<TextData>();
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet dataSet = reader.AsDataSet();
                    DataTable table = dataSet.Tables[(int)Sheet.TEXT_CONFIG];
                    for (int j = 1; j < table.Columns.Count; j++)
                    {
                        for (int i = 1; i < table.Rows.Count; i++)
                        {
                            TextData data = new TextData();
                            data.ID = i + 1;
                            data.Style = table.Rows[i][0].ToString();
                            data.Content = table.Rows[i][j].ToString();
                            datas.Add(data);
                        }

                        WriteLaguage(outputPath+table.Rows[0][j], datas);
                        datas.Clear();
                    }
                }
            }
        }

        private static void WriteLaguage(string path, List<TextData> datas)
        {
            using (var steam = new FileStream(path,FileMode.Create,FileAccess.Write))
            {
                using (var writer = new BinaryWriter(steam))
                {
                    writer.Write(datas.Count);
                    foreach (var data in datas)
                    {
                        writer.Write(data.ID);
                        writer.Write(data.Style);
                        writer.Write(data.Content);
                    }
                }
            }
        }


        private static List<string> ReadStyle(string path)
        {
            List<string> datas = new List<string>();
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet dataSet = reader.AsDataSet();
                    DataTable table = dataSet.Tables[(int)Sheet.STYLE_CONFIG];

                    datas.Add((table.Rows.Count - 1).ToString());
                    for (int i = 1; i < table.Rows.Count; i++)
                    {
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            datas.Add(table.Rows[i][j].ToString());
                        }
                    }
                }
            }

            return datas;
        }

        private static void WriteStyle(string path, List<string> datas)
        {
            using (var steam = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new BinaryWriter(steam))
                {
                    foreach (string data in datas)
                    {
                        writer.Write(data);
                    }
                }
            }
        }
      
    }

    public enum Sheet
    {
        TEXT_CONFIG,
        STYLE_CONFIG
    }
}

