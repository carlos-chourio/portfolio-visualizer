using System.ComponentModel.DataAnnotations;

namespace PortfolioVisualizer.DTO.In
{
    public class Pagination
    {
        [Display(ResourceType = typeof(Properties.Resource), Name = nameof(Properties.Resource.PageSize))]
        [Required(ErrorMessageResourceType = typeof(Properties.Resource), ErrorMessageResourceName = nameof(Properties.Resource.FieldIsRequired))]
        public int? PageSize { get; set; }

        [Display(ResourceType = typeof(Properties.Resource), Name = nameof(Properties.Resource.PageNumber))]
        [Required(ErrorMessageResourceType = typeof(Properties.Resource), ErrorMessageResourceName = nameof(Properties.Resource.FieldIsRequired))]
        public int? PageNumber { get; set; }
    }
}
