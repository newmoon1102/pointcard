using System.Runtime.InteropServices;

namespace _wp_point.Rest.Class
{
    class TcsPc100
    {
        [DllImport("C:\\TCSPC100\\TCSPC100.dll", CharSet = CharSet.Unicode)]
        public static extern int OpenPort(ref System.IntPtr port, short portno, int bps);
        [DllImport("C:\\TCSPC100\\TCSPC100.dll", CharSet = CharSet.Unicode)]
        public static extern int ClosePort(ref System.IntPtr port);
        [DllImport("C:\\TCSPC100\\TCSPC100.dll", CharSet = CharSet.Unicode)]
        public static extern int RmGetStatus(ref System.IntPtr port, ref short card, ref short rwstatus, ref short mvmode, ref short icsstatus, ref int datanum, ref int datamax, ref short errcode);
        [DllImport("C:\\TCSPC100\\TCSPC100.dll", CharSet = CharSet.Unicode)]
        public static extern int RmGetCardData(ref System.IntPtr port, byte[] cardid, ref int ownpoint, ref int usecount, byte[] usedate, ref int salevalue, ref int addpoint, byte[] limit,
        ref short expired, ref short namedata, byte[] birthday, ref short memlist, ref short ptype);
        [DllImport("C:\\TCSPC100\\TCSPC100.dll", CharSet = CharSet.Unicode)]
        public static extern int RmGetName(ref System.IntPtr port, byte[] namedata2);
        [DllImport("C:\\TCSPC100\\TCSPC100.dll", CharSet = CharSet.Unicode)]
        public static extern int RmSendName(ref System.IntPtr port, byte[] namedata3);
        [DllImport("C:\\TCSPC100\\TCSPC100.dll", CharSet = CharSet.Unicode)]
        public static extern int RmGetICM(ref System.IntPtr port, byte[] filename);
        [DllImport("C:\\TCSPC100\\TCSPC100.dll", CharSet = CharSet.Unicode)]
        public static extern int RmGetTrade(ref System.IntPtr port, byte[] filename);


        public static System.IntPtr port_hndl;
        public static int ret { get; set; }

        public static short card = 0;
        public static short rwstatus = 0;
        public static short mvmode = 0;
        public static short icsstatus = 0;
        public static int datanum = 0;
        public static int datamax = 0;
        public static short errcode = 0;

        public static byte[] cardid = new byte[255];
        public static int ownpoint = 0;
        public static int usecount = 0;
        public static byte[] usedate = new byte[255];
        public static int salevalue = 0;
        public static int addpoint = 0;
        public static byte[] limit = new byte[255];
        public static short expired = 0;
        public static short namedata = 0;
        public static byte[] birthday = new byte[255];
        public static short memlist = 0;
        public static short ptype = 0;

        public static byte[] namedata2 = new byte[255];
        public static byte[] namedata3 = new byte[255];
        public static byte[] filename = new byte[255];
    }
}
