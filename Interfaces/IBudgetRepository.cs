using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.Interfaces
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> GetByProjectId(int id);
        Budget GetSumById(int id);
        bool Add(Budget item);
        bool Update(Budget item);
        bool Delete(Budget item);
        bool Save(
);
    }
}