using asp.net_api_teaching.Data;
using asp.net_api_teaching.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace asp.net_api_teaching.Controllers
{
    [Route("api/v1/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _db;
        public InvoiceController(AppDbContext db)
        {
            _db = db;
        }

        // get all invoices
        [HttpPost("get-list")]
        public IActionResult GetAllInvoices()
        {
            try
            {
                var ds = DataManager.ExtractDataSet(_db, "[dbo].[SP_API_INVOICE_LIST_V1]");
                var row = ds.Tables[0].Rows[0];
                int code = Convert.ToInt32(row["code"]);
                string message = row["message"].ToString()!;
                if (code != 0)
                {
                    return BadRequest(new { code, message });
                }
                var invoices = DataManager.ExtractDataTableToObjectList(ds.Tables[1]);
                return Ok(new { code,message , invoices });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { code = 1, message = ex.Message });
            }
        }

        // crated invoice
        [HttpPost("create")]
        public IActionResult CreateInvoice([FromBody] CreateInvoiceBindingReq req)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(req.Items);
                var parms = new SqlParameter[]
                {
                    new SqlParameter("@InvoiceDate", req.InvoiceDate),
                    new SqlParameter("@SubTotalAmount", req.SubTotalAmount),
                    new SqlParameter("@DiscountAmount", req.DiscountAmount),
                    new SqlParameter("@PaidAmount", req.PaidAmount),
                    new SqlParameter("@CustomerId", req.CustomerId),
                    new SqlParameter("@Items", jsonData),
                };
                var dt = DataManager.ExecuteSPReturnDt(_db, "[dbo].[SP_API_INVOICE_CREATE_V1]", parms);
                var row = dt.Rows[0];
                int code = Convert.ToInt32(row["code"]);
                string message = row["message"].ToString()!;

                return Ok(new {code,message});
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { code = 1, message = ex.Message });
            }
        }

        // get all invoices
        [HttpPost("get-data-for-sale")]
        public IActionResult GetDataForSale()
        {
            try
            {
                var ds = DataManager.ExtractDataSet(_db, "[dbo].[SP_API_INVOICE_GET_DATA_SALE_V1]");
                var row = ds.Tables[0].Rows[0];
                int code = Convert.ToInt32(row["code"]);
                string message = row["message"].ToString()!;
                if (code != 0)
                {
                    return BadRequest(new { code, message });
                }
                var customers = DataManager.ExtractDataTableToObjectList(ds.Tables[1]);
                var products = DataManager.ExtractDataTableToObjectList(ds.Tables[2]);
                return Ok(new { code, message, customers ,products});
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { code = 1, message = ex.Message });
            }
        }

    }
}


