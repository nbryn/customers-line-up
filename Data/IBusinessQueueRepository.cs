using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Logic.DTO;

namespace Data
{
    public interface IBusinessQueueRepository
    {
        Task<BusinessQueueDTO> CreateBusinessQueue(CreateBusinessQueueDTO dto);

        Task<BusinessQueueDTO> AddUserToQueue(AddUserToQueueRequest request);

        Task<ICollection<BusinessQueueDTO>> GetQueuesByBusiness(int businessId);
    }
}