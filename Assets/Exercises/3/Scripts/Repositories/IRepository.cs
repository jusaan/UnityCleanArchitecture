using System.Threading.Tasks;

namespace Project.Exersice3.Repositories
{
    public interface IRepository<Data>
    {
        Task<Data> GetData();
    }
}