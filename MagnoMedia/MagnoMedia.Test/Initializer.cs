using MagnoMedia.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Compression;

namespace MagnoMedia.Test
{
    [TestClass]
    public class Initializer
    {
        [TestMethod]
        public void Database()
        {
            IDbConnectionFactory dbFactory =
                new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            using (IDbConnection db = dbFactory.Open())
            {
                db.DropAndCreateTable<Browser>();
                db.DropAndCreateTable<Country>();
                db.DropAndCreateTable<MagnoMedia.Data.Models.OperatingSystem>();
                db.DropAndCreateTable<Referer>();
                db.DropAndCreateTable<ThirdPartyApplication>();
                db.DropAndCreateTable<Revenue>();

                db.DropAndCreateTable<SessionDetail>();
                db.DropAndCreateTable<User>();

                db.DropAndCreateTable<AppBrowserValidity>();
                db.DropAndCreateTable<AppCountryValidity>();
                db.DropAndCreateTable<AppOSValidity>();
                db.DropAndCreateTable<AppReferValidity>();
                db.DropAndCreateTable<UserAppTrack>();
                db.DropAndCreateTable<UserTrack>();

                db.DropAndCreateTable<SessionUserLog>();
               
                //Insert Country
                db.ExecuteSql("INSERT INTO country(Id, Iso, Country_name) VALUES (1,'AD','Andorra'),(2,'AE','United Arab Emirates'),(3,'AF','Afghanistan'),(4,'AG','Antigua and Barbuda'),(5,'AI','Anguilla'),(6,'AL','Albania'),(7,'AM','Armenia'),(8,'AO','Angola'),(9,'AQ','Antarctica'),(10,'AR','Argentina'),(11,'AS','American Samoa'),(12,'AT','Austria'),(13,'AU','Australia'),(14,'AW','Aruba'),(15,'AZ','Azerbaijan'),(16,'BA','Bosnia and Herzegovina'),(17,'BB','Barbados'),(18,'BD','Bangladesh'),(19,'BE','Belgium'),(20,'BF','Burkina Faso'),(21,'BG','Bulgaria'),(22,'BH','Bahrain'),(23,'BI','Burundi'),(24,'BJ','Benin'),(25,'BL','Saint Barthélemy'),(26,'BM','Bermuda'),(27,'BN','Brunei'),(28,'BO','Bolivia'),(29,'BR','Brazil'),(30,'BS','Bahamas'),(31,'BT','Bhutan'),(32,'BV','Bouvet Island'),(33,'BW','Botswana'),(34,'BY','Belarus'),(35,'BZ','Belize'),(36,'CA','Canada'),(37,'CC','Cocos [Keeling] Islands'),(38,'CD','Democratic Republic of the Congo'),(39,'CF','Central African Republic'),(40,'CG','Republic of the Congo'),(41,'CH','Switzerland'),(42,'CI','Ivory Coast'),(43,'CK','Cook Islands'),(44,'CL','Chile'),(45,'CM','Cameroon'),(46,'CN','China'),(47,'CO','Colombia'),(48,'CR','Costa Rica'),(49,'CU','Cuba'),(50,'CV','Cape Verde'),(51,'CW','Curacao'),(52,'CX','Christmas Island'),(53,'CY','Cyprus'),(54,'CZ','Czech Republic'),(55,'DE','Germany'),(56,'DJ','Djibouti'),(57,'DK','Denmark'),(58,'DM','Dominica'),(59,'DO','Dominican Republic'),(60,'DZ','Algeria'),(61,'EC','Ecuador'),(62,'EE','Estonia'),(63,'EG','Egypt'),(64,'EH','Western Sahara'),(65,'ER','Eritrea'),(66,'ES','Spain'),(67,'ET','Ethiopia'),(68,'FI','Finland'),(69,'FJ','Fiji'),(70,'FK','Falkland Islands'),(71,'FM','Micronesia'),(72,'FO','Faroe Islands'),(73,'FR','France'),(74,'GA','Gabon'),(75,'GB','United Kingdom'),(76,'GD','Grenada'),(77,'GE','Georgia'),(78,'GF','French Guiana'),(79,'GG','Guernsey'),(80,'GH','Ghana'),(81,'GI','Gibraltar'),(82,'GL','Greenland'),(83,'GM','Gambia'),(84,'GN','Guinea'),(85,'GP','Guadeloupe'),(86,'GQ','Equatorial Guinea'),(87,'GR','Greece'),(88,'GS','South Georgia and the South Sandwich Islands'),(89,'GT','Guatemala'),(90,'GU','Guam'),(91,'GW','Guinea-Bissau'),(92,'GY','Guyana'),(93,'HK','Hong Kong'),(94,'HM','Heard Island and McDonald Islands'),(95,'HN','Honduras'),(96,'HR','Croatia'),(97,'HT','Haiti'),(98,'HU','Hungary'),(99,'ID','Indonesia'),(100,'IE','Ireland'),(101,'IL','Israel'),(102,'IM','Isle of Man'),(103,'IN','India'),(104,'IO','British Indian Ocean Territory'),(105,'IQ','Iraq'),(106,'IR','Iran'),(107,'IS','Iceland'),(108,'IT','Italy'),(109,'JE','Jersey'),(110,'JM','Jamaica'),(111,'JO','Jordan'),(112,'JP','Japan'),(113,'KE','Kenya'),(114,'KG','Kyrgyzstan'),(115,'KH','Cambodia'),(116,'KI','Kiribati'),(117,'KM','Comoros'),(118,'KN','Saint Kitts and Nevis'),(119,'KP','North Korea'),(120,'KR','South Korea'),(121,'KW','Kuwait'),(122,'KY','Cayman Islands'),(123,'KZ','Kazakhstan'),(124,'LA','Laos'),(125,'LB','Lebanon'),(126,'LC','Saint Lucia'),(127,'LI','Liechtenstein'),(128,'LK','Sri Lanka'),(129,'LR','Liberia'),(130,'LS','Lesotho'),(131,'LT','Lithuania'),(132,'LU','Luxembourg'),(133,'LV','Latvia'),(134,'LY','Libya'),(135,'MA','Morocco'),(136,'MC','Monaco'),(137,'MD','Moldova'),(138,'ME','Montenegro'),(139,'MF','Saint Martin'),(140,'MG','Madagascar'),(141,'MH','Marshall Islands'),(142,'MK','Macedonia'),(143,'ML','Mali'),(144,'MM','Myanmar [Burma]'),(145,'MN','Mongolia'),(146,'MO','Macao'),(147,'MP','Northern Mariana Islands'),(148,'MQ','Martinique'),(149,'MR','Mauritania'),(150,'MS','Montserrat'),(151,'MT','Malta'),(152,'MU','Mauritius'),(153,'MV','Maldives'),(154,'MW','Malawi'),(155,'MX','Mexico'),(156,'MY','Malaysia'),(157,'MZ','Mozambique'),(158,'NA','Namibia'),(159,'NC','New Caledonia'),(160,'NE','Niger'),(161,'NF','Norfolk Island'),(162,'NG','Nigeria'),(163,'NI','Nicaragua'),(164,'NL','Netherlands'),(165,'NO','Norway'),(166,'NP','Nepal'),(167,'NR','Nauru'),(168,'NU','Niue'),(169,'NZ','New Zealand'),(170,'OM','Oman'),(171,'PA','Panama'),(172,'PE','Peru'),(173,'PF','French Polynesia'),(174,'PG','Papua New Guinea'),(175,'PH','Philippines'),(176,'PK','Pakistan'),(177,'PL','Poland'),(178,'PM','Saint Pierre and Miquelon'),(179,'PN','Pitcairn Islands'),(180,'PR','Puerto Rico'),(181,'PS','Palestine'),(182,'PT','Portugal'),(183,'PW','Palau'),(184,'PY','Paraguay'),(185,'QA','Qatar'),(186,'RE','Réunion'),(187,'RO','Romania'),(188,'RS','Serbia'),(189,'RU','Russia'),(190,'RW','Rwanda'),(191,'SA','Saudi Arabia'),(192,'SB','Solomon Islands'),(193,'SC','Seychelles'),(194,'SD','Sudan'),(195,'SE','Sweden'),(196,'SG','Singapore'),(197,'SH','Saint Helena'),(198,'SI','Slovenia'),(199,'SJ','Svalbard and Jan Mayen'),(200,'SK','Slovakia'),(201,'SL','Sierra Leone'),(202,'SM','San Marino'),(203,'SN','Senegal'),(204,'SO','Somalia'),(205,'SR','Suriname'),(206,'SS','South Sudan'),(207,'ST','São Tomé and Príncipe'),(208,'SV','El Salvador'),(209,'SX','Sint Maarten'),(210,'SY','Syria'),(211,'SZ','Swaziland'),(212,'TC','Turks and Caicos Islands'),(213,'TD','Chad'),(214,'TF','French Southern Territories'),(215,'TG','Togo'),(216,'TH','Thailand'),(217,'TJ','Tajikistan'),(218,'TK','Tokelau'),(219,'TL','East Timor'),(220,'TM','Turkmenistan'),(221,'TN','Tunisia'),(222,'TO','Tonga'),(223,'TR','Turkey'),(224,'TT','Trinidad and Tobago'),(225,'TV','Tuvalu'),(226,'TW','Taiwan'),(227,'TZ','Tanzania'),(228,'UA','Ukraine'),(229,'UG','Uganda'),(230,'UM','U.S. Minor Outlying Islands'),(231,'US','United States'),(232,'UY','Uruguay'),(233,'UZ','Uzbekistan'),(234,'VA','Vatican City'),(235,'VC','Saint Vincent and the Grenadines'),(236,'VE','Venezuela'),(237,'VG','British Virgin Islands'),(238,'VI','U.S. Virgin Islands'),(239,'VN','Vietnam'),(240,'VU','Vanuatu'),(241,'WF','Wallis and Futuna'),(242,'WS','Samoa'),(243,'XK','Kosovo'),(244,'YE','Yemen'),(245,'YT','Mayotte'),(246,'ZA','South Africa'),(247,'ZM','Zambia'),(248,'ZW','Zimbabwe');");

                //Insert OS
                db.InsertAll<MagnoMedia.Data.Models.OperatingSystem>(GetOSList());

                //Insert Browser
                db.InsertAll<Browser>(GetBrowserList());

                //Insert 3rd party Softwares
                db.InsertAll<ThirdPartyApplication>(GetSoftwareList());

                //Install Referer
                db.InsertAll<Referer>(GetAllReferers());

                //Install validity
                db.InsertAll<AppBrowserValidity>(GetBrowserValidity(db));

                db.InsertAll<AppOSValidity>(GetOSValidity(db));

                db.InsertAll<AppCountryValidity>(GetCountryValidity(db));

                db.InsertAll<AppReferValidity>(GetReferValidity(db));

                
            }
        }

