using Core.Interfaces;

namespace Test.Integration.TestModels.SkillModels
{
    public class SkillViewModel : IHasId
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string RatingName { get; set; }
    }
}