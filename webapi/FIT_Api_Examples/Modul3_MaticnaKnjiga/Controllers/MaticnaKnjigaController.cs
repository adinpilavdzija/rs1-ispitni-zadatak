using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul2.ViewModels;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.Models;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul2.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class MaticnaKnjigaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public MaticnaKnjigaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<UpisAkGodinaVM>> GetById(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var data = _dbContext.UpisAkGodina
                .Where(s => s.student.id == id && !s.student.isDeleted)
                .Select(x => new UpisAkGodinaVM
                {
                    id = x.id,
                    datumUpisa = x.datumUpisa,
                    godinaStudija = x.godinaStudija,
                    cijenaSkolarine = x.cijenaSkolarine,
                    isObnova = x.isObnova,
                    datumOvjere = x.datumOvjere,
                    ovjeraNapomena = x.ovjeraNapomena,
                    studentId = x.studentId,
                    akGodina = x.akGodina.opis,
                    korisnik = x.korisnik.korisnickoIme
                }).ToList();

            return Ok(data);
        }

        [HttpPut]
        public ActionResult SaveChanges(UpisAkGodina upis)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            UpisAkGodina novi;
            if (upis.id == 0)
            {
                novi = new UpisAkGodina
                {
                    id=upis.id,
                    datumUpisa=upis.datumUpisa,
                    godinaStudija=upis.godinaStudija,
                    cijenaSkolarine=upis.cijenaSkolarine,
                    isObnova=upis.isObnova,
                    studentId=upis.studentId,
                    korisnikId= HttpContext.GetLoginInfo().korisnickiNalog.id,
                    akGodinaId=upis.akGodinaId
                };
                _dbContext.UpisAkGodina.Add(novi);
            }
            else
            {
                novi = _dbContext.UpisAkGodina.Find(upis.id);

                novi.datumOvjere = upis.datumOvjere;
                novi.ovjeraNapomena = upis.ovjeraNapomena;
            }
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
