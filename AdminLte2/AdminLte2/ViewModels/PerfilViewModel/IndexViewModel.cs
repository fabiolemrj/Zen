using AdminLte2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminLte2.ViewModels.PerfilViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Perfil> LstPerfil { get; set; }
        public string filtroDescr { get; set; }


    }
}