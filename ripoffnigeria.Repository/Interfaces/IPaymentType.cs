using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IPaymentType
    {
        RepositoryActionResult<PaymentType> Delete(int id);
        IQueryable<PaymentType> Get();
        RepositoryActionResult<PaymentType> Insert(PaymentType t);
        RepositoryActionResult<PaymentType> Update(PaymentType t);
    }
}