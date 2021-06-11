using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioVisualizer.DTO
{
    public class AssetType
    {
        [Required(ErrorMessageResourceType = typeof(Properties.Resource), ErrorMessageResourceName = nameof(Id))]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
