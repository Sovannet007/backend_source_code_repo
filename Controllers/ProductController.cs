using asp.net_api_teaching.Data;
using asp.net_api_teaching.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace asp.net_api_teaching.Controllers
{
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;
        // constructor
        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        // get all products
        [HttpGet("get-init")]
        public IActionResult GetDataInitProduct()
        {
            var ds = DataManager.ExtractDataSet(_db, "[dbo].[SP_API_GET_DATA_INIT_PRODUCT_SAVE_V1]");
            var brands = DataManager.ExtractDataTableToObjectList(ds.Tables[0]);
            var categories = DataManager.ExtractDataTableToObjectList(ds.Tables[1]);
            var uoms = DataManager.ExtractDataTableToObjectList(ds.Tables[2]);
            return Ok(new { code = 0, brands, categories, uoms });
        }

        // save product (insert or update)
        [HttpPost("save")]
        public IActionResult SaveDataProduct([FromBody] ProductSaveModel req)
        {
            var parms = new[]
            {
                new SqlParameter("@ProductId", req.ProductId),
                new SqlParameter("@Name", req.ProductName),
                new SqlParameter("@Code", req.ProductCode),
                new SqlParameter("@Barcode", req.ProductBarcode),
                new SqlParameter("@StockQty", req.StockQty),
                new SqlParameter("@Cost", req.Cost),
                new SqlParameter("@Retail", req.Retail),
                new SqlParameter("@Whole", req.Whole),
                new SqlParameter("@BrandId", req.BrandId),
                new SqlParameter("@CategoryId", req.CategoryId),
                new SqlParameter("@UomId", req.UomId),
            };
            DataTable dt = DataManager.ExecuteSPReturnDt(_db, "[dbo].[SP_API_PRODUCT_SAVE_V1]", parms);
            DataRow dr = dt.Rows[0];
            int code = Convert.ToInt32(dr["code"]);
            string message = dr["message"].ToString()!;
            return Ok(new { code,message });
        }

        // get product list
        [HttpPost("get-list/{proId?}")]
        public IActionResult GetDataListProduct(int? proId=0)
        {
            var parms = new[]
            {
                new SqlParameter("@ProductId", proId),
            };
            var ds = DataManager.ExtractDataSet(_db, "[dbo].[SP_API_PRODUCT_LIST_V1]", parms);
            var products = DataManager.ExtractDataTableToObjectList(ds.Tables[0]);
            return Ok(new { code = 0, products });
        }
    }
}
