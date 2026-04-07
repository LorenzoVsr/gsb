using Donnee;
using Metier;
using System.Text.RegularExpressions;

namespace Interface
{
    public partial class FrmPraticienAjout : FrmBase
    {
        public FrmPraticienAjout(Session uneSession) : base(uneSession)
        {
            InitializeComponent();
            Load += FrmPraticienAjout_Load;
        }

        private void FrmPraticienAjout_Load(object sender, EventArgs e)
        {
            parametrerComposant();
            centrerFormulaire();
            this.Resize += FrmPraticienAjout_Resize;
        }

        private void parametrerComposant()
        {
            lblTitre.Text = "Ajouter un praticien";

            msgNom.Visible = false;
            msgPrenom.Visible = false;
            msgRue.Visible = false;
            msgVille.Visible = false;
            msgTelephone.Visible = false;
            msgEmail.Visible = false;
            msgType.Visible = false;
            msgSpecialite.Visible = false;

            cbxType.DataSource = session.LesTypesPraticien;
            cbxType.DisplayMember = "Libelle";
            cbxType.ValueMember = "Id";
            cbxType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxType.SelectedIndex = -1;

            cbxSpecialite.DataSource = session.LesSpecialites;
            cbxSpecialite.DisplayMember = "Libelle";
            cbxSpecialite.ValueMember = "Id";
            cbxSpecialite.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxSpecialite.SelectedIndex = -1;

            txtVille.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtVille.AutoCompleteSource = AutoCompleteSource.CustomSource;
            var source = new AutoCompleteStringCollection();
            foreach (Ville ville in session.MesVilles)
            {
                source.Add(ville.Nom);
            }
            txtVille.AutoCompleteCustomSource = source;

            BtnAjouter.Click -= BtnAjouter_Click;
            BtnAjouter.Click += BtnAjouter_Click;
        }

        private bool controlerChamp(TextBox txt, Label lblMessage, string message)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                lblMessage.Text = message;
                lblMessage.Visible = true;
                return false;
            }

            lblMessage.Text = string.Empty;
            lblMessage.Visible = false;
            return true;
        }

        private bool controlerVille()
        {
            if (string.IsNullOrWhiteSpace(txtVille.Text))
            {
                msgVille.Text = "La ville du praticien doit être précisée";
                msgVille.Visible = true;
                return false;
            }

            bool existe = session.MesVilles.Any(v => string.Equals(v.Nom, txtVille.Text, StringComparison.OrdinalIgnoreCase));
            if (!existe)
            {
                msgVille.Text = "La ville doit appartenir à la liste proposée";
                msgVille.Visible = true;
                return false;
            }

            msgVille.Text = string.Empty;
            msgVille.Visible = false;
            return true;
        }

        private bool controlerTelephone()
        {
            if (!mtbTelephone.MaskFull)
            {
                msgTelephone.Text = "Le téléphone du praticien doit être précisé";
                msgTelephone.Visible = true;
                return false;
            }

            msgTelephone.Visible = false;
            return true;
        }

        private bool controlerEmail()
        {
            if (txtEmail.Text == string.Empty)
            {
                msgEmail.Text = "L'adresse mail du praticien doit être précisée";
                msgEmail.Visible = true;
                return false;
            }

            Regex uneExpression = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!uneExpression.IsMatch(txtEmail.Text))
            {
                msgEmail.Text = "L'adresse mail n'est pas valide";
                msgEmail.Visible = true;
                return false;
            }

            msgEmail.Text = string.Empty;
            msgEmail.Visible = false;
            return true;
        }

        private bool controlerType()
        {
            if (cbxType.SelectedIndex == -1)
            {
                msgType.Text = "Veuillez sélectionner le type de praticien.";
                msgType.Visible = true;
                return false;
            }

            msgType.Text = string.Empty;
            msgType.Visible = false;
            return true;
        }

        private void ajout()
        {
            bool nomOk = controlerChamp(txtNom, msgNom, "Le nom du praticien doit être précisé");
            bool prenomOk = controlerChamp(txtPrenom, msgPrenom, "Le prénom du praticien doit être précisé");
            bool rueOk = controlerChamp(txtRue, msgRue, "La rue du praticien doit être précisée");
            bool villeOk = controlerVille();
            bool emailOk = controlerEmail();
            bool telephoneOk = controlerTelephone();
            bool typeOk = controlerType();

            if (nomOk && prenomOk && rueOk && villeOk && emailOk && telephoneOk && typeOk)
            {
                ajouter();
            }
        }

        private void ajouter()
        {
            try
            {
                Ville laVille = session.MesVilles.Find(x => string.Equals(x.Nom, txtVille.Text, StringComparison.OrdinalIgnoreCase))!;
                string codePostal = laVille.Code;

                string idType = ((TypePraticien)cbxType.SelectedItem!).Id;
                string? idSpecialite = cbxSpecialite.SelectedIndex >= 0 ? ((Specialite)cbxSpecialite.SelectedItem!).Id : null;

                int id = Passerelle.ajouterPraticien(
                    txtNom.Text.Trim(),
                    txtPrenom.Text.Trim(),
                    txtRue.Text.Trim(),
                    codePostal,
                    laVille.Nom,
                    mtbTelephone.Text,
                    txtEmail.Text.Trim(),
                    idType,
                    idSpecialite);

                TypePraticien leType = session.LesTypesPraticien.Find(x => x.Id == idType)!;
                Specialite? laSpecialite = idSpecialite is null ? null : session.LesSpecialites.Find(x => x.Id == idSpecialite);

                var praticien = new Praticien(
                    id,
                    txtNom.Text.Trim(),
                    txtPrenom.Text.Trim(),
                    txtRue.Text.Trim(),
                    codePostal,
                    laVille.Nom,
                    txtEmail.Text.Trim(),
                    mtbTelephone.Text,
                    leType,
                    laSpecialite);

                session.MesPraticiens.Add(praticien);
                session.MesPraticiens.Sort();

                txtNom.Clear();
                txtPrenom.Clear();
                txtRue.Clear();
                txtVille.Clear();
                txtEmail.Clear();
                mtbTelephone.Clear();
                cbxType.SelectedIndex = -1;
                cbxSpecialite.SelectedIndex = -1;

                MessageBox.Show("Le praticien a été ajouté avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAjouter_Click(object? sender, EventArgs e)
        {
            ajout();
        }

        private void FrmPraticienAjout_Resize(object? sender, EventArgs e)
        {
            centrerFormulaire();
        }



    }
}
