using System.ComponentModel.DataAnnotations;

namespace Web.Models.SocialMediaLinkModels
{
    public class AddSocialMediaLinkViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
    }
}