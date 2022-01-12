using System;
using System.ComponentModel;

namespace HslStudio.ClassLibrary.Systems
{


    public class SettingHelper 
    {
        
        public string HostName { get; set; } = Environment.MachineName;


        public string UserName { get; set; }

        public string Password { get; set; }
 

        public string CommunicationTypes { get; set; }
 

        public string DatabaseTypes { get; set; }
 
        public string ServerName { get; set; }
 

        public string Database { get; set; }
 
       
    }
}
