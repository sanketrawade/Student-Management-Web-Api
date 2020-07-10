using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.StudentManagement.Manager
{
    public class EnumrationManager
    {
        public enum StatusCode { success=200,created,BadRequest=400, Unauthorized, NotFound=404,MethodNotAllow,AlreadyExists=409 }
    }
}
