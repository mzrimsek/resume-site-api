using Core.Interfaces;

namespace Web.Models.LanguageModels
{
    public class LanguageViewModel : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string RatingName { get; set; }
    }
}