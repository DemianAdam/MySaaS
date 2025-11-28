namespace MySaaS.Domain.Entities
{
    public class Quantity
    {
        public required int UnitId { get; set; }   
        public required double Amount { get; set; }

        public Unit? Unit { get; set; }
    }
}
