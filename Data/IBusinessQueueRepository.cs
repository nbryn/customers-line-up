using System;
using System.Threading.Tasks;
using System.Linq;

using Logic.DTO;

namespace Data
{
    public interface IBusinessQueueRepository
    {
         Task<BusinessQueueDTO> CreateBusinessQueue(CreateBusinessQueueDTO dto);
    }
}