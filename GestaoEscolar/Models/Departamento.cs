using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao.Models
{
    public class Departamento
    {

        [Key]
        
        public long? DepartamentoID { get; set; }

        public string Nome { get; set; }

        [ForeignKey("Instituicao")]

        public long? fk_InstituicaoID { get; set; } // Chave Estrangeira

        public Instituicao? Instituicao { get; set; } // Navegabilidade


    }
}
