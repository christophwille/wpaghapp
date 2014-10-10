using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace WpaGhApp.Services
{
    public class DefaultMessageService : IMessageService
    {

        public async Task ShowAsync(string content)
        {
            var msgDialog = new MessageDialog(content);
            await msgDialog.ShowAsync();
        }
    }
}
