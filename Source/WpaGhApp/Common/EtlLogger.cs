using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Diagnostics;
using Windows.Storage;

namespace WpaGhApp.Common
{
    // Taken from: https://code.msdn.microsoft.com/windowsapps/Logging-Sample-for-Windows-ecd3622f/
    public class EtlLogger
    {
        public LoggingChannel LogChannel { get; private set; }
        public LoggingSession LogSession { get; private set; }

        private StorageFolder logUploadFolder;

        public const string LOG_SESSION_RESROUCE_NAME = "LogSession";

        static private EtlLogger logger;
        private const int DAYS_TO_DELETE = 15;

        public async void InitiateLogger()
        {
            LogChannel = new LoggingChannel("OctoCentralChannel");
            LogSession = new LoggingSession("OctoCentral Session");

            LogSession.AddLoggingChannel(LogChannel);

            await RegisterUnhandledErrorHandler();
        }

        /// <summary> 
        /// Maintains singleton object  
        /// </summary> 
        /// <returns></returns> 
        static public EtlLogger GetLogger()
        {
            if (logger == null)
            {
                logger = new EtlLogger();
            }
            return logger;
        }

        private async Task RegisterUnhandledErrorHandler()
        {
            logUploadFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("MyLogFile",
                CreationCollisionOption.OpenIfExists);

            CoreApplication.UnhandledErrorDetected += CoreApplication_UnhandledErrorDetected;
        }

        /// <summary> 
        /// Any  uncaught exceptions are thrown to here 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        private void CoreApplication_UnhandledErrorDetected(object sender, UnhandledErrorDetectedEventArgs e)
        {
            try
            {
                LogChannel.LogMessage("Caught the exception");
                e.UnhandledError.Propagate();
            }
            catch (Exception ex)
            {
                //logChannel.LogMessage(string.Format("UnhandledErro: 0x{0:X})", ex.HResult), LoggingLevel.Critical); 
                LogChannel.LogMessage(string.Format("Effor Message: {0}", ex.Message));

                if (LogSession != null)
                {
                    //var filename = DateTime.Now.ToString("yyyyMMdd-HHmmssTzz") + ".etl"; 
                    var filename = DateTime.Now.ToString("yyyyMMdd") + ".etl";
                    var logSaveTask = LogSession
                        .SaveToFileAsync(logUploadFolder, filename)
                        .AsTask();

                    logSaveTask.Wait();
                }


                // throw; 
            }
        }

        /// <summary> 
        /// Deelete the files based on the days mentioned 
        /// </summary> 
        public async void DeleteFile()
        {
            try
            {
                var logFiles = await logUploadFolder.GetFilesAsync();

                foreach (var logFile in logFiles)
                {
                    if ((DateTime.Now - logFile.DateCreated).Days > DAYS_TO_DELETE)
                    {
                        await logFile.DeleteAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                LogChannel.LogMessage(ex.Message);
            }
        }
    }
}