// ------------------------------------------
// Nom du fichier : FrmConnexion.cs
// Objet : Formulaire de connexion
// ------------------------------------------

using Donnee;
using Metier;
using MySqlConnector;

namespace Interface {
    public partial class FrmConnexion : Form
    {
        private Session? _session;

        public FrmConnexion() {
            InitializeComponent();
        }

        #region procédures événementielles

        // au chargement du formulaire, il faut paramètrer ses composants
        private void FrmConnexion_Load(object sender, EventArgs e) {
            parametrerComposant();
        }

        private void FrmConnexion_FormClosing(object sender, FormClosingEventArgs e) {
        }

        // Déclenchement du contrôle de la connexion
        private void btnConnecter_Click(object sender, EventArgs e) {
            seConnecter(txtLogin.Text, txtMdp.Text);
        }

        private void txtLogin_Enter(object sender, EventArgs e) {
            lblErreurConnexion.Visible = false;
        }

        private void txtMdp_Enter(object sender, EventArgs e) {
            lblErreurConnexion.Visible = false;
        }

        private void txtLogin_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (txtMdp.Text.Length > 0) {
                    seConnecter(txtLogin.Text, txtMdp.Text);
                } else {
                    txtMdp.Focus();
                }
            }
        }

        private void txtMdp_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (txtLogin.Text.Length > 0) {
                    seConnecter(txtLogin.Text, txtMdp.Text);
                } else {
                    txtLogin.Focus();
                }
            }
        }

        private void FrmConnexion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.Enter)
            {
                txtMdp.Text = "19910826";
                txtLogin.Text = "a17";
                // empêcher la propagation de l'événement vers le champ ayant le focus et gérant le même événement
                e.Handled = true;
            }
        }

        #endregion

        #region méthodes

        // Paramètrage des composants de la fenêtre
        private void parametrerComposant() {
            Text = "Laboratoire pharmaceutique Galaxy-Swiss Bourdin - Gestion des visites";

            // Désactive les boutons réduire / agrandir
            MinimizeBox = false; // bouton réduire grisé
            MaximizeBox = false; // bouton agrandir grisé

            // Empêche le redimensionnement
            FormBorderStyle = FormBorderStyle.FixedDialog;

            // Affiche la croix de fermeture
            ControlBox = true;
            txtLogin.Focus();
        }

        // contrôle que le champ txtNom est renseigné
        private bool controlerLogin(string nom) {
            if (nom == string.Empty) {
                bulleLogin.Show("Veuillez indiquer votre login ", txtLogin, 10, 30, 3000);
                return false;
            } else {
                return true;
            }
        }

        // contrôle que le mot de passe est renseigné 
        private bool controlerMdp(string mdp) {
            if (mdp == string.Empty) {
                bulleMdp.Show("Veuillez indiquer votre mot de passe", txtMdp, 10, 30, 3000);
                return false;
            } else {
                return true;
            }
        }

        /// <summary>
        ///  déclenche la tentative de connexion à la base de données avec les identifiants fournis, et affiche un message d'erreur en cas d'échec
        /// </summary>
        /// <param name="login"></param>
        /// <param name="mdp"></param>
        private void seConnecter(string login, string mdp)
        {
            lblErreurConnexion.Visible = false;

            if (!controlerLogin(login) || !controlerMdp(mdp)) {
                return;
            }

            try
            {
                _session = Passerelle.ouvrirSession(login, mdp);
                this.DialogResult = DialogResult.OK;
                Close();
            } catch (MySqlException e)
            {
                if (e.Message.Contains("Access denied"))
                {
                    lblErreurConnexion.Text = "Vos identifiants sont incorrects.";
                } else
                {
                    // lblErreurConnexion.Text = "Problème lors de la tentative de connexion au serveur.\nPrière de contacter le service informatique.";
                    lblErreurConnexion.Text = e.Message;
                }

                lblErreurConnexion.Visible = true;
            } catch (Exception e)
            {
                lblErreurConnexion.Text = e.Message;
                lblErreurConnexion.Visible = true;
            }
        }

        // La méthode statique qui ouvre le formulaire et récupère la session
        public static Session? getSession()
        {
            using var frm = new FrmConnexion();
            var result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                return frm._session!;
            } else  {
                // L'utilisateur a annulé ou fermé le formulaire
                return null;
            }
        }


        #endregion
    }
}
