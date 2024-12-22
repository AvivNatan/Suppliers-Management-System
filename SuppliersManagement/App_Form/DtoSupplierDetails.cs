using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuppliersManagement.App_Form
{
    public class DtoSupplierDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPhoneNumber { get; set; }
        public string CreateDate { get; set; }
        public string SupplierType { get; set; }
        public string ExtraDetails { get; set; }
    }
}