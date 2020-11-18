using AutoMapper;
using MediatR;
using PrimeDeals.Core.Interfaces.Models;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Services.Services;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeDeals.Services.Queries
{
   public class GetByIdQuery<TGetDTO> : IRequest<ServiceResult<TGetDTO>>
   {
      public string Id { get; set; }
   }

   public abstract class GetByIdHandler<TEntity, TGetDTO> : IRequestHandler<GetByIdQuery<TGetDTO>, ServiceResult<TGetDTO>> where TEntity : IEntity
   {
      //Marked abstract as derived ctor is needed to supply the required repolistory.

      private readonly IMapper _mapper;
      protected IRepository<TEntity> _repository;

      public GetByIdHandler(IMapper mapper)
      {
         _mapper = mapper;
      }

      public async Task<ServiceResult<TGetDTO>> Handle(GetByIdQuery<TGetDTO> request, CancellationToken cancellationToken)
      {
         var entity = await _repository.GetByIdAsync(request.Id);
         return new ServiceResult<TGetDTO> { Success = entity != null, Value = _mapper.Map<TGetDTO>(entity) };
      }
   }
}
