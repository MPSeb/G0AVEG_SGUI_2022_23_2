using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using G0AVEG_ADT_2022_23_1.Data;
using G0AVEG_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_App.Services;

namespace WPF_App.ViewModels
{
    class MainWindowViewModel : ObservableRecipient
    {

        public ICommand CreateFurniture { get; set; }
        public ICommand RemoveFurniture { get; set; }
        public ICommand EditFurniture { get; set; }
        public ICommand WoodsIdsForRetailer{get; set; }
        public ICommand DoesRetailerSellWood{get;set; }
        public ICommand AvgWoodPriceOfRetailer{get;set; }
        public ICommand AverageFurnPerRetailer { get; set; }
        public ICommand WoodUsedInFurnBelowPrice{get; set; }


        public ICommand CreateRetailer { get; set; }
        public ICommand RemoveRetailer { get; set; }
        public ICommand UpdateRetailer { get; set; }

        public ICommand CreateWood { get; set; }
        public ICommand RemoveWood { get; set; }
        public ICommand UpdateWood { get; set; }
        private Retailer selectedRetailer;
        public Retailer SelectedRetailer
        {
            get { return selectedRetailer; }
            set
            {
                SetProperty(ref selectedRetailer, value);
                if (selectedRetailer != null )
                {
                    Name = selectedRetailer.Name;
                }
                (RemoveRetailer as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
                (CreateRetailer as RelayCommand).NotifyCanExecuteChanged();
                (UpdateRetailer as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        private RestService restService;
        public ObservableCollection<Furniture> furnitures { get; } = new ObservableCollection<Furniture>();
        public ObservableCollection<Wood> woods { get; } = new ObservableCollection<Wood>();
        public ObservableCollection<Retailer> retailers { get; } = new ObservableCollection<Retailer>();

        public MainWindowViewModel(RestService restService)
        {
            this.restService = restService;
            retailers.Add(new Retailer { Name = "test", Id = 1 });
            CreateRetailer = new RelayCommand(async () => { await restService.Post(new Retailer { Name = Name }, "retailer"); DownloadRetailers(); }, () => !string.IsNullOrEmpty(Name));
            UpdateRetailer = new RelayCommand(() =>
            {
                SelectedRetailer.Name = Name;
                restService.Put(SelectedRetailer, "retailer");
            }, () => !string.IsNullOrEmpty(Name));
            RemoveRetailer = new RelayCommand(() =>
            {
                restService.Delete(selectedRetailer.Id, "retailer");
                retailers.Remove(SelectedRetailer);
            }, () => SelectedRetailer != null);
            DownloadRetailers();
        }
        private void DownloadRetailers()
        {
            retailers.Clear();
            foreach(var retailer in restService.Get<Retailer>("retailer"))
            {
                retailers.Add(retailer);
            }
        }
        public MainWindowViewModel() : this(Ioc.Default.GetService<RestService>())
        {

        }
    }
}
