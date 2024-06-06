﻿using Hangfire;
using FIPAG_KOBOTOOLBOX.Services;

namespace FIPAG_KOBOTOOLBOX.Jobs
{
    public class CronJobs
    {
        private readonly KOBOService koboService = new KOBOService();

        public void JobHandler()
        {
            RecurringJob.AddOrUpdate(
               "AddEmLevantamentoJB",
               () => koboService.AdicionarLevantamentoBeneficiarios(),
              Cron.Minutely());


            var cronExpression = "0 */2 * * *";

            /*
            RecurringJob.AddOrUpdate(
               "AddFaturasNoKoboJB",
               () => koboService.AdicionarLevantamentoBeneficiarios(),
              cronExpression);
            */
        }

    }
}
