namespace Interface
{
    partial class FrmVisiteModification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVisiteModification));
            panelDroite = new Panel();
            btnModifier = new Button();
            dtpDate = new DateTimePicker();
            label4 = new Label();
            lblDate = new Label();
            label2 = new Label();
            lblNom = new Label();
            label1 = new Label();
            panelGauche = new Panel();
            dgvVisites = new DataGridView();
            Colonne0 = new DataGridViewButtonColumn();
            Colonne1 = new DataGridViewImageColumn();
            Colonne2 = new DataGridViewTextBoxColumn();
            Colonne3 = new DataGridViewTextBoxColumn();
            Colonne4 = new DataGridViewTextBoxColumn();
            Colonne5 = new DataGridViewTextBoxColumn();
            label3 = new Label();
            panelDroite.SuspendLayout();
            panelGauche.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVisites).BeginInit();
            SuspendLayout();
            // 
            // lblTitre
            // 
            lblTitre.Size = new Size(1281, 74);
            lblTitre.Click += lblTitre_Click;
            // 
            // panelDroite
            // 
            panelDroite.Controls.Add(btnModifier);
            panelDroite.Controls.Add(dtpDate);
            panelDroite.Controls.Add(label4);
            panelDroite.Controls.Add(lblDate);
            panelDroite.Controls.Add(label2);
            panelDroite.Controls.Add(lblNom);
            panelDroite.Controls.Add(label1);
            panelDroite.Dock = DockStyle.Right;
            panelDroite.Location = new Point(855, 98);
            panelDroite.Name = "panelDroite";
            panelDroite.Size = new Size(426, 370);
            panelDroite.TabIndex = 13;
            // 
            // btnModifier
            // 
            btnModifier.BackColor = Color.Red;
            btnModifier.Location = new Point(234, 236);
            btnModifier.Name = "btnModifier";
            btnModifier.Size = new Size(96, 30);
            btnModifier.TabIndex = 6;
            btnModifier.Text = "Modifier";
            btnModifier.UseVisualStyleBackColor = false;
            // 
            // dtpDate
            // 
            dtpDate.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.Location = new Point(43, 198);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(200, 23);
            dtpDate.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(43, 168);
            label4.Name = "label4";
            label4.Size = new Size(65, 15);
            label4.TabIndex = 4;
            label4.Text = "est remi au";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDate.Location = new Point(43, 138);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(175, 15);
            lblDate.TabIndex = 3;
            lblDate.Text = "Date et heure du rendez-vous";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 108);
            label2.Name = "label2";
            label2.Size = new Size(115, 15);
            label2.TabIndex = 2;
            label2.Text = "prévu initialement le";
            // 
            // lblNom
            // 
            lblNom.AutoSize = true;
            lblNom.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNom.Location = new Point(43, 78);
            lblNom.Name = "lblNom";
            lblNom.Size = new Size(103, 15);
            lblNom.TabIndex = 1;
            lblNom.Text = "Nom du praticien";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 48);
            label1.Name = "label1";
            label1.Size = new Size(114, 15);
            label1.TabIndex = 0;
            label1.Text = "Le rendez-vous chez";
            // 
            // panelGauche
            // 
            panelGauche.Controls.Add(dgvVisites);
            panelGauche.Controls.Add(label3);
            panelGauche.Dock = DockStyle.Fill;
            panelGauche.Location = new Point(0, 98);
            panelGauche.Name = "panelGauche";
            panelGauche.Padding = new Padding(10);
            panelGauche.Size = new Size(855, 370);
            panelGauche.TabIndex = 14;
            // 
            // dgvVisites
            // 
            dgvVisites.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVisites.Columns.AddRange(new DataGridViewColumn[] { Colonne0, Colonne1, Colonne2, Colonne3, Colonne4, Colonne5 });
            dgvVisites.Dock = DockStyle.Fill;
            dgvVisites.Location = new Point(10, 25);
            dgvVisites.Name = "dgvVisites";
            dgvVisites.Size = new Size(835, 335);
            dgvVisites.TabIndex = 1;
            dgvVisites.CellContentClick += dgvVisites_CellContentClick;
            // 
            // Colonne0
            // 
            Colonne0.HeaderText = "objet visite";
            Colonne0.Name = "Colonne0";
            Colonne0.Resizable = DataGridViewTriState.True;
            Colonne0.SortMode = DataGridViewColumnSortMode.Automatic;
            Colonne0.Visible = false;
            // 
            // Colonne1
            // 
            Colonne1.HeaderText = "";
            Colonne1.Name = "Colonne1";
            Colonne1.Resizable = DataGridViewTriState.True;
            Colonne1.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Colonne2
            // 
            Colonne2.HeaderText = "Programmée le ";
            Colonne2.Name = "Colonne2";
            // 
            // Colonne3
            // 
            Colonne3.HeaderText = "à";
            Colonne3.Name = "Colonne3";
            // 
            // Colonne4
            // 
            Colonne4.HeaderText = "sur";
            Colonne4.Name = "Colonne4";
            // 
            // Colonne5
            // 
            Colonne5.HeaderText = "chez";
            Colonne5.Name = "Colonne5";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Top;
            label3.Location = new Point(10, 10);
            label3.Name = "label3";
            label3.Size = new Size(335, 15);
            label3.TabIndex = 0;
            label3.Text = "Sélectionner une visite afin de modifier la date du rendez-vous";
            // 
            // FrmVisiteModification
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1281, 513);
            Controls.Add(panelGauche);
            Controls.Add(panelDroite);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Location = new Point(0, 0);
            Name = "FrmVisiteModification";
            Text = "FrmVisiteModification";
            Load += FrmVisiteModification_Load;
            Controls.SetChildIndex(lblTitre, 0);
            Controls.SetChildIndex(panelDroite, 0);
            Controls.SetChildIndex(panelGauche, 0);
            panelDroite.ResumeLayout(false);
            panelDroite.PerformLayout();
            panelGauche.ResumeLayout(false);
            panelGauche.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVisites).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelDroite;
        private Label label1;
        private Panel panelGauche;
        private Label label4;
        private Label lblDate;
        private Label label2;
        private Label lblNom;
        private Button btnModifier;
        private DateTimePicker dtpDate;
        private DataGridView dgvVisites;
        private Label label3;
        private DataGridViewButtonColumn Colonne0;
        private DataGridViewImageColumn Colonne1;
        private DataGridViewTextBoxColumn Colonne2;
        private DataGridViewTextBoxColumn Colonne3;
        private DataGridViewTextBoxColumn Colonne4;
        private DataGridViewTextBoxColumn Colonne5;
    }
}