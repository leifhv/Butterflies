using Butterflies.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Butterflies.Server.Controllers
{
    [Route("api/[controller]")]
    public class SortController : Controller
    {
        [HttpPost("[action]")]
        public IEnumerable<int> BubbleSort([FromBody]SortTask sortTask)
        {
            BubbleSorter.Sort(sortTask);
            return sortTask.Integers;
        }
    }
}
