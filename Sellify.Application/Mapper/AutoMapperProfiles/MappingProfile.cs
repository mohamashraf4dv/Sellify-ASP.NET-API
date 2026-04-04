using AutoMapper;
using Sellify.Application.Features.Products.Command.SellerAddProduct;
using Sellify.Application.Features.Products.Command.SellerUpdateProducts;
using Sellify.Application.Features.Seller.Query.GetBySellerIdProducts;

namespace Sellify.Application.Mapper.AutoMapperProfiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, GetBySellerIdProductsQueryDTO>().ReverseMap();
            CreateMap<Product, SellerProductDTO>().ReverseMap();
            CreateMap<Product,SellerUpdateProductsDTO>().ReverseMap();
        }
    }
}
