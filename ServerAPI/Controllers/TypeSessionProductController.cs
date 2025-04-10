using AutoMapper;
using BusinessLogic.DTO.TypeSessionProductDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Extensions.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeSessionProductController : ControllerBase
    {
        private readonly ITypeSessionProductService _typeSessionProductService;
        private readonly IMembershipCardService _membershipCardService;
        private readonly ITypeSessionService _typeSessionService;
        public TypeSessionProductController(ITypeSessionProductService typeSessionProductService,
             IMembershipCardService membershipCardService, ITypeSessionService typeSessionService)
        {
            _typeSessionProductService = typeSessionProductService;
            _membershipCardService = membershipCardService;
            _typeSessionService = typeSessionService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams? pagination)
        {
            var typeSessionProducts = await _typeSessionProductService.GetAllAsync(pagination);
            return Ok(typeSessionProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var typeSessionProduct = await _typeSessionProductService.GetByIdAsync(id);
            if (typeSessionProduct == null)
            {
                return NotFound();
            }
            return Ok(typeSessionProduct);
        }

        [HttpGet("GetByTypeSessionId/{typeSessionId}")]
        public async Task<IActionResult> GetByTypeSessionId(int typeSessionId)
        {
            var typeSessionProducts = await _typeSessionProductService.GetByTypeSessionIdAsync(typeSessionId);
            if (typeSessionProducts == null)
            {
                return NotFound();
            }
            return Ok(typeSessionProducts);
        }

        [HttpGet("mobile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetForMobile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var re = await _membershipCardService.GetByCustomerId(userId);

            var typeSessionProducts = await _typeSessionProductService.GetByLevelMembershipIdAsync(re.LevelMemberShip.Id);

            if(typeSessionProducts == null)
            {
                return NotFound();
            }

            return Ok(typeSessionProducts);

        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TypeSessionProductUpdateRequestDTO typeSessionProduct)
        {
            var isExistTypeSessionProduct = await _typeSessionProductService.GetByIdAsync(id);
            if (isExistTypeSessionProduct == null)
            {
                return NotFound();
            }
            var updatedTypeSessionProduct = await _typeSessionProductService.UpdateAsync(id, typeSessionProduct);
            return Ok(updatedTypeSessionProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _typeSessionProductService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
