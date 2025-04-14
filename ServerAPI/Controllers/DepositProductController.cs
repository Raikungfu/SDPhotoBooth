using AutoMapper;
using BusinessLogic.DTO.DepositProductDTO;
using BusinessLogic.DTO.ExportProductDTO;
using BusinessLogic.Service;
using BusinessLogic.Service.IService;
using DataAccess.Extensions.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Services.IService;
using System.Security.Claims;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositProductController : ControllerBase
    {
        private readonly IDepositProductService _depositProductService;
        private readonly IMembershipCardService _membershipCardService;
        private readonly ICouponService _couponService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;
        public DepositProductController(IDepositProductService depositProductService,
             IMembershipCardService membershipCardService, IExportService exportService, IMapper mapper,
             ICouponService couponService)
        {
            _depositProductService = depositProductService;
            _membershipCardService = membershipCardService;
            _couponService = couponService;
            _exportService = exportService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams? pagination)
        {
            var depositProducts = await _depositProductService.GetAllAsync(pagination);
            return Ok(depositProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var depositProduct = await _depositProductService.GetByIdAsync(id);
            if (depositProduct == null)
            {
                return NotFound();
            }
            return Ok(depositProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _depositProductService.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest("Failed to delete deposit product");
            }
            return NoContent();
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportCsv()
        {
            var typeSessionProduct = await _depositProductService.GetAllIncludeAsync();
            if (typeSessionProduct == null)
            {
                return NotFound();
            }

            var products = _mapper.Map<IEnumerable<ExportProductResponseDTO>>(typeSessionProduct);

            var csvBytes = _exportService.ExportProductsToCsv(products);

            return File(csvBytes, "text/csv", "products.csv");
        }
    }
}
