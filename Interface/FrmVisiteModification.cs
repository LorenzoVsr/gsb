using Donnee;
using Metier;
using Interface.Properties;
using System.Data;

namespace Interface
{
    public partial class FrmVisiteModification : FrmBase
    {
        public FrmVisiteModification(Session uneSession) : base(uneSession)
        {
            InitializeComponent();
        }

        private void FrmVisiteModification_Load(object sender, EventArgs e)
        {
            parametrerComposant();
        }

        private void parametrerComposant()
        {
            lblTitre.Text = "Modifier une visite";
            parametrerDgv(dgvVisites);
            remplirDgv();
        }

        private void parametrerDgv(DataGridView dgv)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            // Comportement général
            dgv.Enabled = true;
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.BackgroundColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Georgia", 11);
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToAddRows = false;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // En-têtes de colonnes
            dgv.ColumnHeadersVisible = true;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Georgia", 12, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 40;

            // Lignes
            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 30;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.White;
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

            // Colonne 0 : objet Visite (cachée)
            DataGridViewTextBoxColumn colVisite = new DataGridViewTextBoxColumn();
            colVisite.Name = "Visite";
            colVisite.HeaderText = "";
            colVisite.Visible = false;
            dgv.Columns.Add(colVisite);

            // Colonne 1 : bouton supprimer (image)
            DataGridViewImageColumn colSupprimer = new DataGridViewImageColumn();
            colSupprimer.Name = "Supprimer";
            colSupprimer.HeaderText = "";
            colSupprimer.Image = Resources.supprimer;
            colSupprimer.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSupprimer.Width = 50;
            colSupprimer.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(colSupprimer);

            // Colonne 2 : date
            DataGridViewTextBoxColumn colDate = new DataGridViewTextBoxColumn();
            colDate.Name = "Date";
            colDate.HeaderText = "Programmée le";
            dgv.Columns.Add(colDate);

            // Colonne 3 : heure
            DataGridViewTextBoxColumn colHeure = new DataGridViewTextBoxColumn();
            colHeure.Name = "Heure";
            colHeure.HeaderText = "à";
            dgv.Columns.Add(colHeure);

            // Colonne 4 : ville
            DataGridViewTextBoxColumn colLieu = new DataGridViewTextBoxColumn();
            colLieu.Name = "Lieu";
            colLieu.HeaderText = "sur";
            dgv.Columns.Add(colLieu);

            // Colonne 5 : praticien
            DataGridViewTextBoxColumn colPraticien = new DataGridViewTextBoxColumn();
            colPraticien.Name = "Praticien";
            colPraticien.HeaderText = "chez";
            dgv.Columns.Add(colPraticien);

            // Désactiver le tri sur toutes les colonnes
            foreach (DataGridViewColumn col in dgv.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void remplirDgv()
        {
            dgvVisites.Rows.Clear();
            foreach (Visite v in session.MesVisites.Where(v => v.Bilan is null).OrderBy(v => v.DateEtHeure))
            {
                int i = dgvVisites.Rows.Add();
                DataGridViewRow row = dgvVisites.Rows[i];

                row.Cells["Visite"].Value = v;
                row.Cells["Supprimer"].Value = Resources.supprimer;
                row.Cells["Date"].Value = v.DateEtHeure.ToShortDateString();
                row.Cells["Heure"].Value = v.DateEtHeure.ToString("HH:mm");
                row.Cells["Lieu"].Value = v.LePraticien.Ville;
                row.Cells["Praticien"].Value = v.LePraticien.NomPrenom;
            }
        }

        private void dgvVisites_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignorer les clics sur l'en-tête
            if (e.RowIndex < 0) return;

            Visite visite = (Visite)dgvVisites.Rows[e.RowIndex].Cells["Visite"].Value;

            // Clic sur le bouton supprimer
            if (e.ColumnIndex == dgvVisites.Columns["Supprimer"].Index)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette visite ?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                try
                {
                    Passerelle.supprimerRendezVous(visite.Id);
                    session.MesVisites.Remove(visite);
                    remplirDgv();
                    MessageBox.Show("La visite a été supprimée avec succès.", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                // Clic sur une autre colonne : afficher les détails
                lblNom.Text = visite.LePraticien.NomPrenom;
                lblDate.Text = visite.DateEtHeure.ToString("dd/MM/yyyy HH:mm");
                dtpDate.Value = visite.DateEtHeure;
            }
        }

        // Clic sur le bouton "modifier"
        private void btnModifier_Click(object sender, EventArgs e) {
            if (dgvVisites.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une visite à modifier.", "Aucune sélection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Visite visite = (Visite)dgvVisites.SelectedRows[0].Cells["Visite"].Value;
            try
            {
                DateTime nouvelleDate = dtpDate.Value;
                Passerelle.modifierRendezVous(visite.Id, nouvelleDate);
                visite.deplacer(nouvelleDate);
                remplirDgv();
                MessageBox.Show("La visite a été modifiée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblTitre_Click(object sender, EventArgs e) { }

        private void panelDroite_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}