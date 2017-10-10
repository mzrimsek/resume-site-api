using System.ComponentModel.DataAnnotations;

namespace Test.Integration.TestModels.SocialMediaLinkModels
{
    public class AddSocialMediaLinkViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
    }
}