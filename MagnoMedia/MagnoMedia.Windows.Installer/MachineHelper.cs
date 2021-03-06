﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using System.Management;
using Microsoft.Win32;
using System.Net;
using System.Globalization;
using System.Threading;

namespace MagnoMedia.Windows
{
    class MachineHelper
    {

        #region variables 
        private static string fingerPrint = string.Empty;
        private static string osName = string.Empty;
        private static string browserName = string.Empty;
        private static string ipAddress = string.Empty;
        private static string countryName = string.Empty;
        private const string registryPath = @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice";

        #endregion

        #region Code for machine finger print

        public static string UniqueIdentifierValue()
        {
            if (string.IsNullOrEmpty(fingerPrint))
            {
                fingerPrint = GetHash("CPU >> " + cpuId() + "\nBIOS >> " + 
			biosId() + "\nBASE >> " + baseId() +
                            //+"\nDISK >> "+ diskId() + "\nVIDEO >> " + 
			videoId() +"\nMAC >> "+ macId()
                                     );
            }
            return fingerPrint;
        }
        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }
        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }
        #region Original Device ID Getting Code
        //Return a hardware identifier
        private static string identifier
        (string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        if( mo[wmiProperty]!=null)
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }
        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }
        //BIOS Identifier
        private static string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
            + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + identifier("Win32_BIOS", "IdentificationCode")
            + identifier("Win32_BIOS", "SerialNumber")
            + identifier("Win32_BIOS", "ReleaseDate")
            + identifier("Win32_BIOS", "Version");
        }
        //Main physical hard drive ID
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model")
            + identifier("Win32_DiskDrive", "Manufacturer")
            + identifier("Win32_DiskDrive", "Signature")
            + identifier("Win32_DiskDrive", "TotalHeads");
        }
        //Motherboard ID
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
            + identifier("Win32_BaseBoard", "Manufacturer")
            + identifier("Win32_BaseBoard", "Name")
            + identifier("Win32_BaseBoard", "SerialNumber");
        }
        //Primary video controller ID
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
            + identifier("Win32_VideoController", "Name");
        }
        //First enabled network card ID
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration",
                "MACAddress", "IPEnabled");
        }
        #endregion

        #endregion


        public static string GetCountryName() {

            if (string.IsNullOrEmpty(countryName))
            {
                countryName = CountryName();
                                   
            }
            return countryName;
          
        
        
        }

        private static string CountryName()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            RegionInfo regionInfo = new RegionInfo(cultureInfo.LCID);
            // or 
            //regionInfo = new RegionInfo(cultureInfo.Name);
            //string englishName = regionInfo.EnglishName;
            //string currencySymbol = regionInfo.CurrencySymbol;
            //string currencyEnglishName = regionInfo.CurrencyEnglishName;
            //string currencyLocalName = regionInfo.CurrencyNativeName;
            if (String.IsNullOrEmpty(regionInfo.TwoLetterISORegionName))
                return "--";
            return regionInfo.TwoLetterISORegionName;
        }

        public static string GetIpAddress()
        {
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = IpAddress();

            }
            return ipAddress;
          
        }

        private static string IpAddress()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            // Get the IP
            string iP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return iP;
        }

        #region Code For getting OS name

        public static string GetOSName() {

            if (string.IsNullOrEmpty(osName))
            {
                osName = OSName();
                             
            }
            return osName;
        
        }

        private static string OSName() { 
         var name = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                      select x.GetPropertyValue("Caption")).FirstOrDefault();
                      return name != null ? name.ToString() : "Unknown";
        
        }

        #endregion

        #region Code For getting OS's default browesr name

        public static string GetDefaultBrowserName()
        {

            if (string.IsNullOrEmpty(browserName))
            {
                browserName = DefaultBrowserName();

            }
            return browserName;

        }

        private static string DefaultBrowserName()
        {
            object progIdValue  = null;
            using (RegistryKey userChoiceKey = Registry.CurrentUser.OpenSubKey(registryPath))
            {
                if (userChoiceKey != null)
                {
                    progIdValue = userChoiceKey.GetValue("Progid");

                }
            }
            if(progIdValue== null){
            return "UNKNOWN";
            
            }
            else{

                return progIdValue.ToString();
            }

        }

        #endregion


  
    }
}
