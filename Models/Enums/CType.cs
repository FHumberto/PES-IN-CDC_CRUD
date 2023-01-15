using System.ComponentModel.DataAnnotations;

namespace CDC.Models.Enums
{
    public enum CType
    {
        [Display(Name = "Artefato")]
        Artifact = 0,
        [Display(Name = "Criatura")]
        Creature = 1,
        [Display(Name = "Encantamento")]
        Enchantment = 2,
        [Display(Name = "Magica Instantânea")]
        Instant = 3,
        [Display(Name = "Terreno")]
        Land = 4,
        [Display(Name = "Planinalta")]
        Planeswalker = 5,
        [Display(Name = "Feitiço")]
        Sorcery = 6,
    }
}