        [TestMethod]
        public void CopySetupBinaries()
        {
            ZipAndCopyChildBinaries();
            CopyInstallerParentCode("../../../MagnoMedia.Web/App_Data/Code");
            CopyInstallerParentCode("../../../MagnoMedia.Web/bin/Debug/App_Data/Code");
            CopyInstallerParentCode("../../../MagnoMedia.Web/bin/Release/App_Data/Code");
        }

        private void ZipAndCopyChildBinaries()
        {
            string setupDirectoryPath = Path.GetFullPath("../../../Wizard/bin/Release");

            string destinationPath = Path.GetFullPath("../../../MagnoMedia.Web/App_Data/Application/vidsoom.zip");

            if(File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }

            ZipFile.CreateFromDirectory(setupDirectoryPath, destinationPath);
        }

        private static void CopyInstallerParentCode(string destinationPath)
        {
            string setupDirectoryPath = Path.GetFullPath("../../../MagnoMedia.Windows.Installer");

            foreach (string dirPath in Directory.GetDirectories(setupDirectoryPath, "*",
    SearchOption.AllDirectories))
            {
                if (!dirPath.EndsWith("obj") && !dirPath.EndsWith("bin"))
                {
                    Directory.CreateDirectory(dirPath.Replace(setupDirectoryPath, destinationPath));
                }
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(setupDirectoryPath, "*.*",
                SearchOption.AllDirectories))
            {
                if (!newPath.Contains("\\obj\\") && !newPath.Contains("\\bin\\"))
                {
                    File.Copy(newPath, newPath.Replace(setupDirectoryPath, destinationPath), true);
                }
            }
        }

