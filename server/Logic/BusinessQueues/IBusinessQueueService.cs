using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.DTO;

namespace Logic.BusinessQueues
{
    public interface IBusinessQueueService
    {
        Task<IEnumerable<BusinessQueueDTO>> GenerateQueues(CreateBusinessQueueRequest request);

        Task<int> AddUserToQueue(string userEmail, int queueId);

        Task<int> RemoverUserFromQueue(string userEmail, int queueId);

    }
}