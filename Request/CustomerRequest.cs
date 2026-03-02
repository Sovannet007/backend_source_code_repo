namespace asp.net_api_teaching.Request
{
    public class CustomerSaveBindingReq
    {
        public int Id { get; set; } = 0;
        public required string Name { get; set; }
        public string? Address { get; set; }
    }
}
