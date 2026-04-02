using AutoMapper;
using Microsoft.AspNetCore.Http;
using Sellify.Application.Contracts;
using Sellify.Application.Contracts.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace Sellify.Application.Features.Products.Command.SellerAddProduct
{
    public class SellerAddProductCommandHandler : IRequestHandler<SellerAddProductCommand, GenericResultDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public SellerAddProductCommandHandler(IUnitOfWork unitOfWork,IProductRepository productRepository ,IFileService fileService , IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._productRepository = productRepository;
            this._fileService = fileService;
            this._mapper = mapper;
        }
        public async Task<GenericResultDTO> Handle(SellerAddProductCommand request, CancellationToken cancellationToken)
        {

            //_productRepository.CreateAsync()
            Product product = _mapper.Map<SellerProductDTO,Product>(request.ProductDTO);

            var imagePath= await _fileService.UploadFile(request.ProductDTO.image);

            product.ThumbnailSource = imagePath;
            product.SellerId = request.SellerId;
            product.ProductImages = new HashSet<ProductImage>
            {
                new ProductImage
                {
                    Url = imagePath,
                    Description = $"Thumbnail of {product.Name}",
                    IsThumbnail = true
                }
            };
            var result = await _productRepository.CreateAsync(product, cancellationToken);
            
            await _unitOfWork.SaveChangesAsync();
            return new GenericResultDTO(null, 201);
        }
    }
}
