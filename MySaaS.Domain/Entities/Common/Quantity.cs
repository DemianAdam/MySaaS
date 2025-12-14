namespace MySaaS.Domain.Entities.Common
{
    public class Quantity
    {
        public required int UnitId { get; set; }   
        public required decimal Amount { get; set; }

        public Unit? Unit { get; set; }
    }
}
