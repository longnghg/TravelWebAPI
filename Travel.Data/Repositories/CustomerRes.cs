﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.CustomerVM;
using Travel.Shared.Ultilities;
using PrUtility;
using Travel.Context.Models.Travel;
using Travel.Context.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Travel.Data.Repositories
{
    public class CustomerRes : ICustomer
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;

        public CustomerRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }

        public string CheckBeforeSave(IFormCollection frmdata, IFormFile file, ref Notification _message, bool isUpdate)
        {
            try
            {
                JObject frmData = JObject.Parse(frmdata["data"]);
                if (frmData != null)
                {
                    var idCustomer = PrCommon.GetString("idCustomer", frmData);
                    if (String.IsNullOrEmpty(idCustomer))
                    {
                        idCustomer = Guid.NewGuid().ToString();
                    }

                    var nameCustomer = PrCommon.GetString("nameCustomer", frmData);
                    if (String.IsNullOrEmpty(nameCustomer))
                    {
                    }

                    var email = PrCommon.GetString("email", frmData);
                    if (String.IsNullOrEmpty(email))
                    {
                    }


                    var phone = PrCommon.GetString("phone", frmData);
                    if (String.IsNullOrEmpty(phone))
                    {
                    }
                    var birthday = PrCommon.GetString("birthday", frmData);
                    if (String.IsNullOrEmpty(birthday))
                    {
                    }


                    var address = PrCommon.GetString("address", frmData);
                    if (String.IsNullOrEmpty(address))
                    {
                    }

                    var password = PrCommon.GetString("password", frmData);
                    if (String.IsNullOrEmpty(password))
                    {
                    }

                    var gender = PrCommon.GetString("gender", frmData);
                    if (String.IsNullOrEmpty(gender))
                    {
                    }

                    var modifyBy = PrCommon.GetString("modifyBy", frmData);
                    if (String.IsNullOrEmpty(modifyBy))
                    {
                    }

                    if (isUpdate)
                    {
                    }
                        CreateCustomerViewModel objCreate = new CreateCustomerViewModel();
                        objCreate.IdCustomer = Guid.Parse(idCustomer);
                        objCreate.NameCustomer = nameCustomer;
                        objCreate.Phone = phone;
                        objCreate.Email = email;
                        objCreate.Address = address;
                        //objCreate.Birthday = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(birthday));
                        objCreate.Password = Ultility.Encryption(password);
                        return JsonSerializer.Serialize(objCreate);
                    }
                    return string.Empty;
                }
            catch (Exception e)
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";

                _message = message;
                return string.Empty;
            }     
        }

        public Response Create(CreateCustomerViewModel input)
        {
            try
            {
                Customer customer = Mapper.MapCreateCustomer(input);
                customer.IdCustomer = Guid.NewGuid();
                customer.Point = 0;
                customer.IsDelete = false;
                _db.Customers.Add(customer);
                _db.SaveChanges();

                res.Notification.DateTime = DateTime.Now;
                res.Notification.Messenge = "Thêm thành công !";
                res.Notification.Type = "Success";
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;

            }
        }

        public Response Gets()
        {
            try
            {                       
                var listCus = (from x in _db.Customers where x.IsDelete == false select x).ToList();
                var result = Mapper.MapCustomer(listCus);

                if (listCus.Count() > 0)
                {
                    res.Content = result;
                }
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }
        public Response GetsHistory(Guid idCustomer)
        {
            try
            {
                var list = (from x in _db.Tourbookings
                            where x.CustomerId == idCustomer
                            select new Tourbooking
                            {
                                Status = x.Status,
                                TotalPrice = x.TotalPrice,
                                TotalPricePromotion = x.TotalPricePromotion,
                                ScheduleId = x.ScheduleId,
                                DateBooking = x.DateBooking,
                                BookingNo = x.BookingNo,
                                ValuePromotion = x.ValuePromotion,
                                TourbookingDetails = (from tbd in _db.tourbookingDetails 
                                                      where tbd.IdTourbookingDetails == x.IdTourbooking 
                                                      select tbd).First(),
                                Schedule = (from s in _db.Schedules
                                            where x.ScheduleId == s.IdSchedule
                                            select new Schedule {
                                                Description = s.Description,
                                                DepartureDate = s.DepartureDate,
                                                ReturnDate = s.ReturnDate,
                                                Tour = (from t in _db.Tour
                                                        where s.TourId == t.IdTour
                                                        select new Tour { 
                                                        Thumbsnail = t.Thumbsnail,
                                                        NameTour = t.NameTour,
                                                        FromPlace = t.FromPlace,
                                                        ToPlace = t.ToPlace
                                                        }).First()
                                            }).First()
                            }).ToList();
                if (list.Count() > 0)
                {
                    var result = Mapper.MapHistoryCustomerViewModel(list);
                    res.Content = result;
                }
                return res;
                //var list = (from x in _db.Tourbookings
                //            where x.CustomerId == idCustomer
                //            select new Tourbooking {
                //                ValuePromotion = x.ValuePromotion,
                //                CustomerId = x.CustomerId,
                //                IsCalled = x.IsCalled,
                //                NameContact = x.NameContact,
                //                NameCustomer = x.NameCustomer,
                //                DateBooking = x.DateBooking,
                //                Deposit = x.Deposit,
                //                VoucherCode = x.VoucherCode,
                //                Address = x.Address,
                //                LastDate = x.LastDate,
                //                ModifyDate = x.ModifyDate,
                //                BookingNo = x.BookingNo,
                //                Email = x.Email,
                //                IdTourbooking = x.IdTourbooking,
                //                ModifyBy = x.ModifyBy,

                //                PaymentId = x.PaymentId,
                //                Phone = x.Phone,
                //                Pincode = x.Pincode,
                //                RemainPrice = x.RemainPrice,
                //                ScheduleId = x.ScheduleId,
                //                TotalPrice = x.TotalPrice,
                //                TotalPricePromotion = x.TotalPricePromotion,
                //                Vat = x.Vat,
                //                Payment = (from p in _db.Payment where p.IdPayment == x.PaymentId select p).First(),
                //                Schedule = (from s in _db.Schedules where s.IdSchedule == x.ScheduleId
                //                            select new Schedule{ 
                //                DepartureDate = s.DepartureDate,
                //                Tour = (from t  in _db.Tour where t.IdTour == s.TourId select t).First()
                //                }).First(),
                //                TourbookingDetails = (from td in _db.tourbookingDetails where td.IdTourbookingDetails == x.IdTourbooking select td).First()
                //            }).ToList();
                //var result = Mapper.MapTourBooking(list);

                //if (list.Count() > 0)
                //{
                //    res.Content = result;
                //}
                //return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        public Response GetCustomer(Guid idCustomer)
        {
            try
            {
                var customer = (from x in _db.Customers
                                where x.IdCustomer == idCustomer
                                select x).First();
                if (customer != null)
                {
                    var result = Mapper.MapCustomer(customer);
                    res.Content = result;
                }
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }
    }
}
