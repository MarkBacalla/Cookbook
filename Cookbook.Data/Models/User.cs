using System;
using System.Collections.Generic;

namespace Cookbook.Data.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
