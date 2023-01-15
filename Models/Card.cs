using System.ComponentModel.DataAnnotations;

using CDC.Models.Enums;

namespace CDC.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cor")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "O campo de {0} é obrigatório."), Display(Name = "Tipo")]
        public CType Type { get; set; }

        [Required(ErrorMessage = "O campo de {0} é obrigatório."), Display(Name = "Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O campo de {0} é obrigatório."), Display(Name = "Coleção")]
        public string? Set { get; set; }

        [Required(ErrorMessage = "O campo de {0} é obrigatório."), Display(Name = "Foil?")]
        public bool IsFoil { get; set; }

        [Required(ErrorMessage = "O campo de {0} é obrigatório."), Display(Name = "Condição")]
        public CCondition Condition { get; set; }

        [Required(ErrorMessage = "O campo de {0} é obrigatório."), Display(Name = "Qtd")]
        [Range(0, int.MaxValue, ErrorMessage = "A {0} deve estar entre {1} e {2}.")]
        public int Quantity { get; set; }

        public Card()
        {
        }

        public Card(int id, string? color, CType type, string? name, string? set, bool isFoil, CCondition condition, int quantity)
        {
            Id = id;
            Color = color;
            Type = type;
            Name = name;
            Set = set;
            IsFoil = isFoil;
            Condition = condition;
            Quantity = quantity;
        }
    }
}
