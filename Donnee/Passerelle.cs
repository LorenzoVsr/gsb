// ------------------------------------------
// Nom du fichier : Passerelle.cs
// Objet : classe Passerelle assurant l'alimentation des objets en mémoire
// ------------------------------------------

using MySqlConnector;
using System.Data;
using Metier;

namespace Donnee
{
    public static class Passerelle
    {
        private static string? _chaineConnexion;

        // instancie une connexion et retourne l'objet connexion ouvert
        private static MySqlConnection ouvrirConnexion()
        {
            MySqlConnection cnx = new MySqlConnection(_chaineConnexion);
            cnx.Open();
            return cnx;
        }

        /// <summary>
        /// Méthode de connexion à la base de données.
        /// </summary>
        public static Session ouvrirSession(string login, string mdp)
        {
            _chaineConnexion = $"Data Source=localhost;Database=gsb;User Id={login};Password={mdp}";

            using MySqlConnection cnx = ouvrirConnexion();
            return chargerDonneesSession(cnx);
        }

        /// <summary>
        /// Charge toutes les données nécessaires à une session.
        /// </summary>
        public static Session chargerDonneesSession(MySqlConnection cnx)
        {
            string nomVisiteur = getNomVisiteur(cnx);

            var lesMotifs = chargerLesMotifs(cnx);
            var lesTypesPraticien = chargerLesTypesPraticien(cnx);
            var lesSpecialites = chargerLesSpecialites(cnx);
            var lesFamilles = chargerLesFamilles(cnx);
            var lesMedicaments = chargerLesMedicaments(cnx, lesFamilles);
            var mesVilles = chargerMesVilles(cnx);
            var mesPraticiens = chargerMesPraticiens(cnx, lesTypesPraticien, lesSpecialites);
            var mesVisites = chargerMesVisites(cnx, lesMotifs, mesPraticiens, lesMedicaments);

            chargerMesEchantillons(cnx, mesVisites, lesMedicaments);

            return new Session(
                nomVisiteur,
                lesMotifs,
                lesTypesPraticien,
                lesSpecialites,
                lesFamilles,
                lesMedicaments,
                mesVilles,
                mesPraticiens,
                mesVisites
            );
        }

        private static string getNomVisiteur(MySqlConnection cnx)
        {
            string sql = "SELECT nomPrenom FROM leVisiteur;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            return (string)cmd.ExecuteScalar()!;
        }

        private static List<Motif> chargerLesMotifs(MySqlConnection cnx)
        {
            List<Motif> lesMotifs = new List<Motif>();
            string sql = "SELECT id, libelle FROM Motif ORDER BY libelle;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();
            while (curseur.Read())
            {
                lesMotifs.Add(new Motif(curseur.GetInt32("id"), curseur.GetString("libelle")));
            }
            return lesMotifs;
        }

        private static List<TypePraticien> chargerLesTypesPraticien(MySqlConnection cnx)
        {
            List<TypePraticien> lesTypes = new List<TypePraticien>();
            string sql = "SELECT id, libelle FROM TypePraticien ORDER BY libelle;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();
            while (curseur.Read())
            {
                lesTypes.Add(new TypePraticien(curseur.GetString("id"), curseur.GetString("libelle")));
            }
            return lesTypes;
        }

        private static List<Specialite> chargerLesSpecialites(MySqlConnection cnx)
        {
            List<Specialite> lesSpecialites = new List<Specialite>();
            string sql = "SELECT id, libelle FROM Specialite ORDER BY libelle;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();
            while (curseur.Read())
            {
                lesSpecialites.Add(new Specialite(curseur.GetString("id"), curseur.GetString("libelle")));
            }
            return lesSpecialites;
        }

        private static List<Famille> chargerLesFamilles(MySqlConnection cnx)
        {
            List<Famille> lesFamilles = new();
            string sql = "SELECT id, libelle FROM Famille ORDER BY libelle;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();
            while (curseur.Read())
            {
                lesFamilles.Add(new Famille(curseur.GetString("id"), curseur.GetString("libelle")));
            }
            return lesFamilles;
        }

        private static List<Ville> chargerMesVilles(MySqlConnection cnx)
        {
            var mesVilles = new List<Ville>();
            string sql = "SELECT nom, codePostal FROM mesVilles;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();
            while (curseur.Read())
            {
                mesVilles.Add(new Ville(curseur.GetString("nom"), curseur.GetString("codePostal")));
            }
            return mesVilles; 
        }

