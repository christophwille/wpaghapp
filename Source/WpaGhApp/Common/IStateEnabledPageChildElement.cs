using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Common
{
    public interface IStateEnabledPageChildElement
    {
        void LoadState(Dictionary<string, object> pageState);
        void SaveState(Dictionary<string, object> pageState);
    }
}
