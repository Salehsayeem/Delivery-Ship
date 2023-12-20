using DeliveryShip.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DeliveryShip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocument _repo;

        public DocumentController(IDocument repo)
        {
            _repo = repo;
        }
        [HttpGet(Name = "Get")]
        public IActionResult Get()
        {
            string imagePath = "./datatable.png";
            string extractedText = _repo.PerformOCR(imagePath);
            DataTable dataTable = _repo.CreateDataTableFromText(extractedText);

            _repo.PrintDataTable(dataTable);
            return Ok(dataTable);
        }

    }
}
