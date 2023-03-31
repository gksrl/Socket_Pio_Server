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
        string Write_Data = "00000000000000000";
        string Receive_Data = "0000000000000000";
        string OLDReceive_Data = "0000000000000000";
        bool Stkerror = true;
        Boolean Tcpkey = false;

        Boolean TF_LoadRequest;
        Boolean TF_UnloadRequest;
        Boolean TF_Ready;
        Boolean TF_Portabnormalerror;
        Boolean TF_Notusedport;
        Boolean TF_IDX_CST_Contain;
        Boolean TF_Chuckstatus;
        Boolean TF_Interlock;
        Boolean TF_Eqpabnormalerror;
        Boolean TF_Dooropen;
        Boolean TF_IDX_Reserved_Req;
        Boolean TF_IDX_Move_Reserved;

        Boolean TF_TrRequest;
        Boolean TF_Busy;
        Boolean TF_Complete;
        Boolean TF_STK_abnormalerror;
        Boolean TF_Notusedstk;
        Boolean TF_STK_CST_Contain;
        Boolean TF_CarrierId;
        Boolean TF_Rackmaster1;
        Boolean TF_Rackmaster2;
        Boolean TF_Rackmaster3;
        Boolean TF_STK_Reserved_Req;
        Boolean TF_STK_Move_Reserved;


        public const int LOAD_REQUEST = 1;
        public const int UNLOAD_REQUEST = 2;
        public const int READY = 3;
        public const int PORT_ABNORMAL_ERROR = 4;
        public const int NOT_USED_PORT = 5;
        public const int IDX_CST_CONTAIN = 6;
        public const int CHUCK_STATUS = 7;
        public const int INTERLOCK = 8;
        public const int EQP_ABNORMAL_ERROR = 9;
        public const int IDX_RESERVED1 = 10;
        public const int DOOR_OPEN = 11;
        public const int IDX_RESERVED2 = 12;
        public const int IDX_RESERVED3 = 13;
        public const int IDX_RESERVED4 = 14;
        public const int IDX_RESERVED_REQ = 15;
        public const int IDX_MOVE_RESERVED = 16;


        public const int TR_REQUEST = 1;
        public const int BUSY = 2;
        public const int COMPLETE = 3;
        public const int STK_ABNORMAL_ERROR = 4;
        public const int NOT_USED_STK = 5;
        public const int STK_CST_CONTAIN = 6;
        public const int CARRIER_ID = 7;
        public const int RACK_MASTER1 = 8;
        public const int RACK_MASTER2 = 9;
        public const int RACK_MASTER3 = 10;
        public const int STK_RESERVED_REQ = 11;
        public const int STK_MOVE_RESERVED = 12;
        public const int RESERVED1 = 13;
        public const int RESERVED2 = 14;
        public const int RESERVED3 = 15;
        public const int RESERVED4 = 16;

        public const int START_BIT = 1;
        public const int END_BIT = 16;

        StreamReader Reader1;
        StreamWriter Writer1;
        public SERVER()
        {
            InitializeComponent();
            this.IDX_GRID.Font = new Font("Tahoma", 10, FontStyle.Bold);
            this.STK_GRID.Font = new Font("Tahoma", 10, FontStyle.Bold);
            IDX_GRID.RowCount = 17;
            STK_GRID.RowCount = 17;

            IDX_GRID.Rows[0].Cells[0].Value = "INDEX"; IDX_GRID.Rows[0].Cells[0].Style.BackColor = Color.Gray;
            IDX_GRID.Rows[1].Cells[0].Value = "LOAD REQUSET";
            IDX_GRID.Rows[2].Cells[0].Value = "UNLOAD REQUSET";
            IDX_GRID.Rows[3].Cells[0].Value = "PORT READY";
            IDX_GRID.Rows[4].Cells[0].Value = "PORT ABNORMAL/ERROR";
            IDX_GRID.Rows[5].Cells[0].Value = "NOT USED PORT";
            IDX_GRID.Rows[6].Cells[0].Value = "CASSETTE CONTAIN";
            IDX_GRID.Rows[7].Cells[0].Value = "CHUCK STATUS";
            IDX_GRID.Rows[8].Cells[0].Value = "INTERLOCK";
            IDX_GRID.Rows[9].Cells[0].Value = "EQP ABNORMAL/ERROR";
            IDX_GRID.Rows[10].Cells[0].Value = "RESERVED";
            IDX_GRID.Rows[11].Cells[0].Value = "DOOP OPEN";
            IDX_GRID.Rows[12].Cells[0].Value = "RESERVED";
            IDX_GRID.Rows[13].Cells[0].Value = "RESERVED";
            IDX_GRID.Rows[14].Cells[0].Value = "RESERVED";
            IDX_GRID.Rows[15].Cells[0].Value = "RESERVED REQ";
            IDX_GRID.Rows[16].Cells[0].Value = "MOVE RESERVED";

            STK_GRID.Rows[0].Cells[0].Value = "STK"; STK_GRID.Rows[0].Cells[0].Style.BackColor = Color.Gray;
            STK_GRID.Rows[1].Cells[0].Value = "TRANSFER REQUSET";
            STK_GRID.Rows[2].Cells[0].Value = "BUSY";
            STK_GRID.Rows[3].Cells[0].Value = "COMPLETE";
            STK_GRID.Rows[4].Cells[0].Value = "STK ABNORMAL/ERROR";
            STK_GRID.Rows[5].Cells[0].Value = "NOT USED STK";
            STK_GRID.Rows[6].Cells[0].Value = "CASSETTE CONTAIN";
            STK_GRID.Rows[7].Cells[0].Value = "CARRIER ID";
            STK_GRID.Rows[8].Cells[0].Value = "RACK MASTER1";
            STK_GRID.Rows[9].Cells[0].Value = "RACK MASETR2";
            STK_GRID.Rows[10].Cells[0].Value = "RACK MASTER3";
            STK_GRID.Rows[11].Cells[0].Value = "RESERVED REQ";
            STK_GRID.Rows[12].Cells[0].Value = "MOVE RESERVED";
            STK_GRID.Rows[13].Cells[0].Value = "RESERVED";
            STK_GRID.Rows[14].Cells[0].Value = "RESERVED";
            STK_GRID.Rows[15].Cells[0].Value = "RESERVED";
            STK_GRID.Rows[16].Cells[0].Value = "RESERVED";
            Thread thread1 = new Thread(Connect);
            thread1.Start();
            bitchange("ON", 4);
        }

        private void DisplayForm()

        {
            if (TF_LoadRequest)
            {
                IDX_GRID.Rows[1].Cells[0].Style.BackColor = Color.Yellow;
            }
            else
            {
                IDX_GRID.Rows[1].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_UnloadRequest)
            {
                IDX_GRID.Rows[2].Cells[0].Style.BackColor = Color.Yellow;
            }
            else
            {
                IDX_GRID.Rows[2].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Ready)
            {
                IDX_GRID.Rows[3].Cells[0].Style.BackColor = Color.Yellow;
            }
            else
            {
                IDX_GRID.Rows[3].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Portabnormalerror)
            {
                IDX_GRID.Rows[4].Cells[0].Style.BackColor = Color.Yellow;
            }
            else
            {
                IDX_GRID.Rows[4].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Notusedport)
            {
                IDX_GRID.Rows[5].Cells[0].Style.BackColor = Color.Yellow;
            }

            else
            {
                IDX_GRID.Rows[5].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_IDX_CST_Contain)
            {
                IDX_GRID.Rows[6].Cells[0].Style.BackColor = Color.Yellow;
            }

            else
            {
                IDX_GRID.Rows[6].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Chuckstatus)
            {
                IDX_GRID.Rows[7].Cells[0].Style.BackColor = Color.Yellow;
            }

            else
            {
                IDX_GRID.Rows[7].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Interlock)
            {
                IDX_GRID.Rows[8].Cells[0].Style.BackColor = Color.Yellow;
            }

            else
            {
                IDX_GRID.Rows[8].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Eqpabnormalerror)
            {
                IDX_GRID.Rows[9].Cells[0].Style.BackColor = Color.Yellow;
            }

            else
            {
                IDX_GRID.Rows[9].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Dooropen)
            {
                IDX_GRID.Rows[11].Cells[0].Style.BackColor = Color.Yellow;
            }
            else
            {
                IDX_GRID.Rows[11].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_IDX_Reserved_Req)
            {
                IDX_GRID.Rows[15].Cells[0].Style.BackColor = Color.Yellow;
            }
            else
            {
                IDX_GRID.Rows[15].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_IDX_Move_Reserved)
            {
                IDX_GRID.Rows[16].Cells[0].Style.BackColor = Color.Yellow;
            }
            else
            {
                IDX_GRID.Rows[16].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_TrRequest)
            {
                STK_GRID.Rows[1].Cells[0].Style.BackColor = Color.Aqua;
            }
            else
            {
                STK_GRID.Rows[1].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Busy)
            {
                STK_GRID.Rows[2].Cells[0].Style.BackColor = Color.Aqua;
            }
            else
            {
                STK_GRID.Rows[2].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Complete)
            {
                STK_GRID.Rows[3].Cells[0].Style.BackColor = Color.Aqua;
            }
            else
            {
                STK_GRID.Rows[3].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_STK_abnormalerror)
            {
                STK_GRID.Rows[4].Cells[0].Style.BackColor = Color.Aqua;
            }
            else
            {
                STK_GRID.Rows[4].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Notusedstk)
            {
                STK_GRID.Rows[5].Cells[0].Style.BackColor = Color.Aqua;
            }
            else
            {
                STK_GRID.Rows[5].Cells[0].Style.BackColor = Color.White;
            }
            if (TF_STK_CST_Contain)
            {
                STK_GRID.Rows[6].Cells[0].Style.BackColor = Color.Aqua;
            }
            else
            {
                STK_GRID.Rows[6].Cells[0].Style.BackColor = Color.White;
            }
            if (TF_CarrierId)
            {
                STK_GRID.Rows[7].Cells[0].Style.BackColor = Color.Aqua;
            }
            else
            {
                STK_GRID.Rows[7].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Rackmaster1)
            {
                STK_GRID.Rows[8].Cells[0].Style.BackColor = Color.Aqua;
            }
            else
            {
                STK_GRID.Rows[8].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Rackmaster2)
            {
                STK_GRID.Rows[9].Cells[0].Style.BackColor = Color.Aqua;
            }
            else
            {
                STK_GRID.Rows[9].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_Rackmaster3)
            {
                STK_GRID.Rows[10].Cells[0].Style.BackColor = Color.Aqua;
            }
            else
            {
                STK_GRID.Rows[10].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_STK_Reserved_Req)
            {
                STK_GRID.Rows[11].Cells[0].Style.BackColor = Color.Aqua;
            }
            else
            {
                STK_GRID.Rows[11].Cells[0].Style.BackColor = Color.White;
            }

            if (TF_STK_Move_Reserved)
            {
                STK_GRID.Rows[12].Cells[0].Style.BackColor = Color.Aqua;
            }
            else
            {
                STK_GRID.Rows[12].Cells[0].Style.BackColor = Color.White;
            }

        }

        private void InitializeBit(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                bitchange("OFF", i);
            }
        }

        private void InitializeBit()
        {
            bitchange("OFF", 1);
            bitchange("OFF", 2);
            bitchange("OFF", 3);

        }

        private void Interlock()
        {
            if (!TF_STK_abnormalerror)
            {
                InitializeBit(1, 3);
                InitializeBit(5, 5);
                InitializeBit(7, 16);
                HandShake_Step = 1;
                //this.Invoke(new Action(delegate ()
                //{
                //    ClientBox.Items.Add("STK가 ABNORMAL 상태 입니다");
                //}));
            }

            else if (!TF_Portabnormalerror)
            {
                InitializeBit(1, 3);
                InitializeBit(5, 5);
                InitializeBit(7, 16);
                HandShake_Step = 1;
                //this.Invoke(new Action(delegate ()
                //{
                //    ClientBox.Items.Add("PORT가 ABNORMAL 상태 입니다.");
                //}));
            }

            else if (!TF_Eqpabnormalerror)
            {
                InitializeBit(1, 3);
                InitializeBit(5, 5);
                InitializeBit(7, 16);
                HandShake_Step = 1;
                //this.Invoke(new Action(delegate ()
                //{
                //    ClientBox.Items.Add("EQ가 ABNORMAL 상태 입니다.");
                //}));
            }
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
                TrueFlase();
                HandShake();
                Write_Data = string.Join("", STK_bit);
                Writer1.WriteLine(Write_Data); // SEND
            }

            else if (oNOFFToolStripMenuItem.Checked == false)
            {

            }

        }
        private void oNOFFToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //if (!oNOFFToolStripMenuItem.Checked)
            //{
            //    Thread thread1 = new Thread(Connect);
            //    thread1.Start();
            //}
        }

        private void TrueFlase()
        {
            if (IDX_bit[LOAD_REQUEST] == 1)         // IDX PART
            {
                TF_LoadRequest = true;
            }
            else
            {
                TF_LoadRequest = false;
            }
            if (IDX_bit[UNLOAD_REQUEST] == 1)
            {
                TF_UnloadRequest = true;
            }
            else
            {
                TF_UnloadRequest = false;
            }
            if (IDX_bit[READY] == 1)
            {
                TF_Ready = true;
            }
            else
            {
                TF_Ready = false;
            }
            if (IDX_bit[PORT_ABNORMAL_ERROR] == 1)
            {
                TF_Portabnormalerror = true;
            }
            else
            {
                TF_Portabnormalerror = false;
            }
            if (IDX_bit[NOT_USED_PORT] == 1)
            {
                TF_Notusedport = true;
            }
            else
            {
                TF_Notusedport = false;
            }
            if (IDX_bit[IDX_CST_CONTAIN] == 1)
            {
                TF_IDX_CST_Contain = true;
            }
            else
            {
                TF_IDX_CST_Contain = false;
            }
            if (IDX_bit[CHUCK_STATUS] == 1)
            {
                TF_Chuckstatus = true;
            }
            else
            {
                TF_Chuckstatus = false;
            }
            if (IDX_bit[INTERLOCK] == 1)
            {
                TF_Interlock = true;
            }
            else
            {
                TF_Interlock = false;
            }
            if (IDX_bit[EQP_ABNORMAL_ERROR] == 1)
            {
                TF_Eqpabnormalerror = true;
            }
            else
            {
                TF_Eqpabnormalerror = false;
            }

            if (IDX_bit[DOOR_OPEN] == 1)
            {
                TF_Dooropen = true;
            }
            else
            {
                TF_Dooropen = false;
            }

            if (IDX_bit[IDX_RESERVED_REQ] == 1)
            {
                TF_IDX_Reserved_Req = true;
            }
            else
            {
                TF_IDX_Reserved_Req = false;
            }

            if (IDX_bit[IDX_MOVE_RESERVED] == 1)
            {
                TF_IDX_Move_Reserved = true;
            }
            else
            {
                TF_IDX_Move_Reserved = false;
            }

            if (STK_bit[TR_REQUEST] == 1)          // STK PART
            {
                TF_TrRequest = true;
            }
            else
            {
                TF_TrRequest = false;
            }
            if (STK_bit[BUSY] == 1)
            {
                TF_Busy = true;
            }
            else
            {
                TF_Busy = false;
            }
            if (STK_bit[COMPLETE] == 1)
            {
                TF_Complete = true;
            }
            else
            {
                TF_Complete = false;
            }
            if (STK_bit[STK_ABNORMAL_ERROR] == 1)
            {
                TF_STK_abnormalerror = true;
            }
            else
            {
                TF_STK_abnormalerror = false;
            }
            if (STK_bit[NOT_USED_STK] == 1)
            {
                TF_Notusedstk = true;
            }
            else
            {
                TF_Notusedstk = false;
            }
            if (STK_bit[STK_CST_CONTAIN] == 1)
            {
                TF_STK_CST_Contain = true;
            }
            else
            {
                TF_STK_CST_Contain = false;
            }
            if (STK_bit[CARRIER_ID] == 1)
            {
                TF_CarrierId = true;
            }
            else
            {
                TF_CarrierId = false;
            }

            if (STK_bit[RACK_MASTER1] == 1)
            {
                TF_Rackmaster1 = true;
            }
            else
            {
                TF_Rackmaster1 = false;
            }

            if (STK_bit[RACK_MASTER2] == 1)
            {
                TF_Rackmaster2 = true;
            }
            else
            {
                TF_Rackmaster2 = false;
            }

            if (STK_bit[RACK_MASTER3] == 1)
            {
                TF_Rackmaster3 = true;
            }
            else
            {
                TF_Rackmaster3 = false;
            }

            if (STK_bit[STK_RESERVED_REQ] == 1)
            {
                TF_STK_Reserved_Req = true;
            }
            else
            {
                TF_STK_Reserved_Req = false;
            }

            if (STK_bit[STK_MOVE_RESERVED] == 1)
            {
                TF_STK_Move_Reserved = true;
            }
            else
            {
                TF_STK_Move_Reserved = false;
            }
        }
        private void HandShake()
        {
            switch (HandShake_Step)
            {
                case 1:
                    if (TF_LoadRequest && !TF_UnloadRequest && !TF_Ready && !TF_IDX_CST_Contain && !TF_Chuckstatus && !TF_TrRequest && !TF_Busy && !TF_Complete)   //LD TR RQ
                    {
                        bitchange("ON", 6);
                        Thread.Sleep(2000);
                        bitchange("ON", 1);
                        HandShake_Step++;
                        Thread.Sleep(1000);
                    }
                    else if (!TF_LoadRequest && TF_UnloadRequest && !TF_Ready && TF_IDX_CST_Contain && !TF_Chuckstatus && !TF_TrRequest && !TF_Busy && !TF_Complete)
                    {
                        HandShake_Step = 6;
                    }

                    break;

                case 2:
                    if (TF_LoadRequest && !TF_UnloadRequest && TF_Ready && !TF_IDX_CST_Contain && !TF_Chuckstatus && TF_TrRequest && !TF_Busy && !TF_Complete && TF_STK_CST_Contain)  // STK BUSY
                    {
                        Thread.Sleep(1000);
                        bitchange("ON", 2);
                        HandShake_Step++;
                        Thread.Sleep(1000);

                    }
                    break;

                case 3:
                    if (!TF_LoadRequest && !TF_UnloadRequest && TF_Ready && TF_IDX_CST_Contain && !TF_Chuckstatus && TF_TrRequest && TF_Busy && !TF_Complete && TF_STK_CST_Contain)
                    {
                        bitchange("OFF", 6);
                        Thread.Sleep(2000);
                        bitchange("OFF", 1);
                        bitchange("OFF", 2);
                        HandShake_Step++;
                    }
                    break;

                case 4:
                    if (!TF_LoadRequest && !TF_UnloadRequest && !TF_Ready && TF_IDX_CST_Contain && TF_Chuckstatus && !TF_TrRequest && !TF_Busy && !TF_Complete && !TF_STK_CST_Contain)
                    {
                        bitchange("ON", 3);
                        HandShake_Step++;
                    }
                    break;

                case 5:
                    if (!TF_LoadRequest && !TF_UnloadRequest && !TF_Ready && TF_IDX_CST_Contain && TF_Chuckstatus && !TF_TrRequest && !TF_Busy && TF_Complete && !TF_STK_CST_Contain)
                    {
                        bitchange("OFF", 3);
                        HandShake_Step = 1;
                    }
                    break;

                case 6:
                    if (!TF_LoadRequest && TF_UnloadRequest && !TF_Ready && TF_IDX_CST_Contain && !TF_Chuckstatus && !TF_TrRequest && !TF_Busy && !TF_Complete && !TF_STK_CST_Contain)  // TR RQ
                    { // UNLOAD RQ
                        bitchange("ON", 1);
                        HandShake_Step++;
                    }
                    break;

                case 7:
                    if (!TF_LoadRequest && TF_UnloadRequest && TF_Ready && TF_IDX_CST_Contain && !TF_Chuckstatus && TF_TrRequest && !TF_Busy && !TF_Complete && !TF_STK_CST_Contain) // STK BUSY 
                    {
                        bitchange("ON", 1);
                        bitchange("ON", 2);
                        HandShake_Step++;
                    }
                    break;

                case 8:
                    if (!TF_LoadRequest && !TF_UnloadRequest && TF_Ready && !TF_IDX_CST_Contain && !TF_Chuckstatus && TF_TrRequest && TF_Busy && !TF_Complete && !TF_STK_CST_Contain) // UNLD RQ DOWN
                    {
                        bitchange("OFF", 1);
                        bitchange("OFF", 2);
                        bitchange("ON", 6);
                        HandShake_Step++;
                    }
                    break;

                case 9:
                    if (!TF_LoadRequest && !TF_UnloadRequest && !TF_Ready && !TF_IDX_CST_Contain && !TF_Chuckstatus && !TF_TrRequest && !TF_Busy && !TF_Complete && TF_STK_CST_Contain) // STK COMPLETE
                    {
                        InitializeBit();
                        bitchange("ON", 3);
                        HandShake_Step++;
                    }
                    break;
                case 10:
                    if (!TF_LoadRequest && !TF_UnloadRequest && !TF_Ready && !TF_IDX_CST_Contain && !TF_Chuckstatus && !TF_TrRequest && !TF_Busy && TF_Complete && TF_STK_CST_Contain) // STK COMPLETE
                    {
                        bitchange("OFF", 6);
                        InitializeBit();
                        HandShake_Step = 1;
                    }
                    break;

            }

        }
        public void Connect()
        {

            //ClientBox.Items.Add("통신완료");//Invoke.(new MethodInvoker(delegate { ClientBox.Items.Add("통신완료"); }));
            TcpListener tcpListener1 = new TcpListener(IPAddress.Parse("127.0.0.1"), int.Parse("1000"));
            tcpListener1.Start();
            //  textBox1.Text = "서버 시작";
            TcpClient tcpClient1 = tcpListener1.AcceptTcpClient();
            Reader1 = new StreamReader(tcpClient1.GetStream());
            Writer1 = new StreamWriter(tcpClient1.GetStream());
            Writer1.AutoFlush = true;
            string tempstorage = "";

            this.Invoke(new Action(delegate ()
            {
                ClientBox.Items.Add("클라이언트 접속");
            }));

            while (tcpClient1.Connected)
            {
                Tcpkey = true;
                Receive_Data = Reader1.ReadLine();


                if (Receive_Data != OLDReceive_Data)
                {

                    // 날라온 bit를 쪼개서 16 쪼개서, 각 각 IDX Bit 전송 & Form Display
                    for (int i = 0; i <= 16; i++)
                    {
                        IDX_bit[i] = Convert.ToInt32(Receive_Data[i] + "");
                    }
                    OLDReceive_Data = tempstorage;
                    tempstorage = "";
                    // IDX 비트에 새로운 비트 갱신
                }
                else if (Receive_Data == OLDReceive_Data)
                {

                }

                //Interlock();
                this.Invoke(new Action(delegate ()
                {
                    DisplayForm();

                }));
            }
        }

        private void Btn_CSTContain_Click(object sender, EventArgs e)
        {
            if (Stkerror == true)
            {
                bitchange("ON", 4);
                Stkerror = false;
            }
            else
            {
                bitchange("OFF", 4);
                Stkerror = true;
            }
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {

        }
    }

}
