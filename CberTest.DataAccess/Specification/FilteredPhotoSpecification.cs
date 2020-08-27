using Ardalis.Specification;
using CberTest.Core.Models;

namespace CberTest.DataAccess.Specification
{
    public class FilteredPhotoSpecification : Specification<Photo>
    {
        public FilteredPhotoSpecification(PhotoOrders order, string name, string description)
        {
            switch (order)
            {
                case PhotoOrders.Name:
                    Query.OrderBy(p => p.Name);
                    break;
                case PhotoOrders.Created:
                    Query.OrderByDescending(p => p.Created);
                    break;
                case PhotoOrders.Type:
                    Query.OrderBy(p => p.File.FileType);
                    break;
            }

            if (!string.IsNullOrEmpty(name))
            {
                Query.Where(p => p.Name.Contains(name.ToLower()));
            }

            if (!string.IsNullOrEmpty(description))
            {
                Query.Where(p => p.Description.Contains(description.ToLower()));
            }
        }
    }
}