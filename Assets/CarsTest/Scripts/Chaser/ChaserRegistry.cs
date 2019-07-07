using System.Collections.Generic;

namespace CarsTest
{
    public class ChaserRegistry
    {
        private readonly List<ChaserFacade> _enemies = new List<ChaserFacade>();
        public IEnumerable<ChaserFacade> Enemies => _enemies;

        public void AddChaser(ChaserFacade enemy)
        {
            _enemies.Add(enemy);
        }

        public void RemoveChaser(ChaserFacade enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}
