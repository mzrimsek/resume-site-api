using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Test.Integration.TestModels.SocialMediaLinkModels
{
    public class UpdateSocialMediaLinkViewModel : AddSocialMediaLinkViewModel, IHasId
    {
        [Required]
        public int Id { get; set; }
    }
}