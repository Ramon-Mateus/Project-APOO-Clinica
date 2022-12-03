using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models.ViewModels
{
    public class ConsultasViewModel
    {
        public long ConsultaId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data_hora { get; set; }
        public string Sintomas { get; set; }
        public List<CheckBoxViewModel> Exames { get; set; }
    }
}