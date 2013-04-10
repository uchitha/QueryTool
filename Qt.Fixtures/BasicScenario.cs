namespace Qt.Fixtures
{
    public class BasicScenario : FixtureBase
    {
        public BasicScenario()
        {
            AddFixture(new UserFixture());
            AddFixture(new QueryFixture());
            AddFixture(new QueryGroupFixture());
        }
    }

}
