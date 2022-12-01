using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteForMovement
{
    public class Shop: ICloneable
    {
        public Shop()
        {
        }

        public Shop(Shop shop)
        {
            shop.CopyTo(this);
        }

        public Shop CloneShop()
        {
            return new Shop(this);
        }

        public string Id { get; set; }
        public string Address { get; set; }       
        public int AreaId { get; set; }

        public void CopyTo(Shop shop)
        {
            shop.Id = this.Id;
            shop.Address = this.Address;
            shop.AreaId = this.AreaId;
        }

        public object Clone()
        {
            return new Shop(this);
            /*Shop other = (Shop)this.MemberwiseClone();
            other.Address = string.Copy(Address);
            return other;*/
        }
    }
}
