using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.Shared.Products
{
    public class ProductQueryParameters
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public ProductSortingOptions? Sort { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductSortingOptions
    {
        NameAsc = 1,
        NameDesc,
        PriceAsc,
        PriceDesc

    }
}
