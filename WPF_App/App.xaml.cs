using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_App.Services;

namespace WPF_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:63958/hub")
            .AddJsonProtocol()
            .WithAutomaticReconnect()
            .ConfigureLogging((log_builder) => log_builder.SetMinimumLevel(LogLevel.Trace)).Build();
             connection.StartAsync().Wait();
            RestService restService = new RestService("http://localhost:63958/");
            Ioc.Default.ConfigureServices(new ServiceCollection().AddSingleton<RestService>(restService).BuildServiceProvider());
        }

    }
}
