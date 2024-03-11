using System;
using System.Collections.Generic;

namespace Questions
{
    //Améliorer le code de cette classe ainsi que sa relation avec la classe Collaborateur.
    public class Question
    {
        #region fields
        private readonly ICollaborateur _collaborateur;
        #endregion

        #region constructors
        public Question(ICollaborateur collaborateur)
        {
            this._collaborateur = collaborateur;
        }
        #endregion

        #region public methods
        public void Traiter(List<string> listeContenu)
        {
            List<string> listeContenuValide = new List<string>();
            string message = string.Empty;

            foreach (var contenu in listeContenu)
            {
                if(Valider(contenu, message))
                {
                    var content = contenu.Substring(0, Math.Min(10, contenu.Length));
                    if (!listeContenuValide.Contains(content))
                    {
                        listeContenuValide.Add(content);
                    }
                }
                else
                {
                    throw new ArgumentException(message);
                }
            }

            if (listeContenuValide.Count > 0)
            {
                listeContenuValide.ForEach(x => _collaborateur.AjouterContenuBD(x));
            }

        }
        #endregion

        #region private methods
        private bool Valider(string contenu, string message)
        {
            message = null;

            if (string.IsNullOrEmpty(contenu))
            {
                message = "Le contenu ne peut être vide";
                return false;
            }

            if (contenu.Length > 10)
            {
                message = "Le contenu est trop long";
                return false;
            }

            return true;
        }
        #endregion

    }
}
