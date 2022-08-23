using Portfolio.Areas._7DTD.Models;
using Portfolio.Areas._7DTD.Services.Repository;

namespace Portfolio_UnitTests.Areas._7DTD.Services
{
    public class RepositoryTest
    {
        private readonly BloodNightRepository _repository;

        public RepositoryTest()
        {
            _repository = new();
        }

        public void GetViewNotNULL()
        {
            BloodNightView res = _repository.GetView();

            Assert.NotNull(res);
        }
    }
}
