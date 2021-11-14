using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalPackage.DAO
{
    public class EmployeeDao
    {
        public long Id { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }        
        public string FamilyLinkId { get; set; }
        public int AnnualSalary { get; set; }
    }
}
