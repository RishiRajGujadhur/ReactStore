using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class FeatureSettings
    {
        [Key]
        public int Id { get; set; }
        public bool IsFeatureEnabled { get; set; } = false;
        public string FeatureName { get; set; }
        public string FeatureDescription { get; set; }
        public int DisplayOrder { get; set; }
        public string FeatureIcon { get; set; }
        public string FeatureRoute { get; set; }
        public string FeatureType { get; set; }
        public string FeatureCategory { get; set; }
        public int ParentFeatureId { get; set; } 
        public bool AdminFeature { get; set; }
        public List<string> EnabledForRoles { get; set; } 
    }
}