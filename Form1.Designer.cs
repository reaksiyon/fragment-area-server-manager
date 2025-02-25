namespace AreaServerLevelEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            serverStatusPB = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            serverNameLbl = new Label();
            levelLbl = new Label();
            ServerNameTextBox = new TextBox();
            label3 = new Label();
            label4 = new Label();
            pictureBox3 = new PictureBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            ServerNameEditButton = new Button();
            ServerLevelEditButton = new Button();
            LandSettingsApplyButton = new Button();
            ServerLevelUpDown = new NumericUpDown();
            LavaLvTb = new NumericUpDown();
            FieryLvTb = new NumericUpDown();
            SandLvTb = new NumericUpDown();
            DesertLvTb = new NumericUpDown();
            RockyLvTb = new NumericUpDown();
            IceLvTb = new NumericUpDown();
            SnowLvTb = new NumericUpDown();
            WoodsLvTb = new NumericUpDown();
            machiveLvTb = new NumericUpDown();
            GrassLvTb = new NumericUpDown();
            SwampLvTb = new NumericUpDown();
            PlainsLvTb = new NumericUpDown();
            connectButton = new Button();
            helloMsgButton = new Label();
            helloMsgTB = new TextBox();
            helloMsgBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)serverStatusPB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ServerLevelUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LavaLvTb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FieryLvTb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SandLvTb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DesertLvTb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RockyLvTb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IceLvTb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SnowLvTb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WoodsLvTb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)machiveLvTb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GrassLvTb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SwampLvTb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PlainsLvTb).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = Properties.Resources.background;
            pictureBox1.Location = new Point(-3, -2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(806, 452);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // serverStatusPB
            // 
            serverStatusPB.BackgroundImageLayout = ImageLayout.None;
            serverStatusPB.Image = Properties.Resources.bg_window_offline;
            serverStatusPB.Location = new Point(12, 12);
            serverStatusPB.Name = "serverStatusPB";
            serverStatusPB.Size = new Size(342, 363);
            serverStatusPB.TabIndex = 1;
            serverStatusPB.TabStop = false;
            serverStatusPB.Click += serverStatusPB_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 43);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 2;
            label1.Text = "Server Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 68);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 3;
            label2.Text = "Server Level:";
            // 
            // serverNameLbl
            // 
            serverNameLbl.AutoSize = true;
            serverNameLbl.Location = new Point(104, 43);
            serverNameLbl.Name = "serverNameLbl";
            serverNameLbl.Size = new Size(78, 15);
            serverNameLbl.TabIndex = 4;
            serverNameLbl.Text = "Unconnected";
            serverNameLbl.Click += serverNameLbl_Click;
            // 
            // levelLbl
            // 
            levelLbl.AutoSize = true;
            levelLbl.Location = new Point(104, 68);
            levelLbl.Name = "levelLbl";
            levelLbl.Size = new Size(13, 15);
            levelLbl.TabIndex = 5;
            levelLbl.Text = "0";
            // 
            // ServerNameTextBox
            // 
            ServerNameTextBox.Location = new Point(104, 123);
            ServerNameTextBox.MaxLength = 20;
            ServerNameTextBox.Name = "ServerNameTextBox";
            ServerNameTextBox.Size = new Size(155, 23);
            ServerNameTextBox.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 127);
            label3.Name = "label3";
            label3.Size = new Size(77, 15);
            label3.TabIndex = 7;
            label3.Text = "Server Name:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 164);
            label4.Name = "label4";
            label4.Size = new Size(72, 15);
            label4.TabIndex = 8;
            label4.Text = "Server Level:";
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImage = Properties.Resources.land_window;
            pictureBox3.Location = new Point(104, 239);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(349, 176);
            pictureBox3.TabIndex = 10;
            pictureBox3.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(118, 275);
            label5.Name = "label5";
            label5.Size = new Size(48, 15);
            label5.TabIndex = 11;
            label5.Text = "Lava Lv:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(117, 304);
            label6.Name = "label6";
            label6.Size = new Size(49, 15);
            label6.TabIndex = 13;
            label6.Text = "Fiery Lv:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(116, 333);
            label7.Name = "label7";
            label7.Size = new Size(50, 15);
            label7.TabIndex = 15;
            label7.Text = "Sand Lv:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(109, 360);
            label8.Name = "label8";
            label8.Size = new Size(57, 15);
            label8.TabIndex = 17;
            label8.Text = "Desert Lv:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(212, 275);
            label9.Name = "label9";
            label9.Size = new Size(61, 15);
            label9.TabIndex = 19;
            label9.Text = "Woods Lv:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(218, 304);
            label10.Name = "label10";
            label10.Size = new Size(53, 15);
            label10.TabIndex = 21;
            label10.Text = "Snow Lv:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(232, 333);
            label11.Name = "label11";
            label11.Size = new Size(39, 15);
            label11.TabIndex = 23;
            label11.Text = "Ice Lv:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(218, 362);
            label12.Name = "label12";
            label12.Size = new Size(56, 15);
            label12.TabIndex = 25;
            label12.Text = "Rocky Lv:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(340, 275);
            label13.Name = "label13";
            label13.Size = new Size(55, 15);
            label13.TabIndex = 27;
            label13.Text = "Plains Lv:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(332, 304);
            label14.Name = "label14";
            label14.Size = new Size(63, 15);
            label14.TabIndex = 29;
            label14.Text = "Swamp Lv:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(343, 333);
            label15.Name = "label15";
            label15.Size = new Size(52, 15);
            label15.TabIndex = 31;
            label15.Text = "Grass Lv:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(325, 362);
            label16.Name = "label16";
            label16.Size = new Size(70, 15);
            label16.TabIndex = 33;
            label16.Text = "Machine Lv:";
            // 
            // ServerNameEditButton
            // 
            ServerNameEditButton.Enabled = false;
            ServerNameEditButton.Location = new Point(265, 123);
            ServerNameEditButton.Name = "ServerNameEditButton";
            ServerNameEditButton.Size = new Size(75, 23);
            ServerNameEditButton.TabIndex = 40;
            ServerNameEditButton.Text = "Apply";
            ServerNameEditButton.UseVisualStyleBackColor = true;
            ServerNameEditButton.Click += ServerNameEditButton_Click;
            // 
            // ServerLevelEditButton
            // 
            ServerLevelEditButton.Enabled = false;
            ServerLevelEditButton.Location = new Point(165, 160);
            ServerLevelEditButton.Name = "ServerLevelEditButton";
            ServerLevelEditButton.Size = new Size(75, 23);
            ServerLevelEditButton.TabIndex = 41;
            ServerLevelEditButton.Text = "Apply";
            ServerLevelEditButton.UseVisualStyleBackColor = true;
            ServerLevelEditButton.Click += ServerLevelEditButton_Click;
            // 
            // LandSettingsApplyButton
            // 
            LandSettingsApplyButton.Enabled = false;
            LandSettingsApplyButton.Location = new Point(375, 386);
            LandSettingsApplyButton.Name = "LandSettingsApplyButton";
            LandSettingsApplyButton.Size = new Size(67, 23);
            LandSettingsApplyButton.TabIndex = 42;
            LandSettingsApplyButton.Text = "Apply";
            LandSettingsApplyButton.UseVisualStyleBackColor = true;
            LandSettingsApplyButton.Click += LandSettingsApplyButton_Click;
            // 
            // ServerLevelUpDown
            // 
            ServerLevelUpDown.Location = new Point(104, 160);
            ServerLevelUpDown.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            ServerLevelUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ServerLevelUpDown.Name = "ServerLevelUpDown";
            ServerLevelUpDown.Size = new Size(55, 23);
            ServerLevelUpDown.TabIndex = 43;
            ServerLevelUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // LavaLvTb
            // 
            LavaLvTb.Location = new Point(170, 273);
            LavaLvTb.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            LavaLvTb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            LavaLvTb.Name = "LavaLvTb";
            LavaLvTb.Size = new Size(41, 23);
            LavaLvTb.TabIndex = 44;
            LavaLvTb.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // FieryLvTb
            // 
            FieryLvTb.Location = new Point(170, 302);
            FieryLvTb.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            FieryLvTb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            FieryLvTb.Name = "FieryLvTb";
            FieryLvTb.Size = new Size(41, 23);
            FieryLvTb.TabIndex = 45;
            FieryLvTb.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // SandLvTb
            // 
            SandLvTb.Location = new Point(170, 329);
            SandLvTb.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            SandLvTb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            SandLvTb.Name = "SandLvTb";
            SandLvTb.Size = new Size(41, 23);
            SandLvTb.TabIndex = 46;
            SandLvTb.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DesertLvTb
            // 
            DesertLvTb.Location = new Point(170, 358);
            DesertLvTb.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            DesertLvTb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            DesertLvTb.Name = "DesertLvTb";
            DesertLvTb.Size = new Size(41, 23);
            DesertLvTb.TabIndex = 47;
            DesertLvTb.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // RockyLvTb
            // 
            RockyLvTb.Location = new Point(277, 358);
            RockyLvTb.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            RockyLvTb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            RockyLvTb.Name = "RockyLvTb";
            RockyLvTb.Size = new Size(41, 23);
            RockyLvTb.TabIndex = 51;
            RockyLvTb.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // IceLvTb
            // 
            IceLvTb.Location = new Point(277, 329);
            IceLvTb.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            IceLvTb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            IceLvTb.Name = "IceLvTb";
            IceLvTb.Size = new Size(41, 23);
            IceLvTb.TabIndex = 50;
            IceLvTb.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // SnowLvTb
            // 
            SnowLvTb.Location = new Point(277, 302);
            SnowLvTb.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            SnowLvTb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            SnowLvTb.Name = "SnowLvTb";
            SnowLvTb.Size = new Size(41, 23);
            SnowLvTb.TabIndex = 49;
            SnowLvTb.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // WoodsLvTb
            // 
            WoodsLvTb.Location = new Point(277, 273);
            WoodsLvTb.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            WoodsLvTb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            WoodsLvTb.Name = "WoodsLvTb";
            WoodsLvTb.Size = new Size(41, 23);
            WoodsLvTb.TabIndex = 48;
            WoodsLvTb.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // machiveLvTb
            // 
            machiveLvTb.Location = new Point(401, 358);
            machiveLvTb.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            machiveLvTb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            machiveLvTb.Name = "machiveLvTb";
            machiveLvTb.Size = new Size(41, 23);
            machiveLvTb.TabIndex = 55;
            machiveLvTb.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // GrassLvTb
            // 
            GrassLvTb.Location = new Point(401, 329);
            GrassLvTb.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            GrassLvTb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            GrassLvTb.Name = "GrassLvTb";
            GrassLvTb.Size = new Size(41, 23);
            GrassLvTb.TabIndex = 54;
            GrassLvTb.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // SwampLvTb
            // 
            SwampLvTb.Location = new Point(401, 302);
            SwampLvTb.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            SwampLvTb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            SwampLvTb.Name = "SwampLvTb";
            SwampLvTb.Size = new Size(41, 23);
            SwampLvTb.TabIndex = 53;
            SwampLvTb.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // PlainsLvTb
            // 
            PlainsLvTb.Location = new Point(401, 273);
            PlainsLvTb.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            PlainsLvTb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            PlainsLvTb.Name = "PlainsLvTb";
            PlainsLvTb.Size = new Size(41, 23);
            PlainsLvTb.TabIndex = 52;
            PlainsLvTb.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // connectButton
            // 
            connectButton.Location = new Point(265, 64);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(75, 23);
            connectButton.TabIndex = 56;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // helloMsgButton
            // 
            helloMsgButton.AutoSize = true;
            helloMsgButton.Location = new Point(26, 200);
            helloMsgButton.Name = "helloMsgButton";
            helloMsgButton.Size = new Size(64, 15);
            helloMsgButton.TabIndex = 57;
            helloMsgButton.Text = "Hello Msg:";
            // 
            // helloMsgTB
            // 
            helloMsgTB.Location = new Point(104, 197);
            helloMsgTB.MaxLength = 100;
            helloMsgTB.Name = "helloMsgTB";
            helloMsgTB.Size = new Size(155, 23);
            helloMsgTB.TabIndex = 58;
            // 
            // helloMsgBtn
            // 
            helloMsgBtn.Enabled = false;
            helloMsgBtn.Location = new Point(265, 197);
            helloMsgBtn.Name = "helloMsgBtn";
            helloMsgBtn.Size = new Size(75, 23);
            helloMsgBtn.TabIndex = 59;
            helloMsgBtn.Text = "Apply";
            helloMsgBtn.UseVisualStyleBackColor = true;
            helloMsgBtn.Click += helloMsgBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(798, 448);
            Controls.Add(helloMsgBtn);
            Controls.Add(helloMsgTB);
            Controls.Add(helloMsgButton);
            Controls.Add(connectButton);
            Controls.Add(machiveLvTb);
            Controls.Add(GrassLvTb);
            Controls.Add(SwampLvTb);
            Controls.Add(PlainsLvTb);
            Controls.Add(RockyLvTb);
            Controls.Add(IceLvTb);
            Controls.Add(SnowLvTb);
            Controls.Add(WoodsLvTb);
            Controls.Add(DesertLvTb);
            Controls.Add(SandLvTb);
            Controls.Add(FieryLvTb);
            Controls.Add(LavaLvTb);
            Controls.Add(ServerLevelUpDown);
            Controls.Add(LandSettingsApplyButton);
            Controls.Add(ServerLevelEditButton);
            Controls.Add(ServerNameEditButton);
            Controls.Add(label16);
            Controls.Add(label15);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(pictureBox3);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(ServerNameTextBox);
            Controls.Add(levelLbl);
            Controls.Add(serverNameLbl);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(serverStatusPB);
            Controls.Add(pictureBox1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Area Server Manager v1.0 by reaksiyon1337";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)serverStatusPB).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)ServerLevelUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)LavaLvTb).EndInit();
            ((System.ComponentModel.ISupportInitialize)FieryLvTb).EndInit();
            ((System.ComponentModel.ISupportInitialize)SandLvTb).EndInit();
            ((System.ComponentModel.ISupportInitialize)DesertLvTb).EndInit();
            ((System.ComponentModel.ISupportInitialize)RockyLvTb).EndInit();
            ((System.ComponentModel.ISupportInitialize)IceLvTb).EndInit();
            ((System.ComponentModel.ISupportInitialize)SnowLvTb).EndInit();
            ((System.ComponentModel.ISupportInitialize)WoodsLvTb).EndInit();
            ((System.ComponentModel.ISupportInitialize)machiveLvTb).EndInit();
            ((System.ComponentModel.ISupportInitialize)GrassLvTb).EndInit();
            ((System.ComponentModel.ISupportInitialize)SwampLvTb).EndInit();
            ((System.ComponentModel.ISupportInitialize)PlainsLvTb).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox serverStatusPB;
        private Label label1;
        private Label label2;
        private Label serverNameLbl;
        private Label levelLbl;
        private TextBox ServerNameTextBox;
        private Label label3;
        private Label label4;
        private PictureBox pictureBox3;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Button ServerNameEditButton;
        private Button ServerLevelEditButton;
        private Button LandSettingsApplyButton;
        private NumericUpDown ServerLevelUpDown;
        private NumericUpDown LavaLvTb;
        private NumericUpDown FieryLvTb;
        private NumericUpDown SandLvTb;
        private NumericUpDown DesertLvTb;
        private NumericUpDown RockyLvTb;
        private NumericUpDown IceLvTb;
        private NumericUpDown SnowLvTb;
        private NumericUpDown WoodsLvTb;
        private NumericUpDown machiveLvTb;
        private NumericUpDown GrassLvTb;
        private NumericUpDown SwampLvTb;
        private NumericUpDown PlainsLvTb;
        private Button connectButton;
        private Label helloMsgButton;
        private TextBox helloMsgTB;
        private Button helloMsgBtn;
    }
}
