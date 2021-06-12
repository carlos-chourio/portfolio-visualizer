using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioVisualizer.DTO.In {

    public class Asset {
        [Required(ErrorMessageResourceType = typeof(Properties.Resource), ErrorMessageResourceName = nameof(Properties.Resource.FieldIsRequired))]
        public string Symbol { get; set; }

        [Required(ErrorMessageResourceType = typeof(Properties.Resource), ErrorMessageResourceName = nameof(Properties.Resource.FieldIsRequired))]
        public Guid? TypeId { get; set; }
    }
}
