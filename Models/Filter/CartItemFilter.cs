namespace store.Models;

public class CartItemFilter : BaseFilter<CartItem>
{
    public override void ApplyFilter(ref IQueryable<CartItem> query)
    {
        if (CreationFrom != null)
            query = query.Where(c => c.CreationAt >= CreationTo);

        if (CreationTo != null)
            query = query.Where(c => c.CreationAt <= CreationTo);

        if (UpdatedFrom != null)
            query = query.Where(c => c.UpdatedAt >= UpdatedFrom);

        if (UpdatedTo != null)
            query = query.Where(c => c.UpdatedAt <= UpdatedTo);
    }
}
