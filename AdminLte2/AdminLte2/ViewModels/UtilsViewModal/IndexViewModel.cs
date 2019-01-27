using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminLte2.ViewModels.UtilsViewModal
{
    public class IndexViewModel
    {
        public string NmRegistro { get; set; }
        [Display(Name = "Deseja Apagar o registro")]
        public bool YesNo { get; set; }
    }
}