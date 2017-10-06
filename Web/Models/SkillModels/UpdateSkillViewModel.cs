using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Web.Models.SkillModels
{
    public class UpdateSkillViewModel : AddSkillViewModel, IHasId
    {
        [Required]
        public int Id { get; set; }
    }
}