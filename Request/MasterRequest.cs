namespace asp.net_api_teaching.Request
{
    public class  MasterBinding
    {
        public required string MapKey { get; set; }
    }

    public class MasterSaveBinding : MasterBinding
    {
        public int Id { get; set; } = 0;
        public required string Name { get; set; }
        public string? Remark { get; set; }
    }
}
