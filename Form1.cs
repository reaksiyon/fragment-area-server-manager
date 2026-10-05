namespace AreaServerLevelEditor
{
    public partial class Form1 : Form
    {
        private PacketLoggerClient? packetLogger;
        private FloatingToolWindow? landWindow;
        private FloatingToolWindow? logsWindow;
        private FloatingToolWindow? eventWindow;
        private FloatingToolWindow? miscWindow;
        private readonly Dictionary<int, PlayerProfile> onlinePlayers = new();
        private static readonly (string Name, string Date)[] SpecialEvents =
        {
            ("New Year's Day", "01-01"),
            ("Setsubun", "02-03"),
            ("Valentine's Day", "02-14"),
            ("White Day", "03-15"),
            ("Tanabata", "07-07"),
            ("Summer Event", "08-07"),
            ("Moon Viewing", "09-28"),
            ("Sports Day", "10-09"),
            ("Christmas", "12-25"),
            ("New Year's Eve", "12-31")
        };

        public Form1()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            InitializeEventSystem();
            InitializeFloatingWindows();
            packetLogger = new PacketLoggerClient(AddChatLine, AddLoggerStatus, AddOrUpdatePlayer, RemovePlayer);
            Shown += async (_, _) => await packetLogger.StartAsync();
            FormClosed += (_, _) => packetLogger.Dispose();
            var statusTimer = new System.Windows.Forms.Timer { Interval = 5000 };
            statusTimer.Tick += async (_, _) =>
            {
                RefreshServerStatus();
                if (packetLogger is not null)
                    await packetLogger.StartAsync();
            };
            statusTimer.Start();
        }

        private void InitializeFloatingWindows()
        {
            mainTabs.TabPages.Remove(landTab);
            mainTabs.TabPages.Remove(logsTab);
            mainTabs.TabPages.Remove(eventTab);
            mainTabs.TabPages.Remove(miscTab);

            mainTabs.Appearance = TabAppearance.FlatButtons;
            mainTabs.ItemSize = new Size(0, 1);
            mainTabs.SizeMode = TabSizeMode.Fixed;
            mainTabs.Location = new Point(0, 36);
            mainTabs.Size = new Size(ClientSize.Width, ClientSize.Height - 36);
            mainTabs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainTabs.Dock = DockStyle.None;
            toolBarPanel.Visible = true;
            toolBarPanel.BringToFront();

            // Land controls used coordinates from the former full-page tab. Move the
            // complete old window to the top-left of its new compact tool window.
            foreach (Control control in landTab.Controls)
                control.Location = new Point(control.Left - 96, control.Top - 230);

            foreach (Label label in new[]
            {
                label5, label6, label7, label8, label9, label10, label11, label12,
                label13, label14, label15, label16
            })
                label.Location = new Point(label.Left - 3, label.Top - 4);

            onlinePlayersPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            chatPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            chatPanel.Width = 576;
            chatPanel.Height = 452;
            onlinePlayersPanel.Height = 452;
            onlinePlayersList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            playerDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chatLog.Dock = DockStyle.Fill;

            specialEventPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            eventDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            specialEventSelector.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            specialEventStatus.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            landWindow = new FloatingToolWindow("Land Settings", landTab, new Size(370, 205));
            logsWindow = new FloatingToolWindow("Logs", logsTab, new Size(920, 500));
            eventWindow = new FloatingToolWindow("Event System", eventTab, new Size(440, 360));
            miscWindow = new FloatingToolWindow("Miscs", miscTab, new Size(410, 270));

            areaEditorButton.Click += (_, _) => ToggleAreaEditorPanel();
            landSettingsButton.Click += (_, _) => landWindow.Toggle(this, new Point(380, 90));
            logsButton.Click += (_, _) => logsWindow.Toggle(this, new Point(150, 80));
            eventSystemButton.Click += (_, _) => eventWindow.Toggle(this, new Point(420, 120));
            miscsButton.Click += (_, _) => miscWindow.Toggle(this, new Point(500, 150));
            clearChatButton.Click += (_, _) => chatLog.Items.Clear();
            saveChatButton.Click += (_, _) => SaveChatLog();

            FormClosed += (_, _) =>
            {
                landWindow.Dispose();
                logsWindow.Dispose();
                eventWindow.Dispose();
                miscWindow.Dispose();
            };
        }

        private void ToggleAreaEditorPanel()
        {
            bool visible = !serverStatusPB.Visible;
            serverStatusPB.Visible = visible;
            label1.Visible = visible;
            label2.Visible = visible;
            label3.Visible = visible;
            label4.Visible = visible;
            serverNameLbl.Visible = visible;
            levelLbl.Visible = visible;
            ServerNameTextBox.Visible = visible;
            ServerNameEditButton.Visible = visible;
            ServerLevelUpDown.Visible = visible;
            ServerLevelEditButton.Visible = visible;
            helloMsgButton.Visible = visible;
            helloMsgTB.Visible = visible;
            helloMsgBtn.Visible = visible;
        }


        private void SaveChatLog()
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FileName = $"chat-log-{DateTime.Now:yyyy-MM-dd-HHmm}.txt"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            File.WriteAllLines(dialog.FileName, chatLog.Items.Cast<object>().Select(item => item.ToString() ?? string.Empty));
        }

        private void EnsureChatLogVisible()
        {
            chatPanel.Visible = true;
        }

        private void InitializeEventSystem()
        {
            applyEventButton.Click += (_, _) => ApplyEventDate();
            useRealDateButton.Click += (_, _) => DisableEventDate();
            specialEventSelector.Items.Add("NO EVENT");
            foreach (var item in SpecialEvents)
                specialEventSelector.Items.Add($"{item.Date}  {item.Name}");
            specialEventSelector.SelectedIndex = 0;
            specialEventSelector.SelectedIndexChanged += (_, _) =>
            {
                if (specialEventSelector.SelectedIndex == 0)
                    DisableEventDate();
            };
            DisableEventDate();
        }

        private static string EventDateFile =>
            @"C:\Users\PC\source\repos\AreaServerDataEditor\Debug\AreaServerEventDate.txt";

        private void ApplyEventDate()
        {
            if (specialEventSelector.SelectedIndex <= 0) return;
            var selected = SpecialEvents[specialEventSelector.SelectedIndex - 1];
            var answer = MessageBox.Show(
                "This change requires AREA SERVER to restart.\r\nAREA SERVER will be closed. Do you approve?",
                "Restart AREA SERVER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer != DialogResult.Yes) return;
            Directory.CreateDirectory(Path.GetDirectoryName(EventDateFile)!);
            File.WriteAllText(EventDateFile, selected.Date);
            if (Program.process is { HasExited: false })
            {
                try { Program.process.Kill(); } catch { }
            }
            specialEventStatus.Text = $"Active: {selected.Name}\r\nLobby date: {selected.Date}\r\nRestart AREA SERVER before reconnecting.";
            specialEventStatus.ForeColor = Color.Gold;
        }

        private void DisableEventDate()
        {
            if (File.Exists(EventDateFile)) File.Delete(EventDateFile);
            specialEventSelector.SelectedIndex = 0;
            specialEventStatus.Text = "Real system date is active.";
            specialEventStatus.ForeColor = Color.LightGreen;
        }

        private void LoadSavedEventDate()
        {
            if (!File.Exists(EventDateFile)) return;
            string date = File.ReadAllText(EventDateFile).Trim();
            int index = Array.FindIndex(SpecialEvents, item => item.Date == date);
            if (index < 0) return;
            specialEventSelector.SelectedIndex = index + 1;
            specialEventStatus.Text = $"Active: {SpecialEvents[index].Name}\r\nForced date: {date}";
            specialEventStatus.ForeColor = Color.Gold;
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
            miscPlayerCountLabel.Text = $"Online players: {onlinePlayers.Count}";
            ShowSelectedPlayer();
        }

        private void RemovePlayer(int socketId)
        {
            if (InvokeRequired) { BeginInvoke(() => RemovePlayer(socketId)); return; }
            if (!onlinePlayers.Remove(socketId)) return;
            onlinePlayersList.Items.Clear();
            foreach (PlayerProfile player in onlinePlayers.Values.OrderBy(p => p.Nickname))
                onlinePlayersList.Items.Add(player);
            miscPlayerCountLabel.Text = $"Online players: {onlinePlayers.Count}";
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
            if (level < 0) return;
            string visibleMessage = new string((message ?? string.Empty)
                .Where(c => !char.IsControl(c) || c == '\t')
                .ToArray()).Trim();
            if (visibleMessage.Length == 0) return;
            string levelText = $"Lv.{level}";
            string timestamp = chatTimestampsCheckBox.Checked ? $"[{DateTime.Now:HH:mm:ss}] " : string.Empty;
            chatLog.Items.Add($"{timestamp}[{nickname}] [{levelText}] : {visibleMessage}");
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
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            EnsureChatLogVisible();
            RefreshServerStatus();
        }

        private void RefreshServerStatus()
        {
            if (Program.process is null || Program.process.HasExited)
                Program.TryAttachToAreaServer();

            if (Program.process is null || Program.process.HasExited)
            {
                serverStatusPB.Image = Properties.Resources.bg_window_offline;
                serverNameLbl.Text = "Offline";
                levelLbl.Text = "0";
                return;
            }

            serverNameLbl.Text = Program.GetServerName(Program.processes) ?? "Unknown";
            levelLbl.Text = Program.GetServerLevel().ToString();
            serverStatusPB.Image = Properties.Resources.bg_window3;
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

        private void helloMsgButton_Click(object sender, EventArgs e)
        {

        }

        private void landSettingsButton_Click(object sender, EventArgs e)
        {

        }
    }
}
