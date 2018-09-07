using NetCoreWebAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Service.Interface
{
    public interface IStudentService
    {
        Student GetStudentByID(int id);
        List<Student> GetAllStudent();
        bool DeleteStudentByID(int studentID);
    }
}
