﻿namespace ProductsFC.Models;

public class Product
{
    [PrimaryKey, AutoIncrement]
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string TrackCode { get; set; }
    public double? Price { get; set; }
    public double? Weight { get; set; }
    public string OrderDate { get; set; }
    public byte IsDelivered { get; set; }
}
