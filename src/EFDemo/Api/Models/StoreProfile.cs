using AutoMapper;
using EFDemo.Domain;

namespace EFDemo.Api.Models;

public class StoreProfile : Profile
{
	public StoreProfile()
	{
		CreateMap<Address, StoreDto>();
		CreateMap<Address, StoreListDto>();

        CreateMap<Product, ProductDto>();

        CreateMap<Store, StoreListDto>();
		CreateMap<Store, StoreDto>()
			.IncludeMembers(x=>x.Address)
			.ForMember(x => x.StoreName, opt => opt.MapFrom(x => x.Name));



        // Reverse mapping
		CreateMap<StoreCreateDto, Store>()
			.ConvertUsing((src, dest, context) =>
			{
				var store = new Store(src.Name);

				return store;
			});

        CreateMap<StoreUpdateDto, Store>()
            .ConvertUsing((src, dest, context) =>
            {
				dest.UpdateName(src.Name);

                return dest;
            });
    }
}
