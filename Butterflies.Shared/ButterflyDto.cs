using System;
using System.Collections.Generic;
using System.Text;

namespace Butterflies.Shared
{
    public class ButterflyDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "";
        public string Color { get; set; } = "#f8f8f8";
        public int Scale { get; set; } = 40;
        public int N { get; set; } = 800;
        public int NumWings { get; set; } = 4;
    }
}
