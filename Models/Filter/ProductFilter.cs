namespace store.Models;

public class ProductFilter : BaseFilter<Product>
{
    public string? Title { get; set; }
    public double? Price { get; set; }
    public double? MinPrice { get; set; } = 0;
    public double? MaxPrice { get; set; } = 10000;

    public override void ApplyFilter(ref IQueryable<Product> query)
    {
        if (CreationFrom != null)
            query = query.Where(c => c.CreationAt >= CreationTo);

        if (CreationTo != null)
            query = query.Where(c => c.CreationAt <= CreationTo);

        if (UpdatedFrom != null)
            query = query.Where(c => c.UpdatedAt >= UpdatedFrom);

        if (UpdatedTo != null)
            query = query.Where(c => c.UpdatedAt <= UpdatedTo);

        if (!string.IsNullOrEmpty(Title))
            query = query.Where(c => c.Title.Contains(Title));

        if (Price != null)
        {
            query = query.Where(p => p.Price == Price);
        }
        else
        {
            if (MinPrice != null)
            {
                query = query.Where(p => p.Price >= MinPrice);
            }

            if (MaxPrice != null)
            {
                query = query.Where(p => p.Price <= MaxPrice);
            }
        }
    }
}
