using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.DTO;

namespace Logic.BusinessQueues
{
    public interface IBusinessQueueService
    {
        Task<IEnumerable<BusinessQueueDTO>> GenerateQueues(CreateBusinessQueueRequest request);

    }
}