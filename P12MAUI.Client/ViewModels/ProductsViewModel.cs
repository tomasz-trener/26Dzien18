using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P06Shop.Shared;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P12MAUI.Client.ViewModels
{
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IConnectivity _connectivity;
        //private readonly ProductDetailsView _productDetailsView;
        //private readonly ISpeechService _speechService;

        [ObservableProperty]
        private ObservableCollection<Product> _products;


        [ObservableProperty]
        private Product _selectedProduct;

   

        public ProductsViewModel(IProductService productService, IMessageDialogService messageDialogService, IConnectivity connectivity
            /*ProductDetailsView productDetailsView*/  /*, ISpeechService speechService*/)
        {
            _productService = productService;
            _messageDialogService = messageDialogService;
            _connectivity = connectivity;

            GetProductsAsync();
        }

        public async Task GetProductsAsync()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not avaible!");
                return;
            }


            var result = await _productService.GetProductsAsync();
            if (result.Success)
            {
                Products = new ObservableCollection<Product>(result.Data);
            }
        }

       

        [RelayCommand]
        public async Task ShowDetails(Product product)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not avaible!");
                return;
            }


            //_productDetailsView.Show();
            SelectedProduct = product;
            //_productDetailsView.DataContext = this;


            await Shell.Current.GoToAsync(nameof(ProductDetailsView), true, new Dictionary<string, object>
            {
                {"Product",product },
                {nameof(ProductsViewModel), this }
            });
        }


     
        [RelayCommand]
        public async Task New()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not avaible!");
                return;
            }


            //_productDetailsView.Show();
            //_productDetailsView.DataContext = this;
            SelectedProduct = new Product();

            await Shell.Current.GoToAsync(nameof(ProductDetailsView), true, new Dictionary<string, object>
            {
                {"Product",SelectedProduct },
                {nameof(ProductsViewModel), this }
            });
        }

        //[RelayCommand]
        //public async Task RecognizeVoice()
        //{
        //    var text = await _speechService.RecognizeAsync();
        //    //  SelectedProduct.Description = text;


        //    SelectedProduct = new Product()
        //    {
        //        Id = _selectedProduct.Id,
        //        Description = text,
        //        Barcode = _selectedProduct.Barcode,
        //        Title = _selectedProduct.Title,
        //        Price = _selectedProduct.Price,
        //        ReleaseDate = _selectedProduct.ReleaseDate
        //    };

        //}
    }
}


 