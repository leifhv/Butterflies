using Butterflies.Shared;
using ButterflyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ButterflyApi.Persistance
{
    internal class Entry<T>
    {
        public Entry(T item)
        {
            Item = item;
        }

        public DateTime RegTime { get; private set; } = DateTime.Now;
        public T Item { get; private set; }
    }

    public class ButterflyStore : IButterflyStore
    {
        private Dictionary<string, Entry<ButterflyDto>> _butterflyEntries = new Dictionary<string, Entry<ButterflyDto>>();

        private object _lockObject = new object();

        public ButterflyStore()
        {
            var defaultButterfly = new ButterflyDto();
            defaultButterfly.Id = "reddefault";
            defaultButterfly.Color = "#ff0000";
            Save(defaultButterfly);
            var defaultButterfly2 = new ButterflyDto();
            defaultButterfly2.Id = "bluedefault";
            defaultButterfly2.Color = "#0000ff";
            Save(defaultButterfly2);
            var defaultButterfly3 = new ButterflyDto();
            defaultButterfly3.Id = "greendefault";
            defaultButterfly3.Color = "#00ff00";
            Save(defaultButterfly3);
        }


        public ButterflyDto Save(ButterflyDto butterfly)
        {
            lock (_lockObject)
            {
                if (!string.IsNullOrEmpty(butterfly.Id)
                && _butterflyEntries.ContainsKey(butterfly.Id))
                {
                    _butterflyEntries[butterfly.Id] = new Entry<ButterflyDto>(butterfly);
                    return butterfly;
                }

                if (_butterflyEntries.Count > 100)
                {
                    var firstEntry = _butterflyEntries.OrderBy(e => e.Value.RegTime).FirstOrDefault();
                    _butterflyEntries.Remove(firstEntry.Key);
                }

                if(string.IsNullOrEmpty(butterfly.Id))
                {
                    butterfly.Id = Guid.NewGuid().ToString();
                }

                _butterflyEntries.Add(butterfly.Id, new Entry<ButterflyDto>(butterfly));
                return butterfly;
            }
        }

        public ButterflyDto Get(string id)
        {
            lock(_lockObject)
            {
                if (_butterflyEntries.ContainsKey(id))
                {
                    return _butterflyEntries[id].Item;
                }
                else
                {
                    return null;
                }
            }
        }


        public void Delete(string id)
        {
            lock(_lockObject)
            {
                if (_butterflyEntries.ContainsKey(id))
                {
                    _butterflyEntries.Remove(id);
                }

            }
        }

        public List<ButterflyDto> GetAllEntries()
        {
            lock (_lockObject)
            {
                return _butterflyEntries.Values.Select(e => e.Item).ToList();
            }
        }
    }
}
