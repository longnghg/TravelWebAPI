﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Shared.Ultilities;

namespace Travel.Shared.ViewModels.Travel
{
    public class TourBookingViewModel
    {
        private string idTourBooking;
        private string nameCustomer;
        private string address;
        private string email;
        private string phone;
        private string nameContact;

        private long dateBooking;
        private long lastDate;
        private double vat;

        private string pincode;
        private string voucherCode;
        private string bookingNo;

        private bool isCalled;
        private float deposit;

        private float remainPrice;
        private float totalPrice;
        private float totalPricePromotion;
        private string modifyBy;
        private long modifyDate;


        private Schedule schedule;
        private TourbookingDetails tourbookingDetails;
        private Payment payment;



        public string IdTourBooking { get => idTourBooking; set => idTourBooking = value; }
        public string NameCustomer { get => nameCustomer; set => nameCustomer = value; }
        public string Address { get => address; set => address = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string NameContact { get => nameContact; set => nameContact = value; }
        public long DateBooking { get => dateBooking; set => dateBooking = value; }
        public long LastDate { get => lastDate; set => lastDate = value; }
        public double Vat { get => vat; set => vat = value; }
        public string Pincode { get => pincode; set => pincode = value; }
        public string VoucherCode { get => voucherCode; set => voucherCode = value; }
        public string BookingNo { get => bookingNo; set => bookingNo = value; }
        public bool IsCalled { get => isCalled; set => isCalled = value; }
        public float Deposit { get => deposit; set => deposit = value; }
        public float RemainPrice { get => remainPrice; set => remainPrice = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public string ModifyBy { get => modifyBy; set => modifyBy = value; }
        public long ModifyDate { get => modifyDate; set => modifyDate = value; }
        public Schedule Schedule { get => schedule; set => schedule = value; }
        public TourbookingDetails TourbookingDetails { get => tourbookingDetails; set => tourbookingDetails = value; }
        public Payment Payment { get => payment; set => payment = value; }
        public float TotalPricePromotion { get => totalPricePromotion; set => totalPricePromotion = value; }
    }
}
