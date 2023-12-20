using System.Data;

namespace DeliveryShip.IRepository
{
    public interface IDocument
    {
        public string PerformOCR(string imagePath);
        public DataTable CreateDataTableFromText(string extractedText);
        public void PrintDataTable(DataTable dataTable);
    }
}
