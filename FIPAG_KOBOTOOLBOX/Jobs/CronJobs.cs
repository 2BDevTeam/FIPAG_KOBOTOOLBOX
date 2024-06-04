using Hangfire;
using FIPAG_KOBOTOOLBOX.Services;

namespace FIPAG_KOBOTOOLBOX.Jobs
{
    public class CronJobs
    {
        private readonly KOBOService koboService = new KOBOService();

        public void JobHandler()
        {
            /*
            BackgroundJob.Delete("SyncDocumentsJB");
            RecurringJob.AddOrUpdate(
               "LiftHandlingJB",
               () => oppService.ProcessarLifts(),
              Cron.Minutely());

            RecurringJob.AddOrUpdate(
               "SyncDocumentsJB",
               () => oppService.SyncDocumentsNotSynced(),
              Cron.Minutely());

            RecurringJob.AddOrUpdate(
               "SyncPreDocumentsJB",
               () => oppService.SyncPreDocumentsNotSynced(),
               Cron.Minutely());
            */
        }

    }
}
