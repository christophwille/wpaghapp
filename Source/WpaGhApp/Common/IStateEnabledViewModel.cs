using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Common
{
    public interface IStateEnabledViewModel
    {
        void LoadState(string state);
        string SaveState();
    }
}
