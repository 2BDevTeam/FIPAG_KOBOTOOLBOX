using Hangfire;
using FIPAG_KOBOTOOLBOX.Services;

namespace FIPAG_KOBOTOOLBOX.Jobs
{
    public class CronJobs
    {
        private readonly KOBOService koboService = new KOBOService();

        public void JobHandler()
        {
            /*-------Jobs Temporários para sincronização de beneficiários já existentes no PHC-------*/
            RecurringJob.AddOrUpdate(
               "AddClDoKoboParaPHC",
               () => koboService.AdicionarLigacoesDeCls(),
              Cron.Minutely());


            RecurringJob.AddOrUpdate(
               "SincronizarFt",
               () => koboService.SincronizarFt(),
              Cron.Minutely());
            /*------- FIM -------*/



            RecurringJob.AddOrUpdate(
               "AddEmLevantamentoJB",
               () => koboService.AdicionarLevantamentoBeneficiarios(),
              Cron.Minutely());

            RecurringJob.AddOrUpdate(
               "SincrinizarDadosUSyncQueue",
               () => koboService.SincrinizarDadosUSyncQueue(),
              Cron.Minutely());

        }

    }
}
