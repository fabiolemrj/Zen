﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.ViewModels.OrcamentoAreasViewModel
{
    public class IndexViewModel
    {
        public int Idpedido { get; set; }
        public int Item { get; set; }
        public int NrSeq { get; set; }
        public List<OrcAreas> LstOrcAreas { get; set; }

    }
}