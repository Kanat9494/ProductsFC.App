namespace ProductsFC.Models;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string TrackCode { get; set; }
    public string Price { get; set; }
    public string Weight { get; set; }
    public DateTime OrderDate { get; set; }
    public byte IsDelivered { get; set; }
}
