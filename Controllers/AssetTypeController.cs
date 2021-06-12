using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortfolioVisualizer.Service;

namespace PortfolioVisualizer.Controllers {
    [ApiController]
    [Route("AssetType")]
    public class AssetTypeController : ControllerBase {

        private readonly IAssetTypeService assetTypeService;
        private readonly IMapper mapper;

        public AssetTypeController(IAssetTypeService assetTypeService, IMapper mapper) {
            this.assetTypeService = assetTypeService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAssetTypes([FromQuery] DTO.In.Pagination pagination) {
            return Ok(await assetTypeService.GetPagesAsync(pagination.PageNumber.Value, pagination.PageSize.Value));
        }

        [HttpPut]
        public async ValueTask<IActionResult> ModifyAssetType(DTO.Out.AssetType assetType) {
            var model = mapper.Map<Model.AssetType>(assetType);
            var existingModel = await assetTypeService.FindAsync(model.Id);
            if (existingModel is null) {
                return NotFound();
            }
            else {
                assetTypeService.Update(model);
                await assetTypeService.SaveChangesAsync();
                var dto = mapper.Map<DTO.Out.AssetType>(model);
                return Ok(dto);
            }
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAssetType(DTO.Out.AssetType assetType) {
            var model = mapper.Map<Model.AssetType>(assetType);
            assetTypeService.Add(model);
            await assetTypeService.SaveChangesAsync();
            return Ok(model);
        }

        [HttpGet("{id:Guid}")]
        public async ValueTask<IActionResult> GetAssetTypeById(Guid id) {
            var model = await assetTypeService.FindAsync(id);
            if (model is null) {
                return NotFound();
            }
            else {
                var dto = mapper.Map<DTO.Out.AssetType>(model);
                return Ok(dto);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async ValueTask<IActionResult> DeleteAsync(Guid id) {
            var model = await assetTypeService.FindAsync(id);
            assetTypeService.Delete(model);
            await assetTypeService.SaveChangesAsync();
            return Ok();
        }
    }
}
