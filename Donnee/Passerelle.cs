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
        /// <summary>
        /// Chaîne de connexion à la base de données, initialisée lors de la première connexion.
        /// </summary>
        private static string? _chaineConnexion;

        // instancie une connexion et retourne l'objet connexion ouvert
        private static MySqlConnection ouvrirConnexion()
        {
            MySqlConnection cnx = new MySqlConnection(_chaineConnexion);
            cnx.Open();
            return cnx;
        }

        // <summary>
        /// Méthode de connexion à la base de données.
        /// Stocke la chaîne de connexion pour les appels suivants.
        /// </summary>
        /// <param name="login">Identifiant de connexion</param>
        /// <param name="mdp">Mot de passe</param>
        public static Session ouvrirSession(string login, string mdp)
        {
            _chaineConnexion = $"Data Source=localhost;Database=gsb;User Id={login};Password={mdp}";

            using MySqlConnection cnx = ouvrirConnexion();
            return chargerDonneesSession(cnx);
        }

        /// <summary>
        /// Charge toutes les données nécessaires à une session.
        /// </summary>
        /// <param name="session">Session à alimenter</param>
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

        /// <summary>
        ///  Récupère le nom du visiteur connecté à partir de la base de données.
        /// </summary>
        /// <param name="cnx"></param>
        /// <returns>nomPrenom de la vue leVisiteur</returns>
        private static string getNomVisiteur(MySqlConnection cnx)
        {
            string sql = "SELECT nomPrenom FROM leVisiteur;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            return (string)cmd.ExecuteScalar()!;
        }

        /// <summary>
        /// Charge la liste des lesMotifs depuis la base de données.
        /// </summary>
        /// <returns>Liste des lesMotifs triés par libellé</returns>
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

        /// <summary>
        /// Charge la liste des lesTypes de lesPraticiens depuis la base de données.
        /// </summary>
        /// <returns>Liste des lesTypes de lesPraticiens triés par libellé</returns>
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

        /// <summary>
        /// Charge la liste des spécialités depuis la base de données.
        /// </summary>
        /// <returns>Liste des spécialités triées par libellé</returns>
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

        /// <summary>
        /// Charge la liste des familles de médicaments depuis la base de données.
        /// </summary>
        /// <returns>Dictionnaire des familles triées par libellé</returns>
        private static List<Famille> chargerLesFamilles(MySqlConnection cnx)
        {
            List<Famille> lesFamilles = new();
            string sql = "SELECT id, libelle FROM Famille ORDER BY libelle;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();
            while (curseur.Read())
            {
                string id = curseur.GetString("id");
                string libelle = curseur.GetString("libelle");
                lesFamilles.Add(new Famille(id, libelle));
            }
            return lesFamilles;
        }

        /// <summary>
        /// Charge la liste des villes associées au visiteur.
        /// </summary>
        /// <returns>Liste des villes : nom, codePostal</returns>
        private static List<Ville> chargerMesVilles(MySqlConnection cnx)
        {
            var mesVilles = new List<Ville>();
            string sql = "SELECT nom, codePostal FROM mesVilles;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();
            while (curseur.Read())
            {
                string nom = curseur.GetString("nom");
                string codePostal = curseur.GetString("codePostal");
                mesVilles.Add(new Ville(nom, codePostal));
            }
            return mesVilles;
        }

        /// <summary>
        /// Charge la liste des médicaments depuis la base de données.
        /// </summary>
        /// <param name="lesFamilles">Collection des familles déjà chargées</param>
        /// <returns>Liste des médicaments triés par nom</returns>
        private static List<Medicament> chargerLesMedicaments(MySqlConnection cnx, List<Famille> lesFamilles)
        {
            // Liste à retourner
            var lesMedicaments = new List<Medicament>();
            // Transformons la liste des familles en dictionnaire pour éviter des recherches linéaires O(n)
            var familles = lesFamilles.ToDictionary(f => f.Id);

            string sql = "SELECT id, nom, composition, effets, contreIndication, idFamille FROM medicament ORDER BY nom;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();
            // récupération de l'index de la colonne idFamille pour éviter de le rechercher à chaque itération
            int indexFamille = curseur.GetOrdinal("idFamille");

            // Parcours du curseur
            while (curseur.Read())
            {
                string id = curseur.GetString("id");
                string nom = curseur.GetString("nom");
                string composition = curseur.GetString("composition");
                string effets = curseur.GetString("effets");
                string contreIndication = curseur.GetString("contreIndication");
                // Récupération de la famille via le dictionnaire (O(1))
                Famille famille = familles[curseur.GetString(indexFamille)];
                var medicament = new Medicament(id, nom, composition, effets, contreIndication, famille);
                lesMedicaments.Add(medicament);
                // Ajout du médicament à sa famille
                famille.ajouterMedicament(medicament);
            }
            return lesMedicaments;
        }

        /// <summary>
        /// Charge la liste des lesPraticiens associés au visiteur.
        /// </summary>
        /// <param name="lesTypes">Liste des types de praticien </param>
        /// <param name="lesSpecialites">Liste des spécialités</param>
        /// <returns>Liste des lesPraticiens</returns>
        private static List<Praticien> chargerMesPraticiens(MySqlConnection cnx, List<TypePraticien> lesTypes, List<Specialite> lesSpecialites)
        {
            // Liste qui sera retournée
            var mesPraticiens = new List<Praticien>();

            // Transformons les listes des types et spécialités en dictionnaires pour éviter des recherches linéaires O(n)
            var types = lesTypes.ToDictionary(t => t.Id);
            var specialites = lesSpecialites.ToDictionary(s => s.Id);

            string sql = "SELECT id, nom, prenom, rue, codePostal, ville, email, telephone, idType, idSpecialite FROM mespraticiens;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();

            // récupération des index des colonnes pour éviter de les rechercher à chaque itération
            int indexSpecialite = curseur.GetOrdinal("idSpecialite");

            while (curseur.Read())
            {
                // Récupération du type via le dictionnaire (O(1))
                string idType = curseur.GetString("idType");
                TypePraticien? type = null;
                types.TryGetValue(idType, out type);

                // La spécialité peut être null, il faut vérifier avant de tenter de la récupérer
                Specialite? specialite = curseur.IsDBNull(indexSpecialite) ? null : specialites[curseur.GetString(indexSpecialite)];

                // Création du praticien
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


        /// <summary>
        /// Charge la liste des visites associées au visiteur.
        /// </summary>
        /// <param name="lesMotifs">Liste des motifs</param>
        /// <param name="lesPraticiens">Liste des praticiens</param>
        /// <param name="lesMedicaments">Liste des médicaments</param>
        /// <returns>Liste des visites</returns>
        private static List<Visite> chargerMesVisites(MySqlConnection cnx, List<Motif> lesMotifs, List<Praticien> lesPraticiens, List<Medicament> lesMedicaments)
        {
            // Liste à retourner
            var mesVisites = new List<Visite>();

            // Transformation des listes en dictionnaires pour accès rapide O(1)
            var motifs = lesMotifs.ToDictionary(m => m.Id);
            var praticiens = lesPraticiens.ToDictionary(p => p.Id);
            var medicaments = lesMedicaments.ToDictionary(m => m.Id);

            string sql = "SELECT id, dateEtHeure, idMotif, idPraticien, bilan, premierMedicament, secondMedicament FROM mesVisites;";
            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();

            // Récupération des index des colonnes pour éviter de les rechercher à chaque itération
            int indexBilan = curseur.GetOrdinal("bilan");
            int indexSecondMedicament = curseur.GetOrdinal("secondMedicament");

            while (curseur.Read())
            {
                int id = curseur.GetInt32("id");
                int idPraticien = curseur.GetInt32("idPraticien");
                int idMotif = curseur.GetInt32("idMotif");
                DateTime dateEtHeure = curseur.GetDateTime("dateEtHeure");

                // Accès via dictionnaire (O(1))
                Praticien praticien = praticiens[idPraticien];
                Motif motif = motifs[idMotif];

                var visite = new Visite(id, praticien, motif, dateEtHeure);

                // Si le bilan n'est pas null, enregistrer le bilan et les médicaments associés
                if (!curseur.IsDBNull(indexBilan))
                {
                    string bilan = curseur.GetString("bilan");
                    Medicament premier = medicaments[curseur.GetString("premierMedicament")];

                    // Le second médicament peut être null
                    Medicament? second = curseur.IsDBNull(indexSecondMedicament) ? null : medicaments[curseur.GetString(indexSecondMedicament)];

                    visite.enregistrerBilan(bilan, premier, second);
                }

                mesVisites.Add(visite);
            }

            return mesVisites;
        }


        /// <summary>
        /// Charge les échantillons associés aux visites.
        /// </summary>
        /// <param name="visites">Liste des visites déjà chargées</param>
        /// <param name="medicaments">Liste des médicaments déjà chargés</param>
        private static void chargerMesEchantillons(MySqlConnection cnx, List<Visite> lesVisites, List<Medicament> lesMedicaments)
        {

            // transformation de la liste des visites en dictionnaire pour accès rapide O(1)
            var visites = lesVisites.ToDictionary(v => v.Id);
            // transformation de la liste des médicaments en dictionnaire pour accès rapide O(1)
            var medicaments = lesMedicaments.ToDictionary(m => m.Id);

            // la requête SQL pour récupérer les échantillons
            string sql = "SELECT idVisite, idMedicament, quantite FROM mesEchantillons;";

            using MySqlCommand cmd = new MySqlCommand(sql, cnx);
            using MySqlDataReader curseur = cmd.ExecuteReader();
            while (curseur.Read())
            {
                int idVisite = curseur.GetInt32("idVisite");
                string idMedicament = curseur.GetString("idMedicament");
                int quantite = curseur.GetInt32("quantite");
                // Récupération de la visite et du médicament via les dictionnaires (O(1))
                Visite visite = visites[idVisite];
                Medicament medicament = medicaments[idMedicament];
                // Ajout de l'échantillon à la visite
                visite.ajouterEchantillon(medicament, quantite);
            }
        }


        /// <summary>
        /// Ajoute un nouveau rendez-vous en base de données.
        /// </summary>
        /// <param name="idPraticien">ID du praticien</param>
        /// <param name="idMotif">ID du motif</param>
        /// <param name="uneDate">Date et heure du rendez-vous</param>
        /// <returns>ID de la nouvelle visite, ou 0 en cas d'erreur</returns>
        static public int ajouterRendezVous(int idPraticien, int idMotif, DateTime uneDate)
        {
            string sql = "ajouterRendezVous";

            using MySqlConnection cnx = ouvrirConnexion();

            using var cmd = new MySqlCommand(sql, cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            // passer les paramètres
            cmd.Parameters.AddWithValue("_idPraticien", idPraticien);
            cmd.Parameters.AddWithValue("_idMotif", idMotif);
            cmd.Parameters.AddWithValue("_dateEtHeur", uneDate);

            /*
			// solution A
			var paramSortie = new MySqlParameter("_idVisite", MySqlDbType.Int32);
            paramSortie.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(paramSortie);
            cmd.ExecuteNonQuery();
            return (int) paramSortie.Value!;
            
            // solution B
            return Convert.ToInt32(cmd.ExecuteScalar());
            */
            // solution C
            cmd.ExecuteNonQuery();
            return Convert.ToInt32(cmd.LastInsertedId);

        }

        /// <summary>
        /// Supprime un rendez-vous de la base de données.
        /// </summary>
        /// <param name="idVisite">ID de la visite à supprimer</param>
        static public void supprimerRendezVous(int idVisite)
        {
            using MySqlConnection cnx = ouvrirConnexion();
            using var cmd = new MySqlCommand("supprimerRendezVous", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("_idVisite", idVisite);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Modifie la date et l'heure d'un rendez-vous.
        /// </summary>
        /// <param name="idVisite">ID de la visite à modifier</param>
        /// <param name="uneDateEtHeure">Nouvelle date et heure</param>
        static public void modifierRendezVous(int idVisite, DateTime uneDateEtHeure)
        {
            using MySqlConnection cnx = ouvrirConnexion();
            using var cmd = new MySqlCommand("modifierRendezVous", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("_idVisite", idVisite);
            cmd.Parameters.AddWithValue("_dateEtHeure", uneDateEtHeure);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Enregistre le bilan d'une visite et les échantillons associés.
        /// </summary>
        /// <param name="uneVisite">Visite à enregistrer</param>
        static public void enregistrerBilan(Visite uneVisite)
        {


        }

        /// <summary>
        /// Ajoute un nouveau praticien en base de données.
        /// </summary>
        /// <param name="nom">Nom du praticien</param>
        /// <param name="prenom">Prénom du praticien</param>
        /// <param name="rue">Adresse</param>
        /// <param name="codePostal">Code postal</param>
        /// <param name="ville">Ville</param>
        /// <param name="telephone">Téléphone</param>
        /// <param name="email">Email</param>
        /// <param name="unType">Type de praticien</param>
        /// <param name="uneSpecialite">Spécialité</param>
        /// <returns>ID du nouveau praticien</returns>
        static public int ajouterPraticien(string nom, string prenom, string rue, string codePostal, string ville, string telephone, string email, string unType, string uneSpecialite)
        {
            return 0;
        }

        /// <summary>
        /// <summary>
        /// Modifie les informations d'un praticien en base de données.
        /// </summary>
        /// <param name="lePraticien">Objet Praticien contenant les nouvelles informations</param>
        static public void modifierPraticien(Praticien lePraticien)
        {
        }

        /// <summary>
        /// Supprime un praticien de la base de données.
        /// </summary>
        /// <param name="id">ID du praticien à supprimer</param>
        static public void supprimerPraticien(int id)
        {
        }




    }
}
