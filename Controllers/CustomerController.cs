using asp.net_api_teaching.Data;
using asp.net_api_teaching.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace asp.net_api_teaching.Controllers
{
    [Route("api/v1/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CustomerController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("get-list")]
        public IActionResult GetCustomerList()
        {
            var ds = DataManager.ExtractDataSet(_db, "[dbo].[SP_API_CUSTOMER_LIST_V1]");
            var row = ds.Tables[0].Rows[0];
            int code = Convert.ToInt32(row["code"]);
            string message = row["message"].ToString()!;
            if (code != 0)
            {
                return Ok(new { code, message });
            }
            var customer = DataManager.ExtractDataTableToObjectList(ds.Tables[1]);
            return Ok(new { code, message, customer });
        }

        [HttpPost("save")]
        public IActionResult CustomerSave([FromBody] CustomerSaveBindingReq req)
        {
            var parms = new SqlParameter[]
            {
                new SqlParameter("@Id", req.Id),
                new SqlParameter("@Name", req.Name),
                new SqlParameter("@Address",req.Address)
            };
            var dt = DataManager.ExecuteSPReturnDt(_db, "[dbo].[SP_API_CUSTOMER_SAVE_V1]", parms);
            var row = dt.Rows[0];
            int code = Convert.ToInt32(row["code"]);
            string message = row["message"].ToString()!;
            return Ok(new { code, message });
        }
    }
}
