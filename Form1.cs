namespace AreaServerLevelEditor
{
    public partial class Form1 : Form
    {
        private PacketLoggerClient? packetLogger;
        private readonly Dictionary<int, PlayerProfile> onlinePlayers = new();

        public Form1()
        {
            InitializeComponent();
            EnsureChatLogVisible();
            packetLogger = new PacketLoggerClient(AddChatLine, AddLoggerStatus, AddOrUpdatePlayer, RemovePlayer);
            Shown += async (_, _) => await packetLogger.StartAsync();
            FormClosed += (_, _) => packetLogger.Dispose();
        }

        private void EnsureChatLogVisible()
        {
            ClientSize = new Size(1412, 448);
            MinimumSize = new Size(1428, 487);
            onlinePlayersPanel.Location = new Point(812, 12);
            chatPanel.Location = new Point(1070, 12);
            chatPanel.Size = new Size(330, 424);
            chatPanel.Visible = true;
            chatPanel.BringToFront();
            onlinePlayersPanel.BringToFront();
        }

        private void AddOrUpdatePlayer(PlayerProfile profile)
        {
            if (InvokeRequired) { BeginInvoke(() => AddOrUpdatePlayer(profile)); return; }
            onlinePlayers[profile.SocketId] = profile;
            int selectedSocket = onlinePlayersList.SelectedItem is PlayerProfile selected ? selected.SocketId : -1;
            onlinePlayersList.BeginUpdate();
            onlinePlayersList.Items.Clear();
            foreach (PlayerProfile player in onlinePlayers.Values.OrderBy(p => p.Nickname))
            {
                int index = onlinePlayersList.Items.Add(player);
                if (player.SocketId == selectedSocket || (selectedSocket < 0 && player.SocketId == profile.SocketId))
                    onlinePlayersList.SelectedIndex = index;
            }
            onlinePlayersList.EndUpdate();
            ShowSelectedPlayer();
        }

        private void RemovePlayer(int socketId)
        {
            if (InvokeRequired) { BeginInvoke(() => RemovePlayer(socketId)); return; }
            if (!onlinePlayers.Remove(socketId)) return;
            onlinePlayersList.Items.Clear();
            foreach (PlayerProfile player in onlinePlayers.Values.OrderBy(p => p.Nickname))
                onlinePlayersList.Items.Add(player);
            ShowSelectedPlayer();
        }

        private void OnlinePlayersList_SelectedIndexChanged(object? sender, EventArgs e) => ShowSelectedPlayer();

        private void ShowSelectedPlayer()
        {
            if (onlinePlayersList.SelectedItem is not PlayerProfile player)
            {
                playerDetails.Text = "Select a player to view profile details.";
                return;
            }
            playerDetails.Text = $"Nickname: {player.Nickname}\r\n" +
                                 $"Class: {player.ClassName}\r\n" +
                                 $"Level: {player.Level}\r\n" +
                                 $"EXP: {player.Experience}/{player.ExperienceRequired}\r\n" +
                                 $"Gold: {player.Gold}\r\n" +
                                 $"Model: {player.Model}";
        }

        private void AddChatLine(string nickname, int level, string message)
        {
            if (InvokeRequired) { BeginInvoke(() => AddChatLine(nickname, level, message)); return; }
            if (string.IsNullOrWhiteSpace(message)) return;
            string levelText = level >= 0 ? $"Lv.{level}" : "Lv.?";
            chatLog.Items.Add($"[{nickname}] [{levelText}] : {message}");
            chatLog.TopIndex = chatLog.Items.Count - 1;
        }

        private void AddLoggerStatus(string status)
        {
            if (InvokeRequired) { BeginInvoke(() => AddLoggerStatus(status)); return; }
            chatLog.Items.Add($"-- {status} --");
            chatLog.TopIndex = chatLog.Items.Count - 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnsureChatLogVisible();
            RefreshServerStatus();
        }

        private void RefreshServerStatus()
        {
            if (Program.processes is null || Program.processes.Length == 0 || Program.process.HasExited)
                return;

            serverNameLbl.Text = Program.GetServerName(Program.processes) ?? "Unknown";
            levelLbl.Text = Program.GetServerLevel().ToString();
            serverStatusPB.Image = Properties.Resources.bg_window3;
            connectButton.Text = "Connected!";
            connectButton.Enabled = false;
            ServerNameEditButton.Enabled = true;
            ServerLevelEditButton.Enabled = true;
            LandSettingsApplyButton.Enabled = true;
            helloMsgBtn.Enabled = true;
            AutoFillButton.Enabled = true;
        }

        private void ServerNameEditButton_Click(object sender, EventArgs e)
        {
            Program.SetServerName(ServerNameTextBox.Text);

            serverNameLbl.Text = Program.GetServerName(Program.processes);
            // MessageBox.Show(Program.GetServerName(Program.processes));
            Program.ReloadUI();
        }

        private void ServerLevelEditButton_Click(object sender, EventArgs e)
        {
            Program.SetServerLevel(Convert.ToInt32(ServerLevelUpDown.Value));

            levelLbl.Text = ServerLevelUpDown.Value.ToString();
        }

        private void LandSettingsApplyButton_Click(object sender, EventArgs e)
        {
            Program.SetLandLevels((int)LavaLvTb.Value, (int)FieryLvTb.Value, (int)SandLvTb.Value, (int)DesertLvTb.Value, (int)WoodsLvTb.Value, (int)SnowLvTb.Value, (int)IceLvTb.Value, (int)RockyLvTb.Value, (int)PlainsLvTb.Value, (int)SwampLvTb.Value, (int)GrassLvTb.Value, (int)machiveLvTb.Value);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            serverNameLbl.Text = Program.GetServerName(Program.processes);
            levelLbl.Text = Program.GetServerLevel().ToString();
            //SymbolText.Text = Program.GetServerTag().ToString();

            if (Program.GetServerLevel() == 0)
            {
                MessageBox.Show("ALTIMIT - Area Server is not online!");
                return;
            }

            connectButton.Text = "Connected!";
            connectButton.Enabled = false;

            //SymbolText.Text = Program.GetServerTag();
            serverStatusPB.Image = Properties.Resources.bg_window3;
            ServerNameEditButton.Enabled = true;
            ServerLevelEditButton.Enabled = true;
            LandSettingsApplyButton.Enabled = true;
            //SetServerSymbolButton.Enabled = true;
            helloMsgBtn.Enabled = true;
            AutoFillButton.Enabled = true;
        }

        private void serverNameLbl_Click(object sender, EventArgs e)
        {

        }

        private void helloMsgBtn_Click(object sender, EventArgs e)
        {
            Program.SetServerHelloMessage(helloMsgTB.Text);
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void SetServerSymbolButton_Click(object sender, EventArgs e)
        {
            //Program.SetServerTag(serverTagTB.Text);

            //SymbolText.Text = Program.GetServerTag();
        }

        private void serverStatusPB_Click(object sender, EventArgs e)
        {

        }
        private void AutoFillButton_Click(object sender, EventArgs e)
        {
            LavaLvTb.Value = 70;
            FieryLvTb.Value = 60;
            SandLvTb.Value = 25;
            DesertLvTb.Value = 20;
            WoodsLvTb.Value = 25;
            SnowLvTb.Value = 30;
            IceLvTb.Value = 40;
            RockyLvTb.Value = 50;
            PlainsLvTb.Value = 10;
            SwampLvTb.Value = 15;
            GrassLvTb.Value = 3;
            machiveLvTb.Value = 35;
        }
    }
}
