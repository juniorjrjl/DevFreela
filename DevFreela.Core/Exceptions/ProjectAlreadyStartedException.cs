
namespace DevFreela.Core.Exceptions
{
    public class ProjectAlreadyStartedException : Exception
    {
        public ProjectAlreadyStartedException(int id) : base($"Project {id} already started")
        {
            
        }
    }
}