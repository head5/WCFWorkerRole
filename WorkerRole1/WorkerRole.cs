using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net.Sockets;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("WorkerRole1 is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("WorkerRole1 has been started");


            var internalEndPoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["port"];
            var internalmetadataEndPoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["mexport"];
            var inputEndpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["Input"];

            Uri baseAddress=new Uri(string.Format("http://{0}/WorkerRole1/Service1",inputEndpoint.IPEndpoint));

            ServiceHost host = new ServiceHost(typeof(NodeHealth),baseAddress);
            
            BasicHttpBinding binding = new BasicHttpBinding();

            binding.Security.Mode = BasicHttpSecurityMode.None;
            
            host.AddServiceEndpoint(typeof(INodeHealth), binding, string.Format("http://{0}/Service", internalEndPoint.IPEndpoint));

            host.Description.Behaviors.Remove<ServiceMetadataBehavior>();

                ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                behavior.HttpGetEnabled = true;
                behavior.HttpGetUrl = new Uri(string.Format("http://{0}/Service/metadata", internalmetadataEndPoint.IPEndpoint));
                host.Description.Behaviors.Add(behavior);            

            host.Open();
            // For information on handling configuration changes 
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357. 

            Trace.WriteLine("WCF Listening At: " + string.Format("http://{0}/Service", internalEndPoint.IPEndpoint));
            Trace.WriteLine("WCF MetaData Listening At: " + string.Format("http://{0}/Service/metadata", internalmetadataEndPoint.IPEndpoint));
            Trace.WriteLine("Service started at : " + string.Format("http://{0}/WorkerRole1/Service1", inputEndpoint.IPEndpoint));
            AddHostRecord();
            return base.OnStart(); 
                   
            
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole1 is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRole1 has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }

        public void AddHostRecord()
        {
            string connectiostring = CloudConfigurationManager.GetSetting("ConnectionString");
            string hosname = SystemInformation.ComputerName;
            string ipaddress = null;
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addr = host.AddressList;
            foreach (IPAddress ipaddr in addr)
            {
                if (ipaddr.AddressFamily == AddressFamily.InterNetwork)
                    ipaddress = ipaddr.ToString();
            }
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectiostring;
            
            string query = "INSERT INTO HostInfo (hostname, ipaddress) VALUES ('" +SystemInformation.ComputerName + "', '" + ipaddress+"')";

            SqlCommand myCommand = new SqlCommand(query, conn);
            conn.Open();
            myCommand.ExecuteNonQuery();

            //       myinfo.Text = "connection to db is made";
            conn.Close();


        }
    }
}
