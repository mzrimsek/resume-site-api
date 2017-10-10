using Core.Interfaces;

namespace Web.Models.SocialMediaLinkModels
{
    public class SocialMediaLinkViewModel : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}