using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioVisualizer.DTO
{
    public class AssetType
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Properties.Resource), ErrorMessageResourceName = nameof(Properties.Resource.Name))]
        public string Name { get; set; }
    }
}
