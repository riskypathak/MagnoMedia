﻿
using MagnoMedia.Data.APIRequestDTO;
using MagnoMedia.Data.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace MagnoMedia.Web.Api.Controllers
{
     [RoutePrefix("api/installer")]
    public class InstallerController : ApiController
    {

          [HttpPost(), Route("SaveInstallerState")]
         public bool SaveInstallerState(InstallerData installerRequestData)
         {

             IDbConnectionFactory dbFactory =
               new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);


             InstallationReport _InstallationReportEntity = new InstallationReport
             {
                 MachineUID = installerRequestData.MachineUID,
                 Message = installerRequestData.Message,
                 ThirdPartyApplicationId = installerRequestData.ThirdPartyApplicationId,
                 ThirdPartyApplicationState = installerRequestData.ThirdPartyApplicationState.ToString()
             };

             using (IDbConnection db = dbFactory.Open())
             {

                 db.Insert<InstallationReport>(_InstallationReportEntity);
                  return true;
             }

         }

        
    }
}
