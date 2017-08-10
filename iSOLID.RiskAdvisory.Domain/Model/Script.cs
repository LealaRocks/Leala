using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public class Script : AggregateRoot
    {
        public Script()
        {
            this.Tabs = new List<Tab>();
            this.ExceptionCategories = new List<DataExceptionCategory>();
        }

        public string Name { get; set; }


        public List<Tab> Tabs { get; set; }

        public List<DataExceptionCategory> ExceptionCategories { get; set; }
        
    }
}
