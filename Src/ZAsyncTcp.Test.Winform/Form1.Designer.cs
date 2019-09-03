namespace ZAsyncTcp.Test.Winform
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox_Type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Send = new System.Windows.Forms.TextBox();
            this.button_Connect = new System.Windows.Forms.Button();
            this.textBox_Received = new System.Windows.Forms.TextBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.button_ClearReceived = new System.Windows.Forms.Button();
            this.button_ClearSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox_Type
            // 
            this.comboBox_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Type.FormattingEnabled = true;
            this.comboBox_Type.Items.AddRange(new object[] {
            "Server"});
            this.comboBox_Type.Location = new System.Drawing.Point(67, 19);
            this.comboBox_Type.Name = "comboBox_Type";
            this.comboBox_Type.Size = new System.Drawing.Size(100, 23);
            this.comboBox_Type.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "IP";
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(67, 57);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(100, 25);
            this.textBox_IP.TabIndex = 3;
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(67, 97);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(100, 25);
            this.textBox_Port.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Port";
            // 
            // textBox_Send
            // 
            this.textBox_Send.Location = new System.Drawing.Point(198, 12);
            this.textBox_Send.Multiline = true;
            this.textBox_Send.Name = "textBox_Send";
            this.textBox_Send.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Send.Size = new System.Drawing.Size(401, 75);
            this.textBox_Send.TabIndex = 6;
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(67, 137);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(100, 23);
            this.button_Connect.TabIndex = 7;
            this.button_Connect.Text = "Connect";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // textBox_Received
            // 
            this.textBox_Received.Location = new System.Drawing.Point(198, 122);
            this.textBox_Received.Multiline = true;
            this.textBox_Received.Name = "textBox_Received";
            this.textBox_Received.ReadOnly = true;
            this.textBox_Received.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Received.Size = new System.Drawing.Size(401, 266);
            this.textBox_Received.TabIndex = 8;
            // 
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(510, 93);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(89, 23);
            this.button_Send.TabIndex = 9;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // button_ClearReceived
            // 
            this.button_ClearReceived.Location = new System.Drawing.Point(510, 394);
            this.button_ClearReceived.Name = "button_ClearReceived";
            this.button_ClearReceived.Size = new System.Drawing.Size(89, 23);
            this.button_ClearReceived.TabIndex = 10;
            this.button_ClearReceived.Text = "Clear";
            this.button_ClearReceived.UseVisualStyleBackColor = true;
            // 
            // button_ClearSend
            // 
            this.button_ClearSend.Location = new System.Drawing.Point(415, 93);
            this.button_ClearSend.Name = "button_ClearSend";
            this.button_ClearSend.Size = new System.Drawing.Size(89, 23);
            this.button_ClearSend.TabIndex = 11;
            this.button_ClearSend.Text = "Clear";
            this.button_ClearSend.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 428);
            this.Controls.Add(this.button_ClearSend);
            this.Controls.Add(this.button_ClearReceived);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.textBox_Received);
            this.Controls.Add(this.button_Connect);
            this.Controls.Add(this.textBox_Send);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_Type);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Send;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.TextBox textBox_Received;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Button button_ClearReceived;
        private System.Windows.Forms.Button button_ClearSend;
    }
}

