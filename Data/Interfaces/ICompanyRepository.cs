using Common.Models;

namespace Data.Interfaces
{
    /// <summary>
    /// Interface Company
    /// </summary>
    public interface ICompanyRepository
    {
       Task <IEnumerable<Company>> GetAllCompaniesAsync();
    }
}
