using Librairie.Entities;
using Librairie.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Librairie.Services
{
    public class ServiceClient : IServiceClient
    {
        #region fields
        private Client client;
        private IServiceBD _service;
        #endregion

        #region constructor
        public ServiceClient(IServiceBD service)
        {
            _service = service;
        }

        #endregion

        #region public methods
        public Client CreerClient(string nomClient)
        {
            if (!string.IsNullOrEmpty(nomClient))
            {
                client = new Client();
                client.NomUtilisateur = nomClient;
                var clientExistant = this._service.ObtenirClient(client.NomUtilisateur);

                if (clientExistant is null)
                {                    
                    this._service.AjouterClient(client);
                    return client;
                }
                return client;
            }
            return null;
        }
        public void RenommerClient(Guid clientId, string nouveauNomClient)
        {
            client = new Client();

            client = this._service.ObtenirClient(clientId);

            //Valider que le client existe
            if (client != null)
            {
                //Vérifier que le nom a changé
                if (nouveauNomClient != client.NomUtilisateur)
                {
                    //Valider que le nom d'utilisateur est renseigné and Vérifier que le nom n'est pas déjà utilisé par un autre client
                    if (validerNomClientExistantparNom(nouveauNomClient))
                    {
                        client.NomUtilisateur = nouveauNomClient;
                        this._service.ModifierClient(client);
                    }
                }
            }
        }

        #endregion

        #region private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomCLient"></param>
        /// <returns></returns>
        public bool validerNomClientExistantparNom(string nomCLient)
        {
            var client = this._service.ObtenirClient(nomCLient);

            if (client != null)
            {
                if (client.NomUtilisateur == nomCLient)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return true;
            }            
        }

        public bool validerNomClientExistantparGuid(Guid clientId)
        {
            var client = this._service.ObtenirClient(clientId);

            if (client == null)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
