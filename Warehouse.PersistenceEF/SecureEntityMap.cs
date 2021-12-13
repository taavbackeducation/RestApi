using Microsoft.EntityFrameworkCore.DataEncryption;

namespace Warehouse.PersistenceEF
{
    public class SecureEntityMap
    {
        protected readonly IEncryptionProvider _provider;

        public SecureEntityMap(IEncryptionProvider provider)
        {
            _provider = provider;
        }
    }
}
