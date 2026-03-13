// ------------------------------------------
// Nom du fichier : praticien.cs
// Objet : classe Praticien
// Auteur : M. Verghote
// Date   : 23/11/2022
// ------------------------------------------

using System;

using System.Collections.Generic;

namespace Metier
{
    public class Praticien : IComparable<Praticien>
    {

        // Le tri par défaut d'un conteneur d'objets Praticien se fera sur le nom et le prénom

        public int CompareTo(Praticien p) => NomPrenom.CompareTo(p.NomPrenom);

        // attribut
        List<Visite> lesVisites;

        // Propriétés automatiques

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public TypePraticien? Type { get; set; }
        public Specialite? Specialite { get; set; }

        // Constructeur
        public Praticien(int id, string nom, string prenom, string rue, string codePostal, string ville, string email, string telephone, TypePraticien? type, Specialite? specialite)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Rue = rue;
            CodePostal = codePostal;
            Ville = ville;
            Email = email;
            Telephone = telephone;
            Type = type;
            Specialite = specialite;
            lesVisites = new List<Visite>();
        }

        // Propriété en lecture seule

        // retourne le nom et prénom du praticien
        public string NomPrenom { get => Nom + " " + Prenom; }

        // retourne le nombre de visites réalisées chez le praticien
        public int NbVisite { get => lesVisites.Count; }

        // méthode

        // ajoute une visite concernant le praticien
        public void ajouteVisite(Visite v) => lesVisites.Add(v);

    }
}
