using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EMixCepFinder.Domain.Model
{
    [Table("CEP")]
    public class AddressInfo
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Column("Cep", TypeName = "char(9)")]
        public string PostalCode { get; set; }

        [Column("Logradouro", TypeName = "nvarchar(500)")]
        public string Street { get; set; }

        [Column("Complemento", TypeName = "nvarchar(500)")]
        public string Complement { get; set; }

        [Column("Bairro", TypeName = "nvarchar(500)")]
        public string Neighborhood { get; set; }

        [Column("Localidade", TypeName = "nvarchar(500)")]
        public string City { get; set; }

        [Column("UF", TypeName = "char(2)")]
        public string State { get; set; }

        [Column("Unidade", TypeName = "bigint")]
        public long Unit { get; set; }

        [Column("Ibge", TypeName = "int")]
        public int IBGE { get; set; }

        [Column("Gia", TypeName = "nvarchar(500)")]
        public string GIA { get; set; }

        [Column("Ddd", TypeName = "nvarchar(3)")]
        public string DDD { get; set; }
    }
}
