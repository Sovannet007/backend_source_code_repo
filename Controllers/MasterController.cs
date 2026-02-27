using asp.net_api_teaching.Data;
using asp.net_api_teaching.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace asp.net_api_teaching.Controllers
{
    [Route("api/v1/master")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly AppDbContext _db;
        public MasterController(AppDbContext db)
        {
            _db = db;
        }

        // get master list
        [HttpPost("get-list")]
        public IActionResult GetMasterList([FromBody] MasterBinding req)
        {
            var parms = new SqlParameter[]
            {
                new SqlParameter("@MapKey", req.MapKey)
            };
            var ds = DataManager.ExtractDataSet(_db, "[dbo].[SP_API_MASTER_LIST_V1]", parms);
            var row = ds.Tables[0].Rows[0];
            int code = Convert.ToInt32(row["code"]);
            string message = row["message"].ToString()!;
            if (code !=0)
            {
                return Ok(new { code, message });
            }
            var master = DataManager.ExtractDataTableToObjectList(ds.Tables[1]);
            return Ok(new {code,message, master });
        }

        [HttpPost("save")]
        public IActionResult MasterSave([FromBody] MasterSaveBinding req)
        {
            var parms = new SqlParameter[]
            {
                new SqlParameter("@MapKey", req.MapKey),
                new SqlParameter("@Id", req.Id),
                new SqlParameter("@Name", req.Name),
                new SqlParameter("@Remark", req.Remark),
            };
            var dt = DataManager.ExecuteSPReturnDt(_db, "[dbo].[SP_API_MASTER_SAVE_V1]", parms);
            var row = dt.Rows[0];
            string code = row["code"].ToString()!;
            string message = row["message"].ToString()!;
            return Ok(new { code, message });
        }
    }
}
