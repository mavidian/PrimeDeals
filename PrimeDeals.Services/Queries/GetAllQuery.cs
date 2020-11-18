using AutoMapper;
using MediatR;
using PrimeDeals.Core.Interfaces.Models;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Interfaces.Services;
using PrimeDeals.Services.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeDeals.Services.Queries
{
   public class GetAllQuery<TGetDTO> : IRequest<IAsyncEnumerable<IServiceResult<TGetDTO>>> { }


   public abstract class GetAllQueryHandler<TEntity, TGetDTO> : IRequestHandler<GetAllQuery<TGetDTO>, IAsyncEnumerable<IServiceResult<TGetDTO>>> where TEntity : IEntity
   {
      //Marked abstract as derived ctor is needed to supply the required repolistory.

      private readonly IMapper _mapper;
      protected IRepository<TEntity> _repository;

      public GetAllQueryHandler(IMapper mapper)
      {
         _mapper = mapper;
      }


      //IMPORTANT NOTE: The approach below is a "kludge" that does not work! (the lambda in Task ctor returns IAsyncEnumerable, which never completes)
      //                It only fits the MediatR interface, i.e. IRequestHandler<GetAllQuery<TGetDTO>, IAsyncEnumerable<IServiceResult<TGetDTO>>>
      //                Note that as of Nov 2020, MediatR doesn't support IAsyncEnumerable (https://github.com/jbogard/MediatR/pull/574).


      public async Task<IAsyncEnumerable<IServiceResult<TGetDTO>>> Handle(GetAllQuery<TGetDTO> request, CancellationToken cancellationToken)
      {
         return await new Task<IAsyncEnumerable<IServiceResult<TGetDTO>>>(() => HandleAsync(request, cancellationToken));
         //A kludge to satisfy MediatR interface: entire async enumerable is wrapped in a task and awaited for - not to be used in a real app!
         //Note that as of Nov 2020, MediatR doesn't support IAsyncEnumerable (https://github.com/jbogard/MediatR/pull/574).
      }


      private async IAsyncEnumerable<IServiceResult<TGetDTO>> HandleAsync(GetAllQuery<TGetDTO> request, CancellationToken cancellationToken)
      {
         await foreach (var entity in _repository.GetAllAsync())
         {
            yield return new ServiceResult<TGetDTO> { Value = _mapper.Map<TGetDTO>(entity) };
         }
      }
   }
}
