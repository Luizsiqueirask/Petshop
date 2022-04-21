using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models.Animal
{
    public class Places
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe cidade")]
        [DisplayName("Cidade")]
        public string City { get; set; }
        [Required(ErrorMessage = "Informe a rua")]
        [DisplayName("Rua")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Informe número")]
        [DisplayName("Número")]
        public int Number { get; set; }
    }

    public class Schedule
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a tipo de serviço")]
        [DisplayName("Serviço para seu animal")]
        public string Services { get; set; }
        public Places Places { get; set; }

        [Required(ErrorMessage = "Informe a data do agendamento")]
        [DisplayName("Data do agendamento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, HtmlEncode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Informe a hora do agendamento")]
        [DisplayName("Hora do Agendamento")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = @"{0:hh\\:mm}", ApplyFormatInEditMode = true, HtmlEncode = false)]
        public DateTime Time { get; set; }
    }
}
