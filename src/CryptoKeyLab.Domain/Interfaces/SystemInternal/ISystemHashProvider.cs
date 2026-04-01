using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces.SystemInternal
{
    public interface ISystemHashProvider
    {
        string ComputeHash(string input);
    }
}
