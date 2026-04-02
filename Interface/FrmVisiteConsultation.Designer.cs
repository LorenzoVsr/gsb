namespace Interface
{
    partial class FrmVisiteConsultation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVisiteConsultation));
            panelCentral = new Panel();
            lblMotif = new Label();
            label6 = new Label();
            label7 = new Label();
            label5 = new Label();
            panel5 = new Panel();
            label8 = new Label();
            panel4 = new Panel();
            lblMediacament = new Label();
            label4 = new Label();
            panel3 = new Panel();
            lblSpecialite = new Label();
            lblType = new Label();
            lblEmail = new Label();
            lblTelephone = new Label();
            lblRue = new Label();
            lblPraticien = new Label();
            dataGridView2 = new DataGridView();
            label2 = new Label();
            label1 = new Label();
            dgvVisites = new DataGridView();
            panelCentral.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvVisites).BeginInit();
            SuspendLayout();
            // 
            // lblTitre
            // 
            lblTitre.Size = new Size(1280, 74);
            lblTitre.Click += lblTitre_Click;
            // 
            // panelCentral
            // 
            panelCentral.Anchor = AnchorStyles.Top;
            panelCentral.BorderStyle = BorderStyle.FixedSingle;
            panelCentral.Controls.Add(lblMotif);
            panelCentral.Controls.Add(label6);
            panelCentral.Controls.Add(label7);
            panelCentral.Controls.Add(label5);
            panelCentral.Controls.Add(panel5);
            panelCentral.Controls.Add(panel4);
            panelCentral.Controls.Add(label4);
            panelCentral.Controls.Add(panel3);
            panelCentral.Controls.Add(dataGridView2);
            panelCentral.Controls.Add(label2);
            panelCentral.Controls.Add(label1);
            panelCentral.Controls.Add(dgvVisites);
            panelCentral.Location = new Point(20, 89);
            panelCentral.Name = "panelCentral";
            panelCentral.Size = new Size(1140, 620);
            panelCentral.TabIndex = 13;
            panelCentral.Paint += panel2_Paint;
            // 
            // lblMotif
            // 
            lblMotif.AutoSize = true;
            lblMotif.Location = new Point(507, 150);
            lblMotif.Name = "lblMotif";
            lblMotif.Size = new Size(0, 15);
            lblMotif.TabIndex = 19;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(449, 173);
            label6.Name = "label6";
            label6.Size = new Size(96, 15);
            label6.TabIndex = 1;
            label6.Text = "Bilan de la visite";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(449, 379);
            label7.Name = "label7";
            label7.Size = new Size(140, 15);
            label7.TabIndex = 2;
            label7.Text = "Médicaments présentés";
            label7.Click += label7_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(449, 150);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 0;
            label5.Text = "Motif";
            label5.Click += label5_Click;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(label8);
            panel5.Location = new Point(449, 206);
            panel5.Name = "panel5";
            panel5.Size = new Size(430, 170);
            panel5.TabIndex = 0;
            // 
            // label8
            // 
            label8.Dock = DockStyle.Fill;
            label8.Location = new Point(0, 0);
            label8.Name = "label8";
            label8.Size = new Size(428, 168);
            label8.TabIndex = 0;
            label8.Text = "lblBilan";
            label8.Click += label8_Click;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(lblMediacament);
            panel4.Location = new Point(449, 402);
            panel4.Name = "panel4";
            panel4.Size = new Size(214, 75);
            panel4.TabIndex = 1;
            panel4.Paint += panel4_Paint;
            // 
            // lblMediacament
            // 
            lblMediacament.Dock = DockStyle.Fill;
            lblMediacament.Location = new Point(0, 0);
            lblMediacament.Name = "lblMediacament";
            lblMediacament.Size = new Size(212, 73);
            lblMediacament.TabIndex = 6;
            lblMediacament.Text = "lblMediacament";
            lblMediacament.Click += lblMediacament_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(890, 173);
            label4.Name = "label4";
            label4.Size = new Size(114, 15);
            label4.TabIndex = 1;
            label4.Text = "Echantillions fournis";
            label4.Click += label4_Click;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(lblSpecialite);
            panel3.Controls.Add(lblType);
            panel3.Controls.Add(lblEmail);
            panel3.Controls.Add(lblTelephone);
            panel3.Controls.Add(lblRue);
            panel3.Controls.Add(lblPraticien);
            panel3.Location = new Point(449, 45);
            panel3.Name = "panel3";
            panel3.Size = new Size(686, 102);
            panel3.TabIndex = 18;
            // 
            // lblSpecialite
            // 
            lblSpecialite.AutoSize = true;
            lblSpecialite.Location = new Point(250, 35);
            lblSpecialite.Name = "lblSpecialite";
            lblSpecialite.Size = new Size(70, 15);
            lblSpecialite.TabIndex = 5;
            lblSpecialite.Text = "lblSpecialite";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(250, 13);
            lblType.Name = "lblType";
            lblType.Size = new Size(44, 15);
            lblType.TabIndex = 4;
            lblType.Text = "lblType";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(19, 65);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(49, 15);
            lblEmail.TabIndex = 3;
            lblEmail.Text = "lblEmail";
            lblEmail.Click += label10_Click;
            // 
            // lblTelephone
            // 
            lblTelephone.AutoSize = true;
            lblTelephone.Location = new Point(19, 50);
            lblTelephone.Name = "lblTelephone";
            lblTelephone.Size = new Size(74, 15);
            lblTelephone.TabIndex = 2;
            lblTelephone.Text = "lblTelephone";
            lblTelephone.Click += lblTelephone_Click;
            // 
            // lblRue
            // 
            lblRue.AutoSize = true;
            lblRue.Location = new Point(18, 35);
            lblRue.Name = "lblRue";
            lblRue.Size = new Size(40, 15);
            lblRue.TabIndex = 1;
            lblRue.Text = "lblRue";
            lblRue.Click += lblRue_Click;
            // 
            // lblPraticien
            // 
            lblPraticien.AutoSize = true;
            lblPraticien.Location = new Point(19, 13);
            lblPraticien.Name = "lblPraticien";
            lblPraticien.Size = new Size(66, 15);
            lblPraticien.TabIndex = 0;
            lblPraticien.Text = "lblPraticien";
            lblPraticien.Click += lblPraticien_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(890, 206);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(245, 271);
            dataGridView2.TabIndex = 17;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(449, 27);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 16;
            label2.Text = "Praticien";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 9);
            label1.Name = "label1";
            label1.Size = new Size(229, 15);
            label1.TabIndex = 15;
            label1.Text = "Sélectionner la visite pour afficher le détail";
            label1.Click += label1_Click;
            // 
            // dgvVisites
            // 
            dgvVisites.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvVisites.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVisites.Location = new Point(27, 38);
            dgvVisites.Name = "dgvVisites";
            dgvVisites.Size = new Size(416, 555);
            dgvVisites.TabIndex = 14;
            dgvVisites.CellContentClick += dataGridView1_CellContentClick;
            // 
            // FrmVisiteConsultation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 780);
            Controls.Add(panelCentral);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Location = new Point(0, 0);
            Name = "FrmVisiteConsultation";
            Text = "FrmVisiteConsultation";
            Load += FrmVisiteConsultation_Load;
            Controls.SetChildIndex(lblTitre, 0);
            Controls.SetChildIndex(panelCentral, 0);
            panelCentral.ResumeLayout(false);
            panelCentral.PerformLayout();
            panel5.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvVisites).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Panel panelCentral;
        private Label label1;
        private DataGridView dgvVisites;
        private Label label2;
        private Panel panel3;
        private Label lblPraticien;
        private DataGridView dataGridView2;
        private Label label4;
        private Panel panel4;
        private Panel panel5;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label lblMotif;
        private Label label8;
        private Label lblType;
        private Label lblEmail;
        private Label lblTelephone;
        private Label lblRue;
        private Label lblSpecialite;
        private Label lblMediacament;
    }
}