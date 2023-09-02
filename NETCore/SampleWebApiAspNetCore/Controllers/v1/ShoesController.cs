using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SampleWebApiAspNetCore.Dtos;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Helpers;
using SampleWebApiAspNetCore.Services;
using SampleWebApiAspNetCore.Models;
using SampleWebApiAspNetCore.Repositories;
using System.Text.Json;

namespace SampleWebApiAspNetCore.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ShoesController : ControllerBase
    {
        private readonly IShoeRepository _shoeRepository;
        private readonly IMapper _mapper;
        private readonly ILinkService<ShoesController> _linkService;

        public ShoesController(
            IShoeRepository shoeRepository,
            IMapper mapper,
            ILinkService<ShoesController> linkService)
        {
            _shoeRepository = shoeRepository;
            _mapper = mapper;
            _linkService = linkService;
        }

        [HttpGet(Name = nameof(GetAllShoes))]
        public ActionResult GetAllShoes(ApiVersion version, [FromQuery] QueryParameters queryParameters)
        {
            List<ShoeEntity> shoeItems = _shoeRepository.GetAll(queryParameters).ToList();

            var allItemCount = _shoeRepository.Count();

            var paginationMetadata = new
            {
                totalCount = allItemCount,
                pageSize = queryParameters.PageCount,
                currentPage = queryParameters.Page,
                totalPages = queryParameters.GetTotalPages(allItemCount)
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            var links = _linkService.CreateLinksForCollection(queryParameters, allItemCount, version);
            var toReturn = shoeItems.Select(x => _linkService.ExpandSingleShoeItem(x, x.Id, version));

            return Ok(new
            {
                value = toReturn,
                links = links
            });
        }

        [HttpGet]
        [Route("{id:int}", Name = nameof(GetSingleShoe))]
        public ActionResult GetSingleShoe(ApiVersion version, int id)
        {
            ShoeEntity shoeItem = _shoeRepository.GetSingle(id);

            if (shoeItem == null)
            {
                return NotFound();
            }

            ShoeDto item = _mapper.Map<ShoeDto>(shoeItem);

            return Ok(_linkService.ExpandSingleShoeItem(item, item.Id, version));
        }

        [HttpPost(Name = nameof(AddShoe))]
        public ActionResult<ShoeDto> AddShoe(ApiVersion version, [FromBody] ShoeCreateDto shoeCreateDto)
        {
            if (shoeCreateDto == null)
            {
                return BadRequest();
            }

            ShoeEntity toAdd = _mapper.Map<ShoeEntity>(shoeCreateDto);

            _shoeRepository.Add(toAdd);

            if (!_shoeRepository.Save())
            {
                throw new Exception("Creating a shoeitem failed on save.");
            }

            ShoeEntity newShoeItem = _shoeRepository.GetSingle(toAdd.Id);
            ShoeDto shoeDto = _mapper.Map<ShoeDto>(newShoeItem);

            return CreatedAtRoute(nameof(GetSingleShoe),
                new { version = version.ToString(), id = newShoeItem.Id },
                _linkService.ExpandSingleShoeItem(shoeDto, shoeDto.Id, version));
        }

        [HttpPatch("{id:int}", Name = nameof(PartiallyUpdateShoe))]
        public ActionResult<ShoeDto> PartiallyUpdateShoe(ApiVersion version, int id, [FromBody] JsonPatchDocument<ShoeUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            ShoeEntity existingEntity = _shoeRepository.GetSingle(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            ShoeUpdateDto shoeUpdateDto = _mapper.Map<ShoeUpdateDto>(existingEntity);
            patchDoc.ApplyTo(shoeUpdateDto);

            TryValidateModel(shoeUpdateDto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(shoeUpdateDto, existingEntity);
            ShoeEntity updated = _shoeRepository.Update(id, existingEntity);

            if (!_shoeRepository.Save())
            {
                throw new Exception("Updating a shoeitem failed on save.");
            }

            ShoeDto shoeDto = _mapper.Map<ShoeDto>(updated);

            return Ok(_linkService.ExpandSingleShoeItem(shoeDto, shoeDto.Id, version));
        }

        [HttpDelete]
        [Route("{id:int}", Name = nameof(RemoveShoe))]
        public ActionResult RemoveShoe(int id)
        {
            ShoeEntity shoeItem = _shoeRepository.GetSingle(id);

            if (shoeItem == null)
            {
                return NotFound();
            }

            _shoeRepository.Delete(id);

            if (!_shoeRepository.Save())
            {
                throw new Exception("Deleting a shoeitem failed on save.");
            }

            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}", Name = nameof(UpdateShoe))]
        public ActionResult<ShoeDto> UpdateShoe(ApiVersion version, int id, [FromBody] ShoeUpdateDto shoeUpdateDto)
        {
            if (shoeUpdateDto == null)
            {
                return BadRequest();
            }

            var existingShoeItem = _shoeRepository.GetSingle(id);

            if (existingShoeItem == null)
            {
                return NotFound();
            }

            _mapper.Map(shoeUpdateDto, existingShoeItem);

            _shoeRepository.Update(id, existingShoeItem);

            if (!_shoeRepository.Save())
            {
                throw new Exception("Updating a shoeitem failed on save.");
            }

            ShoeDto shoeDto = _mapper.Map<ShoeDto>(existingShoeItem);

            return Ok(_linkService.ExpandSingleShoeItem(shoeDto, shoeDto.Id, version));
        }

        [HttpGet("GetRandomMeal", Name = nameof(GetRandomMeal))]
        public ActionResult GetRandomMeal()
        {
            ICollection<ShoeEntity> shoeItems = _shoeRepository.GetRandomMeal();

            IEnumerable<ShoeDto> dtos = shoeItems.Select(x => _mapper.Map<ShoeDto>(x));

            var links = new List<LinkDto>();

            // self 
            links.Add(new LinkDto(Url.Link(nameof(GetRandomMeal), null), "self", "GET"));

            return Ok(new
            {
                value = dtos,
                links = links
            });
        }
    }
}
