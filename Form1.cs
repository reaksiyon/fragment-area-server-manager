namespace AreaServerLevelEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ServerNameEditButton_Click(object sender, EventArgs e)
        {
            Program.SetServerName(ServerNameTextBox.Text);

            serverNameLbl.Text = Program.GetServerName(Program.processes);
            MessageBox.Show(Program.GetServerName(Program.processes));
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
    }
}
