namespace Project.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Client
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
