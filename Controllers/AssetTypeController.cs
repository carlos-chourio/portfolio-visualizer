using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioVisualizer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioVisualizer.Controllers
{
    [ApiController]
    [Route("AssetType")]
    public class AssetTypeController : ControllerBase
    {
        private readonly IAssetTypeService assetTypeService;
        private readonly IMapper mapper;

        public AssetTypeController(IAssetTypeService assetTypeService, IMapper mapper)
        {
            this.assetTypeService = assetTypeService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAssetTypes(DTO.Pagination pagination)
        {
            return Ok(await assetTypeService.GetPagesAsync(pagination.PageNumber, pagination.PageSize));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssetType(DTO.AssetType assetType)
        {
            var model = mapper.Map<Model.AssetType>(assetType);
            assetTypeService.Add(model);
            await assetTypeService.SaveChangesAsync();
            return Ok(model);
        }

        [HttpGet("{id:Guid}")]
        public async ValueTask<IActionResult> GetAssetTypeById(Guid id)
        {
            var model = await assetTypeService.FindAsync(id);
            if (model is null) {
                return NotFound();
            }
            else {
                var dto = mapper.Map<DTO.AssetType>(model);
                return Ok(dto);
            }
        }
    }
}
