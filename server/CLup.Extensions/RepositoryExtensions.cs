using AutoMapper;
using System.Collections.Generic;
using System.Linq;

using CLup.Context;
using CLup.Util;

namespace CLup.Extensions
{
    public static class RepositoryExtensions
    {

        public static ServiceResponse<IList<T2>> AssembleResponse<T1, T2>(
            this IRepository<T1> repository,
            IList<T1> entities,
            IMapper mapper)
            where T1 : class
            where T2 : class
        {
            var response = new ServiceResponse<IList<T2>>();

            if (entities == null)
            {
                response._statusCode = HttpCode.NotFound;
                return response;
            }

            response._response = entities.Select(b => mapper.Map<T2>(b)).ToList();
            return response;

        }

        public static ServiceResponse<T2> AssembleResponse<T1, T2>(
            this IRepository<T1> repository,
            T1 entity,
            IMapper mapper)
            where T1 : class
            where T2 : class
        {
            var response = new ServiceResponse<T2>();

            if (entity == null)
            {
                response._statusCode = HttpCode.NotFound;
                return response;
            }

        
            response._response = mapper.Map<T2>(entity);
            return response;

        }
    }
}