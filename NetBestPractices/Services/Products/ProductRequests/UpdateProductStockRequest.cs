namespace Services.Products.ProductRequests
{
    public record UpdateProductStockRequest(string productId, int Quantity);
}
