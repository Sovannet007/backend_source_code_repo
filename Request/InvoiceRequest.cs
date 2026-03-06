namespace asp.net_api_teaching.Request
{
    public class CreateInvoiceBindingReq
    {
        public required string InvoiceDate { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal SaleQty { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
