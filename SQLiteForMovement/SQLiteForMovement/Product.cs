using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteForMovement
{
    public class Product
    {
        public Product()
        {

        }

        public Product(Product product)
        {
            product.CopyTo(this);
        }

        public Product Clone()
        {
            return new Product(this);
        }

        public int Id { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Count { get; set; }
        public int VendorId { get; set; }

        public void CopyTo(Product product)
        {
            product.Id = this.Id;
            product.Department = this.Department;
            product.Name = this.Name;
            product.Unit = this.Unit;
            product.Count = this.Count;
            product.VendorId = this.VendorId;
        }
    }
}
