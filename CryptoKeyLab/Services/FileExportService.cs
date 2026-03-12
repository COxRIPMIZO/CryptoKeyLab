using System.Text;
using Microsoft.AspNetCore.Components;

namespace CryptoKeyLab.Services
{
    public class FileExportService
    {
        private readonly NavigationManager _navigationManager;

        public FileExportService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void ExportText(string fileName, string content, string contentType = "text/plain")
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            var base64 = Convert.ToBase64String(bytes);
            var url = $"data:{contentType};base64,{base64}";
            _navigationManager.NavigateTo(url, forceLoad: true);
        }
    }
}