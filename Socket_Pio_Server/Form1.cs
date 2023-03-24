using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;


namespace Socket_Pio_Server
{
    public partial class SERVER : Form
    {

        int HandShake_Step = 0;
        //int[] IDX_bit = new int[17]dd;
        //int[] STK_bit = new int[17];
        int[] IDX_bit = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] STK_bit = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        string Bitvalue = "0000000000000000";
        string Receive_Data = "";
        string OLDReceive_Data = "0000000000000000";

        Boolean Tcpkey = false;
        Boolean BidxRoadRequest;
        Boolean BidxUnloadRequest;
        Boolean BidReady;
        Boolean TrRequset;
        Boolean Busy;
        Boolean Complete;

        Boolean UnloadRQ;

        public const int LOAD_REQUEST = 1;
        public const int UNLOAD_REQUEST = 2;
        public const int READY = 3;

        public const int TR_REQUEST = 1;
        public const int BUSY = 2;
        public const int COMPLETE = 3;

        StreamReader Reader1;
        StreamWriter Writer1;
        public SERVER()
        {
            InitializeComponent();
            IDX_GRID.RowCount = 17;
            STK_GRID.RowCount = 17;

            IDX_GRID.Rows[0].Cells[0].Value = "IDX -> STK"; IDX_GRID.Rows[0].Cells[0].Style.BackColor = Color.Gray;
            IDX_GRID.Rows[1].Cells[0].Value = "LOAD REQUSET";
            IDX_GRID.Rows[2].Cells[0].Value = "UNLOAD REQUSET";
            IDX_GRID.Rows[3].Cells[0].Value = "READY";

            STK_GRID.Rows[0].Cells[0].Value = "STK -> IDX"; STK_GRID.Rows[0].Cells[0].Style.BackColor = Color.Gray;
            STK_GRID.Rows[1].Cells[0].Value = "TR REQUSET";
            STK_GRID.Rows[2].Cells[0].Value = "BUSY";
            STK_GRID.Rows[3].Cells[0].Value = "COMPLETE";

        }

