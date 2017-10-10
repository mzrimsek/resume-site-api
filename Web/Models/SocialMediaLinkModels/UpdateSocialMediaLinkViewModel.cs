using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Web.Models.SocialMediaLinkModels
{
    public class UpdateSocialMediaLinkViewModel : AddSocialMediaLinkViewModel, IHasId
    {
        [Required]
        public int Id { get; set; }
    }
}