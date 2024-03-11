using Librairie.Entities;
using Librairie.Services;
using Librairie.Services.Interfaces;
using Moq;

namespace LibrarieTest.Moq
{
    [TestFixture]
    public class ServiceClientTests
    {

        #region fields
        private Mock<IServiceBD> serviceBDMock;
        private Guid ID_CLIENT = new Guid();
        const string NOM_CLIENT_VIDE = null;
        const string NOM_CLIENT = "Wendy";
        const string NOM_CLIENT_EXISTANT = "Wendy";
        const string NOUVEAU_NOM_CLIENT = "TinkerBell";
        #endregion

        #region constructor
        public ServiceClientTests()
        {
            serviceBDMock = new Mock<IServiceBD>();
        }

        #endregion

        #region public methods

        #region CreerClient

        [Test]
        public void CreerClient_SansNomUtilisateurRenseigne_RetourneNull()
        {
            // Arrange
            ServiceClient sut = new ServiceClient(serviceBDMock.Object);

            // Act
            var newClient = sut.CreerClient(NOM_CLIENT_VIDE);

            // Assert
            Assert.IsNull(newClient);
        }

        [Test]
        public void CreerClient_AvecNomUtilisateurExistant_RetourneClient()
        {
            // Arrange
            ServiceClient sut = new ServiceClient(serviceBDMock.Object);
            Client client = new Client { NomUtilisateur = NOM_CLIENT_EXISTANT };
            serviceBDMock.Setup(sbm => sbm.ObtenirClient(NOM_CLIENT_EXISTANT)).Returns(client);

            // Act
            var clientExistant = sut.CreerClient(NOM_CLIENT_EXISTANT);

            // Assert
            Assert.That(clientExistant.NomUtilisateur, Is.EqualTo(NOM_CLIENT_EXISTANT));
        }

        [Test]
        public void CreerClient_AvecNomUtilisateurNonExistant_AjoutClient()
        {
            // Arrange
            ServiceClient sut = new ServiceClient(serviceBDMock.Object);
            Client client = new Client { NomUtilisateur = NOM_CLIENT };
            serviceBDMock.Setup(sbm => sbm.ObtenirClient(NOM_CLIENT)).Returns(() => null);
            serviceBDMock.Setup(cli => cli.AjouterClient(client)).Verifiable();

            // Act
            var nouveauClient = sut.CreerClient(NOM_CLIENT);

            // Assert
            Assert.That(nouveauClient.NomUtilisateur, Is.EqualTo(NOM_CLIENT));
            serviceBDMock.Verify(r => r.AjouterClient(It.IsAny<Client>()));
        }

        #endregion

        #region RenommerClient

        [Test]
        public void RenommerClient_ClientExistant_NExistepas()
        {
            // Arrange
            ServiceClient sut = new ServiceClient(serviceBDMock.Object);
            serviceBDMock.Setup(sbm => sbm.ObtenirClient(NOM_CLIENT)).Returns(() => null);

            // Act
            sut.RenommerClient(ID_CLIENT, NOM_CLIENT_VIDE);

            // Assert
            serviceBDMock.Verify(r => r.ObtenirClient(ID_CLIENT));
        }

        [Test]
        public void RenommerClient_ClientExistant_Exist()
        {
            // Arrange
            ServiceClient sut = new ServiceClient(serviceBDMock.Object);
            Client client = new Client { Id = ID_CLIENT, NomUtilisateur = NOM_CLIENT };
            serviceBDMock.Setup(sbm => sbm.ObtenirClient(ID_CLIENT)).Returns(client);

            // Act
            sut.RenommerClient(ID_CLIENT, NOM_CLIENT_EXISTANT);

            // Assert
            Assert.That(client.NomUtilisateur, Is.EqualTo(NOM_CLIENT_EXISTANT));
            serviceBDMock.Verify(r => r.ObtenirClient(ID_CLIENT));
        }

        [Test]
        public void RenommerClient_ClientExistant_NomUtilise()
        {
            // Arrange
            ServiceClient sut = new ServiceClient(serviceBDMock.Object);
            Client client = new Client { Id = ID_CLIENT, NomUtilisateur = NOM_CLIENT };

            serviceBDMock.Setup(sbm => sbm.ObtenirClient(ID_CLIENT)).Returns(client);
            serviceBDMock.Setup(sbm => sbm.ObtenirClient(NOM_CLIENT_EXISTANT)).Returns(client);

            // Act
            sut.RenommerClient(ID_CLIENT, NOM_CLIENT_EXISTANT);

            // Assert
            Assert.That(client.NomUtilisateur, Is.EqualTo(NOM_CLIENT_EXISTANT));
            serviceBDMock.Verify(r => r.ObtenirClient(ID_CLIENT));
        }

        [Test]
        public void RenommerClient_ClientExistant_NomUtiliseNonUtilise()
        {
            // Arrange
            ServiceClient sut = new ServiceClient(serviceBDMock.Object);
            Client client = new Client { Id = ID_CLIENT, NomUtilisateur = NOUVEAU_NOM_CLIENT };
            serviceBDMock.Setup(sbm => sbm.ObtenirClient(ID_CLIENT)).Returns(client);
            serviceBDMock.Setup(sbm => sbm.ObtenirClient(NOUVEAU_NOM_CLIENT)).Returns(client);

            // Act
            sut.RenommerClient(ID_CLIENT, NOUVEAU_NOM_CLIENT);

            // Assert
            Assert.That(client.NomUtilisateur, Is.EqualTo(NOUVEAU_NOM_CLIENT));

        }

        #endregion

        #endregion

    }
}
