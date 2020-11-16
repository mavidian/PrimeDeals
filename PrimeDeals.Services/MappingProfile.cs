using AutoMapper;
using PrimeDeals.Core.DTOs.Broker;
using PrimeDeals.Core.DTOs.Policy;
using PrimeDeals.Core.DTOs.Sale;
using PrimeDeals.Core.Models;
using System;
using static PrimeDeals.Core.Models.Broker;
using static PrimeDeals.Core.Models.Policy;
using static PrimeDeals.Core.Models.Sale;

namespace PrimeDeals.Services
{
   public class MappingProfile : Profile
   {
      public MappingProfile()
      {
         CreateMap<Broker, GetBrokerDTO>();
         CreateMap<AddBrokerDTO, Broker>()
            .ForMember(b => b.State, o => o.MapFrom(d => d.State.ToEnum<BrokerState>()));
         CreateMap<ReplaceBrokerDTO, Broker>()
            .ForMember(b => b.State, o => o.MapFrom(d => d.State.ToEnum<BrokerState>()));

         CreateMap<Sale, GetSaleDTO>()
            .ForMember(d => d.BrokerId, o => o.MapFrom(s => s.ParentId));
         CreateMap<AddSaleDTO, Sale>()
            .ForMember(s => s.ParentId, o => o.MapFrom(d => d.BrokerId))
             .ForMember(b => b.State, o => o.MapFrom(d => d.State.ToEnum<SaleState>()));
         CreateMap<ReplaceSaleDTO, Sale>()
            .ForMember(s => s.ParentId, o => o.MapFrom(d => d.BrokerId))
             .ForMember(b => b.State, o => o.MapFrom(d => d.State.ToEnum<SaleState>()));

         CreateMap<Policy, GetPolicyDTO>()
            .ForMember(d => d.SaleId, o => o.MapFrom(p => p.ParentId));
         CreateMap<AddPolicyDTO, Policy>()
            .ForMember(p => p.ParentId, o => o.MapFrom(d => d.SaleId))
              .ForMember(b => b.State, o => o.MapFrom(d => d.State.ToEnum<PolicyState>()));
         CreateMap<ReplacePolicyDTO, Policy>()
            .ForMember(p => p.ParentId, o => o.MapFrom(d => d.SaleId))
              .ForMember(b => b.State, o => o.MapFrom(d => d.State.ToEnum<PolicyState>()));
      }

   }

   internal static class MappingProfileHelpers
   {
      internal static TEnum ToEnum<TEnum>(this string value) where TEnum : struct, Enum
      {
         const bool ignoreCase = true;
         TEnum enumVal;
         if (Enum.TryParse(value, ignoreCase, out enumVal)) return enumVal;
         return default;
      }
   }
}
