using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;
using X.PagedList;

namespace Zen.Web.ViewModels.ClienteViewModel
{
    public class IndexViewModel
    {
        public string Filtro { get; set; }
        public int Tpfiltro { get; set; }
        public bool EhDesigner { get; set; }
        public List<Cliente> LstClientes { get; set; }
        public IPagedList<Cliente> ListaClientes { get; set; }

    }
}