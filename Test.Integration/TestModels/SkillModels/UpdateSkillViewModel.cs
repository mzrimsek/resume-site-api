using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Test.Integration.TestModels.SkillModels
{
    public class UpdateSkillViewModel : AddSkillViewModel, IHasId
    {
        [Required]
        public int Id { get; set; }
    }
}