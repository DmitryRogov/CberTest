using CberTest.Core.Models;
using CberTest.DataAccess;
using CberTest.DataAccess.Specification;
using CberTest.Services;
using CberTest.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CberTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService service;
        private readonly IUnitOfWork unitOfWork;

        public PhotosController(IPhotoService service, IUnitOfWork unitOfWork)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PhotoListQueryModel model)
        {
            var filtredPagedSpec = new FilteredPagedPhotoSpecification(model.Order, model.Limit, model.Offset, model.Name, model.Description);
            var filtredSpec = new FilteredPhotoSpecification(model.Order, model.Name, model.Description);
            var photos = await unitOfWork.PhotoRepository.ListAsync(filtredPagedSpec);
            var total = await unitOfWork.PhotoRepository.CountAsync(filtredSpec);
            var result = new PagedList<Photo>
            {
                Items = photos,
                Total = total
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var photoWithIdSpec = new PhotoWithIdSpecification(id);
            var photo = await unitOfWork.PhotoRepository.FirstOrDefaultAsync(photoWithIdSpec);
            if (photo == null)
            {
                return NotFound();
            }

            return Ok(photo);
        }

        [HttpGet("{id}/file")]
        public async Task<IActionResult> GetFile(string id)
        {
            var photoWithIdSpec = new PhotoWithIdSpecification(id);
            var photo = await unitOfWork.PhotoRepository.FirstOrDefaultAsync(photoWithIdSpec);
            if (photo == null)
            {
                return NotFound();
            }

            return File(photo.File.Content, photo.File.ContentType, photo.File.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreatePhotoBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var ms = new MemoryStream();
            await model.File.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            var result = await service.CreatePhotoAsync(model.Name, model.Description, model.File.FileName, model.File.ContentType, fileBytes);
            if (result.IsError)
            {
                return BadRequest(result.ErrorMessage);
            }

            return CreatedAtAction(nameof(Get), new { id = result.Result.Id }, result.Result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await service.DeletePhotoAsync(id);
            if (result.IsError)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }
    }
}
