using MangoMediaData;
using MangoMediaData.APIRequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MediaMongoAPI.Controllers
{

    [RoutePrefix("api/software")]
    public class SoftwareController : ApiController
    {
        // GET api/software
        [Route(Name="list")]
        public IEnumerable<Othersoftware> Get([FromUri] InitialUserData request)
        {

            // Apply Filter
            return GetSoftwareList();
        }

        private  IEnumerable<Othersoftware> GetSoftwareList()
        {
            return new List<Othersoftware>()
            {
           
                new Othersoftware
                 { 
                     DownloadUrl ="http://download.skype.com/cc8c0832c80579731d528a2dabcb134c/SkypeSetup.exe",
                     HasUrl = true,
                     Name= "Robots",
                     Url = "www.Notepadplus.com/about",
                     InstallerName = "SkypeSetup.exe",
                     Arguments = ""
                 },
                 new Othersoftware
                 { 
                     DownloadUrl ="http://download.skype.com/cc8c0832c80579731d528a2dabcb134c/SkypeSetup.exe",
                     Name= "Robots2",
                     Url = "www.Notepadplus.com/about",
                     InstallerName = "SkypeSetup.exe",
                     Arguments = ""
                 }
          
           
           };
        }

        
        
    }
}
