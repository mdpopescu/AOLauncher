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
            cbInstallations = new ComboBox();
            btnEditInstallations = new Button();
            panel1 = new Panel();
            btnEditAccounts = new Button();
            lbAccounts = new ListBox();
            label2 = new Label();
            btnLoginSelected = new Button();
            accountBindingSource1 = new BindingSource(components);
            accountBindingSource = new BindingSource(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)accountBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)accountBindingSource).BeginInit();
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
            // cbInstallations
            // 
            cbInstallations.DropDownStyle = ComboBoxStyle.DropDownList;
            cbInstallations.FormattingEnabled = true;
            cbInstallations.Location = new Point(141, 11);
            cbInstallations.Name = "cbInstallations";
            cbInstallations.Size = new Size(413, 26);
            cbInstallations.TabIndex = 2;
            cbInstallations.SelectedIndexChanged += cbInstallations_SelectedIndexChanged;
            // 
            // btnEditInstallations
            // 
            btnEditInstallations.Image = (Image)resources.GetObject("btnEditInstallations.Image");
            btnEditInstallations.Location = new Point(566, 11);
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
            panel1.Location = new Point(12, 54);
            panel1.Name = "panel1";
            panel1.Size = new Size(594, 351);
            panel1.TabIndex = 4;
            // 
            // btnEditAccounts
            // 
            btnEditAccounts.Image = (Image)resources.GetObject("btnEditAccounts.Image");
            btnEditAccounts.Location = new Point(128, 14);
            btnEditAccounts.Name = "btnEditAccounts";
            btnEditAccounts.Size = new Size(40, 26);
            btnEditAccounts.TabIndex = 4;
            btnEditAccounts.UseVisualStyleBackColor = true;
            btnEditAccounts.Click += btnEditAccounts_Click;
            // 
            // lbAccounts
            // 
            lbAccounts.FormattingEnabled = true;
            lbAccounts.ItemHeight = 18;
            lbAccounts.Location = new Point(20, 59);
            lbAccounts.Name = "lbAccounts";
            lbAccounts.ScrollAlwaysVisible = true;
            lbAccounts.SelectionMode = SelectionMode.MultiSimple;
            lbAccounts.Size = new Size(362, 238);
            lbAccounts.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 18);
            label2.Name = "label2";
            label2.Size = new Size(81, 18);
            label2.TabIndex = 2;
            label2.Text = "Accounts";
            // 
            // btnLoginSelected
            // 
            btnLoginSelected.Location = new Point(20, 309);
            btnLoginSelected.Margin = new Padding(4);
            btnLoginSelected.Name = "btnLoginSelected";
            btnLoginSelected.Size = new Size(208, 26);
            btnLoginSelected.TabIndex = 1;
            btnLoginSelected.Text = "Login Selected";
            btnLoginSelected.UseVisualStyleBackColor = true;
            btnLoginSelected.Click += btnLoginSelected_Click;
            // 
            // accountBindingSource1
            // 
            accountBindingSource1.DataSource = typeof(Models.Account);
            // 
            // accountBindingSource
            // 
            accountBindingSource.DataSource = typeof(Models.Account);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(618, 417);
            Controls.Add(panel1);
            Controls.Add(btnEditInstallations);
            Controls.Add(cbInstallations);
            Controls.Add(label1);
            Font = new Font("Verdana", 12F);
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "AOLauncher V1.0";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)accountBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)accountBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ComboBox cbInstallations;
        private Button btnEditInstallations;
        private Panel panel1;
        private ListBox lbAccounts;
        private Label label2;
        private Button btnLoginSelected;
        private Button btnEditAccounts;
        private BindingSource accountBindingSource1;
        private BindingSource accountBindingSource;
    }
}
