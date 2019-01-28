using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("fornecedores")]
    public class Fornecedor
    {
        [Key]
        [Column(name: "IDFORNECEDOR")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column(name: "NOME")]
        public string Nome { get; set; }

        [MaxLength(50)]
        [Column(name: "CONTATO")]
        public string Contato { get; set; }

        [MaxLength(20)]
        [Column(name: "CARGO")]
        public string Cargo { get; set; }

        [MaxLength(50)]
        [Column(name: "ENDERECO")]
        public string Endereco { get; set; }

        [MaxLength(25)]
        [Column(name: "BAIRRO")]
        public string Bairro { get; set; }

        [MaxLength(30)]
        [Column(name: "CIDADE")]
        public string Cidade { get; set; }

        [MaxLength(2)]
        [Column(name: "UF")]
        public string Uf { get; set; }

        [MaxLength(9)]
        [Column(name: "CEP")]
        public string Cep { get; set; }
        
        [MaxLength(15)]
        [Column(name: "TEL1")]
        public string Tel1 { get; set; }

        [MaxLength(10)]
        [Column(name: "RAMAL1")]
        public string Ramal1 { get; set; }

        [MaxLength(15)]
        [Column(name: "TEL2")]
        public string Tel2 { get; set; }

        [MaxLength(10)]
        [Column(name: "RAMAL2")]
        public string Ramal2 { get; set; }

        [MaxLength(15)]
        [Column(name: "TEL3")]
        public string Tel3 { get; set; }

        [MaxLength(10)]
        [Column(name: "RAMAL3")]
        public string Ramal3 { get; set; }

        [MaxLength(15)]
        [Column(name: "CELULAR1")]
        public string Celular1 { get; set; }

        [MaxLength(15)]
        [Column(name: "CELULAR2")]
        public string Celular2 { get; set; }

        [MaxLength(15)]
        [Column(name: "FAX1")]
        public string Fax1 { get; set; }

        [MaxLength(10)]
        [Column(name: "RAMALF1")]
        public string RamalF1 { get; set; }

        [MaxLength(15)]
        [Column(name: "FAX2")]
        public string Fax2 { get; set; }

        [MaxLength(10)]
        [Column(name: "RAMALF2")]
        public string RamalF2 { get; set; }
        
        [MaxLength(15)]
        [Column(name: "TEL0800")]
        public string Tel0800 { get; set; }
        
        [MaxLength(50)]
        [Column(name: "EMAIL")]
        public string Email { get; set; }
        
        [MaxLength(50)]
        [Column(name: "SITE")]
        public string Site { get; set; }

        [MaxLength(50)]
        [Column(name: "RAZAO_SOCIAL")]
        public string RazaoSocial { get; set; }

        [MaxLength(50)]
        [Column(name: "END_RS")]
        public string EnderecoRs { get; set; }

        [MaxLength(25)]
        [Column(name: "BAIRRO_RS")]
        public string BairroRs { get; set; }

        [MaxLength(30)]
        [Column(name: "CIDADE_RS")]
        public string CidadeRs { get; set; }

        [MaxLength(2)]
        [Column(name: "UF_RS")]
        public string UfRs { get; set; }

        [MaxLength(9)]
        [Column(name: "CEP_RS")]
        public string CepRs { get; set; }

        [MaxLength(18)]
        [Column(name: "CPF")]
        public string CPF { get; set; }

        [MaxLength(18)]
        [Column(name: "CNPJ")]
        public string CNPJ { get; set; }

        [MaxLength(18)]
        [Column(name: "IM")]
        public string InscMunicipal { get; set; }

        [MaxLength(18)]
        [Column(name: "IE")]
        public string InscEstadual { get; set; }

        [MaxLength(1)]
        [Column(name: "DADOS_OK")]
        public string DadosOk { get; set; }

        [Column(name: "DTATU")]
        public DateTime? DtAtu { get; set; }

        [Column(name: "DTINS")]
        public DateTime? DtIns { get; set; }

        [Column(name: "OBS")]
        public string Obs { get; set; }

        [MaxLength(50)]
        [Column(name: "EMAIL2")]
        public string Email2 { get; set; }

        [MaxLength(1)]
        [Column(name: "ATIVO")]
        public string EhAtivo { get; set; }

        [MaxLength(50)]
        [Column(name: "Contato2")]
        public string Contato2 { get; set; }
    }
}