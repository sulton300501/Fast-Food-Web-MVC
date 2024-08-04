﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public double DisCount { get; set; }
        public double MinumumAmount { get; set; }
        public byte[] CouponPicture {  get; set; }
        public bool isActive { get; set; }

    }


    public enum CouponType
    {
        Percent=0,
        Currency =1
    }


}
