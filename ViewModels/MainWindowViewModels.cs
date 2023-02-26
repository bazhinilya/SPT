using SPT.Commands;
using SPT.Models;
using SPT.Models.Context;
using SPT.ViewModels.Base;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Input;

namespace SPT.ViewModels
{
    public class MainWindowViewModels : MainWindowBase
    {
        public MainWindowViewModels()
        {
            _clients = _db.Clients.ToList();
            _products = _db.Products.ToList();
            _managers = _db.Managers.ToList();

            UpdateCommand = new RelayCommand(OnUpdateCommandExecuted, CanUpdateCommandExecute);
            AddProductCommand = new RelayCommand(OnAddProductCommandExecuted, CanAddProductCommandExecute);
        }

        private readonly StpContext _db = new StpContext();
        private string _tittle = "SPT";
        private Clients _selectedClient;
        private Products _selectedProduct;

        private List<Clients> _clients;
        private List<Products> _products;
        private List<Managers> _managers;

        private List<DisplayedProducts> _displayedProducts;

        public string Tittle
        {
            get => _tittle;
            set => Set(ref _tittle, value);
        }

        public List<Clients> Clients
        {
            get => _clients;
            set => Set(ref _clients, value);
        }
        public List<Products> Products
        {
            get => _products;
            set => Set(ref _products, value);
        }
        public List<Managers> Managers
        {
            get => _managers;
            set => Set(ref _managers, value);
        }

        public Clients SelectedClient
        {
            get => GetSelectedClient();
            set => Set(ref _selectedClient, value);
        }
        public Products SelectedProducts
        {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value);
        }
        public List<DisplayedProducts> DisplayedProducts
        {
            get => _displayedProducts;
            set => Set(ref _displayedProducts, value);
        }

        private Clients GetSelectedClient()
        {
            if (_selectedClient != null)
            {
                SetDisplayedProducts();
            }
            return _selectedClient;
        }
        private void SetDisplayedProducts()
        {
            if (_selectedClient == null)
            {
                DisplayedProducts.Clear();
                return;
            }
            DisplayedProducts = _db.ClientsProducts
                .Where(cp => cp.ClientId == _selectedClient.Id)
                .Join(_db.Products, cp => cp.ProductId, p => p.Id,
                    (cp, p) =>
                    new DisplayedProducts
                    {
                        Name = p.Name,
                        ProductId = p.Id,
                        ClientId = cp.ClientId,
                        ClientProductId = cp.Id,
                        Price = p.Price,
                        SubscriptionPeriod = p.SubscriptionPeriod,
                        Type = p.Type
                    })
                .ToList();
        }

        public Products SelectedProduct
        {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value);
        }

        public ICommand UpdateCommand { get; }
        public ICommand AddProductCommand { get; }

        private bool CanUpdateCommandExecute(object p) => true;
        private void OnUpdateCommandExecuted(object p)
        {
            _db.Clients.AddOrUpdate(_clients.ToArray());
            _db.SaveChanges();
        }

        private bool CanAddProductCommandExecute(object p) => true;
        private void OnAddProductCommandExecuted(object p)
        {
            var displayedProducts = DisplayedProducts[DisplayedProducts.Count - 1];
            var clientProductsToCreate = new ClientsProducts
            {
                ClientId = SelectedClient.Id,
                ProductId = displayedProducts.ProductId
            };
            _db.ClientsProducts.Add(clientProductsToCreate);
            _db.SaveChanges();
        }
    }
}
