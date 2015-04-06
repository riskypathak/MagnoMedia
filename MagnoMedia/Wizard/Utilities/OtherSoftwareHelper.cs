using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Model;

namespace Wizard.Utilities
{
   public class OtherSoftwareHelper
    {


       internal static IEnumerable<Othersoftware> GetAllApplicableSoftWare() {
       
           // it will get all softwares to install in background based on users location
           // return some junk values as for now 

           return new List<Othersoftware>(){
           new Othersoftware
                 { 
                     DownloadUrl ="http://download.skype.com/cc8c0832c80579731d528a2dabcb134c/SkypeSetup.exe",
                     HasUrl = true,
                     Name= "Robots",
                     Url = "www.Notepadplus.com/about",
                     InstallerName = "SkypeSetup.exe"
                 },
                 new Othersoftware
                 { 
                     DownloadUrl ="http://download.skype.com/cc8c0832c80579731d528a2dabcb134c/SkypeSetup.exe",
                     Name= "Robots2",
                     Url = "www.Notepadplus.com/about",
                     InstallerName = "SkypeSetup.exe"
                 }
          
           
           };
          
           
          

       
       }
    }
}
