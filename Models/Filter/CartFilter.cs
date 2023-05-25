namespace store.Models;

public class CartFilter : BaseFilter<Cart>
{
    public override void ApplyFilter(ref IQueryable<Cart> query)
    {
        throw new NotImplementedException();
    }
}