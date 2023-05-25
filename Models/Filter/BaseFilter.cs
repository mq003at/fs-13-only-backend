namespace store.Models;

public abstract class BaseFilter<TModel>
{
    public bool? SortDescending { get; set; }
    public DateTime? CreationFrom { get; set; }
    public DateTime? CreationTo { get; set; }
    public DateTime? UpdatedFrom { get; set; }
    public DateTime? UpdatedTo { get; set; }

    public abstract void ApplyFilter(ref IQueryable<TModel> query);
}

