using Core.Interfaces;

namespace Test.Integration.TestModels.SocialMediaLinkModels
{
    public class SocialMediaLinkViewModel : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}