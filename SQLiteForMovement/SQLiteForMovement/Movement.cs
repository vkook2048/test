using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLiteForMovement
{
    public class Movement: ICloneable
    {
        [NotMapped]
        public ApplicationContext Context { get; set; }

        [NotMapped]
        public string[] Operations { get; set; } = new string[] { "Поступление", "Продажа" };

        [NotMapped]
        public List<Product> Products { 
            get { return new List<Product>(Context.Products); }
        }

        [NotMapped]
        public List<Shop> Shops
        {
            get { return new List<Shop>(Context.Shops); }
        }

        public int Id { get; set; }
        public string Date { get; set; }
        public Shop Shop { get; set; }
        public Product Product { get; set; }

        public int Count { get; set; }
        public string Operation { get; set; }
        public double Price { get; set; }

        public object Clone()
        {
            Movement other = (Movement)this.MemberwiseClone();
            other.Shop = new Shop(Shop);
            other.Product = new Product(Product);
            other.Date = String.Copy(Date);
            other.Operation = String.Copy(Operation);
            return other;
        }

        public void CopyTo(Movement movement)
        {
            movement.Id = this.Id;
            movement.Date = this.Date;
            movement.Shop = this.Shop;
            movement.Product = this.Product;
            movement.Count = this.Count;
            movement.Operation = this.Operation;
            movement.Price = this.Price;
        }
    }
}
