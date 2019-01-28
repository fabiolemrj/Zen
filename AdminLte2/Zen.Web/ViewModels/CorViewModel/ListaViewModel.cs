using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.ViewModels.CorViewModel
{
    public class ListaViewModel
    {
        [Display(Name = "Cor Destino")]
        public int IdCorDest { get; set; }
        public List<Cor> ListaCores { get; set; }
    }
}