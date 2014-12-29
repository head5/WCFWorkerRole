using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
   public class NodeDetails
    {
        private version versionInfo;
        private List<Drive> driveInfo;       
        private Computerinfo computerInfo;       
        private ProcessRunning processConsumeMemory;

        public NodeDetails()
        {
            versionInfo = new version();
            driveInfo = new List<Drive>();
            computerInfo = new Computerinfo();
            processConsumeMemory = new ProcessRunning();
        }
        public version VersionInfo
        {
            get { return versionInfo; }
            set { versionInfo = value; }
        }
        public List<Drive> DriveInfo
        {
            get { return driveInfo; }
            set { driveInfo = value; }
        }
        public Computerinfo ComputerInfo
        {
            get { return computerInfo; }
            set { computerInfo = value; }
        }
        public ProcessRunning ProcessConsumeMemory
        {
            get { return processConsumeMemory; }
            set { processConsumeMemory = value; }
        }
    }

    public class version
    {
        private string version_String;
        private int major_Version;
        private int minor_Version;
        private int majorRevison_Version;
        private int minorRevison_Version;
        private int build_Version;



        public int Major_Version
        {
            get { return major_Version; }
            set { major_Version = value; }
        }
        public int Minor_Version
        {
            get { return minor_Version; }
            set { minor_Version = value; }
        }
        public int MajorRevison_Version
        {
            get { return majorRevison_Version; }
            set { majorRevison_Version = value; }
        }
        public int MinorRevison_Version
        {
            get { return minorRevison_Version; }
            set { minorRevison_Version = value; }
        }
        public int Build_Version
        {
            get { return build_Version; }
            set { build_Version = value; }
        }
        public string Version_String
        {
            get { return version_String; }
            set { version_String = value; }
        }


    }
    public class Drive
    {
        private string driveName;
        private string totalSpace;
        private string freeSpace;
        private string usedSpace;

        public string DriveName
        {
            get { return driveName; }
            set { driveName = value; }
        }
        public string TotalSpace
        {
            get { return totalSpace; }
            set { totalSpace = value; }
        }
        public string FreeSpace
        {
            get { return freeSpace; }
            set { freeSpace = value; }
        }
        public string UsedSpace
        {
            get { return usedSpace; }
            set { usedSpace = value; }
        }
    }
    public class Computerinfo
    {
        private string computerName;
        private string domainName;
        private string userName;
        private double ram_Usable;
        private string ipAddress;

        public string ComputerName
        {
            get { return computerName; }
            set { computerName = value; }
        }
        public string DomainName
        {
            get { return domainName; }
            set { domainName = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public double Ram_Usable
        {
            get { return ram_Usable; }
            set { ram_Usable = value; }
        }
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

    }
    public class ProcessRunning
    {
        private string process_Name;
        private int process_ID;

        public string Process_Name
        {
            get { return process_Name; }
            set { process_Name = value; }
        }
        public int Process_ID
        {
            get { return process_ID; }
            set { process_ID = value; }
        }

    }
}
