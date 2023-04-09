using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.Models
{
    public class UpisAkGodina
    {
        [Key]
        public int id { get; set; }
        public DateTime datumUpisa { get; set; }
        public int godinaStudija { get; set; }
        public float cijenaSkolarine { get; set; }
        public bool isObnova { get; set; }
        public DateTime? datumOvjere { get; set; }
        public string? ovjeraNapomena { get; set; }

        [ForeignKey(nameof(student))]
        public int studentId { get; set; }
        public Student student { get; set; }

        [ForeignKey(nameof(korisnik))]
        public int korisnikId { get; set; }
        public KorisnickiNalog korisnik { get; set; }

        [ForeignKey(nameof(akGodina))]
        public int akGodinaId { get; set; }
        public AkademskaGodina akGodina { get; set; }
    }
}
