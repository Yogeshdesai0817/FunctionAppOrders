using Common.Models;

namespace Data.Interfaces
{
    public interface ICompanyRepository
    {
       Task <IEnumerable<Company>> GetAllCompaniesAsync();
    }
}
