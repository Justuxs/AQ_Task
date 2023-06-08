using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Models;

namespace Task.Helpers
{
    public class HousingMismatch
    {
        public Harness_wire harness_wire1 { get; set; }
        public Harness_wire harness_wire2 { get; set; }
        public string message { get; set; } = string.Empty;

        public HousingMismatch(Harness_wire harness_wire1, Harness_wire harness_wire2, string message)
        {
            this.harness_wire1 = harness_wire1;
            this.harness_wire2 = harness_wire2;
            this.message = message;
        }
    }
}
