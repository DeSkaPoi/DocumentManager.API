using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;


namespace DocumentManagerUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddFontAwesomeIcons()
                .AddBootstrapProviders()
                .AddBootstrapComponents();

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
/*<DataGridColumn Field="@nameof(Document.Salary)" Caption="Salary" DisplayFormat="{0:C}" Editable>
            <EditTemplate>
                <NumericEdit TValue="decimal" Value="@((decimal)context.CellValue)" ValueChanged="@( v => context.CellValue = v)" />
            </EditTemplate>
        </DataGridColumn>

 <DataGridCommandColumn NewCommandAllowed="false" EditCommandAllowed="false" DeleteCommandAllowed="false">
            <SaveCommandTemplate>
                <Button ElementId="btnSave" Type="ButtonType.Submit" PreventDefaultOnSubmit Color="Color.Primary" Clicked="@context.Clicked">@context.LocalizationString</Button>
            </SaveCommandTemplate>
            <CancelCommandTemplate>
                <Button ElementId="btnCancel" Color="Color.Secondary" Clicked="@context.Clicked">@context.LocalizationString</Button>
            </CancelCommandTemplate>
        </DataGridCommandColumn>
 
 
 */