using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace WpaGhApp.Common
{
    public class StateEnabledPage : Page
    {
        private readonly StateHelper _stateHelper;

        public StateEnabledPage()
        {
            this._stateHelper = new StateHelper(this);
            this._stateHelper.LoadState += this.StateHelper_LoadState;
            this._stateHelper.SaveState += this.StateHelper_SaveState;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._stateHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this._stateHelper.OnNavigatedFrom(e);
        }

        private const string PageStateDictionaryKey = "LocalPageState";
        private void StateHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            var vm = DataContext as IStateEnabledViewModel;

            if (null != vm)
            {
                if (e.PageState != null && e.PageState.ContainsKey(PageStateDictionaryKey))
                {
                    string json = e.PageState[PageStateDictionaryKey].ToString();
                    vm.LoadState(json);
                }
            }

            if (e.PageState != null) LoadState(e.PageState);
        }

        private void StateHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            var vm = DataContext as IStateEnabledViewModel;

            if (null != vm)
            {
                string json = vm.SaveState();
                e.PageState[PageStateDictionaryKey] = json;
            }

            SaveState(e.PageState);
        }

        // We need to keep this around because child controls are loaded *way* after the OnNavigatedTo event
        private Dictionary<string, object> _pageStateOnNavigatedTo;
        public virtual void LoadState(Dictionary<string, object> pageState)
        {
            _pageStateOnNavigatedTo = pageState;
        }

        public virtual void SaveState(Dictionary<string, object> pageState)
        {
            foreach (var element in _registeredStateEnabledPageChildElements)
            {
                element.SaveState(pageState);
            }
        }

        private readonly List<IStateEnabledPageChildElement> _registeredStateEnabledPageChildElements = new List<IStateEnabledPageChildElement>();
        public void RegisterForSaveStateAndImmediateLoad(IStateEnabledPageChildElement pageChildElement)
        {
            _registeredStateEnabledPageChildElements.Add(pageChildElement);
            if (null != _pageStateOnNavigatedTo) pageChildElement.LoadState(_pageStateOnNavigatedTo);
        }

        public static StateEnabledPage FindAsParent(DependencyObject obj)
        {
            DependencyObject current = obj;

            while (null != current)
            {
                if (current is StateEnabledPage)
                    return current as StateEnabledPage;

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }

        public static void FindAndRegister(DependencyObject obj)
        {
            if (!(obj is IStateEnabledPageChildElement)) throw new ArgumentException("Element must support state");

            var stateEnabledParentPage = FindAsParent(obj);

            if (null != stateEnabledParentPage)
            {
                stateEnabledParentPage.RegisterForSaveStateAndImmediateLoad(obj as IStateEnabledPageChildElement);
            }
        }
    }
}
