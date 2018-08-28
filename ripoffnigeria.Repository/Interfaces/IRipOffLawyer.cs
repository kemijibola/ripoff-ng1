using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IRipOffLawyer
    {
        RepositoryActionResult<RipOffLawyer> Delete(int id);
        IQueryable<RipOffLawyer> Get();
        RepositoryActionResult<RipOffLawyer> Insert(RipOffLawyer t);
        RepositoryActionResult<RipOffLawyer> Update(RipOffLawyer t);  
    }
}