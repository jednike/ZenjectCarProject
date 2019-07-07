using UnityStandardAssets.Cameras;

namespace CarsTest
{
    public class PlayerCameraHandler
    {
        public PlayerCameraHandler(CarView view, Camera cameraBehavior, LookatTarget lookatTarget)
        {
            cameraBehavior.Target = view;
            lookatTarget.Target = view.CarTransform;
        }
    }
}