        private static List<Medicament> chargerLesMedicaments(MySqlConnection cnx, List<Famille> lesFamilles)
        {
            var lesMedicaments = new List<Medicament>();
            var familles = lesFamilles.ToDictionary(f => f.Id);

            string sql = "SELECT id, nom, composition, effets, contreIndication, idFamille FROM medicament ORDER BY nom;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();
            int indexFamille = curseur.GetOrdinal("idFamille");

            while (curseur.Read())
            {
                string id = curseur.GetString("id");
                string nom = curseur.GetString("nom");
                string composition = curseur.GetString("composition");
                string effets = curseur.GetString("effets");
                string contreIndication = curseur.GetString("contreIndication");

                Famille famille = familles[curseur.GetString(indexFamille)];
                var medicament = new Medicament(id, nom, composition, effets, contreIndication, famille);
                lesMedicaments.Add(medicament);
                famille.ajouterMedicament(medicament);
            }
            return lesMedicaments;
        }

        private static List<Praticien> chargerMesPraticiens(MySqlConnection cnx, List<TypePraticien> lesTypes, List<Specialite> lesSpecialites)
        {
            var mesPraticiens = new List<Praticien>();
            var types = lesTypes.ToDictionary(t => t.Id);
            var specialites = lesSpecialites.ToDictionary(s => s.Id);

            string sql = "SELECT id, nom, prenom, rue, codePostal, ville, email, telephone, idType, idSpecialite FROM mespraticiens;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();
            int indexSpecialite = curseur.GetOrdinal("idSpecialite");

            while (curseur.Read())
            {
                string idType = curseur.GetString("idType");
                types.TryGetValue(idType, out TypePraticien? type);

                Specialite? specialite = curseur.IsDBNull(indexSpecialite) ? null : specialites[curseur.GetString(indexSpecialite)];

                mesPraticiens.Add(new Praticien(
                    curseur.GetInt32("id"),
                    curseur.GetString("nom"),
                    curseur.GetString("prenom"),
                    curseur.GetString("rue"),
                    curseur.GetString("codePostal"),
                    curseur.GetString("ville"),
                    curseur.GetString("email"),
                    curseur.GetString("telephone"),
                    type,
                    specialite));
            }
            return mesPraticiens;
        }

        private static List<Visite> chargerMesVisites(MySqlConnection cnx, List<Motif> lesMotifs, List<Praticien> lesPraticiens, List<Medicament> lesMedicaments)
        {
            var mesVisites = new List<Visite>();
            var motifs = lesMotifs.ToDictionary(m => m.Id);
            var praticiens = lesPraticiens.ToDictionary(p => p.Id);
            var medicaments = lesMedicaments.ToDictionary(m => m.Id);

            string sql = "SELECT id, dateEtHeure, idMotif, idPraticien, bilan, premierMedicament, secondMedicament FROM mesVisites;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();

            int indexBilan = curseur.GetOrdinal("bilan");
            int indexSecondMedicament = curseur.GetOrdinal("secondMedicament");

            while (curseur.Read())
            {
                int id = curseur.GetInt32("id");
                int idPraticien = curseur.GetInt32("idPraticien");
                int idMotif = curseur.GetInt32("idMotif");
                DateTime dateEtHeure = curseur.GetDateTime("dateEtHeure");

                Praticien praticien = praticiens[idPraticien];
                Motif motif = motifs[idMotif];

                var visite = new Visite(id, praticien, motif, dateEtHeure);

                if (!curseur.IsDBNull(indexBilan))
                {
                    string bilan = curseur.GetString("bilan");
                    Medicament premier = medicaments[curseur.GetString("premierMedicament")];
                    Medicament? second = curseur.IsDBNull(indexSecondMedicament) ? null : medicaments[curseur.GetString(indexSecondMedicament)];

                    visite.enregistrerBilan(bilan, premier, second);
                }
                mesVisites.Add(visite);
            }
            return mesVisites;
        }

        private static void chargerMesEchantillons(MySqlConnection cnx, List<Visite> lesVisites, List<Medicament> lesMedicaments)
        {
            var visites = lesVisites.ToDictionary(v => v.Id);
            var medicaments = lesMedicaments.ToDictionary(m => m.Id);

            string sql = "SELECT idVisite, idMedicament, quantite FROM mesEchantillons;";

            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();

            while (curseur.Read())
            {
                int idVisite = curseur.GetInt32("idVisite");
                string idMedicament = curseur.GetString("idMedicament");
                int quantite = curseur.GetInt32("quantite");

                if (visites.TryGetValue(idVisite, out Visite? visite) && medicaments.TryGetValue(idMedicament, out Medicament? medicament))
                {
                    visite.ajouterEchantillon(medicament, quantite);
                }
            }
        }

        // Méthodes CRUD (à implémenter)
        public static int ajouterRendezVous(int idPraticien, int idMotif, DateTime uneDate) => 0;
        public static void supprimerRendezVous(int idVisite) { }
        public static void modifierRendezVous(int idVisite, DateTime uneDateEtHeure) { }
        public static void enregistrerBilan(Visite uneVisite) { }
        public static int ajouterPraticien(string nom, string prenom, string rue, string codePostal, string ville, string telephone, string email, string unType, string uneSpecialite) => 0;
        public static void modifierPraticien(Praticien lePraticien) { }
        public static void supprimerPraticien(int id) { }
    }
}