using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.Shared.Dtos
{
    public class ProductQueryParameters
    {
        private const int DEFAULTPAGESIZE = 6;
        private const int MAXPAGESIZE = 10;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public ProductSortingOptions? Sort { get; set; }
        public int PageIndex { get; set; } = 1; 
        private int pageSize = DEFAULTPAGESIZE;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MAXPAGESIZE ? MAXPAGESIZE 
                              : value < DEFAULTPAGESIZE ? DEFAULTPAGESIZE : value;
        }
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