        private IEnumerable<AppBrowserValidity> GetBrowserValidity(IDbConnection db)
        {
            List<AppBrowserValidity> list = new List<AppBrowserValidity>();


            foreach (var app in db.Select<ThirdPartyApplication>())
            {
                foreach (var browser in db.Select<Browser>())
                {
                    list.Add(new AppBrowserValidity() { ApplicationId = app.Id, BrowserId = browser.Id });
                }
            }


            return list;
        }

        private IEnumerable<AppOSValidity> GetOSValidity(IDbConnection db)
        {
            List<AppOSValidity> list = new List<AppOSValidity>();

            foreach (var app in db.Select<ThirdPartyApplication>())
            {
                foreach (var os in db.Select<MagnoMedia.Data.Models.OperatingSystem>())
                {
                    list.Add(new AppOSValidity() { ApplicationId = app.Id, OSId = os.Id });
                }
            }

            return list;
        }

        private IEnumerable<AppReferValidity> GetReferValidity(IDbConnection db)
        {
            List<AppReferValidity> list = new List<AppReferValidity>();

            foreach (var app in db.Select<ThirdPartyApplication>())
            {
                foreach (var refer in db.Select<MagnoMedia.Data.Models.Referer>())
                {
                    list.Add(new AppReferValidity() { ApplicationId = app.Id, ReferId = refer.Id });
                }
            }

            return list;
        }

