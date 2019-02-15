
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.ViewModels.PerfilViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Perfil> LstPerfil { get; set; }
        public string filtroDescr { get; set; }


    }
}