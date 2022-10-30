﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Context.Models
{
    public class Restaurant
    {
        public Guid IdRestaurant { get; set; }
        public Guid ContractId { get; set; }
        public string NameRestaurant { get; set; }
        public float ComboPrice { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Approve { get; set; }
        public string IdAction { get; set; }
        public string TypeAction { get; set; }
        public string ModifyBy { get; set; }
        public long ModifyDate { get; set; }
        public virtual ICollection<CostTour> CostTours { get; set; }
        public virtual ICollection<TourbookingDetails> TourBookingDetails { get; set; }

    }
}
