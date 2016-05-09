using StudentsManagement.Models;

namespace StudentsManagement.Helpers
{
    public class StudentPathProvider : IPathProvider<Student>
    {
        public string GetPathToDownloadFrom()
        {
            return System.Web.Hosting.HostingEnvironment.MapPath("~\\App_Data\\App_LocalResources\\students.csv");
        }
    }
}