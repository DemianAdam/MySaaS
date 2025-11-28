namespace MySaaS.Domain.Entities
{
    public class Supply
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