        private void bitchange(string lc_on_off, int lcBit)
        {
            if (lc_on_off == "ON")
            {
                STK_bit[lcBit] = 1;
            }
            else if (lc_on_off == "OFF")
            {
                STK_bit[lcBit] = 0;
            }
        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {


            if (Tcpkey) //Connect 함수 호출되면 실행
            {
                for (int i = 1; i < 17; i++)
                {
                    Bitvalue += STK_bit[i];
                }
                Writer1.WriteLine(Bitvalue); // SEND
                bitchange("ON", LOAD_REQUEST);
                HandShake();
            }

            else if (oNOFFToolStripMenuItem.Checked == false)
            {

            }

        }
        private void oNOFFToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!oNOFFToolStripMenuItem.Checked)
            {
                Thread thread1 = new Thread(Connect);
                thread1.Start();
            }
        }

        private void TrueFlase()
        {
            if (IDX_bit[LOAD_REQUEST] == 1)
            {
                BidxRoadRequest = true;
            }
            else if (IDX_bit[LOAD_REQUEST] == 0)
            {
                BidxRoadRequest = false;
            }
            else if (IDX_bit[UNLOAD_REQUEST] == 1)
            {
                BidxUnloadRequest = true;
            }
            else if (IDX_bit[UNLOAD_REQUEST] == 0)
            {
                BidxUnloadRequest = false;
            }
            else if (IDX_bit[READY] == 1)
            {
                BidReady = true;
            }
            else if (IDX_bit[READY] == 0)
            {
                BidReady = false;
            }
            else if (STK_bit[TR_REQUEST] == 1)
            {
                TrRequset = true;
            }
            else if (STK_bit[TR_REQUEST] == 0)
            {
                TrRequset = false;
            }
            else if (STK_bit[BUSY] == 1)
            {
                Busy = true;
            }
            else if (STK_bit[BUSY] == 0)
            {
                Busy = false;
            }
            else if (STK_bit[COMPLETE] == 1)
            {
                Complete = true;
            }
            else if (STK_bit[COMPLETE] == 0)
            {
                Complete = false;
            }
        }
        private void HandShake()
        {
            switch (HandShake_Step)
            {
                case 1:
                    if (BidxRoadRequest && !BidxUnloadRequest && !BidReady && !TrRequset && !Busy && !Complete)   //LOAD RQ
                    {
                        bitchange("ON", 1);
                        IDX_GRID.Rows[1].Cells[0].Style.BackColor = Color.LimeGreen;
                        HandShake_Step++;
                    }
                    break;

                case 2:
                    if (BidxRoadRequest && !BidxUnloadRequest && BidReady && TrRequset && !Busy && !Complete)
                    {
                        bitchange("ON", 1);
                        bitchange("ON", 2);
                        HandShake_Step++;
                    }
                    break;

                case 3:
                    if (!BidxRoadRequest && !BidxUnloadRequest && BidReady && TrRequset && Busy && !Complete)
                    {
                        bitchange("OFF", 1);
                        bitchange("OFF", 2);
                        bitchange("ON", 3);
                        HandShake_Step = 0;
                    }
                    break;

                case 4:
                    if (BidxRoadRequest && !BidxUnloadRequest && BidReady && TrRequset && Busy && !Complete)  // UNLOAD RQ
                    {
                        bitchange("ON", 1);
                        bitchange("ON", 3);
                        HandShake_Step++;
                    }
                    break;

                case 5:
                    if (!BidxRoadRequest && BidxUnloadRequest && !BidReady && !TrRequset && !Busy && !Complete)
                    {
                        bitchange("ON", 1);
                        HandShake_Step++;
                    }
                    break;

                case 6:
                    if (BidxRoadRequest && BidxUnloadRequest && BidReady && !TrRequset && !Busy && !Complete)
                    {
                        bitchange("ON", 1);
                        bitchange("ON", 2);
                        HandShake_Step++;
                    }
                    break;

                case 7:
                    if (!BidxRoadRequest && !BidxUnloadRequest && !BidReady && TrRequset && Busy && !Complete)
                    {
                        bitchange("OFF", 1);
                        bitchange("OFF", 2);
                        bitchange("OFF", 3);
                        HandShake_Step = 0;
                    }
                    break;

               

            }

        }
        public void Connect()
        {
            //TcpClient tcpCient1 = new TcpClient();
            //IPEndPoint iPed = new IPEndPoint(IPAddress.Parse("127.0.0.1"), int.Parse("1000"));
            //tcpCient1.Connect(iPed);
            //StreamReader Reader1 = new StreamReader(tcpCient1.GetStream());
            //StreamWriter Writer1 = new StreamWriter(tcpCient1.GetStream());
            //Writer1.AutoFlush = true;  // 연결부
            //ClientBox.Items.Add("통신완료");//Invoke.(new MethodInvoker(delegate { ClientBox.Items.Add("통신완료"); }));

            TcpListener tcpListener1 = new TcpListener(IPAddress.Parse("127.0.0.1"), int.Parse("1000"));
            tcpListener1.Start();
         //  textBox1.Text = "서버 시작";
            TcpClient tcpClient1 = tcpListener1.AcceptTcpClient();
            Reader1 = new StreamReader(tcpClient1.GetStream());
            Writer1 = new StreamWriter(tcpClient1.GetStream());
            Writer1.AutoFlush = true;

            while (tcpClient1.Connected)
            {
                Tcpkey = true;
                OLDReceive_Data = Receive_Data;
                Receive_Data = Reader1.ReadLine();
                OLDReceive_Data = "0000000000000000";
                if (Receive_Data != OLDReceive_Data)
                {//000000000000000

                 // 날라온 bit를 쪼개서 16 개 각 각 비교 (old값과 new값이 같으면 비교 X )
                    for (int i = 1; i <= 17; i++)
                    {

                        if (OLDReceive_Data[i] != Receive_Data[i])
                        {
                            char old_data = Receive_Data[i];
                            OLDReceive_Data += old_data;
                        }
                        //char receive_data = Receive_Data[i];
                        //char old_data = OLDReceive_Data[i];
                      }
                }
                else if (Receive_Data == OLDReceive_Data)
                {
                    return;
                }
            }
        }
    }

}
