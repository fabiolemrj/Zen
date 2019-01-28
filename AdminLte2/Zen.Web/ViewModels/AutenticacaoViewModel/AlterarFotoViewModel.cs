using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.AutenticacaoViewModel
{
    public class AlterarFotoViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nome")]
        public string NmUsuario { get; set; }
        public byte[] Foto { get; set; }


    }
}