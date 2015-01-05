using System.Diagnostics;
using System.Threading.Tasks;
using Caliburn.Micro;
using WpaGhApp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using WpaGhApp.Github;
using WpaGhApp.Services;
using WpaGhApp.ViewModels;
using WpaGhApp.ViewModels.Main;
using WpaGhApp.ViewModels.Org;
using WpaGhApp.ViewModels.Repository;
using WpaGhApp.Views;
using WpaGhApp.Views.Main;

namespace WpaGhApp
{

    public sealed partial class App
    {
        private TransitionCollection transitions;

        private WinRTContainer _container;
        private INavigationService _navigationService;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        protected override void Configure()
        {
            LogManager.GetLog = t => new DebugLog(t);

            _container = new WinRTContainer();
            _container.RegisterWinRTServices();

            _container.RegisterPerRequest(typeof(IMessageService), null, typeof(DefaultMessageService));

            _container.RegisterInstance(typeof(IResourceLoader), null, new DefaultResourceLoader());
            _container.RegisterSingleton(typeof(IGitHubService), null, typeof(GhService));

            _container
                .PerRequest<MainViewModel>()
                    .PerRequest<NewsViewModel>()
                    .PerRequest<RepositoriesViewModel>()
                    .PerRequest<FollowersViewModel>()
                    .PerRequest<FollowingViewModel>()
                    .PerRequest<OrgsListViewModel>()
                .PerRequest<RepositoryViewModel>()
                    .PerRequest<RepositoryCommitsViewModel>()
                    .PerRequest<RepositoryIssuesViewModel>()
                    .PerRequest<RepositoryInfoViewModel>()
                    .PerRequest<RepositoryContentsViewModel>()
                .PerRequest<OrgViewModel>()
                    .PerRequest<MembersViewModel>()
                .PerRequest<AboutViewModel>()
                .PerRequest<AuthorizeViewModel>()
                .PerRequest<HtmlUrlViewModel>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            _navigationService = _container.RegisterNavigationService(rootFrame);
            SuspensionManager.RegisterFrame(rootFrame, "AppFrame");
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = _container.GetInstance(service, key);
            if (instance != null)
                return instance;
            throw new Exception("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        async Task<bool> CommonApplicationLaunchAsync(ApplicationExecutionState previousExecutionState)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }

            //// Initiate Log channel and Log sessions 
            //EtlLogger.GetLogger().InitiateLogger();

            //// Delete the logfile which are beyond the dates 
            //EtlLogger.GetLogger().DeleteFile();

            //EtlLogger.GetLogger().LogChannel.LogMessage("Starting up in CommonApplicationLaunchAsync");
#endif

            Initialize();

            PrepareViewFirst();

            var resumed = false;

            if (previousExecutionState == ApplicationExecutionState.Terminated)
            {
                // Restore the saved navigation & session state
                try
                {
                    resumed = _navigationService.ResumeState();
                    await SuspensionManager.RestoreAsync();
                }
                catch (SuspensionManagerException)
                {
                    // Something went wrong restoring state.
                    // Assume there is no state and continue.
                }
            }

            return resumed;
        }

        protected async override void OnLaunched(LaunchActivatedEventArgs args)
        {
            bool resumed = await CommonApplicationLaunchAsync(args.PreviousExecutionState);

            if (!resumed)
            {
                if (!GhService.IsAppAuthorized()) // The app cannot work unauthorized at all
                {
                    DisplayRootView<AuthorizeView>();
                    return;
                }

                DisplayRootView<MainView>();
            }
        }

        protected async override void OnActivated(IActivatedEventArgs args)
        {
            bool resumed = await CommonApplicationLaunchAsync(args.PreviousExecutionState);

            // Intentionally *not* using the ContinuationManager from the samples (need one ActivationKind only)
            var continuationEventArgs = args as IContinuationActivatedEventArgs;
            if (continuationEventArgs != null)
            {
                if (continuationEventArgs.Kind == ActivationKind.WebAuthenticationBrokerContinuation)
                {
                    var wabEventArgs = args as WebAuthenticationBrokerContinuationEventArgs;
                    var rootFrame = Window.Current.Content as Frame;

                    if (null != rootFrame && null != wabEventArgs)
                    {
                        var authorizeView = ((AuthorizeView)rootFrame.Content);

                        if (null != authorizeView)
                        {
                            var vm = authorizeView.DataContext as AuthorizeViewModel;
                            await vm.ContinueAuthorizationAsync(wabEventArgs.WebAuthenticationResult);
                        }
                    }
                }
            }
        }

        //private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        //{
        //    var rootFrame = sender as Frame;
        //    rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
        //    rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        //}

        protected async override void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            try
            {
                _navigationService.SuspendState();
                await SuspensionManager.SaveAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}
