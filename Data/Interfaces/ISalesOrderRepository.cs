using Common.Models;

namespace Data.Interfaces
{
    public interface ISalesOrderRepository
    {
        Task<IEnumerable<SalesOrder>> GetSalesOrdersAsync(int companyId);
    }
}
