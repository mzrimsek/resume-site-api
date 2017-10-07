using Core.Interfaces;

namespace Web.Models.SkillModels
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