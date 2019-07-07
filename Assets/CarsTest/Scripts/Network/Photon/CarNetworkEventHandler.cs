using Photon.Pun;

namespace CarsTest.Photon
{
    public class CarNetworkEventHandler
    {
        private readonly NetworkCarFacade _carFacade;
        public CarNetworkEventHandler(NetworkCarFacade carFacade)
        {
            _carFacade = carFacade;
        }

        [PunRPC]
        public void DestroyCar()
        {
            _carFacade.Die();
        }
    }
}