using Librairie.Entities;
using Librairie.Services.Interfaces;
using Librairie.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Questions;

namespace LibrarieTest.Moq
{
    public class ServiceLivreTest
    {
        #region fields
        private Mock<IServiceBD> serviceBDMock;
        private Guid ID_CLIENT_EXISTANT = new Guid();
        private Guid ID_LIVRE = new Guid();
        const string NOM_CLIENT_EXISTANT = "Wendy";
        const Decimal MONTANT_LIVRE = 0;
        #endregion

        #region constructor
        public ServiceLivreTest()
        {
            serviceBDMock = new Mock<IServiceBD>();
        }
        #endregion

        #region public methods

        #region Acheter un livre

        [Test]
        public void Acheterunlivre_ClientExistant_RetournerClient()
        {
            // Arrange
            ServiceLivre sut = new ServiceLivre(serviceBDMock.Object);
            Client client = new Client { Id = ID_CLIENT_EXISTANT, NomUtilisateur = NOM_CLIENT_EXISTANT };
            serviceBDMock.Setup(sbm => sbm.ObtenirClient(ID_CLIENT_EXISTANT)).Returns(client);

            // Act
            var reponse = sut.AcheterLivre(ID_CLIENT_EXISTANT, ID_LIVRE, MONTANT_LIVRE);

            // Assert
            serviceBDMock.Verify(l => l.ObtenirClient(ID_CLIENT_EXISTANT));
        }

        [Test]
        public void Acheterunlivre_LivreExistant_RetournerLivre()
        {
            // Arrange
            ServiceLivre sut = new ServiceLivre(serviceBDMock.Object);
            Client client = new Client { Id = ID_CLIENT_EXISTANT, NomUtilisateur = NOM_CLIENT_EXISTANT };
            Livre livre = new Livre { Id = ID_LIVRE, Valeur = 1 };
            serviceBDMock.Setup(sbm => sbm.ObtenirClient(ID_CLIENT_EXISTANT)).Returns(client);
            serviceBDMock.Setup(sbm => sbm.ObtenirLivre(ID_LIVRE)).Returns(livre);

            // Act
            var reponse = sut.AcheterLivre(ID_CLIENT_EXISTANT, livre.Id, livre.Valeur);

            // Assert
            serviceBDMock.Verify(l => l.ObtenirLivre(ID_LIVRE));
        }

        [Test]
        public void Acheterunlivre_MontantEstEgaleInferiorZero_RetournerMontant()
        {
            // Arrange
            ServiceLivre sut = new ServiceLivre(serviceBDMock.Object);
            Client client = new Client { Id = ID_CLIENT_EXISTANT, NomUtilisateur = NOM_CLIENT_EXISTANT };
            Livre livre = new Livre { Id = ID_LIVRE, Valeur = MONTANT_LIVRE };
            serviceBDMock.Setup(sbm => sbm.ObtenirClient(ID_CLIENT_EXISTANT)).Returns(client);
            serviceBDMock.Setup(sbm => sbm.ObtenirLivre(ID_LIVRE)).Returns(livre);

            // Act
            var reponse = sut.AcheterLivre(ID_CLIENT_EXISTANT, livre.Id, livre.Valeur);

            // Assert
            Assert.That(reponse, Is.AtMost(MONTANT_LIVRE));
        }

        #endregion

        #region Rembourser un livre

        //[Test]
        //public void RenommerClient_ClientExistant_NExistepas()
        //{
        //    // Arrange
        //    ServiceClient sut = new ServiceClient(serviceBDMock.Object);
        //    serviceBDMock.Setup(sbm => sbm.ObtenirClient(NOM_CLIENT)).Returns(() => null);

        //    // Act
        //    sut.RenommerClient(ID_CLIENT, NOM_CLIENT_VIDE);

        //    // Assert
        //    serviceBDMock.Verify(r => r.ObtenirClient(ID_CLIENT));
        //}

        //[Test]
        //public void RenommerClient_ClientExistant_Exist()
        //{
        //    // Arrange
        //    ServiceClient sut = new ServiceClient(serviceBDMock.Object);
        //    Client client = new Client { Id = ID_CLIENT, NomUtilisateur = NOM_CLIENT };
        //    serviceBDMock.Setup(sbm => sbm.ObtenirClient(ID_CLIENT)).Returns(client);

        //    // Act
        //    sut.RenommerClient(ID_CLIENT, NOM_CLIENT_EXISTANT);

        //    // Assert
        //    Assert.That(client.NomUtilisateur, Is.EqualTo(NOM_CLIENT_EXISTANT));
        //    serviceBDMock.Verify(r => r.ObtenirClient(ID_CLIENT));
        //}

        //[Test]
        //public void RenommerClient_ClientExistant_NomUtilise()
        //{
        //    // Arrange
        //    ServiceClient sut = new ServiceClient(serviceBDMock.Object);
        //    Client client = new Client { Id = ID_CLIENT, NomUtilisateur = NOM_CLIENT };

        //    serviceBDMock.Setup(sbm => sbm.ObtenirClient(ID_CLIENT)).Returns(client);
        //    serviceBDMock.Setup(sbm => sbm.ObtenirClient(NOM_CLIENT_EXISTANT)).Returns(client);

        //    // Act
        //    sut.RenommerClient(ID_CLIENT, NOM_CLIENT_EXISTANT);

        //    // Assert
        //    Assert.That(client.NomUtilisateur, Is.EqualTo(NOM_CLIENT_EXISTANT));
        //    serviceBDMock.Verify(r => r.ObtenirClient(ID_CLIENT));
        //}

        //[Test]
        //public void RenommerClient_ClientExistant_NomUtiliseNonUtilise()
        //{
        //    // Arrange
        //    ServiceClient sut = new ServiceClient(serviceBDMock.Object);
        //    Client client = new Client { Id = ID_CLIENT, NomUtilisateur = NOUVEAU_NOM_CLIENT };
        //    serviceBDMock.Setup(sbm => sbm.ObtenirClient(ID_CLIENT)).Returns(client);
        //    serviceBDMock.Setup(sbm => sbm.ObtenirClient(NOUVEAU_NOM_CLIENT)).Returns(client);

        //    // Act
        //    sut.RenommerClient(ID_CLIENT, NOUVEAU_NOM_CLIENT);

        //    // Assert
        //    Assert.That(client.NomUtilisateur, Is.EqualTo(NOUVEAU_NOM_CLIENT));

        //}

        #endregion

        #endregion
    }
}
