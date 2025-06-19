using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    //definimos la firma de métodos generales para todas las interfaces
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(); //todos los datos
        Task<T?> GetByIdAsync(int id); //busqueda por id
        Task AddAsync(T entity); //agregar
        Task UpdateAsync(T entity); //actualizar
        Task DeleteAsync(int id); //eliminar
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
