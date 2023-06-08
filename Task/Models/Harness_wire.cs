using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Models
{
    public class Harness_wire
    {
        public int ID { get; set; }
        public int harness_ID { get; set; }
        public float? length { get; set; }
        public string? color { get; set; }
        public string? housing_1 { get; set; }
        public string? housing_2 { get; set; }

        public Harness_wire()
        {

        }
        public override string ToString()
        {
            return $"ID: {ID}, Harness ID: {harness_ID}, Length: {length}, Color: {color}, Housing 1: {housing_1}, Housing 2: {housing_2}";
        }

    }
}
