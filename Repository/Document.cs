using DeliveryShip.IRepository;
using System.Data;
using Tesseract;

namespace DeliveryShip.Repository
{
    public class Document : IDocument
    {
        public string PerformOCR(string imagePath)
        {
            using (var engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        return page.GetText();
                    }
                }
            }
        }

        public DataTable CreateDataTableFromText(string extractedText)
        {

            DataTable dataTable = new DataTable("ExtractedData");

            // Split text into rows based on newlines
            string[] rows = extractedText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Split the first row (headers) into columns based on spaces
            string[] headers = rows[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string header in headers)
            {
                dataTable.Columns.Add(header.Trim());
            }

            // Add data rows
            for (int i = 1; i < rows.Length; i++)
            {
                string[] values = rows[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                DataRow dataRow = dataTable.NewRow();
                for (int j = 0; j < values.Length; j++)
                {
                    dataRow[j] = values[j].Trim();
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }


        public void PrintDataTable(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn col in dataTable.Columns)
                {
                    Console.Write($"{col.ColumnName}: {row[col]} | ");
                }
                Console.WriteLine();
            }
        }
    }
}
