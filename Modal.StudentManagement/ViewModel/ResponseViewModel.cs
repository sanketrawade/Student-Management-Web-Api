using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.StudentManagement.ViewModel
{
    public class ResponseViewModel<T>
    {
        public T data { get; set; }
        public ErrorViewModel errorViewModel { get; set; }
    }
}
