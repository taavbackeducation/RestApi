namespace Warehouse.Services.RequestNeeds.Contracts.Dtos
{
    public class RegisterRequestNeedDto
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string Section { get; set; }
    }
}
