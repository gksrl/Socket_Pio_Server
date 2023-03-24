
namespace Socket_Pio_Server
{
    partial class SERVER
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Btn_Clear = new System.Windows.Forms.Button();
            this.Btn_CSTContain = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oNOFFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.STK_GRID = new System.Windows.Forms.DataGridView();
            this.IDX_GRID = new System.Windows.Forms.DataGridView();
            this.ClientBox = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.STK_GRID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IDX_GRID)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.Location = new System.Drawing.Point(577, 475);
            this.Btn_Clear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.Size = new System.Drawing.Size(152, 74);
            this.Btn_Clear.TabIndex = 18;
            this.Btn_Clear.Text = "CLEAR";
            this.Btn_Clear.UseVisualStyleBackColor = true;
            // 
            // Btn_CSTContain
            // 
            this.Btn_CSTContain.Location = new System.Drawing.Point(577, 41);
            this.Btn_CSTContain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_CSTContain.Name = "Btn_CSTContain";
            this.Btn_CSTContain.Size = new System.Drawing.Size(152, 74);
            this.Btn_CSTContain.TabIndex = 17;
            this.Btn_CSTContain.Text = "CASSETTE\r\nCONTAIN\r\n";
            this.Btn_CSTContain.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1022, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // clientToolStripMenuItem
            // 
            this.clientToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oNOFFToolStripMenuItem});
            this.clientToolStripMenuItem.Name = "clientToolStripMenuItem";
            this.clientToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.clientToolStripMenuItem.Text = "Server";
            // 
            // oNOFFToolStripMenuItem
            // 
            this.oNOFFToolStripMenuItem.Name = "oNOFFToolStripMenuItem";
            this.oNOFFToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.oNOFFToolStripMenuItem.Text = "ON/OFF";
            this.oNOFFToolStripMenuItem.Click += new System.EventHandler(this.oNOFFToolStripMenuItem_Click_1);
            // 
            // STK_GRID
            // 
            this.STK_GRID.AllowUserToAddRows = false;
            this.STK_GRID.AllowUserToDeleteRows = false;
            this.STK_GRID.AllowUserToResizeColumns = false;
            this.STK_GRID.AllowUserToResizeRows = false;
            this.STK_GRID.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.STK_GRID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.STK_GRID.ColumnHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.STK_GRID.DefaultCellStyle = dataGridViewCellStyle5;
            this.STK_GRID.Enabled = false;
            this.STK_GRID.Location = new System.Drawing.Point(281, 37);
            this.STK_GRID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.STK_GRID.Name = "STK_GRID";
            this.STK_GRID.RowHeadersVisible = false;
            this.STK_GRID.RowTemplate.Height = 23;
            this.STK_GRID.Size = new System.Drawing.Size(274, 518);
            this.STK_GRID.TabIndex = 22;
            this.STK_GRID.TabStop = false;
            // 
            // IDX_GRID
            // 
            this.IDX_GRID.AllowUserToAddRows = false;
            this.IDX_GRID.AllowUserToDeleteRows = false;
            this.IDX_GRID.AllowUserToResizeColumns = false;
            this.IDX_GRID.AllowUserToResizeRows = false;
            this.IDX_GRID.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.IDX_GRID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IDX_GRID.ColumnHeadersVisible = false;
            this.IDX_GRID.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.IDX_GRID.DefaultCellStyle = dataGridViewCellStyle6;
            this.IDX_GRID.Enabled = false;
            this.IDX_GRID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.IDX_GRID.Location = new System.Drawing.Point(0, 37);
            this.IDX_GRID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.IDX_GRID.MultiSelect = false;
            this.IDX_GRID.Name = "IDX_GRID";
            this.IDX_GRID.RowHeadersVisible = false;
            this.IDX_GRID.RowTemplate.Height = 23;
            this.IDX_GRID.Size = new System.Drawing.Size(274, 518);
            this.IDX_GRID.TabIndex = 21;
            // 
            // ClientBox
            // 
            this.ClientBox.FormattingEnabled = true;
            this.ClientBox.ItemHeight = 12;
            this.ClientBox.Location = new System.Drawing.Point(738, 41);
            this.ClientBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ClientBox.Name = "ClientBox";
            this.ClientBox.Size = new System.Drawing.Size(269, 508);
            this.ClientBox.TabIndex = 20;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(74, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 23;
            // 
            // SERVER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 562);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Btn_Clear);
            this.Controls.Add(this.Btn_CSTContain);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.STK_GRID);
            this.Controls.Add(this.IDX_GRID);
            this.Controls.Add(this.ClientBox);
            this.Name = "SERVER";
            this.Text = "SERVER";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.STK_GRID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IDX_GRID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Clear;
        private System.Windows.Forms.Button Btn_CSTContain;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oNOFFToolStripMenuItem;
        private System.Windows.Forms.DataGridView STK_GRID;
        private System.Windows.Forms.DataGridView IDX_GRID;
        private System.Windows.Forms.ListBox ClientBox;
        private System.Windows.Forms.TextBox textBox1;
    }
}

