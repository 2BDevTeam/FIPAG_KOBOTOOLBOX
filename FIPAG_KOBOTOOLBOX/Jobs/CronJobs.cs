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
               () => KOBOService.ProcessarFormularios("OnTS_OBA_AdRC_TETE"),
              Cron.Yearly());
            /*
            RecurringJob.AddOrUpdate(
               "ProcessarFormularios_OnBD_FIPAG",
               () => KOBOService.ProcessarFormularios("OnTS_OBA_AdRC_TETE"),
              Cron.Minutely());
            */ 



            RecurringJob.AddOrUpdate(
               "SincronizarClientesOBA",
               () => KOBOService.SyncClientesOBA(),
              Cron.Yearly());
            
            /*
            RecurringJob.AddOrUpdate(
               "SincronizarFt",
               () => KOBOService.SincronizarFt(),
              Cron.Minutely());
            */



            /*-------Jobs Temporários para sincronização de beneficiários já existentes no PHC-------*/
            /*
            RecurringJob.AddOrUpdate(
               "AddClDoKoboParaPHC",
               () => koboService.AdicionarLigacoesDeCls("OnBD"),
              Cron.Minutely());


            */
            /*------- FIM -------*/


        }

    }
}
