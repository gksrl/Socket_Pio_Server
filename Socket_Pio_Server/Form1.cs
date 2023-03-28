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

        int HandShake_Step = 1;
        //int[] IDX_bit = new int[17]dd;
        //int[] STK_bit = new int[17];
        int[] IDX_bit = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] STK_bit = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        string Bitvalue = "00000000000000000";
        string Receive_Data = "0000000000000000";
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
            IDX_GRID.Rows[3].Cells[0].Value = "PORT READY";
            IDX_GRID.Rows[4].Cells[0].Value = "PORT ABNORMAL";
            IDX_GRID.Rows[5].Cells[0].Value = "NOT USED PORT";
            IDX_GRID.Rows[6].Cells[0].Value = "CST CONTAIN";
            IDX_GRID.Rows[7].Cells[0].Value = "CHUCK STATUS";
            IDX_GRID.Rows[8].Cells[0].Value = "EQP ABNORMAL";
            IDX_GRID.Rows[9].Cells[0].Value = "INTERLOCK";
            IDX_GRID.Rows[10].Cells[0].Value = "RESERVED";
            IDX_GRID.Rows[11].Cells[0].Value = "DOOR OPEN";
            IDX_GRID.Rows[12].Cells[0].Value = "BUFFER MODE";
            IDX_GRID.Rows[13].Cells[0].Value = "RESERVED";
            IDX_GRID.Rows[14].Cells[0].Value = "AUTO TEACHING";
            IDX_GRID.Rows[15].Cells[0].Value = "RESERVE REQUEST";
            IDX_GRID.Rows[16].Cells[0].Value = "MOVE RESERVED";

            STK_GRID.Rows[0].Cells[0].Value = "STK -> IDX"; STK_GRID.Rows[0].Cells[0].Style.BackColor = Color.Gray;
            STK_GRID.Rows[1].Cells[0].Value = "TR REQUSET";
            STK_GRID.Rows[2].Cells[0].Value = "STK BUSY";
            STK_GRID.Rows[3].Cells[0].Value = "STK COMPLETE";
            STK_GRID.Rows[4].Cells[0].Value = "STK ABNORMAL";
            STK_GRID.Rows[5].Cells[0].Value = "NOT USED STK";
            STK_GRID.Rows[6].Cells[0].Value = "STK CST CONTAIN";
            STK_GRID.Rows[7].Cells[0].Value = "CARRIER ID";
            STK_GRID.Rows[8].Cells[0].Value = "RACK MASTER1";
            STK_GRID.Rows[9].Cells[0].Value = "RACK MASTER2";
            STK_GRID.Rows[10].Cells[0].Value = "RACK MASTER3";
            STK_GRID.Rows[11].Cells[0].Value = "RESERVED REQUSET";
            STK_GRID.Rows[12].Cells[0].Value = "MOVE RESERVED";
            STK_GRID.Rows[13].Cells[0].Value = "RESERVED";
            STK_GRID.Rows[14].Cells[0].Value = "RESERVED";
            STK_GRID.Rows[15].Cells[0].Value = "RESERVED";
            STK_GRID.Rows[16].Cells[0].Value = "RESERVED";

        }

        private void CleanRows()
        {
            IDX_GRID.Rows[0].Cells[0].Style.BackColor = Color.White;
            IDX_GRID.Rows[1].Cells[0].Style.BackColor = Color.White;
            IDX_GRID.Rows[2].Cells[0].Style.BackColor = Color.White;
            IDX_GRID.Rows[3].Cells[0].Style.BackColor = Color.White;

            STK_GRID.Rows[0].Cells[0].Style.BackColor = Color.White;
            STK_GRID.Rows[1].Cells[0].Style.BackColor = Color.White;
            STK_GRID.Rows[2].Cells[0].Style.BackColor = Color.White;
            STK_GRID.Rows[3].Cells[0].Style.BackColor = Color.White;
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
                //for (int i = 1; i < 17; i++)
                //{
                //    Bitvalue += STK_bit[i];
                //}
                TrueFlase();
                HandShake();
                Bitvalue = string.Join("", STK_bit);
                //string t = Bitvalue.Substring(Bitvalue.Length - 16);
                Writer1.WriteLine(Bitvalue); // SEND
                //bitchange("ON", LOAD_REQUEST);
            }

            else if (oNOFFToolStripMenuItem.Checked == false)
            {

            }

        }
        private void oNOFFToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!oNOFFToolStripMenuItem.Checked)
            {
                textBox1.Text = "클라이언트 접속";
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
            else
            {
                BidxRoadRequest = false;
            }
            if (IDX_bit[UNLOAD_REQUEST] == 1)
            {
                BidxUnloadRequest = true;
            }
            else
            {
                BidxUnloadRequest = false;
            }
            if (IDX_bit[READY] == 1)
            {
                BidReady = true;
            }
            else
            {
                BidReady = false;
            }
            if (STK_bit[TR_REQUEST] == 1)
            {
                TrRequset = true;
            }
            else
            {
                TrRequset = false;
            }
            if (STK_bit[BUSY] == 1)
            {
                Busy = true;
            }
            else
            {
                Busy = false;
            }
            if (STK_bit[COMPLETE] == 1)
            {
                Complete = true;
            }
            else
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
                        CleanRows();
                        STK_GRID.Rows[1].Cells[0].Style.BackColor = Color.Aqua;
                        IDX_GRID.Rows[1].Cells[0].Style.BackColor = Color.Yellow;
                        bitchange("ON", 1);
                    }
                    HandShake_Step++;

                    break;

                case 2:
                    if (BidxRoadRequest && !BidxUnloadRequest && BidReady && TrRequset && !Busy && !Complete)
                    {
                        CleanRows();
                        STK_GRID.Rows[1].Cells[0].Style.BackColor = Color.Aqua;
                        STK_GRID.Rows[2].Cells[0].Style.BackColor = Color.Aqua;
                        IDX_GRID.Rows[1].Cells[0].Style.BackColor = Color.Yellow;
                        IDX_GRID.Rows[3].Cells[0].Style.BackColor = Color.Yellow;
                        bitchange("ON", 1);
                        bitchange("ON", 2);
                    }
                    HandShake_Step++;
                    break;

                case 3:
                    if (BidxRoadRequest && !BidxUnloadRequest && BidReady && TrRequset && Busy && !Complete)
                    {
                        CleanRows();
                        STK_GRID.Rows[3].Cells[0].Style.BackColor = Color.Aqua;
                        bitchange("OFF", 1);
                        bitchange("OFF", 2);
                        bitchange("ON", 3);
                    }
                    HandShake_Step++;
                    break;

                case 4:
                    if (!BidxRoadRequest && BidxUnloadRequest && !BidReady && !TrRequset && !Busy && !Complete)  // TR RQ
                    { // UNLOAD RQ
                        CleanRows();
                        STK_GRID.Rows[1].Cells[0].Style.BackColor = Color.Aqua;
                        IDX_GRID.Rows[2].Cells[0].Style.BackColor = Color.Yellow;
                        bitchange("ON", 1);
                    }
                    HandShake_Step++;
                    break;

                case 5:
                    if (!BidxRoadRequest && BidxUnloadRequest && BidReady && TrRequset && !Busy && !Complete) // STK BUSY
                    {
                        CleanRows();
                        STK_GRID.Rows[1].Cells[0].Style.BackColor = Color.Aqua;
                        STK_GRID.Rows[2].Cells[0].Style.BackColor = Color.Aqua;
                        IDX_GRID.Rows[2].Cells[0].Style.BackColor = Color.Yellow;
                        IDX_GRID.Rows[3].Cells[0].Style.BackColor = Color.Yellow;
                        bitchange("ON", 1);
                        bitchange("ON", 2);
                    }
                    HandShake_Step++;
                    break;

                case 6:
                    if (!BidxRoadRequest && BidxUnloadRequest && BidReady && TrRequset && Busy && !Complete) // STK COMPLETE
                    {
                        CleanRows();
                        STK_GRID.Rows[3].Cells[0].Style.BackColor = Color.Aqua;
                        bitchange("OFF", 1);
                        bitchange("OFF", 2);
                        bitchange("ON", 3);
                    }
                    HandShake_Step = 1;
                    break;

                //case 7:
                //    if (!BidxRoadRequest && !BidxUnloadRequest && !BidReady && TrRequset && Busy && !Complete)
                //    {
                //        CleanRows();
                //        STK_GRID.Rows[1].Cells[0].Style.BackColor = Color.Yellow;
                //        bitchange("OFF", 1);
                //        bitchange("OFF", 2);
                //        bitchange("OFF", 3);
                //    }
                //    HandShake_Step++;
                //    break;



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
            string tempstorage = "";

            while (tcpClient1.Connected)
            {
                Tcpkey = true;
                //OLDReceive_Data = Receive_Data;
                Receive_Data = Reader1.ReadLine();


                if (Receive_Data != OLDReceive_Data)
                {//000000000000000

                    // 날라온 bit를 쪼개서 16 쪼개서, 각 각 IDX Bit 전송 & Form Display
                    for (int i = 0; i <= 16; i++)
                    {

                        IDX_bit[i] = Convert.ToInt32(Receive_Data[i] + "");

                        //if (OLDReceive_Data[i] != Receive_Data[i])
                        //{
                        //    char old_data = Receive_Data[i];
                        //    IDX_bit[i] = old_data;
                        //}
                        //tempstorage += Convert.ToString(Convert.ToChar(IDX_bit[i]));
                    }
                    OLDReceive_Data = tempstorage;
                    tempstorage = "";
                    // IDX 비트에 새로운 비트 갱신
                }
                else if (Receive_Data == OLDReceive_Data)
                {

                }
            }
        }

        private void Btn_CSTContain_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            //string t3 = string.Join("",IDX_bit);
            //int intValue = int.Parse(t3);
            //textBox1.Text = intValue.ToString();

            for (int i = 0; i <= 15; i++)
            {


                textBox1.Text += Convert.ToString(Convert.ToChar(IDX_bit[i]));
            }

            //Bitvalue += textBox1.Text;

        }
    }

}
