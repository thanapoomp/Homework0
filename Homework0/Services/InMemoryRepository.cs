using Homework0.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework0.Services
{
    public class InMemoryRepository
    {
        private List<Counter> _counters;
        public InMemoryRepository()
        {
            _counters = new List<Counter>();
        }

        public List<Counter> GetAllCounters()
        {
            return _counters;
        }

        public Counter GetById(int id)
        {
            var exsists = _counters.Where(x => x.Id == id).Any();
            if (!exsists)
            {
                return null;
            }

            var result = _counters.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public void Clear()
        {
            _counters.Clear();
        }

        public void Set(int count)
        {
            for (int i = 1; i < count + 1; i++)
            {
                _counters.Add(new Counter(){ Id= i, Clicked = 0});
            }
        }
    }
}
