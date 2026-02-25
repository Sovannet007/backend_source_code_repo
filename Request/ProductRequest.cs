namespace asp.net_api_teaching.Request
{
    public class ProductSaveModel
    {
        public int ProductId { get; set; } = 0;
        public required string ProductName { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductBarcode { get; set; }
        public decimal StockQty { get; set; }
        public decimal Cost { get; set; }
        public decimal Retail { get; set; }
        public decimal Whole { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int? UomId { get; set; }
    }
}
