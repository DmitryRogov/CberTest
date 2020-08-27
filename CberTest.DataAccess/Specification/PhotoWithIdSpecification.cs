using Ardalis.Specification;
using CberTest.Core.Models;

namespace CberTest.DataAccess.Specification
{
    public class PhotoWithIdSpecification : Specification<Photo>
    {
        public PhotoWithIdSpecification(string id)
        {
            Query.Include(p => p.File);
            Query.Where(p => p.Id == id);
        }
    }
}
