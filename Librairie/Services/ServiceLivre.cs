using Librairie.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie.Services
{
    public class ServiceLivre : IServiceLivre
    {

        #region fields
        private ServiceClient _serviceclient;        
        private IServiceBD _sbd;
        #endregion

        #region constructor
        public ServiceLivre(IServiceBD serviceBD) {
            this._serviceclient = new ServiceClient(serviceBD);
            this._sbd = serviceBD;
        }
        #endregion

        #region public methods
        public decimal AcheterLivre(Guid IdClient, Guid IdLivre, decimal montant)
        {
            //Valider que le client existe
            if (this._serviceclient.validerNomClientExistantparGuid(IdClient))
            {
                //Valider que le montant est supérieur à 0
                if (montant > 0)
                {
                    var livre = _sbd.ObtenirLivre(IdLivre);
                    //Valider que le livre existe
                    if (livre != null)
                    {
                        return 1;
                    }
                }
                return 0;
            }

            //Vérifier qu'il reste au moins un exemplaire
            //Valider que le montant est égal ou supérieur à la valeur du livre
            //Décrémanter le nombre d'exemplaire disponible du livre
            //Ajouter un exemplaire vendu du livre au client
            //Retourner le montant restant suite à l'achat

            return 0;
        }

        public decimal RembourserLivre(Guid IdClient, Guid idLivre)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
