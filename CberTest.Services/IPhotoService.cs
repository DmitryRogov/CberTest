using CberTest.Core.Models;
using CberTest.Services.Results;
using System.Threading.Tasks;

namespace CberTest.Services
{
    public interface IPhotoService
    {
        Task<OperationResult<Photo>> CreatePhotoAsync(string name, string description, string fileName, string contentType, byte[] content);

        Task<OperationResult> DeletePhotoAsync(string photoId);
    }
}