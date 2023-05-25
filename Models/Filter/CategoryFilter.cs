namespace store.Models;

public class CategoryFilter : BaseFilter<Category>
{
    public string? Name { get; set; }

    public override void ApplyFilter(ref IQueryable<Category> query)
    {
        if (CreationFrom != null)
            query = query.Where(c => c.CreationAt >= CreationTo);

        if (CreationTo != null)
            query = query.Where(c => c.CreationAt <= CreationTo);

        if (UpdatedFrom != null)
            query = query.Where(c => c.UpdatedAt >= UpdatedFrom);

        if (UpdatedTo != null)
            query = query.Where(c => c.UpdatedAt <= UpdatedTo);

        if (!string.IsNullOrEmpty(Name))
            query = query.Where(c => c.Name.Contains(Name));
    }
}
