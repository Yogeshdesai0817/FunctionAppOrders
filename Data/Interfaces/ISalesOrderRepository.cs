using Common.Models;

namespace Data.Interfaces
{
    /// <summary>
    /// Interface Sales order Repo
    /// </summary>
    public interface ISalesOrderRepository
    {
        Task<IEnumerable<SalesOrder>> GetSalesOrdersAsync(int companyId);
    }
}
