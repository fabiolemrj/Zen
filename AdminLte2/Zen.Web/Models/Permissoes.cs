using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    public class Permissoes
    {
        [Key]
        public int Id { get; set; }
        public string Descr { get; set; }
        public bool Selecionado { get; set; }
    }
}