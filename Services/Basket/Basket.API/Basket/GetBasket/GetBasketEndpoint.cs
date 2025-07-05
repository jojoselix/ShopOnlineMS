namespace Basket.API.Basket.GetBasket;

//public record GetBasketRequest(string UserName); 
public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
}
