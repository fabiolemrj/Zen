﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    public class FormaPagGrupo
    {
        [Key]
        public int Id { get; set; }
        
        public string Nome { get; set; }
        public double Valor { get; set; }
    }
}