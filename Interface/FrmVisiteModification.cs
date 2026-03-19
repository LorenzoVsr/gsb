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

        #region procédures événementielles

        private void FrmVisiteModification_Load(object sender, EventArgs e)
        {
            parametrerComposant();
        }

        #endregion

        #region procédures

        private void parametrerComposant()
        {
            this.lblTitre.Text = "Modifier une visite";
            parametrerDgv(dgvVisites);
            remplirDgv();
        }

        private void parametrerDgv(DataGridView dgv)
        {
            // initialisation du datagridview : suppression des colonnes et des lignes ajoutées par défaut
            dgv.Rows.Clear();

            #region paramètrage concernant le datagridview dans son ensemble

            // Accessibilité
            dgv.Enabled = true;

            // style de bordure
            dgv.BorderStyle = BorderStyle.FixedSingle;

            // couleur de fond 
            dgv.BackgroundColor = Color.White;

            // couleur de texte  
            dgv.ForeColor = Color.Black;

            // police de caractères par défaut
            dgv.DefaultCellStyle.Font = new Font("Georgia", 11);

            // mode de sélection : FullRowSelect
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // sélection multiple 
            dgv.MultiSelect = false;

            // l'utilisateur peut-il ajouter ou supprimer des lignes ?
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToAddRows = false;

            // L'utilisateur peut-il modifier le contenu des cellules ?
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;

            // l'utilisateur peut-il redimensionner les colonnes et les lignes ?
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;

            // l'utilisateur peut-il modifier l'ordre des colonnes ?
            dgv.AllowUserToOrderColumns = false;

            // le composant accepte t'il le 'déposer' dans un Glisser - Déposer ?
            dgv.AllowDrop = false;

            // ajustement automatique de la taille des colonnes
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            #endregion

            #region paramètrage concernant la ligne d'entête 

            // visibilité
            dgv.ColumnHeadersVisible = true;

            // bordure
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // style
            dgv.EnableHeadersVisualStyles = false;
            DataGridViewCellStyle style = dgv.ColumnHeadersDefaultCellStyle;
            style.BackColor = Color.WhiteSmoke;
            style.ForeColor = Color.Black;
            style.SelectionBackColor = Color.WhiteSmoke;
            style.SelectionForeColor = Color.Black;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.Font = new Font("Georgia", 12, FontStyle.Bold);

            // hauteur 
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 40;

            #endregion

            #region paramètrage au niveau des lignes

            // Hauteur 
            dgv.RowTemplate.Height = 30;

            #endregion

            #region paramètrage au niveau des cellules

            // style de bordure 
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;

            // couleur de fond
            dgv.RowsDefaultCellStyle.BackColor = Color.White;

            #endregion

            #region paramètrage au niveau de la zone sélectionnée

            // couleur de fond et du texte
            dgv.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            dgv.RowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            #endregion

            #region paramètrage des colonnes

            // Colonne 0 : Visite (cachée)
            dgv.Columns[0].Visible = false;

            // Colonne 1 : Image pour supprimer
            dgv.Columns[1].Width = 50;
            dgv.Columns[1].HeaderText = "";
            dgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Colonne 2 : Date
            dgv.Columns[2].HeaderText = "Programmée le";
            dgv.Columns[2].Width = 150;
            dgv.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Colonne 3 : Heure
            dgv.Columns[3].HeaderText = "à";
            dgv.Columns[3].Width = 60;
            dgv.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Colonne 4 : Ville
            dgv.Columns[4].HeaderText = "sur";
            dgv.Columns[4].Width = 150;
            dgv.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Colonne 5 : Praticien
            dgv.Columns[5].HeaderText = "chez";
            dgv.Columns[5].Width = 250;
            dgv.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // désactiver le tri sur toutes les colonnes
            for (int i = 0; i < dgv.ColumnCount; i++)
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            #endregion
        }

        private void remplirDgv()
        {
            dgvVisites.Rows.Clear();
            foreach (Visite v in session.MesVisites.OrderBy(v => v.DateEtHeure))
            {
                int rowIndex = dgvVisites.Rows.Add();
                DataGridViewRow row = dgvVisites.Rows[rowIndex];

                // Colonne 0 : Visite (objet caché)
                row.Cells[0].Value = v;

                // Colonne 1 : Image de suppression
                row.Cells[1].Value = Resources.supprimer;

                // Colonne 2 : Date
                row.Cells[2].Value = v.DateEtHeure.ToShortDateString();

                // Colonne 3 : Heure
                row.Cells[3].Value = v.DateEtHeure.ToString("HH:mm");

                // Colonne 4 : Ville
                row.Cells[4].Value = v.LePraticien.Ville;

                // Colonne 5 : Praticien
                row.Cells[5].Value = v.LePraticien.NomPrenom;
            }
        }

        private void dgvVisites_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // Si clic sur la colonne de suppression (Colonne 1)
            if (e.ColumnIndex == 1)
            {
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette visite ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Récupérer l'objet visite depuis la colonne 0
                    Visite visite = (Visite)dgvVisites.Rows[e.RowIndex].Cells[0].Value;

                    try
                    {
                        // Supprimer de la base de données
                        Passerelle.supprimerRendezVous(visite.Id);

                        // Supprimer de la session
                        session.MesVisites.Remove(visite);

                        // Rafraîchir le datagridview
                        remplirDgv();

                        MessageBox.Show("La visite a été supprimée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            // Si clic sur une autre colonne, afficher les détails de la visite
            else if (e.RowIndex >= 0)
            {
                Visite visite = (Visite)dgvVisites.Rows[e.RowIndex].Cells[0].Value;

                // Afficher les informations du praticien et de la visite
                lblNom.Text = visite.LePraticien.NomPrenom;
                dtpDate.Value = visite.DateEtHeure;
            }
        }

        private void lblTitre_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}