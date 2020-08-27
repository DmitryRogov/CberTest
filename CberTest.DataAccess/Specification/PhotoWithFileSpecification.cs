using Ardalis.Specification;
using CberTest.Core.Models;

namespace CberTest.DataAccess.Specification
{
    public class PhotoWithFileSpecification : Specification<Photo, MediaFile>
    {
        public PhotoWithFileSpecification(string photoId)
        {
            Query.Include(p => p.File);
            Query.Where(p => p.Id == photoId);
        }
    }
}
