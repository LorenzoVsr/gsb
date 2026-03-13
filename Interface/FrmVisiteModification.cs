using Donnee;
using Metier;
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
        }
        #endregion

        private void dgvVisites_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}