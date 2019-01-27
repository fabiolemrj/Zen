using AdminLte2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminLte2.ViewModels.PermissoesViewModel
{
    public class IndexViewModel
    {
        
        public int idAcoes { get; set; }
        public int idPerfil { get; set; }
        [Display(Name = "Usuário")]
        public string NmUsuario { get; set; }
        public List<Permissoes> LstPermAbaCadastro { get; set; }
        public List<Permissoes> LstPermAbaTesouraria { get; set; }
        public List<Permissoes> LstPermAbaTabAux { get; set; }
        public List<Permissoes> LstPermAbaTabRel { get; set; }
        public List<Permissoes> LstPermAbaTabOrcamento { get; set; }
        public List<Permissoes> LstPermAbaTabProducao { get; set; }
    }
}