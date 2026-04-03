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
            this.Resize += FrmVisiteConsultation_Resize;
            remplirDgvVisites();
        }

        private void FrmVisiteConsultation_Resize(object? sender, EventArgs e)
        {
            centrerFormulaire();
        }

        private void parametrerComposant()
        {
            lblTitre.Text = "Consultation des visites";

            parametrerDgv(dgvVisites);
            parametrerDgvEchantillons();
            dgvVisites.SelectionChanged += dgvVisites_SelectionChanged;
            ViderAffichage();
        }

        private void parametrerDgv(DataGridView dgv)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            dgv.Enabled = true;
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.BackgroundColor = Color.White;
            dgv.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Georgia", 11);
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowDrop = false;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersVisible = true;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Georgia", 12, FontStyle.Bold);
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 40;
            dgv.RowTemplate.Height = 30;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.Lavender;
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

            DataGridViewTextBoxColumn colVisite = new DataGridViewTextBoxColumn();
            colVisite.Name = "Visite";
            colVisite.HeaderText = "";
            colVisite.Visible = false;
            dgv.Columns.Add(colVisite);

            DataGridViewTextBoxColumn colDate = new DataGridViewTextBoxColumn();
            colDate.Name = "Date";
            colDate.HeaderText = "Programmée le";
            colDate.FillWeight = 50;
            colDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns.Add(colDate);

            DataGridViewTextBoxColumn colHeure = new DataGridViewTextBoxColumn();
            colHeure.Name = "Heure";
            colHeure.HeaderText = "à";
            colHeure.FillWeight = 20;
            colHeure.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colHeure.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(colHeure);

            DataGridViewTextBoxColumn colLieu = new DataGridViewTextBoxColumn();
            colLieu.Name = "Lieu";
            colLieu.HeaderText = "sur";
            colLieu.FillWeight = 30;
            colLieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns.Add(colLieu);

            foreach (DataGridViewColumn col in dgv.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

        }

        private void parametrerDgvEchantillons()
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();

            dataGridView2.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.RowHeadersVisible = false;

            DataGridViewTextBoxColumn colMedicament = new DataGridViewTextBoxColumn();
            colMedicament.Name = "Medicament";
            colMedicament.HeaderText = "Médicament";
            colMedicament.Width = 170;
            dataGridView2.Columns.Add(colMedicament);

            DataGridViewTextBoxColumn colQuantite = new DataGridViewTextBoxColumn();
            colQuantite.Name = "Quantite";
            colQuantite.HeaderText = "Qté";
            colQuantite.Width = 75;
            colQuantite.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colQuantite.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns.Add(colQuantite);

            foreach (DataGridViewColumn col in dataGridView2.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

        }

        private void centrerFormulaire()
        {
            panelCentral.Left = (this.ClientSize.Width - panelCentral.Width) / 2;
            panelCentral.Top = lblTitre.Bottom;
        }

        private void remplirDgvVisites()
        {
            dgvVisites.Rows.Clear();
            foreach (Visite v in session.MesVisites.OrderBy(v => v.DateEtHeure))
            {
                int index = dgvVisites.Rows.Add(
                    v,
                    v.DateEtHeure.ToLongDateString(),
                    v.DateEtHeure.ToString("HH:mm"),
                    v.LePraticien.Ville);

                if (v.DateEtHeure < DateTime.Now)
                    dgvVisites.Rows[index].DefaultCellStyle.ForeColor = Color.SeaGreen;
            }

            if (dgvVisites.Rows.Count > 0)
            {
                dgvVisites.Rows[0].Selected = true;
                afficher();
            }
            else
            {
                ViderAffichage();
            }
        }

        private void afficher()
        {
            Visite? visite = getVisite();
            if (visite is null)
            {
                ViderAffichage();
                return;
            }

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

        private void ViderAffichage()
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

        private Visite? getVisite()
        {
            if (dgvVisites.SelectedRows.Count == 0)
                return null;

            return dgvVisites.SelectedRows[0].Cells["Visite"].Value as Visite;
        }


        private void dgvVisites_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignorer les clics sur l'en-tête
            if (e.RowIndex < 0) return;

            afficher();
        }

        private void dgvVisites_SelectionChanged(object? sender, EventArgs e)
        {
            afficher();
        }
    }
}
