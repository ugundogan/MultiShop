using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
	public class ProductDetailService : IProductDetailService
	{
		private readonly IMapper _mapper;
		private readonly IMongoCollection<ProductDetail> ProductDetailCollection;

		public ProductDetailService(IMapper mapper, IDatabaseSettings _databaseSettings)
		{
			var client = new MongoClient(_databaseSettings.ConnectionString);
			var database = client.GetDatabase(_databaseSettings.DatabaseName);
			ProductDetailCollection = database.GetCollection<ProductDetail>(_databaseSettings.ProductDetailCollectionName);
			_mapper = mapper;
		}

		public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
		{
			var value = _mapper.Map<ProductDetail>(createProductDetailDto);
			await ProductDetailCollection.InsertOneAsync(value);
		}

		public async Task DeleteProductDetailAsync(string id)
		{
			await ProductDetailCollection.DeleteOneAsync(x => x.ProductDetailID == id);
		}

		public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
		{
			var values = await ProductDetailCollection.Find(x => true).ToListAsync();
			return _mapper.Map<List<ResultProductDetailDto>>(values);
		}

		public async Task<ResultProductDetailDto> GetByIdProductDetailAsync(string id)
		{
			var value = await ProductDetailCollection.Find<ProductDetail>(x => x.ProductDetailID==id).FirstOrDefaultAsync();
			return _mapper.Map<ResultProductDetailDto>(value);
		}

		public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
		{
			var value = _mapper.Map<ProductDetail>(updateProductDetailDto);
			await ProductDetailCollection.FindOneAndReplaceAsync(x => x.ProductDetailID == updateProductDetailDto.ProductID, value);
		}
	}
}
