namespace Interface
{
    partial class FrmVisiteAjout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVisiteAjout));
            panelDroite = new Panel();
            panel2 = new Panel();
            btnAjouter = new Button();
            dtpDate = new DateTimePicker();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            cbxMotif = new ComboBox();
            cbxPraticien = new ComboBox();
            label1 = new Label();
            panel3 = new Panel();
            dgvVisites = new DataGridView();
            label5 = new Label();
            panelDroite.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVisites).BeginInit();
            SuspendLayout();
            // 
            // lblTitre
            // 
            lblTitre.Size = new Size(940, 74);
            lblTitre.Click += lblTitre_Click;
            // 
            // panelDroite
            // 
            panelDroite.AutoSize = true;
            panelDroite.Controls.Add(panel2);
            panelDroite.Dock = DockStyle.Right;
            panelDroite.Location = new Point(533, 98);
            panelDroite.Name = "panelDroite";
            panelDroite.Size = new Size(407, 406);
            panelDroite.TabIndex = 13;
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.Controls.Add(btnAjouter);
            panel2.Controls.Add(dtpDate);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(cbxMotif);
            panel2.Controls.Add(cbxPraticien);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(6, 10);
            panel2.Name = "panel2";
            panel2.Size = new Size(398, 396);
            panel2.TabIndex = 0;
            // 
            // btnAjouter
            // 
            btnAjouter.BackColor = Color.Red;
            btnAjouter.Location = new Point(170, 232);
            btnAjouter.Name = "btnAjouter";
            btnAjouter.Size = new Size(181, 35);
            btnAjouter.TabIndex = 7;
            btnAjouter.Text = "Ajouter";
            btnAjouter.UseVisualStyleBackColor = false;
            btnAjouter.Click += btnAjouter_Click;
            // 
            // dtpDate
            // 
            dtpDate.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.Location = new Point(170, 182);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(181, 23);
            dtpDate.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(46, 188);
            label4.Name = "label4";
            label4.Size = new Size(77, 15);
            label4.TabIndex = 5;
            label4.Text = "Date et heure";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(46, 131);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 4;
            label3.Text = "Motif";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(46, 79);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 3;
            label2.Text = "Praticien";
            // 
            // cbxMotif
            // 
            cbxMotif.FormattingEnabled = true;
            cbxMotif.Location = new Point(170, 128);
            cbxMotif.Name = "cbxMotif";
            cbxMotif.Size = new Size(181, 23);
            cbxMotif.TabIndex = 2;
            // 
            // cbxPraticien
            // 
            cbxPraticien.FormattingEnabled = true;
            cbxPraticien.Location = new Point(170, 76);
            cbxPraticien.Name = "cbxPraticien";
            cbxPraticien.Size = new Size(181, 23);
            cbxPraticien.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(46, 25);
            label1.Name = "label1";
            label1.Size = new Size(121, 15);
            label1.TabIndex = 0;
            label1.Text = "Nouveau rendez vous";
            // 
            // panel3
            // 
            panel3.AutoSize = true;
            panel3.Controls.Add(dgvVisites);
            panel3.Controls.Add(label5);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 98);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(10);
            panel3.Size = new Size(533, 406);
            panel3.TabIndex = 14;
            // 
            // dgvVisites
            // 
            dgvVisites.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVisites.Dock = DockStyle.Fill;
            dgvVisites.Location = new Point(10, 33);
            dgvVisites.Name = "dgvVisites";
            dgvVisites.Size = new Size(513, 363);
            dgvVisites.TabIndex = 1;
            // 
            // label5
            // 
            label5.Dock = DockStyle.Top;
            label5.Location = new Point(10, 10);
            label5.Name = "label5";
            label5.Size = new Size(513, 23);
            label5.TabIndex = 0;
            label5.Text = "Liste des rendez-vous déjà programmés";
            // 
            // FrmVisiteAjout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(940, 549);
            Controls.Add(panel3);
            Controls.Add(panelDroite);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Location = new Point(0, 0);
            Name = "FrmVisiteAjout";
            Text = "FrmVisiteAjout";
            Load += FrmVisiteAjout_Load;
            Controls.SetChildIndex(lblTitre, 0);
            Controls.SetChildIndex(panelDroite, 0);
            Controls.SetChildIndex(panel3, 0);
            panelDroite.ResumeLayout(false);
            panelDroite.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVisites).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelDroite;
        private Panel panel2;
        private Label label2;
        private ComboBox cbxMotif;
        private ComboBox cbxPraticien;
        private Label label1;
        private Label label3;
        private Button btnAjouter;
        private DateTimePicker dtpDate;
        private Label label4;
        private Panel panel3;
        private DataGridView dgvVisites;
        private Label label5;
    }
}