﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Consulta
    {
        public long ConsultaId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataHora { get; set; }
        public string Sintomas { get; set; }
        public virtual ICollection<Exame> Exames { get; set; }
    }
}