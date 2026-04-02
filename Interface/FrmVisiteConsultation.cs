using Metier;

namespace Interface
{
    public partial class FrmVisiteConsultation : FrmBase
    {
        public FrmVisiteConsultation(Session uneSession) : base(uneSession)
        {
            InitializeComponent();
        }

        private void FrmVisiteConsultation_Load(object sender, EventArgs e)
        {
            parametrerComposant();
            centrerFormulaire();
            remplirDgvVisites();
        }

        private void parametrerComposant()
        {
            lblTitre.Text = "Consultation des visites";

            parametrerDgvVisites();
            parametrerDgvEchantillons();
            viderDetailsVisite();
        }

        private void centrerPanelCentral()
        {
            panelCentral.Left = Math.Max(0, (ClientSize.Width - panelCentral.Width) / 2);
            panelCentral.Top = lblTitre.Bottom;
        }

        private void parametrerDgvVisites()
        {
            dgvVisites.Columns.Clear();
            dgvVisites.Rows.Clear();

            dgvVisites.ReadOnly = true;
            dgvVisites.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVisites.MultiSelect = false;
            dgvVisites.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVisites.AllowUserToAddRows = false;
            dgvVisites.AllowUserToDeleteRows = false;
            dgvVisites.RowHeadersVisible = false;

            DataGridViewTextBoxColumn colVisite = new DataGridViewTextBoxColumn();
            colVisite.Name = "Visite";
            colVisite.HeaderText = "";
            colVisite.Visible = false;
            dgvVisites.Columns.Add(colVisite);

            DataGridViewTextBoxColumn colDate = new DataGridViewTextBoxColumn();
            colDate.Name = "Date";
            colDate.HeaderText = "Programmée le";
            dgvVisites.Columns.Add(colDate);

            DataGridViewTextBoxColumn colHeure = new DataGridViewTextBoxColumn();
            colHeure.Name = "Heure";
            colHeure.HeaderText = "à";
            colHeure.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colHeure.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVisites.Columns.Add(colHeure);

            DataGridViewTextBoxColumn colLieu = new DataGridViewTextBoxColumn();
            colLieu.Name = "Lieu";
            colLieu.HeaderText = "sur";
            dgvVisites.Columns.Add(colLieu);

            DataGridViewTextBoxColumn colPraticien = new DataGridViewTextBoxColumn();
            colPraticien.Name = "Praticien";
            colPraticien.HeaderText = "chez";
            dgvVisites.Columns.Add(colPraticien);

            foreach (DataGridViewColumn col in dgvVisites.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void parametrerDgvEchantillons()
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();

            dataGridView2.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.RowHeadersVisible = false;

            DataGridViewTextBoxColumn colMedicament = new DataGridViewTextBoxColumn();
            colMedicament.Name = "Medicament";
            colMedicament.HeaderText = "Médicament";
            dataGridView2.Columns.Add(colMedicament);

            DataGridViewTextBoxColumn colQuantite = new DataGridViewTextBoxColumn();
            colQuantite.Name = "Quantite";
            colQuantite.HeaderText = "Qté";
            colQuantite.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colQuantite.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns.Add(colQuantite);

            foreach (DataGridViewColumn col in dataGridView2.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void centrerFormulaire()
        {
            panelCentral.Left = (this.ClientSize.Width - panelCentral.Width) / 2;
        }

        private void remplirDgvVisites()
        {
            dgvVisites.Rows.Clear();
            foreach (Visite v in session.MesVisites.OrderBy(v => v.DateEtHeure))
            {
                int index = dgvVisites.Rows.Add(
                    v,
                    v.DateEtHeure.ToShortDateString(),
                    v.DateEtHeure.ToString("HH:mm"),
                    v.LePraticien.Ville,
                    v.LePraticien.NomPrenom);

                if (!string.IsNullOrWhiteSpace(v.Bilan))
                    dgvVisites.Rows[index].DefaultCellStyle.ForeColor = Color.SeaGreen;
            }

            if (dgvVisites.Rows.Count > 0)
            {
                dgvVisites.Rows[0].Selected = true;
                Visite visite = (Visite)dgvVisites.Rows[0].Cells["Visite"].Value;
                afficherDetailVisite(visite);
            }
            else
            {
                viderDetailsVisite();
            }
        }

        private void afficherDetailVisite(Visite visite)
        {
            lblPraticien.Text = visite.LePraticien.NomPrenom;
            lblRue.Text = $"{visite.LePraticien.Rue}, {visite.LePraticien.CodePostal} {visite.LePraticien.Ville}";
            lblTelephone.Text = visite.LePraticien.Telephone;
            lblEmail.Text = visite.LePraticien.Email;
            lblType.Text = visite.LePraticien.Type?.Libelle ?? "-";
            lblSpecialite.Text = visite.LePraticien.Specialite?.Libelle ?? "-";

            lblMotif.Text = visite.LeMotif.Libelle;
            label8.Text = string.IsNullOrWhiteSpace(visite.Bilan) ? "Aucun bilan" : visite.Bilan;

            var medicaments = new List<string>();
            if (visite.PremierMedicament is not null)
                medicaments.Add(visite.PremierMedicament.Id);
            if (visite.SecondMedicament is not null)
                medicaments.Add(visite.SecondMedicament.Id);
            lblMediacament.Text = medicaments.Count == 0 ? "Aucun" : string.Join(", ", medicaments);

            dataGridView2.Rows.Clear();
            foreach (var echantillon in visite)
                dataGridView2.Rows.Add(echantillon.Key.Id, echantillon.Value);
        }

        private void viderDetailsVisite()
        {
            lblPraticien.Text = string.Empty;
            lblRue.Text = string.Empty;
            lblTelephone.Text = string.Empty;
            lblEmail.Text = string.Empty;
            lblType.Text = string.Empty;
            lblSpecialite.Text = string.Empty;
            lblMotif.Text = string.Empty;
            label8.Text = string.Empty;
            lblMediacament.Text = string.Empty;
            dataGridView2.Rows.Clear();
        }


        private void lblTitre_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void lblTelephone_Click(object sender, EventArgs e)
        {

        }

        private void lblRue_Click(object sender, EventArgs e)
        {

        }

        private void dgvVisites_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignorer les clics sur l'en-tête
            if (e.RowIndex < 0) return;

            Visite visite = (Visite)dgvVisites.Rows[e.RowIndex].Cells["Visite"].Value;
            afficherDetailVisite(visite);
        }


        private void lblPraticien_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvVisites_CellContentClick(sender, e);
        }

        private void lblMediacament_Click(object sender, EventArgs e)
        {

        }
    }
}
