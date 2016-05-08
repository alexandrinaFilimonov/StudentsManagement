using Microsoft.Practices.Unity;
using System.Web.Http;
using StudentsManagement.DataLayer;
using StudentsManagement.Models;
using Unity.WebApi;

namespace StudentsManagement
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IDataLayer<Subject>, SubjectService>();
            container.RegisterType<IDataLayer<StudentToSubject>, StudentToSubjectService>();
            container.RegisterType<IJoiner<StudentToSubject, Subject>, StudentSubjectJoiner>();
            container.RegisterType<IDataLayer<Student>, StudentService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}