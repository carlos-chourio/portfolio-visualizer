using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortfolioVisualizer.Service;

namespace PortfolioVisualizer.Controllers {

    [ApiController]
    [Route("Asset")]
    public class AssetController : ControllerBase {

        private readonly IAssetService assetService;
        private readonly IMapper mapper;

        public AssetController(IAssetService assetService, IMapper mapper) {
            this.assetService = assetService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAssets([FromQuery] DTO.In.Pagination pagination) {
            var pages = await assetService.GetPagesAsync(pagination.PageNumber.Value, pagination.PageSize.Value);
            var pagesDto = mapper.Map<Model.PagedResult<DTO.Out.Asset>>(pages);
            return Ok(pagesDto);
        }

        [HttpPut]
        public async ValueTask<IActionResult> ModifyAsset(DTO.Out.Asset asset) {
            var model = mapper.Map<Model.Asset>(asset);
            var existingModel = await assetService.FindAsync(model.Symbol);
            if (existingModel is null) {
                return NotFound();
            }
            else {
                assetService.Update(model);
                await assetService.SaveChangesAsync();
                var dto = mapper.Map<DTO.Out.Asset>(model);
                return Ok(dto);
            }
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsset(DTO.In.Asset asset) {
            var model = mapper.Map<Model.Asset>(asset);
            assetService.Add(model);
            await assetService.SaveChangesAsync();
            model = await assetService.FindAsync(model.Symbol);
            return Ok(mapper.Map<DTO.Out.Asset>(model));
        }

        [HttpGet("{symbol}")]
        public async ValueTask<IActionResult> GetAssetById(string symbol) {
            var model = await assetService.FindAsync(symbol);
            if (model is null) {
                return NotFound();
            }
            else {
                var dto = mapper.Map<DTO.Out.Asset>(model);
                return Ok(dto);
            }
        }

        [HttpDelete("{symbol}")]
        public async ValueTask<IActionResult> DeleteAsync(string symbol) {
            var model = await assetService.FindAsync(symbol);
            assetService.Delete(model);
            await assetService.SaveChangesAsync();
            return Ok();
        }
    }
}
