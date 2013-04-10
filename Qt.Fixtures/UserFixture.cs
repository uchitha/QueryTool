using Qt.Data;
using Qt.Domain.Entity;

namespace Qt.Fixtures
{
    class UserFixture : IFixture
    {
        public void Run(IRepositoryLocator locator)
        {
            Create2Users(locator);
        }

        private static void Create2Users(IRepositoryLocator locator)
        {
            var user = new User()
                           {
                               Name = "Chris Ranasinghe",
                           };

            locator.Save(user);

            var user2 = new User()
            {
                Name = "Anil De Silva",
            };

            locator.Save(user2);

        }

        private static void CreateUsersFromCsv(IRepositoryLocator locator)
        {

            var loader = new EntityLoader<User>("user.csv");
            var users = loader.LoadEntities();

            foreach (var user in users)
            {
                locator.Save(user);
            }
        }
    }
}
