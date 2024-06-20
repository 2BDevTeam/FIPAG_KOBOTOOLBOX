using Hangfire;
using FIPAG_KOBOTOOLBOX.Services;
using FIPAG_KOBOTOOLBOX.Persistence.Contexts;
using FIPAG_KOBOTOOLBOX.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using FIPAG_KOBOTOOLBOX.Persistence.Repositories;

namespace FIPAG_KOBOTOOLBOX.Jobs
{
    public class CronJobs
    {


        private KOBOService KOBOService = new KOBOService();


        public void JobHandler()
        {

            RecurringJob.AddOrUpdate(
               "ProcessarFormularios_OnBD_FIPAG",
               () => KOBOService.ProcessarFormularios("OnBD_FIPAG"),
              Cron.Minutely());

            RecurringJob.AddOrUpdate(
               "ProcessarFormularios_OnTS_FIPAG",
               () => KOBOService.ProcessarFormularios("OnTS_FIPAG"),
              Cron.Minutely());

            /*-------Jobs Temporários para sincronização de beneficiários já existentes no PHC-------*/
            /*
            RecurringJob.AddOrUpdate(
               "AddClDoKoboParaPHC",
               () => koboService.AdicionarLigacoesDeCls("OnBD"),
              Cron.Minutely());


            RecurringJob.AddOrUpdate(
               "SincronizarFt",
               () => koboService.SincronizarFt("OnBD"),
              Cron.Minutely());
            */
            /*------- FIM -------*/


            /*
            RecurringJob.AddOrUpdate(
               "AddEmLevantamentoJB_OnBD",
               () => _koboServiceBD.AdicionarLevantamentoBeneficiarios("OnBD"),
              Cron.Minutely());
            */

            /*
            RecurringJob.AddOrUpdate(
               "AddEmLevantamentoJB_OnTS",
               () => koboService.AdicionarLevantamentoBeneficiarios("OnTS"),
              Cron.Minutely());
            */
            /*
            RecurringJob.AddOrUpdate(
               "SincrinizarDadosUSyncQueue_OnTS",
               () => koboService.SincrinizarDadosUSyncQueue("OnTS"),
              Cron.Minutely());
            */

            /*
            RecurringJob.AddOrUpdate(
             "ProcessarBeira",
             () => _koboServiceBD.sINCRONIZAR("Beira"),
            Cron.Minutely(80));

            RecurringJob.AddOrUpdate(
            "ProcessarChokwe",
            () => _koboServiceBD.sINCRONIZAR("Chokwe"),
           Cron.Minutely(60));
            */
        }

    }
}
