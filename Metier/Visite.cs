// ------------------------------------------
// Nom du fichier : visite.cs
// Objet : Définition de la classe Visite
// Auteur : M. Verghote
// ------------------------------------------

using System.Collections;

namespace Metier
{
    [Serializable]
    public class Visite : IEnumerable<KeyValuePair<Medicament, int>>, IComparable<Visite>
    {

        // la classe doit implémenter une méthode de comparaison car les objets Visite
        // seront utilisés dans une collection qui sera triée après chaque ajout

        public int CompareTo(Visite? o)
        {
            return DateEtHeure.CompareTo(o!.DateEtHeure);
        }

        // attribut privé
        private SortedDictionary<Medicament, int> lesEchantillons;

        // Propriétés automatiques
        public int Id { get; }
        public Praticien LePraticien { get; private set; }
        public Motif LeMotif { get; private set; }
        public DateTime DateEtHeure { get; private set; }
        public string? Bilan { get; private set; }
        public Medicament? PremierMedicament { get; private set; }
        public Medicament? SecondMedicament { get; private set; }


        // Constructeur

        public Visite(int id, Praticien unPraticien, Motif unMotif, DateTime uneDateEtHeure)
        {
            Id = id;
            LePraticien = unPraticien;
            LeMotif = unMotif;
            DateEtHeure = uneDateEtHeure;
            Bilan = null;
            PremierMedicament = null;
            SecondMedicament = null;
            lesEchantillons = new SortedDictionary<Medicament, int>();

            // mise à jour de la relation bidirectionnelle avec l'objet Praticien
            LePraticien.ajouteVisite(this);
        }

        // méthode enregistrerBilan : alimente les propriétés Bilan, PremierMedicament, SecondMedicament
        public void enregistrerBilan(string bilan, Medicament premierMedicament, Medicament? secondMedicament)
        {
            (Bilan, PremierMedicament, SecondMedicament) = (bilan, premierMedicament, secondMedicament);
        }

        // méthode déplacer ; alimente la propriété DateEtHeure
        public void deplacer(DateTime uneDateEtHeure) => DateEtHeure = uneDateEtHeure;


        // ajoute un échantillon 
        // si le médicament est déjà dans le dictionnaire on cumule les quantités
        public void ajouterEchantillon(Medicament unMedicament, int uneQuantite)
        {
            if (lesEchantillons.ContainsKey(unMedicament))
            {
                lesEchantillons[unMedicament] += uneQuantite;
               
            }
            else {
                lesEchantillons.Add(unMedicament, uneQuantite);
            }
        }

        // supprimer un échantillon
        public void supprimerEchantillon(Medicament unMedicament)
        {
            lesEchantillons.Remove(unMedicament);
        }


        // retourne true si le médicament est dans les échantillons de cette visite
        public bool contenir(Medicament unMedicament) => lesEchantillons.ContainsKey(unMedicament);
        
        public int getQuantite(Medicament unMedicament) => lesEchantillons[unMedicament];


        // définition d'un itérateur permettant de parcourir les éléments du dictionnaire lesEchantillons
        public IEnumerator<KeyValuePair<Medicament, int>> GetEnumerator() => lesEchantillons.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

}
