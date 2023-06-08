using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Models
{
    public class Harness_drawing
    {
        public int ID { get; set; }
        public string? harness { get; set; }
        public string? harness_version { get; set; }
        public string? drawing { get; set; }
        public string? drawing_version { get; set; }
        public List<Harness_wire> harness_wires { get; set; } = new List<Harness_wire>(); 

        public Harness_drawing()
        {

        }

        public override string ToString()
        {
            string wiresInfo = "\n ---->"+ string.Join("\n ---->", harness_wires);
            return $"ID: {ID}, Harness: {harness}, Harness Version: {harness_version}, Drawing: {drawing}, Drawing Version: {drawing_version}, Harness Wires: {wiresInfo}";
        }

    }
}
