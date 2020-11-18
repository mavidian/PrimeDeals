using AutoMapper;
using MediatR;
using PrimeDeals.Core.Interfaces.Models;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Interfaces.Services;
using PrimeDeals.Services.Services;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeDeals.Services.Commands
{
   public class AddCommand<TAddDTO,TGetDTO> : IRequest<ServiceResult<TGetDTO>>
   {
      public TAddDTO NewEntity { get; set; }
   }


   public abstract class AddCommandHandler<TEntity, TAddDTO, TGetDTO> : IRequestHandler<AddCommand<TAddDTO, TGetDTO>, ServiceResult<TGetDTO>> where TEntity : IEntity
   {
      //Marked abstract as derived ctor is needed to supply the required repolistory.
      private readonly IMapper _mapper;
      protected IRepository<TEntity> _repository;
      public AddCommandHandler(IMapper mapper)
      {
         _mapper = mapper;
      }

      public async Task<ServiceResult<TGetDTO>> Handle(AddCommand<TAddDTO, TGetDTO> request, CancellationToken cancellationToken)
      {
         var entity = _mapper.Map<TEntity>(request.NewEntity);
         await _repository.AddAsync(entity);  //will assign Id
         return new ServiceResult<TGetDTO> { Value = _mapper.Map<TGetDTO>(entity) };
      }
   }
}
