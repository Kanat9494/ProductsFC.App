namespace ProductsFC.Models;

public class Product
{
    [PrimaryKey, AutoIncrement]
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string TrackCode { get; set; }
    public double? Price { get; set; } = null;
    public double? Weight { get; set; } = null;
    public string OrderDate { get; set; }
    public byte IsDelivered { get; set; }
    public string ImageUrl { get; set; }
}
