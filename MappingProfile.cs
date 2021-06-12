namespace PortfolioVisualizer {
    public class MappingProfile : AutoMapper.Profile {
        public MappingProfile() {
            CreateMap<Model.AssetType, DTO.Out.AssetType>();
            CreateMap<Model.Asset, DTO.Out.Asset>();
            CreateMap<DTO.In.Asset, Model.Asset>();
            CreateMap<Model.PagedResult<Model.Asset>, Model.PagedResult<DTO.Out.Asset>>();
        }
    }
}
