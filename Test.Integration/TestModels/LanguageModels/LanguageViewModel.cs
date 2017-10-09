using Core.Interfaces;

namespace Test.Integration.TestModels.LanguageModels
{
    public class LanguageViewModel : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string RatingName { get; set; }
    }
}