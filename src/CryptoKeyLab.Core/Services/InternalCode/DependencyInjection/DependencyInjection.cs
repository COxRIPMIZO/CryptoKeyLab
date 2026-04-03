using CryptoKeyLab.Core.Services.InternalCode.ApiKeyHashing;
using CryptoKeyLab.Cryptography.Hashing.Cryptographic.Sha3;
using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Interfaces.SystemInternal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Core.Services.InternalCode.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiHashingService(this IServiceCollection serviceDescriptors)
        {
            // 1. The API project calls this ONE method. 
            // 2. This class is ALLOWED to reference Cryptography because it's inside Core.

            serviceDescriptors.AddScoped<IHashAlgorithm, Sha3_512Algorithm>();
            serviceDescriptors.AddScoped<ISystemHashProvider, SystemHashProvider>();

            return serviceDescriptors;
        }
    }
}
