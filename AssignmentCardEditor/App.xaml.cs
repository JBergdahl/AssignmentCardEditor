using System.Windows;
using AssignmentCardEditor.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using Data;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace AssignmentCardEditor
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private const string MongoDbUrl = "mongodb://localhost:27017";
        private const string CardEditorDbName = "CardEditor";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RegisterServices();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();

            var client = new MongoClient(MongoDbUrl);
            var database = client.GetDatabase(CardEditorDbName);
            var serviceProvider = services.AddSingleton(database)
                .AddSingleton<ICardEditorDbContext, CardEditorDbContext>()
                .AddSingleton<IDbMethods, DbMethods>()
                .AddTransient<MainWindowViewModel>()
                .AddTransient<CardViewModel>()
                .AddTransient<CardTypeViewModel>()
                .AddTransient<BrowserViewModel>()
                .BuildServiceProvider();

            Ioc.Default.ConfigureServices(serviceProvider);
        }
    }
}