        private IEnumerable<AppCountryValidity> GetCountryValidity(IDbConnection db)
        {
            List<AppCountryValidity> list = new List<AppCountryValidity>();

            foreach (var app in db.Select<ThirdPartyApplication>())
            {
                for (int i = 1; i <= 248; i++)
                {
                    list.Add(new AppCountryValidity() { ApplicationId = app.Id, CountryId = i });
                }
            }


            return list;
        }

        private IEnumerable<Referer> GetAllReferers()
        {
            return new List<Referer>()
            {

                new Referer(){RefererCode="ADBOO", Name = "Ad Booth"},
                new Referer(){RefererCode="NOREFER", Name = "No Referer"}
            };
        }

        private IEnumerable<Browser> GetBrowserList()
        {
            return new List<Browser>()
            {
                new Browser()
                {
                    BrowserName = "IE"
                },

                new Browser()
                {
                    BrowserName = "Chrome"
                },

                new Browser()
                {
                    BrowserName = "Firefox"
                },

                new Browser()
                {
                    BrowserName = "Safari"
                }
            };
        }

        private IEnumerable<Data.Models.OperatingSystem> GetOSList()
        {
            return new List<Data.Models.OperatingSystem>()
            {
                new Data.Models.OperatingSystem()
                {
                    OSName = "Windows XP"
                },
                new Data.Models.OperatingSystem()
                {
                    OSName = "Windows Vista"
                },
                new Data.Models.OperatingSystem()
                {
                    OSName = "Windows 7"
                },
                new Data.Models.OperatingSystem()
                {
                    OSName = "Windows 8"
                }
            };
        }

        private static IEnumerable<ThirdPartyApplication> GetSoftwareList()
        {
            return new List<ThirdPartyApplication>()
            {

                new ThirdPartyApplication
                      { 
                          DownloadUrl ="http://aff-software.s3-website-us-east-1.amazonaws.com/2ab4a67f644e190dfd416f2fb7f36c06/Cloud_Backup_Setup.exe",
                          HasUrl = true,
                          Name= "Cloud_Backup",
                          Url = "www.Notepadplus.com/about",
                          InstallerName = "Cloud_Backup_Setup.exe",
                          OtherNames = "MyPC Backup"
                      },

                     new ThirdPartyApplication
                      { 
                          DownloadUrl ="http://188.42.227.39/vidsoom/unicobrowser.exe",
                          HasUrl = true,
                          Name= "unicobrowser",
                          Url = "www.unicobrowser.com/about",
                          InstallerName = "unicobrowser.exe",
                          RegistryCheck = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Clara\AppInstanceUid"+
                          @"|HKEY_LOCAL_MACHINE\SOFTWARE\Clara\AppInstanceUid"+
                          @"|HKEY_CURRENT_USER\Software\BoBrowser\AppInstanceUid"+
                          @"|HKEY_CURRENT_USER\Software\1stBrowser\AppInstanceUid"+
                          @"|HKEY_CURRENT_USER\Software\Beamrise\AppInstanceUid"+
                          @"|HKEY_CURRENT_USER\Software\Appiance\AppInstanceUid"+
                          @"|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\I - Cinema\DisplayName"+
                          @"|HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\I – Cinema\DisplayName",
                          Arguments = "/bagkey=Abxesy4lLJ8U /configid=260"
                      }
                };
        }
    }
}
