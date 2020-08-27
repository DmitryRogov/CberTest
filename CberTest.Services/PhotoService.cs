using CberTest.Core.Models;
using CberTest.DataAccess;
using CberTest.Services.Results;
using System;
using System.Threading.Tasks;

namespace CberTest.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork unitOfWork;

        public PhotoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Photo>> CreatePhotoAsync(string name, string description, string fileName, string contentType, byte[] content)
        {
            var file = new MediaFile(fileName, contentType, content);
            var photo = new Photo(name, description, file);
            unitOfWork.PhotoRepository.Add(photo);
            try
            {
                await unitOfWork.CompleteAsync();
                return OperationResult<Photo>.Success(photo);
            }
            catch (Exception)
            {
                return OperationResult<Photo>.Error($"Ошибка добавляния фотографии");
            }
        }

        public async Task<OperationResult> DeletePhotoAsync(string id)
        {
            var photo = await unitOfWork.PhotoRepository.GetByIdAsync(id);
            if (photo == null)
            {
                return OperationResult.Error($"Фотография с указанным идентфиикатором {id} не найдена");
            }

            unitOfWork.PhotoRepository.Delete(photo);
            try
            {
                await unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {
                return OperationResult.Error($"Ошибка удаления фотографии");
            }

            return OperationResult.Success();
        }
    }
}
