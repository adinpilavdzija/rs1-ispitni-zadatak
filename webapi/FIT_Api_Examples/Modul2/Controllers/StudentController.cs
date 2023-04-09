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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul2.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<Student>> GetAll(string ime_prezime)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var data = _dbContext.Student
                .Include(s => s.opstina_rodjenja.drzava)
                .Where(x => !x.isDeleted && (ime_prezime == null || (x.ime + " " + x.prezime).StartsWith(ime_prezime) || (x.prezime + " " + x.ime).StartsWith(ime_prezime)))
                .OrderByDescending(s => s.id)
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpPut]
        public ActionResult SaveChanges(Student student)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            Student novi;
            if (student.id == 0)
            {
                novi = new Student
                {
                    id = student.id,
                    ime = student.ime,
                    prezime = student.prezime,
                    broj_indeksa = "NOT_SET",
                    opstina_rodjenja_id = student.opstina_rodjenja_id,
                    created_time = DateTime.Now
                };
                _dbContext.Student.Add(novi);
            }
            else
            {
                novi = _dbContext.Student.Find(student.id);
                novi.ime = student.ime;
                novi.prezime = student.prezime;
                novi.opstina_rodjenja_id = student.opstina_rodjenja_id;
            }
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            _dbContext.Student.Find(id).isDeleted = true;
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public ActionResult<Student> GetById(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var data = _dbContext.Student.Find(id);

            return Ok(data);
        }
    }
}
