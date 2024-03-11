using Librairie.Entities;
using Librairie.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie.Services
{
    public class ServiceBD : IServiceBD
    {

        #region fields
        #endregion

        #region constructor
        public ServiceBD() { }
        #endregion

        #region public methods
        public void AjouterClient(Client client)
        {
            throw new NotImplementedException();
        }

        public void ModifierClient(Client client)
        {
            throw new NotImplementedException();
        }

        public void ModifierLivre(Livre livre)
        {
            throw new NotImplementedException();
        }

        public Client ObtenirClient(Guid ID)
        {
            throw new NotImplementedException();
        }

        public Client ObtenirClient(string nomClient)
        {
            throw new NotImplementedException();
        }

        public Livre ObtenirLivre(Guid idLivre)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
