namespace ETıcaretAPI.Application.Features.Orders.Queries.GetById
{
    public class GetByIdOrderQueryResponse
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public object BasketItems { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
