namespace AreaServerLevelEditor
{
    internal sealed class FloatingToolWindow : Form
    {
        private readonly Control content;

        public FloatingToolWindow(string title, Control content, Size initialSize)
        {
            this.content = content;
            Text = title;
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            SizeGripStyle = SizeGripStyle.Show;
            ShowInTaskbar = false;
            MinimumSize = new Size(320, 220);
            ClientSize = initialSize;

            content.Dock = DockStyle.Fill;
            if (content is TabPage tabPage)
            {
                var host = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = tabPage.BackColor,
                    BackgroundImage = tabPage.BackgroundImage,
                    BackgroundImageLayout = tabPage.BackgroundImageLayout,
                    Padding = tabPage.Padding
                };
                while (tabPage.Controls.Count > 0)
                    host.Controls.Add(tabPage.Controls[0]);
                Controls.Add(host);
                tabPage.Dispose();
            }
            else
            {
                Controls.Add(content);
            }

            FormClosing += (_, e) =>
            {
                if (e.CloseReason != CloseReason.FormOwnerClosing)
                {
                    e.Cancel = true;
                    Hide();
                }
            };
        }

        public void Toggle(Form owner, Point offset)
        {
            if (Visible)
            {
                Hide();
                return;
            }

            if (Owner is null)
                Owner = owner;

            if (Location == Point.Empty)
                Location = new Point(owner.Left + offset.X, owner.Top + offset.Y);

            Show(owner);
            BringToFront();
        }
    }
}
