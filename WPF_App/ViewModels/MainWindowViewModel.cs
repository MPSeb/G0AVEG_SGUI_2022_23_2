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
        public ICommand WoodsIdsForRetailer { get; set; }
        public ICommand DoesRetailerSellWood { get; set; }
        public ICommand AvgWoodPriceOfRetailer { get; set; }
        public ICommand AverageFurnPerRetailer { get; set; }
        public ICommand WoodUsedInFurnBelowPrice { get; set; }



        public ICommand CreateFurniture { get; set; }
        public ICommand RemoveFurniture { get; set; }
        public ICommand UpdateFurniture { get; set; }
        public ICommand CreateRetailer { get; set; }
        public ICommand RemoveRetailer { get; set; }
        public ICommand UpdateRetailer { get; set; }
        public ICommand CreateWood { get; set; }
        public ICommand RemoveWood { get; set; }
        public ICommand UpdateWood { get; set; }

        private Wood selectedWood;
        public Wood SelectedWood
        {
            get { return selectedWood; }
            set
            {
                SetProperty(ref selectedWood, value);
                if (selectedWood != null)
                {
                    Name = selectedWood.Name;
                }
                (RemoveWood as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        private string nameWood;
        public string NameWood
        {
            get { return nameWood; }
            set
            {
                SetProperty(ref nameWood, value);
                (CreateWood as RelayCommand).NotifyCanExecuteChanged();
                (UpdateWood as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Furniture selectedFurniture;
        public Furniture SelectedFurniture
        {
            get { return selectedFurniture; }
            set
            {
                SetProperty(ref selectedFurniture, value);
                if(selectedFurniture != null)
                {
                    Name = selectedFurniture.Name;
                }
                (RemoveFurniture as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private string nameFurniture;
        public string NameFurniture
        {
            get { return nameFurniture; }
            set
            {
                SetProperty(ref nameFurniture, value);
                (CreateFurniture as RelayCommand).NotifyCanExecuteChanged();
                (UpdateFurniture as RelayCommand).NotifyCanExecuteChanged();
            }
        }

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
            retailers.Add(new Retailer { Name = "test", Id = 100 });
            woods.Add(new Wood { Name = "test", Id = 100 });
            furnitures.Add(new Furniture { Name = "test", Id = 100 });

            
            CreateRetailer = new RelayCommand(async () => { await restService.Post(new Retailer { Name = Name }, "retailer"); DownloadData(); }, () => !string.IsNullOrEmpty(Name));
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

            CreateWood = new RelayCommand(async () => { await restService.Post(new Wood { Name = Name }, "wood"); DownloadData(); }, () => !string.IsNullOrEmpty(Name));
            UpdateWood = new RelayCommand(() =>
            {
                SelectedWood.Name = Name;
                restService.Put(SelectedWood, "wood");
            }, () => !string.IsNullOrEmpty(Name));
            RemoveWood = new RelayCommand(() =>
            {
                restService.Delete(selectedWood.Id, "wood");
                woods.Remove(SelectedWood);
            }, () => SelectedRetailer != null);

            CreateFurniture = new RelayCommand(async () => { await restService.Post(new Furniture { Name = Name }, "furniture"); DownloadData(); }, () => !string.IsNullOrEmpty(Name));
            UpdateFurniture = new RelayCommand(() =>
            {
                SelectedFurniture.Name = Name;
                restService.Put(SelectedFurniture, "furniture");
            }, () => !string.IsNullOrEmpty(Name));
            RemoveFurniture = new RelayCommand(() =>
            {
                restService.Delete(selectedFurniture.Id, "furniture");
                furnitures.Remove(SelectedFurniture);
            }, () => SelectedFurniture != null);



            DownloadData();

        }
        private void DownloadData()
        {
            woods.Clear();
            furnitures.Clear();
            retailers.Clear();
            foreach(var retailer in restService.Get<Retailer>("retailer"))
            {
                retailers.Add(retailer);
            }
            foreach(var wood in restService.Get<Wood>("wood"))
            {
                woods.Add(wood);
            }
            foreach(var furniture in restService.Get<Furniture>("furniture"))
            {
                furnitures.Add(furniture);
            }
        }
        public MainWindowViewModel() : this(Ioc.Default.GetService<RestService>())
        {

        }
    }
}
