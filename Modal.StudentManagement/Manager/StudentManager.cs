using Modal.StudentManagement.Data;
using Modal.StudentManagement.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Modal.StudentManagement.Manager
{
    public class StudentManager
    {

        public static ResponseViewModel<List<Student>> GetStudentList()
        {
            ResponseViewModel<List<Student>> responseViewModel = new ResponseViewModel<List<Student>>();
            List<Student> students = new List<Student>();
            using(StudentManagementEntities entities = new StudentManagementEntities())
            {
                students = entities.Students.ToList();
                responseViewModel.data = students;
            }
            return responseViewModel;
        }

        public static ResponseViewModel<GetStudentById_Result> GetStudentById(int id)
        {
            ResponseViewModel<GetStudentById_Result> responseViewModel = new ResponseViewModel<GetStudentById_Result>();
            using(StudentManagementEntities entities = new StudentManagementEntities())
            {
                GetStudentById_Result student = entities.GetStudentById(id).FirstOrDefault();
                responseViewModel.data = student;
            }
            return responseViewModel;
        }

        public static ResponseViewModel<User> StudentRegister()
        {
            ResponseViewModel<User> responseViewModel = new ResponseViewModel<User>();
            HttpRequest httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["Image"];
            var filePath = HttpContext.Current.Server.MapPath("~/Images/Student/" + postedFile.FileName);
            postedFile.SaveAs(filePath);
            User user = JsonConvert.DeserializeObject<User>(httpRequest["UserDetails"]);
            Student student = JsonConvert.DeserializeObject<Student>(httpRequest["StudentDetails"]);
            using (StudentManagementEntities entities = new StudentManagementEntities())
            {
                if (!CheckDuplicate(user))
                {
                    entities.Users.Add(user);
                    entities.Students.Add(student);
                    responseViewModel.data = user;
                    entities.SaveChanges();
                }
                else
                {
                    responseViewModel.errorViewModel = new ErrorViewModel();
                    responseViewModel.errorViewModel.statusCode = EnumrationManager.StatusCode.AlreadyExists;
                }
            }
            return responseViewModel;
        }


        public static ResponseViewModel<Student> AddStudent(Student student)
        {
            ResponseViewModel<Student> responseViewModel = new ResponseViewModel<Student>();
            using(StudentManagementEntities entities = new StudentManagementEntities())
            {
                entities.Students.Add(student);
                entities.SaveChanges();
            }
            responseViewModel.data = student;
            return responseViewModel;
        }

        public static ResponseViewModel<Student> UpdateStudent(Student student)
        {
            ResponseViewModel<Student> responseViewModel = new ResponseViewModel<Student>();
            using(StudentManagementEntities entities = new StudentManagementEntities())
            {
                Student studentDb = entities.Students.Where(entry => entry.ID == student.ID).FirstOrDefault();
                if (studentDb!=null)
                {
                    studentDb.Age = student.Age;
                    studentDb.ClassID = student.ClassID;
                    studentDb.DepartmentID = student.DepartmentID;
                    studentDb.ParantsMobileNo = student.ParantsMobileNo;
                }
                else
                {
                    responseViewModel.errorViewModel = new ErrorViewModel();
                    responseViewModel.errorViewModel.statusCode = EnumrationManager.StatusCode.NotFound;
                }
                entities.SaveChanges();
            }
            responseViewModel.data = student;
            return responseViewModel;
        }

        public static void DeleteStudent(int id)
        {
            ResponseViewModel<Student> responseViewModel = new ResponseViewModel<Student>();
            using(StudentManagementEntities entities = new StudentManagementEntities())
            {
                Student student = entities.Students.Where(entry => entry.ID == id).FirstOrDefault();
                entities.Students.Remove(student);
                entities.SaveChanges();
            }
        }


        public static Boolean CheckDuplicate(User user)
        {
            using(StudentManagementEntities entities = new StudentManagementEntities())
            {
                if(entities.Users.Any(entry => entry.EmailId==user.EmailId && entry.Password == user.Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
