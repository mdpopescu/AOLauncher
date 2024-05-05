using AOLauncher.Library.Models;

namespace AOLauncher
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            label1 = new Label();
            rbRk5 = new RadioButton();
            rbRk19 = new RadioButton();
            cbInstallations = new ComboBox();
            btnEditInstallations = new Button();
            panel1 = new Panel();
            btnEditAccounts = new Button();
            lbAccounts = new ListBox();
            label2 = new Label();
            btnLoginSelected = new Button();
            niMain = new NotifyIcon(components);
            statusStrip1 = new StatusStrip();
            tslNotification = new ToolStripStatusLabel();
            panel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(103, 18);
            label1.TabIndex = 1;
            label1.Text = "Installation";
            // 
            // rbRk5
            // 
            rbRk5.Checked = true;
            rbRk5.Location = new Point(141, 43);
            rbRk5.Name = "rbRk5";
            rbRk5.Size = new Size(100, 29);
            rbRk5.TabIndex = 2;
            rbRk5.TabStop = true;
            rbRk5.Text = "Rubi-Ka";
            // 
            // rbRk19
            // 
            rbRk19.Location = new Point(255, 43);
            rbRk19.Name = "rbRk19";
            rbRk19.Size = new Size(143, 29);
            rbRk19.TabIndex = 2;
            rbRk19.Text = "Rubi-Ka 2019";
            // 
            // cbInstallations
            // 
            cbInstallations.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbInstallations.DropDownStyle = ComboBoxStyle.DropDownList;
            cbInstallations.FormattingEnabled = true;
            cbInstallations.Location = new Point(141, 11);
            cbInstallations.Name = "cbInstallations";
            cbInstallations.Size = new Size(257, 26);
            cbInstallations.TabIndex = 2;
            cbInstallations.SelectedIndexChanged += cbInstallations_SelectedIndexChanged;
            // 
            // btnEditInstallations
            // 
            btnEditInstallations.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditInstallations.Image = (Image)resources.GetObject("btnEditInstallations.Image");
            btnEditInstallations.Location = new Point(410, 11);
            btnEditInstallations.Name = "btnEditInstallations";
            btnEditInstallations.Size = new Size(40, 26);
            btnEditInstallations.TabIndex = 3;
            btnEditInstallations.UseVisualStyleBackColor = true;
            btnEditInstallations.Click += btnEditInstallations_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btnEditAccounts);
            panel1.Controls.Add(lbAccounts);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnLoginSelected);
            panel1.Location = new Point(0, 78);
            panel1.Name = "panel1";
            panel1.Size = new Size(462, 314);
            panel1.TabIndex = 4;
            // 
            // btnEditAccounts
            // 
            btnEditAccounts.Image = (Image)resources.GetObject("btnEditAccounts.Image");
            btnEditAccounts.Location = new Point(128, 10);
            btnEditAccounts.Name = "btnEditAccounts";
            btnEditAccounts.Size = new Size(40, 26);
            btnEditAccounts.TabIndex = 4;
            btnEditAccounts.UseVisualStyleBackColor = true;
            btnEditAccounts.Click += btnEditAccounts_Click;
            // 
            // lbAccounts
            // 
            lbAccounts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbAccounts.FormattingEnabled = true;
            lbAccounts.ItemHeight = 18;
            lbAccounts.Location = new Point(11, 41);
            lbAccounts.Name = "lbAccounts";
            lbAccounts.ScrollAlwaysVisible = true;
            lbAccounts.SelectionMode = SelectionMode.MultiExtended;
            lbAccounts.Size = new Size(437, 256);
            lbAccounts.TabIndex = 3;
            lbAccounts.DoubleClick += lbAccounts_DoubleClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 14);
            label2.Name = "label2";
            label2.Size = new Size(81, 18);
            label2.TabIndex = 2;
            label2.Text = "Accounts";
            // 
            // btnLoginSelected
            // 
            btnLoginSelected.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoginSelected.Location = new Point(288, 10);
            btnLoginSelected.Margin = new Padding(4);
            btnLoginSelected.Name = "btnLoginSelected";
            btnLoginSelected.Size = new Size(160, 26);
            btnLoginSelected.TabIndex = 1;
            btnLoginSelected.Text = "Login Selected";
            btnLoginSelected.UseVisualStyleBackColor = true;
            btnLoginSelected.Click += btnLoginSelected_Click;
            // 
            // niMain
            // 
            niMain.Icon = (Icon)resources.GetObject("niMain.Icon");
            niMain.Text = "AO Launcher";
            niMain.Visible = true;
            niMain.DoubleClick += niMain_DoubleClick;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tslNotification });
            statusStrip1.Location = new Point(0, 395);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(462, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // tslNotification
            // 
            tslNotification.Name = "tslNotification";
            tslNotification.Size = new Size(23, 17);
            tslNotification.Text = "OK";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(462, 417);
            Controls.Add(statusStrip1);
            Controls.Add(panel1);
            Controls.Add(btnEditInstallations);
            Controls.Add(cbInstallations);
            Controls.Add(rbRk19);
            Controls.Add(rbRk5);
            Controls.Add(label1);
            Font = new Font("Verdana", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MinimumSize = new Size(478, 0);
            Name = "MainForm";
            Text = "AO Launcher V1.0";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private RadioButton rbRk5;
        private RadioButton rbRk19;
        private ComboBox cbInstallations;
        private Button btnEditInstallations;
        private Panel panel1;
        private ListBox lbAccounts;
        private Label label2;
        private Button btnLoginSelected;
        private Button btnEditAccounts;
        private NotifyIcon niMain;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tslNotification;
    }
}
