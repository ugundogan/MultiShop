using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
	public class ProductDetail
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string ProductDetailID { get; set; }
        public string ProductDescription { get; set; }
        public string ProductInformation { get; set; }
        public string ProductID { get; set; }

		[BsonIgnore]
		public Product Product { get; set; }
    }
}
