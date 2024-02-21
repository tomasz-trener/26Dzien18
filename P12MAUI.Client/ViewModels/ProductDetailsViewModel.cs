using CommunityToolkit.Mvvm.ComponentModel;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P12MAUI.Client.ViewModels
{
    [QueryProperty(nameof(Product),nameof(Product))]
    [QueryProperty(nameof(ProductsViewModel),nameof(ProductsViewModel))]
    public partial class ProductDetailsViewModel : ObservableObject
    {
        IProductService _productService;
        public ProductDetailsViewModel(IProductService productService)
        {
            _productService = productService;
        }

        [ObservableProperty]
        private Product product;

    }
}
