using CberTest.Core.Models;
using System;
using System.Threading.Tasks;

namespace CberTest.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<Photo, string> PhotoRepository { get; }

        Task<int> CompleteAsync();
    }
}
