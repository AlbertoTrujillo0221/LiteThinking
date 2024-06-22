using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IExternalService<T>
        where T : notnull
    {
        Task<T> GetList();
        Task<bool> Create(T model);
    }
}
