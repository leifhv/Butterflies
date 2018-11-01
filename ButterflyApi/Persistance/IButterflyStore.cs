using System.Collections.Generic;
using Butterflies.Shared;
using ButterflyApi.Models;

namespace ButterflyApi.Persistance
{
    public interface IButterflyStore
    {
        void Delete(string id);
        ButterflyDto Get(string id);
        List<ButterflyDto> GetAllEntries();
        ButterflyDto Save(ButterflyDto butterfly);
    }
}