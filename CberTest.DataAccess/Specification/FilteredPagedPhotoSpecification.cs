namespace CberTest.DataAccess.Specification
{
    public class FilteredPagedPhotoSpecification : FilteredPhotoSpecification
    {
        public FilteredPagedPhotoSpecification(PhotoOrders order, int limit, int offset, string name, string description)
            : base(order, name, description)
        {
            Query.Paginate(offset, limit);
        }
    }
}
