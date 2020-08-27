using CberTest.DataAccess.Specification;

namespace CberTest.WebApi.Models
{
    public class PhotoListQueryModel
    {
        public PhotoOrders Order { get; set; } = PhotoOrders.Created;

        public string Name { get; set; }

        public string Description { get; set; }

        public int Limit { get; set; } = 20;

        public int Offset { get; set; } = 0;
    }
}
