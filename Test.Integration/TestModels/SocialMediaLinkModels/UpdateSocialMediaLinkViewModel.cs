using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Test.Integration.TestModels.SocialMediaLinkModels
{
    public class UpdateSocialMediaLinkViewModel : Web.Models.SocialMediaLinkModels.AddSocialMediaLinkViewModel, IHasId
    {
        [Required]
        public int Id { get; set; }
    }
}