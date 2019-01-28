using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.ViewModels
{
    public class IndexViewModel
    {
        public string filtroNome { get; set; }
        public List<Usuario> LstUsuarios { get; set; }
    }
}