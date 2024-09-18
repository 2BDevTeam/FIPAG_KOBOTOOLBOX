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
            /*
            RecurringJob.AddOrUpdate(
               "ProcessarFormularios_OnBD_FIPAG",
               () => KOBOService.ProcessarFormularios("OnBD_FIPAG", "Cidade_de_Nacala"),
              Cron.Hourly());
            
            */


            RecurringJob.AddOrUpdate(
             "ProcessarFormularios_OnBD_AdRN_Nacala",
             () => KOBOService.ProcessarFormularios("OnBD_AdRN_Nacala", "Cidade_de_Nacala"),
            "*/20 * * * *");


            RecurringJob.AddOrUpdate(
             "ProcessarFormularios_OnBD_AdRN_Pemba",
             () => KOBOService.ProcessarFormularios("OnBD_AdRN_Pemba", "Cidade_de_Pemba"),
            "*/20 * * * *");


            /*
            RecurringJob.AddOrUpdate(
               "ProcessarFormularios_AdRC_MOATIZE",
               () => KOBOService.ProcessarFormularios("OnBD_AdRC_MOATIZE", "Moatize"),
              Cron.Hourly());
            */


            /*
            RecurringJob.AddOrUpdate(
               "ProcessarFormularios_OnBD_FIPAG_Beira",
               () => KOBOService.ProcessarFormularios("OnBD_Fipag_Beira", "Beira"),
              Cron.MinuteInterval(10));
            */

            /*
            RecurringJob.AddOrUpdate(
               "ProcessarFormularios_OnBD_FIPAG_TETE",
               () => KOBOService.ProcessarFormularios("OnBD_FIPAG_TETE", "Cidade_de_Tete"),
              Cron.Hourly());
            */


        }

    }
}
