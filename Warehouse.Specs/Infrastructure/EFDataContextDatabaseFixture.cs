using Warehouse.PersistenceEF;
using Xunit;

namespace Warehouse.Specs.Infrastructure
{
    [Collection(nameof(ConfigurationFixture))]
    public class EFDataContextDatabaseFixture : DatabaseFixture
    {
        //readonly ConfigurationFixture _configuration;

        public EFDataContextDatabaseFixture(/*ConfigurationFixture configuration*/)
        {
            //_configuration = configuration;
        }

        public EFDataContext CreateDataContext()
        {
            return new EFDataContext("server=.;database=WarehouseTest;trusted_connection=true;");
        }
    }
}
