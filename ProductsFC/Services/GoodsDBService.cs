namespace ProductsFC.Services;

public class GoodsDBService
{
    public GoodsDBService()
    {

    }

    SQLiteAsyncConnection _localDb;

    async Task Init()
    {
        if (_localDb is not null)
            return;

        _localDb = new SQLiteAsyncConnection(ProductFCConstants.DatabasePath, ProductFCConstants.Flags);
        var result = await _localDb.CreateTableAsync<Product>();
    }

    public async Task<List<Product>> GetItemsAsync()
    {
        await Init();
        return await _localDb.Table<Product>().ToListAsync();
    }

    public async Task<List<Product>> GetItemsNotDelivered()
    {
        await Init();
        return await _localDb.Table<Product>().Where(t => t.IsDelivered == 1).ToListAsync();
    }

    public async Task<Product> GetItemAsync(int itemId)
    {
        await Init();
        return await _localDb.Table<Product>().FirstOrDefaultAsync(i => i.ProductId == itemId);
    }

    public async Task<int> SaveItemAsync(Product product)
    {
        await Init();
        if (product.ProductId != 0)
            return await _localDb.UpdateAsync(product);
        else
            return await _localDb.InsertAsync(product);
    }

    public async Task<int> DeleteItemAsync(Product product)
    {
        await Init();
        return await _localDb.DeleteAsync(product);
    }
}
