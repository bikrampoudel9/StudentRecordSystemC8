using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMangementSystemC8.Database;
using StudentMangementSystemC8.Database.Entities;

namespace StudentMangementSystemC8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext database;
        public StudentController(AppDbContext database)
        {
            this.database = database;
        }

        [HttpPost("add")]
        public IActionResult AddStudent(Student student)
        {
            database.Students.Add(student); //save in EF
            database.SaveChanges(); //save in actual database
            return Ok(student);
        }

        [HttpGet("get-all")]
        public IActionResult GetAllStudents()
        {
            List<Student> allStudents = database.Students.ToList();
            return Ok(allStudents);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetStudentById(int id)
        {
            Student? student =  database.Students.FirstOrDefault(st => st.Id == id );
            return Ok(student);
        }

        //Update
        [HttpPut("update/{id}")]
        public IActionResult UpdateStudent(int id,Student student)
        {
            //fetch student record from database having parameter id
            Student? st = database.Students.FirstOrDefault(st => st.Id == id);

            //make changes to st
            if(st is not null)
            {
                st.Name = student.Name;
                st.Email = student.Email;
                st.Phone = student.Phone;
                database.SaveChanges();
                return Ok(st);
            }
            return NotFound("Id not found");

        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            int row = database.Students.Where(st => st.Id == id).ExecuteDelete();
            return Ok(row);
        }
    }
}
