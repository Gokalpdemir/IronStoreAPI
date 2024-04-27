namespace ETıcaretAPI.Application.Features.Basket.Query.GetBasketItem
{
    public class GetBasketItemQueryResponse
    {
        public string BasketItemId { get; set; }
        public string Name { get; set; }
        public float Price {  get; set; }
        public int Quantity { get; set; }

    }
}
