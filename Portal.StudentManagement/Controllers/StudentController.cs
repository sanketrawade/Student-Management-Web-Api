using Modal.StudentManagement.Data;
using Modal.StudentManagement.Manager;
using Modal.StudentManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace Portal.StudentManagement.Controllers
{
    public class StudentController : BaseController
    {
        [Route("gtstd")]
        [HttpGet]
        public IHttpActionResult GetStudents()
        {
            try
            {
                return Ok(StudentManager.GetStudentList());
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [Route("gtstdbyid")]
        [HttpPost]
        public IHttpActionResult GetStudentById([FromBody] IdViewModel idViewModel)
        {
            try
            {
                return Ok(StudentManager.GetStudentById(idViewModel.id));
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [Route("stdreg")]
        [HttpPost]
        public IHttpActionResult StudentRegister()
        {
            try
            {
                return Ok(StudentManager.StudentRegister());
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [Route("adstd")]
        [HttpPost]
        public IHttpActionResult AddStudent([FromBody] Student student)
        {
            try
            {
                return Ok(StudentManager.AddStudent(student));
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [Route("updstd")]
        [HttpPost]
        public IHttpActionResult UpdateStudent([FromBody] Student student)
        {
            try
            {
                return Ok(StudentManager.UpdateStudent(student));
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

       [Route("delstd")]
       [HttpPost]
       public IHttpActionResult DeleteStudent([FromUri] IdViewModel idViewModel)
        {
            try
            {
                StudentManager.DeleteStudent(idViewModel.id);
                return Ok();
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}