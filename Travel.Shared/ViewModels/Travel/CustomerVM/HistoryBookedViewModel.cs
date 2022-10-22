﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels.Travel.CustomerVM
{
    public class HistoryBookedViewModel
    {
        private string bookingNo ;
        private string idSchedule ;
        private long dateBooking ;
        private float totalPrice ;
        private int valuePromotion ;
        private string thumbsnail ;
        private int status;
        private long departureDate ;
        private long returnDate ;

        private string description;
        private string nameTour;


        private int adult;
        private int child;
        private int baby;
        public long DepartureDate { get => departureDate; set => departureDate = value; }
        public long ReturnDate { get => returnDate; set => returnDate = value; }
        public int ValuePromotion { get => valuePromotion; set => valuePromotion = value; }
        public string Thumbsnail { get => thumbsnail; set => thumbsnail = value; }
        public string BookingNo { get => bookingNo; set => bookingNo = value; }
        public string IdSchedule { get => idSchedule; set => idSchedule = value; }
        public long DateBooking { get => dateBooking; set => dateBooking = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public int Status { get => status; set => status = value; }
        public string Description { get => description; set => description = value; }
        public string NameTour { get => nameTour; set => nameTour = value; }
        public int Adult { get => adult; set => adult = value; }
        public int Child { get => child; set => child = value; }
        public int Baby { get => baby; set => baby = value; }
    }
}
