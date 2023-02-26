using SPT.Commands;
using SPT.Models;
using SPT.Models.Context;
using SPT.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Managers _selectedManager;

        private List<Clients> _clients;
        private List<Products> _products;
        private List<Managers> _managers;

        private List<DisplayedProducts> _displayedProducts;
        private ObservableCollection<Clients> _clientsByManager;

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
        public Products SelectedProduct
        {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value);
        }
        public Managers SelectedManager
        {
            get => GetSelectedManager();
            set => Set(ref _selectedManager, value);
        }

        

        public List<DisplayedProducts> DisplayedProducts
        {
            get => _displayedProducts;
            set => Set(ref _displayedProducts, value);
        }
        public ObservableCollection<Clients> ClientsByManager
        {
            get => _clientsByManager;
            set => Set(ref _clientsByManager, value);
        }

        private Clients GetSelectedClient()
        {
            if (_selectedClient != null)
            {
                SetDisplayedProducts();
            }
            return _selectedClient;
        }
        private Managers GetSelectedManager()
        {
            if(_selectedManager != null)
            {
                SetClientsByManager();
            }
            return _selectedManager;
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
        private void SetClientsByManager()
        {
            if (_selectedManager == null)
            {
                ClientsByManager.Clear();
                return;
            }
            ClientsByManager = new ObservableCollection<Clients>(_db.Clients.Where(c => c.ManagerId == _selectedManager.Id));
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
            if (DisplayedProducts == null || DisplayedProducts.Count < 1) return;
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
