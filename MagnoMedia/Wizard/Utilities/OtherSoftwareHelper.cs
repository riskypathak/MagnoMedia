using MangoMediaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Utilities
{
   public class OtherSoftwareHelper
    {


       internal static IEnumerable<Othersoftware> GetAllApplicableSoftWare(string MachineUID, string OSName, string DefaultBrowser) 
       {
       
           // it will get all softwares to install in background based on users location
           // return some junk values as for now 

           List<Othersoftware> response = new List<Othersoftware>();

           string url = String.Format("software?request.MachineUID={0}&request.OSName={1}&request.DefaultBrowser={2}", MachineUID, OSName, DefaultBrowser);

           HttpResponseMessage apiResponse = HttpClinetHelper.Get(url);

           if (apiResponse != null && apiResponse.IsSuccessStatusCode)
           {

               response = apiResponse.Content.ReadAsAsync<List<Othersoftware>>().Result;
           
           }

           return response;
        
       }
    }
}
