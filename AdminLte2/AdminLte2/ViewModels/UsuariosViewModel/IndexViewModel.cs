using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminLte2.Models;

namespace AdminLte2.ViewModels
{
    public class IndexViewModel
    {
        public string filtroNome { get; set; }
        public List<Usuario> LstUsuarios { get; set; }
    }
}