﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PrUtility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;
using static Travel.Shared.Ultilities.Enums;

namespace Travel.Data.Repositories
{

    public class ScheduleRes : ISchedule
    {
        private readonly TravelContext _db;
        private Notification message;
        private long dateTimeNow;

        public ScheduleRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            dateTimeNow = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now.AddMinutes(-3));
        }

        private void UpdateDatabase(Schedule input)
        {
            _db.Entry(input).State = EntityState.Modified;
        }
        private void DeleteDatabase(Schedule input)
        {
            _db.Entry(input).State = EntityState.Deleted;
        }
        private void CreateDatabase(Schedule input)
        {
            _db.Entry(input).State = EntityState.Added;
        }
        private async Task SaveChangeAsync()
        {
            await _db.SaveChangesAsync();
        }
        private void SaveChange()
        {
            _db.SaveChanges();
        }
        public string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate)
        {
            try
            {
                var idSchedule = PrCommon.GetString("idSchedule", frmData);
                if (String.IsNullOrEmpty(idSchedule))
                {
                }
                #region check update when having tour booking
                if (isUpdate)
                {
                    if (CheckAnyBookingInSchedule(idSchedule))
                    {
                        _message = Ultility.Responses("Chuyến đi này đang có booking !", Enums.TypeCRUD.Warning.ToString()).Notification;
                        return null;
                    }
                }

                #endregion
                var tourId = PrCommon.GetString("tourId", frmData);

                if (String.IsNullOrEmpty(tourId))
                {
                }
                var carId = PrCommon.GetString("carId", frmData);
                if (String.IsNullOrEmpty(carId))
                {
                }
                var employeeId = PrCommon.GetString("employeeId", frmData);
                if (String.IsNullOrEmpty(employeeId))
                {
                }
                var promotionId = PrCommon.GetString("promotionId", frmData);
                if (String.IsNullOrEmpty(promotionId))
                {
                }
                var departurePlace = PrCommon.GetString("departurePlace", frmData);
                if (String.IsNullOrEmpty(departurePlace))
                {
                }
                var departureDate = PrCommon.GetString("departureDate", frmData);
                if (String.IsNullOrEmpty(departureDate))
                {
                }
                var returnDate = PrCommon.GetString("returnDate", frmData);
                if (String.IsNullOrEmpty(returnDate))
                {
                }
                var beginDate = PrCommon.GetString("beginDate", frmData);
                if (String.IsNullOrEmpty(beginDate))
                {
                }
                var endDate = PrCommon.GetString("endDate", frmData);
                if (String.IsNullOrEmpty(endDate))
                {
                }
                var timePromotion = PrCommon.GetString("timePromotion", frmData);
                if (String.IsNullOrEmpty(timePromotion))
                {
                }
                var description = PrCommon.GetString("description", frmData);
                if (String.IsNullOrEmpty(description))
                {
                }
                var vat = PrCommon.GetString("vat", frmData);
                if (String.IsNullOrEmpty(vat))
                {
                }
                var minCapacity = PrCommon.GetString("minCapacity", frmData);
                if (String.IsNullOrEmpty(minCapacity))
                {
                }
                var maxCapacity = PrCommon.GetString("maxCapacity", frmData);
                if (String.IsNullOrEmpty(maxCapacity))
                {
                }

                var idUserModify = PrCommon.GetString("idUserModify", frmData);
                if (String.IsNullOrEmpty(idUserModify))
                {
                }
                var typeAction = PrCommon.GetString("typeAction", frmData);

                if (isUpdate)
                {
                    UpdateScheduleViewModel updateObj = new UpdateScheduleViewModel();
                    updateObj.TourId = tourId;
                    updateObj.CarId = Guid.Parse(carId);
                    updateObj.EmployeeId = Guid.Parse(employeeId);
                    updateObj.PromotionId = Convert.ToInt32(promotionId);
                    updateObj.Description = description;
                    updateObj.DeparturePlace = departurePlace;

                    updateObj.DepartureDate = long.Parse(departureDate);
                    updateObj.ReturnDate = long.Parse(returnDate);
                    updateObj.BeginDate = long.Parse(beginDate);
                    updateObj.EndDate = long.Parse(endDate);
                    updateObj.TimePromotion = long.Parse(timePromotion);

                    updateObj.MinCapacity = Convert.ToInt16(minCapacity);
                    updateObj.MaxCapacity = Convert.ToInt16(maxCapacity);
                    updateObj.IdSchedule = idSchedule;
                    updateObj.TypeAction = typeAction;
                    updateObj.IdUserModify = Guid.Parse(idUserModify);

                    // price 
                    updateObj.Vat = float.Parse(vat);
                    return JsonSerializer.Serialize(updateObj);
                }
                CreateScheduleViewModel createObj = new CreateScheduleViewModel();
                createObj.TourId = tourId;
                createObj.CarId = Guid.Parse(carId);
                createObj.EmployeeId = Guid.Parse(employeeId);
                createObj.PromotionId = Convert.ToInt32(promotionId);
                createObj.Description = description;
                createObj.Vat = float.Parse(vat);
                createObj.DeparturePlace = departurePlace;
                createObj.DepartureDate = long.Parse(departureDate);
                createObj.ReturnDate = long.Parse(returnDate);
                createObj.BeginDate = long.Parse(beginDate);
                createObj.EndDate = long.Parse(endDate);
                createObj.TimePromotion = long.Parse(timePromotion);
                createObj.MinCapacity = Convert.ToInt16(minCapacity);
                createObj.MaxCapacity = Convert.ToInt16(maxCapacity);
                createObj.IdSchedule = $"{tourId}-S{Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now)}";
                createObj.IdUserModify = Guid.Parse(idUserModify);

                return JsonSerializer.Serialize(createObj);
            }
            catch (Exception e)
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";
                _message = message;
                return null;
            }
        }

        public Response Gets()
        {
            try
            {
                var stopWatchEntitya1 = Stopwatch.StartNew();
                var schedule = (from x in _db.Schedules.AsNoTracking()
                                where x.IdSchedule == "1667699141567-x459298"
                                select x).FirstOrDefault();
                var b2 = schedule.Alias;

                schedule.Alias = "Ngay hom nay";
                _db.Entry(schedule).State = EntityState.Deleted;
                _db.SaveChanges();
                var dont1 = stopWatchEntitya1.Elapsed;

                var b = schedule.Alias;
                var c = 0;
                //var bab = _db.ChangeTracker.LazyLoadingEnabled;

                //_db.ChangeTracker.LazyLoadingEnabled = false;
                //var b32 = _db.ChangeTracker.LazyLoadingEnabled;






                //var ls11 = _db.Tour.ToList();
                //var ls121 = _db.Tour.AsNoTracking().ToList();
                //var ls1221 = (from x in _db.Tour select x).ToList();
                //var ls2 = (from x in _db.Schedules select x).AsNoTracking().OrderByDescending(x=> x.IdSchedule).ToList();

                //var kls2 = (from x in _db.Schedules select x).OrderByDescending(x => x.IdSchedule).ToList();


                //var kl3s2 = _db.Schedules.OrderByDescending(x => x.IdSchedule).ToList();


                //var ls = (from x in _db.Tour select x).ToList();

                //var kut = _db.Schedules;
                //var b = 1;






                //var ls31 = _db.Schedules.AsSingleQuery().ToList();
                //var ls3 = (from x in _db.Schedules select x).AsSingleQuery().ToList();
                //var stopWatch1 = Stopwatch.StartNew();
                //var ls331 = _db.Schedules.AsSplitQuery().ToList();
                //var b1 = stopWatch1.Elapsed;

                //var stopWatch2 = Stopwatch.StartNew();
                //var ls33 = (from x in _db.Schedules select x).AsSplitQuery().ToList();
                //var b2 = stopWatch2.Elapsed;
                //for (int i = 0; i < 10000; i++)
                //{
                //    var schedule = new Schedule();
                //    schedule.IdSchedule = $"{ Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now)}-{Ultility.GenerateRandomCode()}";
                //    schedule.AdditionalPrice = 0;
                //    schedule.IdUserModify = Guid.Empty;
                //    schedule.TourId = "DLNT-1666353913295";
                //    schedule.CarId = Guid.Parse("F9EB1352-3649-4AC3-A5AA-FE4FCFF0118A");
                //    schedule.EmployeeId = Guid.Parse("6E6C3C53-DFB9-4B69-B66E-2BE93C677DA7");
                //    schedule.PromotionId = 1;
                //    schedule.DepartureDate = 1;
                //    schedule.BeginDate = 1;
                //    schedule.EndDate = 1;
                //    schedule.Approve = 1;
                //    schedule.ReturnDate = 1;
                //    schedule.TimePromotion = 1;
                //    schedule.MinCapacity = 0;
                //    schedule.MaxCapacity = 0;
                //    schedule.QuantityAdult = 1;
                //    schedule.QuantityBaby = 1;
                //    schedule.QuantityChild = 1;
                //    _db.Schedules.Add(schedule);
                //}
                //_db.SaveChanges();


                //var stopWatchEntity1 = Stopwatch.StartNew();
                //var list42444 = _db.Schedules.Where(x => x.Isdelete == false &&
                //x.Approve == (int)Enums.ApproveStatus.Approved).Include(x => x.CostTour).Include(x => x.Timelines).Include(x => x.Promotions).Include(x => x.Tour).OrderBy(x => x.DepartureDate).AsNoTracking().ToList();
                //var entity1 = stopWatchEntity1.Elapsed;

                //var stopWatchLinq = Stopwatch.StartNew();
                var list = (from s in _db.Schedules.AsNoTracking()
                            where s.Isdelete == false &&
                            s.Approve == (int)Enums.ApproveStatus.Approved
                            select new Schedule
                            {
                                Alias = s.Alias,
                                Approve = s.Approve,
                                BeginDate = s.BeginDate,
                                QuantityAdult = s.QuantityAdult,
                                QuantityBaby = s.QuantityBaby,
                                QuantityChild = s.QuantityChild,
                                CarId = s.CarId,
                                Description = s.Description,
                                DepartureDate = s.DepartureDate,
                                ReturnDate = s.ReturnDate,
                                EndDate = s.EndDate,
                                Isdelete = s.Isdelete,
                                EmployeeId = s.EmployeeId,
                                IdSchedule = s.IdSchedule,
                                MaxCapacity = s.MaxCapacity,
                                MinCapacity = s.MinCapacity,
                                PromotionId = s.PromotionId,
                                DeparturePlace = s.DeparturePlace,
                                Status = s.Status,
                                TourId = s.TourId,
                                FinalPrice = s.FinalPrice,
                                FinalPriceHoliday = s.FinalPriceHoliday,
                                AdditionalPrice = s.AdditionalPrice,
                                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                IsHoliday = s.IsHoliday,
                                Profit = s.Profit,
                                QuantityCustomer = s.QuantityCustomer,
                                TimePromotion = s.TimePromotion,
                                Vat = s.Vat,
                                TotalCostTourNotService = s.TotalCostTourNotService,
                                CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Promotions = (from p in _db.Promotions where p.IdPromotion == s.PromotionId select p).FirstOrDefault(),
                                Tour = (from t in _db.Tour
                                        where s.TourId == t.IdTour
                                        select new Tour
                                        {
                                            Thumbnail = t.Thumbnail,
                                            ToPlace = t.ToPlace,
                                            IdTour = t.IdTour,
                                            NameTour = t.NameTour,
                                            Alias = t.Alias,
                                            ApproveStatus = t.ApproveStatus,
                                            CreateDate = t.CreateDate,
                                            IsActive = t.IsActive,
                                            IsDelete = t.IsDelete,
                                            ModifyBy = t.ModifyBy,
                                            ModifyDate = t.ModifyDate,
                                            Rating = t.Rating,
                                            Status = t.Status
                                        }).First(),

                            }).OrderBy(x => x.DepartureDate).ToList();
                //var linq = stopWatchLinq.Elapsed;

                //var stopWatchLinq1 = Stopwatch.StartNew();
                //var list2 = (from s in _db.Schedules
                //            where s.Isdelete == false &&
                //            s.Approve == (int)Enums.ApproveStatus.Approved
                //            select new Schedule
                //            {
                //                Alias = s.Alias,
                //                Approve = s.Approve,
                //                BeginDate = s.BeginDate,
                //                QuantityAdult = s.QuantityAdult,
                //                QuantityBaby = s.QuantityBaby,
                //                QuantityChild = s.QuantityChild,
                //                CarId = s.CarId,
                //                Description = s.Description,
                //                DepartureDate = s.DepartureDate,
                //                ReturnDate = s.ReturnDate,
                //                EndDate = s.EndDate,
                //                Isdelete = s.Isdelete,
                //                EmployeeId = s.EmployeeId,
                //                IdSchedule = s.IdSchedule,
                //                MaxCapacity = s.MaxCapacity,
                //                MinCapacity = s.MinCapacity,
                //                PromotionId = s.PromotionId,
                //                DeparturePlace = s.DeparturePlace,
                //                Status = s.Status,
                //                TourId = s.TourId,
                //                FinalPrice = s.FinalPrice,
                //                FinalPriceHoliday = s.FinalPriceHoliday,
                //                AdditionalPrice = s.AdditionalPrice,
                //                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                //                IsHoliday = s.IsHoliday,
                //                Profit = s.Profit,
                //                QuantityCustomer = s.QuantityCustomer,
                //                TimePromotion = s.TimePromotion,
                //                Vat = s.Vat,
                //                TotalCostTourNotService = s.TotalCostTourNotService,
                //                CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                //                Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                //                Promotions = (from p in _db.Promotions where p.IdPromotion == s.PromotionId select p).FirstOrDefault(),
                //                Tour = (from t in _db.Tour
                //                        where s.TourId == t.IdTour
                //                        select new Tour
                //                        {
                //                            Thumbnail = t.Thumbnail,
                //                            ToPlace = t.ToPlace,
                //                            IdTour = t.IdTour,
                //                            NameTour = t.NameTour,
                //                            Alias = t.Alias,
                //                            ApproveStatus = t.ApproveStatus,
                //                            CreateDate = t.CreateDate,
                //                            IsActive = t.IsActive,
                //                            IsDelete = t.IsDelete,
                //                            ModifyBy = t.ModifyBy,
                //                            ModifyDate = t.ModifyDate,
                //                            Rating = t.Rating,
                //                            Status = t.Status
                //                        }).First(),

                //            }).OrderBy(x => x.DepartureDate).AsNoTracking().ToList();
                //var linq1 = stopWatchLinq1.Elapsed;

                //var stopWatchEntity = Stopwatch.StartNew();
                //var list4444 = _db.Schedules.Where(x => x.Isdelete == false &&
                //x.Approve == (int)Enums.ApproveStatus.Approved).Include(x => x.CostTour).Include(x => x.Timelines).Include(x => x.Promotions).Include(x => x.Tour).OrderBy(x => x.DepartureDate).AsNoTracking().ToList();
                //var entity = stopWatchEntity.Elapsed;



                // tét


                var stopWatchEntitya2 = Stopwatch.StartNew();
                var schedule1 = (from x in _db.Schedules.AsNoTracking()
                                 where x.IdSchedule == "DLNT-1666353913295-S1666417503679"
                                 select x).FirstOrDefault();
                var dont2 = stopWatchEntitya2.Elapsed;


                //var stopWatchEntitya1b = Stopwatch.StartNew();
                //var schedule1 = (from x in _db.Schedules
                //                where x.IdSchedule == "DLNT-1666353913295-S1666417503679"
                //                select x).FirstOrDefault();
                //var dont2 = stopWatchEntitya1b.Elapsed;


                // end tét
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), list);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response Create(CreateScheduleViewModel input)
        {
            try
            {
                Schedule schedule =
                schedule = Mapper.MapCreateSchedule(input);
                string nameTour = (from x in _db.Tour.AsNoTracking()
                                   where x.IdTour == input.TourId
                                   select x).FirstOrDefault().NameTour;
                schedule.Alias = $"S{Ultility.SEOUrl(nameTour)}";
                schedule.TypeAction = "insert";
                var userLogin = (from x in _db.Employees.AsNoTracking()
                                 where x.IdEmployee == input.IdUserModify
                                 select x).FirstOrDefault();
                schedule.ModifyBy = userLogin.NameEmployee;
                schedule.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                CreateDatabase(schedule);
                SaveChange();
                return Ultility.Responses("Thêm thành công !", Enums.TypeCRUD.Success.ToString(), schedule.IdSchedule);
            }
            catch (Exception e)
            {

                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }



        public Response GetsSchedulebyIdTour(string idTour, bool isDelete)
        {
            try
            {
                var list = (from s in _db.Schedules.AsNoTracking()
                            where s.TourId == idTour
                            && s.Isdelete == isDelete
                            && s.Approve == (int)Enums.ApproveStatus.Approved &&
                            s.IsTempData == false
                            select new Schedule
                            {
                                Alias = s.Alias,
                                Approve = s.Approve,
                                BeginDate = s.BeginDate,
                                QuantityAdult = s.QuantityAdult,
                                QuantityBaby = s.QuantityBaby,
                                QuantityChild = s.QuantityChild,
                                CarId = s.CarId,
                                DepartureDate = s.DepartureDate,
                                ReturnDate = s.ReturnDate,
                                EndDate = s.EndDate,
                                DeparturePlace = s.DeparturePlace,
                                Description = s.Description,
                                MetaDesc = s.MetaDesc,
                                MetaKey = s.MetaKey,
                                Isdelete = s.Isdelete,
                                EmployeeId = s.EmployeeId,
                                IdSchedule = s.IdSchedule,
                                MaxCapacity = s.MaxCapacity,
                                MinCapacity = s.MinCapacity,
                                PromotionId = s.PromotionId,
                                Status = s.Status,
                                TourId = s.TourId,
                                FinalPrice = s.FinalPrice,
                                FinalPriceHoliday = s.FinalPriceHoliday,
                                AdditionalPrice = s.AdditionalPrice,
                                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                IsHoliday = s.IsHoliday,
                                Profit = s.Profit,
                                QuantityCustomer = s.QuantityCustomer,
                                TimePromotion = s.TimePromotion,
                                Vat = s.Vat,
                                IdUserModify = s.IdUserModify,
                                TotalCostTourNotService = s.TotalCostTourNotService,
                                CostTour = (from c in _db.CostTours.AsNoTracking() where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                Timelines = (from t in _db.Timelines.AsNoTracking() where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Tour = (from t in _db.Tour.AsNoTracking()
                                        where s.TourId == t.IdTour
                                        select new Tour
                                        {
                                            Thumbnail = t.Thumbnail,
                                            ToPlace = t.ToPlace,
                                            IdTour = t.IdTour,
                                            NameTour = t.NameTour,
                                            Alias = t.Alias,
                                            ApproveStatus = t.ApproveStatus,
                                            CreateDate = t.CreateDate,
                                            IsActive = t.IsActive,
                                            IsDelete = t.IsDelete,
                                            ModifyBy = t.ModifyBy,
                                            ModifyDate = t.ModifyDate,
                                            Rating = t.Rating,
                                            Status = t.Status,
                                            QuantityBooked = t.QuantityBooked,
                                        }).FirstOrDefault(),

                            }).ToList();


                var result = Mapper.MapSchedule(list);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response CusGetsSchedulebyIdTour(string idTour)
        {
            try
            {
                var list = (from s in _db.Schedules.AsNoTracking()
                            where s.TourId == idTour
                            && s.Isdelete == false
                            && s.EndDate > dateTimeNow
                            && s.Status == (int)Enums.StatusSchedule.Free
                            && s.IsTempData == false
                            orderby s.DepartureDate
                            select new Schedule
                            {
                                DepartureDate = s.DepartureDate,
                                ReturnDate = s.ReturnDate,
                                DeparturePlace = s.DeparturePlace,
                                Description = s.Description,
                                BeginDate = s.BeginDate,
                                EndDate = s.EndDate,
                                MetaDesc = s.MetaDesc,
                                MetaKey = s.MetaKey,
                                AdditionalPrice = s.AdditionalPrice,
                                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                Alias = s.Alias,
                                Status = s.Status,
                                Approve = s.Approve,
                                FinalPrice = s.FinalPrice,
                                FinalPriceHoliday = s.FinalPriceHoliday,
                                IdSchedule = s.IdSchedule,
                                IsHoliday = s.IsHoliday,
                                MinCapacity = s.MinCapacity,
                                MaxCapacity = s.MaxCapacity,
                                PriceAdult = s.PriceAdult,
                                PriceAdultHoliday = s.PriceAdultHoliday,
                                PriceChild = s.PriceChild,
                                PriceBabyHoliday = s.PriceChildHoliday,
                                PriceBaby = s.PriceBaby,
                                PriceChildHoliday = s.PriceChildHoliday,
                                QuantityAdult = s.QuantityAdult,
                                QuantityBaby = s.QuantityBaby,
                                QuantityChild = s.QuantityChild,
                                QuantityCustomer = s.QuantityCustomer,
                                TotalCostTourNotService = s.TotalCostTourNotService,
                                Vat = s.Vat,
                                Profit = s.Profit,
                                TimePromotion = s.TimePromotion,
                                Promotions = (from pro in _db.Promotions.AsNoTracking()
                                              where pro.IdPromotion == s.PromotionId
                                              select pro).FirstOrDefault(),
                                Car = (from car in _db.Cars.AsNoTracking()
                                       where car.IdCar == s.CarId
                                       select car).First(),
                                Timelines = (from timeline in _db.Timelines.AsNoTracking()
                                             where timeline.IdSchedule == s.IdSchedule
                                             && timeline.IsDelete == false
                                             select new Timeline
                                             {
                                                 Description = timeline.Description,
                                                 FromTime = timeline.FromTime,
                                                 ToTime = timeline.ToTime,
                                             }).ToList(),
                                CostTour = (from c in _db.CostTours.AsNoTracking()
                                            where c.IdSchedule == s.IdSchedule
                                            select c).First(),
                                Employee = (from e in _db.Employees.AsNoTracking()
                                            where e.IdEmployee == s.EmployeeId
                                            select e).First()
                            }).ToList();


                //var result = Mapper.MapSchedule(list);
                var result = list;
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response GetSchedulebyIdTourWaiting(string idTour)
        {
            try
            {
                var list = (from s in _db.Schedules.AsNoTracking()
                            where s.TourId == idTour
                            where s.Isdelete == false &&
                            s.Approve == (int)Enums.ApproveStatus.Waiting 
                            select new Schedule
                            {
                                Alias = s.Alias,
                                Approve = s.Approve,
                                BeginDate = s.BeginDate,
                                QuantityAdult = s.QuantityAdult,
                                QuantityBaby = s.QuantityBaby,
                                QuantityChild = s.QuantityChild,
                                CarId = s.CarId,
                                DepartureDate = s.DepartureDate,
                                ReturnDate = s.ReturnDate,
                                DeparturePlace = s.DeparturePlace,
                                Description = s.Description,
                                MetaDesc = s.MetaDesc,
                                MetaKey = s.MetaKey,
                                EndDate = s.EndDate,
                                Isdelete = s.Isdelete,
                                EmployeeId = s.EmployeeId,
                                IdSchedule = s.IdSchedule,
                                MaxCapacity = s.MaxCapacity,
                                MinCapacity = s.MinCapacity,
                                PromotionId = s.PromotionId,
                                Status = s.Status,
                                TourId = s.TourId,
                                FinalPrice = s.FinalPrice,
                                FinalPriceHoliday = s.FinalPriceHoliday,
                                AdditionalPrice = s.AdditionalPrice,
                                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                IsHoliday = s.IsHoliday,
                                Profit = s.Profit,
                                QuantityCustomer = s.QuantityCustomer,
                                TimePromotion = s.TimePromotion,
                                Vat = s.Vat,
                                TotalCostTourNotService = s.TotalCostTourNotService,
                                TypeAction = s.TypeAction,
                                IdUserModify = s.IdUserModify,
                                ModifyBy = s.ModifyBy,
                                ModifyDate = s.ModifyDate,
                                CostTour = (from c in _db.CostTours.AsNoTracking()
                                            where c.IdSchedule == s.IdSchedule
                                            select c).First(),
                                Timelines = (from t in _db.Timelines.AsNoTracking()
                                             where t.IdSchedule == s.IdSchedule
                                             select t).ToList(),
                                Tour = (from t in _db.Tour.AsNoTracking()
                                        where s.TourId == t.IdTour
                                        select new Tour
                                        {
                                            Thumbnail = t.Thumbnail,
                                            ToPlace = t.ToPlace,
                                            IdTour = t.IdTour,
                                            NameTour = t.NameTour,
                                            Alias = t.Alias,
                                            ApproveStatus = t.ApproveStatus,
                                            CreateDate = t.CreateDate,
                                            IsActive = t.IsActive,
                                            IsDelete = t.IsDelete,
                                            ModifyBy = t.ModifyBy,
                                            ModifyDate = t.ModifyDate,
                                            Rating = t.Rating,
                                            Status = t.Status,
                                            QuantityBooked = t.QuantityBooked,
                                        }).First(),

                            }).ToList();


                var result = Mapper.MapSchedule(list);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response RestoreShedule(string idSchedule, Guid idUser)
        {
            try
            {
                var schedule = (from x in _db.Schedules.AsNoTracking()
                                where x.IdSchedule == idSchedule
                            && x.Isdelete == true
                                select x).FirstOrDefault();
                var userLogin = (from x in _db.Employees.AsNoTracking()
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                if (schedule != null)
                {
                    schedule.Isdelete = false;
                    schedule.IdUserModify = userLogin.IdEmployee;
                    schedule.Approve = (int)ApproveStatus.Waiting;
                    schedule.TypeAction = "restore";
                    UpdateDatabase(schedule);
                    SaveChange();

                    return Ultility.Responses($"Đã gửi yêu cầu khôi phục !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses($"Không tìm thấy !", Enums.TypeCRUD.Warning.ToString());
                }
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }


        public Response UpdatePromotion(string idSchedule, int idPromotion)
        {
            try
            {
                var schedule = (from x in _db.Schedules.AsNoTracking()
                                where x.IdSchedule == idSchedule
                                select x).FirstOrDefault();
                if (schedule != null)
                {
                    var promotion = (from x in _db.Promotions.AsNoTracking()
                                     where x.IdPromotion == idPromotion
                                     select x).FirstOrDefault();
                    if (promotion != null)
                    {
                        schedule.PromotionId = promotion.IdPromotion;
                        schedule.TimePromotion = promotion.ToDate;
                        UpdateDatabase(schedule);
                        SaveChange();
                    }
                    return Ultility.Responses("Cập nhật thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses($"Không tìm thấy Id [{idSchedule}] !", Enums.TypeCRUD.Warning.ToString());
                }
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
        // chưa cập nhật
        public async Task UpdateCapacity(string idSchedule, int adult = 1, int child = 0, int baby = 0)
        {
            try
            {
                var schedule = await (from x in _db.Schedules.AsNoTracking()
                                      where x.IdSchedule == idSchedule
                                      select x).FirstOrDefaultAsync();
                int availableQuantity = schedule.QuantityCustomer;
                int quantity = availableQuantity + (adult + child);
                schedule.QuantityAdult = adult;
                schedule.QuantityBaby = baby;
                schedule.QuantityChild = child;
                schedule.QuantityCustomer = quantity;

                UpdateDatabase(schedule);
                await SaveChangeAsync();

            }
            catch (Exception e)
            {
            }
        }

        public async Task<Response> Get(string idSchedule)
        {
            try
            {
                int approve = Convert.ToInt16(Enums.ApproveStatus.Approved);
                var schedule = await (from x in _db.Schedules.AsNoTracking()
                                      where x.EndDate > dateTimeNow
                                      && x.Isdelete == false
                                      && x.Approve == approve
                                      && x.IdSchedule == idSchedule
                                      && x.IsTempData == false
                                      select new Schedule
                                      {
                                          IdSchedule = x.IdSchedule,
                                          MinCapacity = x.MinCapacity,
                                          MaxCapacity = x.MaxCapacity,
                                          QuantityCustomer = x.QuantityCustomer,
                                          AdditionalPrice = x.AdditionalPrice,
                                          AdditionalPriceHoliday = x.AdditionalPriceHoliday,
                                          Alias = x.Alias,
                                          FinalPrice = x.FinalPrice,
                                          FinalPriceHoliday = x.FinalPriceHoliday,
                                          PriceAdult = x.PriceAdult,
                                          PriceBaby = x.PriceBaby,
                                          PriceChild = x.PriceChild,
                                          PriceAdultHoliday = x.PriceAdultHoliday,
                                          PriceBabyHoliday = x.PriceBabyHoliday,
                                          PriceChildHoliday = x.PriceChildHoliday,
                                          QuantityAdult = x.QuantityAdult,
                                          QuantityBaby = x.QuantityBaby,
                                          QuantityChild = x.QuantityChild,
                                          BeginDate = x.BeginDate,
                                          EndDate = x.EndDate,
                                          DepartureDate = x.DepartureDate,
                                          DeparturePlace = x.DeparturePlace,
                                          ReturnDate = x.ReturnDate,
                                          Description = x.Description,
                                          IsHoliday = x.IsHoliday,
                                          Promotions = (from p in _db.Promotions.AsNoTracking()
                                                        where p.IdPromotion == x.PromotionId
                                                        select p).FirstOrDefault(),
                                          CostTour = (from c in _db.CostTours.AsNoTracking()
                                                      where c.IdSchedule == x.IdSchedule
                                                      select c).FirstOrDefault(),
                                          Timelines = (from t in _db.Timelines.AsNoTracking()
                                                       where t.IdSchedule == x.IdSchedule
                                                       select t).ToList(),
                                          Tour = (from tour in _db.Tour.AsNoTracking()
                                                  where x.TourId == tour.IdTour
                                                  select tour).FirstOrDefault()
                                      }).FirstAsync();
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), schedule);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public async Task<Response> SearchTour(string from, string to, DateTime? departureDate, DateTime? returnDate)
        {
            try
            {
                if (departureDate != null && returnDate != null)
                {
                    var list = await (from x in _db.Schedules
                                      where x.EndDate > dateTimeNow
                                      && x.Isdelete == false
                                      && x.Approve == (int)Enums.ApproveStatus.Approved
                                      select x
                                      ).ToListAsync();
                    if (departureDate != null)
                    {
                        long unixDepartureDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(departureDate.Value);
                        list = (from x in list
                                where x.DepartureDate >= unixDepartureDate
                                select x).ToList();
                    }
                    if (returnDate != null)
                    {
                        long unixReturnDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(returnDate.Value);
                        list = (from x in list
                                where x.DepartureDate <= unixReturnDate
                                select x).ToList();
                    }
                    if (!string.IsNullOrEmpty(from))
                    {
                        string keyFrom = Ultility.removeVietnameseSign(from.ToLower());
                        list = (from x in list
                                where Ultility.removeVietnameseSign(x.DeparturePlace.ToLower()).Contains(keyFrom)
                                select x).ToList();
                    }
                    list = (from s in list
                            select new Schedule
                            {
                                Alias = s.Alias,
                                Approve = s.Approve,
                                BeginDate = s.BeginDate,
                                QuantityAdult = s.QuantityAdult,
                                QuantityBaby = s.QuantityBaby,
                                QuantityChild = s.QuantityChild,
                                CarId = s.CarId,
                                Description = s.Description,
                                DepartureDate = s.DepartureDate,
                                ReturnDate = s.ReturnDate,
                                EndDate = s.EndDate,
                                Isdelete = s.Isdelete,
                                EmployeeId = s.EmployeeId,
                                IdSchedule = s.IdSchedule,
                                MaxCapacity = s.MaxCapacity,
                                MinCapacity = s.MinCapacity,
                                PromotionId = s.PromotionId,
                                DeparturePlace = s.DeparturePlace,
                                Status = s.Status,
                                TourId = s.TourId,
                                FinalPrice = s.FinalPrice,
                                FinalPriceHoliday = s.FinalPriceHoliday,
                                AdditionalPrice = s.AdditionalPrice,
                                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                IsHoliday = s.IsHoliday,
                                Profit = s.Profit,
                                QuantityCustomer = s.QuantityCustomer,
                                TimePromotion = s.TimePromotion,
                                Vat = s.Vat,
                                TotalCostTourNotService = s.TotalCostTourNotService,
                                CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Tour = (from t in _db.Tour
                                        where s.TourId == t.IdTour
                                        select new Tour
                                        {
                                            Thumbnail = t.Thumbnail,
                                            ToPlace = t.ToPlace,
                                            IdTour = t.IdTour,
                                            NameTour = t.NameTour,
                                            Alias = t.Alias,
                                            ApproveStatus = t.ApproveStatus,
                                            CreateDate = t.CreateDate,
                                            IsActive = t.IsActive,
                                            IsDelete = t.IsDelete,
                                            ModifyBy = t.ModifyBy,
                                            ModifyDate = t.ModifyDate,
                                            Rating = t.Rating,
                                            Status = t.Status
                                        }).FirstOrDefault(),

                            }).ToList();
                    if (!string.IsNullOrEmpty(to))
                    {
                        string keyTo = Ultility.removeVietnameseSign(to.ToLower());
                        list = (from x in list
                                where Ultility.removeVietnameseSign(x.Tour.ToPlace.ToLower()).Contains(keyTo)
                                select x).OrderByDescending(x => x.DepartureDate).ToList();
                    }
                    var result = Mapper.MapSchedule(list);
                    return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
                }
                else if (departureDate == null && returnDate == null)
                {
                    var list2 = await (from x in _db.Schedules
                                       where x.EndDate > dateTimeNow
                                       && x.Isdelete == false
                                      && x.Approve == (int)Enums.ApproveStatus.Approved
                                       select x
                                     ).ToListAsync();

                    if (!string.IsNullOrEmpty(from))
                    {
                        string keyFrom = Ultility.removeVietnameseSign(from.ToLower());
                        list2 = (from x in list2
                                 where Ultility.removeVietnameseSign(x.DeparturePlace.ToLower()).Contains(keyFrom)
                                 select x).ToList();
                    }
                    list2 = (from s in list2
                             select new Schedule
                             {
                                 Alias = s.Alias,
                                 Approve = s.Approve,
                                 BeginDate = s.BeginDate,
                                 QuantityAdult = s.QuantityAdult,
                                 QuantityBaby = s.QuantityBaby,
                                 QuantityChild = s.QuantityChild,
                                 CarId = s.CarId,
                                 Description = s.Description,
                                 DepartureDate = s.DepartureDate,
                                 ReturnDate = s.ReturnDate,
                                 EndDate = s.EndDate,
                                 Isdelete = s.Isdelete,
                                 EmployeeId = s.EmployeeId,
                                 IdSchedule = s.IdSchedule,
                                 MaxCapacity = s.MaxCapacity,
                                 MinCapacity = s.MinCapacity,
                                 PromotionId = s.PromotionId,
                                 DeparturePlace = s.DeparturePlace,
                                 Status = s.Status,
                                 TourId = s.TourId,
                                 FinalPrice = s.FinalPrice,
                                 FinalPriceHoliday = s.FinalPriceHoliday,
                                 AdditionalPrice = s.AdditionalPrice,
                                 AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                 IsHoliday = s.IsHoliday,
                                 Profit = s.Profit,
                                 QuantityCustomer = s.QuantityCustomer,
                                 TimePromotion = s.TimePromotion,
                                 Vat = s.Vat,
                                 TotalCostTourNotService = s.TotalCostTourNotService,
                                 CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                 Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                                 Tour = (from t in _db.Tour
                                         where s.TourId == t.IdTour
                                         select new Tour
                                         {
                                             Thumbnail = t.Thumbnail,
                                             ToPlace = t.ToPlace,
                                             IdTour = t.IdTour,
                                             NameTour = t.NameTour,
                                             Alias = t.Alias,
                                             ApproveStatus = t.ApproveStatus,
                                             CreateDate = t.CreateDate,
                                             IsActive = t.IsActive,
                                             IsDelete = t.IsDelete,
                                             ModifyBy = t.ModifyBy,
                                             ModifyDate = t.ModifyDate,
                                             Rating = t.Rating,
                                             Status = t.Status
                                         }).FirstOrDefault(),

                             }).ToList();
                    if (!string.IsNullOrEmpty(to))
                    {
                        string keyTo = Ultility.removeVietnameseSign(to.ToLower());
                        list2 = (from x in list2
                                 where Ultility.removeVietnameseSign(x.Tour.ToPlace.ToLower()).Contains(keyTo)
                                 select x).OrderByDescending(x => x.DepartureDate).ToList();
                    }
                    var result = Mapper.MapSchedule(list2);
                    return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);

                }
                else
                {
                    var list1 = new List<Schedule>();
                    if (departureDate != null)
                    {
                        var fromDepartTureDate1 = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(departureDate.Value);
                        var toDepartTureDate1 = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(departureDate.Value.AddDays(1).AddMinutes(-1));
                        // cách 1
                        list1 = await (from x in _db.Schedules
                                       where x.EndDate > dateTimeNow

                                        && x.DepartureDate >= fromDepartTureDate1
                                       && x.DepartureDate <= toDepartTureDate1
                                       && x.Isdelete == false
                                       && x.Approve == (int)Enums.ApproveStatus.Approved
                                       select x
                                      ).ToListAsync();
                        // cách 2 
                        //list1 = await (from x in _db.Schedules
                        //               where x.EndDate <= dateTimeNowUnix1
                        //               && (x.DepartureDate >= fromDepartTureDate1 && x.DepartureDate <= toDepartTureDate1)
                        //               && x.Isdelete == false
                        //               && x.Approve == (int)Enums.ApproveStatus.Approved
                        //               select x
                        //              ).ToListAsync();
                    }
                    else
                    {
                        var fromReturnDate1 = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(returnDate.Value);
                        var toReturnDate1 = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(returnDate.Value.AddDays(1).AddMinutes(-1));
                        list1 = await (from x in _db.Schedules
                                       where x.EndDate > dateTimeNow
                                       && x.ReturnDate >= fromReturnDate1
                                       && x.ReturnDate <= toReturnDate1
                                       && x.Isdelete == false
                                       && x.Approve == (int)Enums.ApproveStatus.Approved
                                       select x
                                                             ).ToListAsync();
                    }


                    if (!string.IsNullOrEmpty(from))
                    {
                        string keyFrom = Ultility.removeVietnameseSign(from.ToLower());
                        list1 = (from x in list1
                                 where Ultility.removeVietnameseSign(x.DeparturePlace.ToLower()).Contains(keyFrom)
                                 select x).ToList();
                    }
                    list1 = (from s in list1
                             select new Schedule
                             {
                                 Alias = s.Alias,
                                 Approve = s.Approve,
                                 BeginDate = s.BeginDate,
                                 QuantityAdult = s.QuantityAdult,
                                 QuantityBaby = s.QuantityBaby,
                                 QuantityChild = s.QuantityChild,
                                 CarId = s.CarId,
                                 Description = s.Description,
                                 DepartureDate = s.DepartureDate,
                                 ReturnDate = s.ReturnDate,
                                 EndDate = s.EndDate,
                                 Isdelete = s.Isdelete,
                                 EmployeeId = s.EmployeeId,
                                 IdSchedule = s.IdSchedule,
                                 MaxCapacity = s.MaxCapacity,
                                 MinCapacity = s.MinCapacity,
                                 PromotionId = s.PromotionId,
                                 DeparturePlace = s.DeparturePlace,
                                 Status = s.Status,
                                 TourId = s.TourId,
                                 FinalPrice = s.FinalPrice,
                                 FinalPriceHoliday = s.FinalPriceHoliday,
                                 AdditionalPrice = s.AdditionalPrice,
                                 AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                 IsHoliday = s.IsHoliday,
                                 Profit = s.Profit,
                                 QuantityCustomer = s.QuantityCustomer,
                                 TimePromotion = s.TimePromotion,
                                 Vat = s.Vat,
                                 TotalCostTourNotService = s.TotalCostTourNotService,
                                 CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                 Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                                 Tour = (from t in _db.Tour
                                         where s.TourId == t.IdTour
                                         select new Tour
                                         {
                                             Thumbnail = t.Thumbnail,
                                             ToPlace = t.ToPlace,
                                             IdTour = t.IdTour,
                                             NameTour = t.NameTour,
                                             Alias = t.Alias,
                                             ApproveStatus = t.ApproveStatus,
                                             CreateDate = t.CreateDate,
                                             IsActive = t.IsActive,
                                             IsDelete = t.IsDelete,
                                             ModifyBy = t.ModifyBy,
                                             ModifyDate = t.ModifyDate,
                                             Rating = t.Rating,
                                             Status = t.Status
                                         }).FirstOrDefault(),

                             }).ToList();
                    if (!string.IsNullOrEmpty(to))
                    {
                        string keyTo = Ultility.removeVietnameseSign(to.ToLower());
                        list1 = (from x in list1
                                 where Ultility.removeVietnameseSign(x.Tour.ToPlace.ToLower()).Contains(keyTo)
                                 select x).OrderByDescending(x => x.DepartureDate).ToList();
                    }
                    var result1 = Mapper.MapSchedule(list1);
                    return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result1);
                }
                //var list = await (from s in _db.Schedules
                //            where s.Isdelete == false
                //            && s.Approve == (int)Enums.ApproveStatus.Approved
                //            select new Schedule
                //            {
                //                Alias = s.Alias,
                //                Approve = s.Approve,
                //                BeginDate = s.BeginDate,
                //                QuantityAdult = s.QuantityAdult,
                //                QuantityBaby = s.QuantityBaby,
                //                QuantityChild = s.QuantityChild,
                //                CarId = s.CarId,
                //                DepartureDate = s.DepartureDate,
                //                ReturnDate = s.ReturnDate,
                //                DeparturePlace = s.DeparturePlace,
                //                Description = s.Description,
                //                MetaDesc = s.MetaDesc,
                //                MetaKey = s.MetaKey,
                //                EndDate = s.EndDate,
                //                Isdelete = s.Isdelete,
                //                EmployeeId = s.EmployeeId,
                //                IdSchedule = s.IdSchedule,
                //                MaxCapacity = s.MaxCapacity,
                //                MinCapacity = s.MinCapacity,
                //                PromotionId = s.PromotionId,
                //                Status = s.Status,
                //                TourId = s.TourId,
                //                FinalPrice = s.FinalPrice,
                //                FinalPriceHoliday = s.FinalPriceHoliday,
                //                AdditionalPrice = s.AdditionalPrice,
                //                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                //                IsHoliday = s.IsHoliday,
                //                Profit = s.Profit,
                //                QuantityCustomer = s.QuantityCustomer,
                //                TimePromotion = s.TimePromotion,
                //                Vat = s.Vat,
                //                TotalCostTourNotService = s.TotalCostTourNotService,
                //                CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).First(),
                //                Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                //                Tour = (from t in _db.Tour
                //                        where s.TourId == t.IdTour
                //                        select new Tour
                //                        {
                //                            Thumbsnail = t.Thumbsnail,
                //                            ToPlace = t.ToPlace,
                //                            IdTour = t.IdTour,
                //                            NameTour = t.NameTour,
                //                            Alias = t.Alias,
                //                            ApproveStatus = t.ApproveStatus,
                //                            CreateDate = t.CreateDate,
                //                            IsActive = t.IsActive,
                //                            IsDelete = t.IsDelete,
                //                            ModifyBy = t.ModifyBy,
                //                            ModifyDate = t.ModifyDate,
                //                            Rating = t.Rating,
                //                            Status = t.Status,
                //                            QuantityBooked = t.QuantityBooked,
                //                        }).First(),

                //            }).ToListAsync();

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public async Task<Response> GetsSchedule()
        {
            try
            {
                var list = await (from s in _db.Schedules.AsNoTracking()
                                  where s.Isdelete == false &&
                            s.Approve == (int)Enums.ApproveStatus.Approved
                            && s.PromotionId == 1
                            select new Schedule
                            {
                                Alias = s.Alias,
                                Approve = s.Approve,
                                BeginDate = s.BeginDate,
                                QuantityAdult = s.QuantityAdult,
                                QuantityBaby = s.QuantityBaby,
                                QuantityChild = s.QuantityChild,
                                CarId = s.CarId,
                                Description = s.Description,
                                DepartureDate = s.DepartureDate,
                                ReturnDate = s.ReturnDate,
                                EndDate = s.EndDate,
                                Isdelete = s.Isdelete,
                                EmployeeId = s.EmployeeId,
                                IdSchedule = s.IdSchedule,
                                MaxCapacity = s.MaxCapacity,
                                MinCapacity = s.MinCapacity,
                                PromotionId = s.PromotionId,
                                DeparturePlace = s.DeparturePlace,
                                Status = s.Status,
                                TourId = s.TourId,
                                FinalPrice = s.FinalPrice,
                                FinalPriceHoliday = s.FinalPriceHoliday,
                                AdditionalPrice = s.AdditionalPrice,
                                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                IsHoliday = s.IsHoliday,
                                Profit = s.Profit,
                                QuantityCustomer = s.QuantityCustomer,
                                TimePromotion = s.TimePromotion,
                                Vat = s.Vat,
                                TotalCostTourNotService = s.TotalCostTourNotService,
                                CostTour = (from c in _db.CostTours.AsNoTracking() where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                Timelines = (from t in _db.Timelines.AsNoTracking() where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Promotions = (from p in _db.Promotions.AsNoTracking() where p.IdPromotion == s.PromotionId select p).FirstOrDefault(),
                                Tour = (from t in _db.Tour.AsNoTracking()
                                        where s.TourId == t.IdTour
                                        select new Tour
                                        {
                                            Thumbnail = t.Thumbnail,
                                            ToPlace = t.ToPlace,
                                            IdTour = t.IdTour,
                                            NameTour = t.NameTour,
                                            Alias = t.Alias,
                                            ApproveStatus = t.ApproveStatus,
                                            CreateDate = t.CreateDate,
                                            IsActive = t.IsActive,
                                            IsDelete = t.IsDelete,
                                            ModifyBy = t.ModifyBy,
                                            ModifyDate = t.ModifyDate,
                                            Rating = t.Rating,
                                            Status = t.Status
                                        }).First(),

                            }).OrderBy(x => x.DepartureDate).ToListAsync();


                var result = Mapper.MapSchedule(list);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);

            }
        }

        public async Task<Response> GetsScheduleFlashSale()
        {
            try
            {
                var flashSaleDay = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(Ultility.GetDateZeroTime(DateTime.Now.AddDays(3))); // sau này gắn config
                var dateTimeNow = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                var list = await (from s in _db.Schedules.AsNoTracking()
                                  where s.Isdelete == false
                                  && s.Approve == (int)Enums.ApproveStatus.Approved
                                  && s.EndDate >= dateTimeNow
                                  && s.EndDate <= flashSaleDay
                                  select new Schedule
                                  {
                                      Alias = s.Alias,
                                      Approve = s.Approve,
                                      BeginDate = s.BeginDate,
                                      QuantityAdult = s.QuantityAdult,
                                      QuantityBaby = s.QuantityBaby,
                                      QuantityChild = s.QuantityChild,
                                      CarId = s.CarId,
                                      Description = s.Description,
                                      DepartureDate = s.DepartureDate,
                                      ReturnDate = s.ReturnDate,
                                      EndDate = s.EndDate,
                                      Isdelete = s.Isdelete,
                                      EmployeeId = s.EmployeeId,
                                      IdSchedule = s.IdSchedule,
                                      MaxCapacity = s.MaxCapacity,
                                      MinCapacity = s.MinCapacity,
                                      PromotionId = s.PromotionId,
                                      DeparturePlace = s.DeparturePlace,
                                      Status = s.Status,
                                      TourId = s.TourId,
                                      FinalPrice = s.FinalPrice,
                                      FinalPriceHoliday = s.FinalPriceHoliday,
                                      AdditionalPrice = s.AdditionalPrice,
                                      AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                      IsHoliday = s.IsHoliday,
                                      Profit = s.Profit,
                                      QuantityCustomer = s.QuantityCustomer,
                                      TimePromotion = s.TimePromotion,
                                      Vat = s.Vat,
                                      TotalCostTourNotService = s.TotalCostTourNotService,
                                      CostTour = (from c in _db.CostTours.AsNoTracking() where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                      Timelines = (from t in _db.Timelines.AsNoTracking() where t.IdSchedule == s.IdSchedule select t).ToList(),
                                      Promotions = (from p in _db.Promotions.AsNoTracking() where p.IdPromotion == s.PromotionId select p).FirstOrDefault(),
                                      Tour = (from t in _db.Tour.AsNoTracking()
                                              where s.TourId == t.IdTour
                                              select new Tour
                                              {
                                                  Thumbnail = t.Thumbnail,
                                                  ToPlace = t.ToPlace,
                                                  IdTour = t.IdTour,
                                                  NameTour = t.NameTour,
                                                  Alias = t.Alias,
                                                  ApproveStatus = t.ApproveStatus,
                                                  CreateDate = t.CreateDate,
                                                  IsActive = t.IsActive,
                                                  IsDelete = t.IsDelete,
                                                  ModifyBy = t.ModifyBy,
                                                  ModifyDate = t.ModifyDate,
                                                  Rating = t.Rating,
                                                  Status = t.Status
                                              }).First(),

                                  }).OrderBy(x => x.DepartureDate).ToListAsync();


                var result = Mapper.MapSchedule(list);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public async Task<Response> GetsSchedulePromotion()
        {
            try
            {
                var list = await (from s in _db.Schedules.AsNoTracking()
                            where s.Isdelete == false &&
                            s.Approve == (int)Enums.ApproveStatus.Approved
                            && s.PromotionId > 1
                            select new Schedule
                            {
                                Alias = s.Alias,
                                Approve = s.Approve,
                                BeginDate = s.BeginDate,
                                QuantityAdult = s.QuantityAdult,
                                QuantityBaby = s.QuantityBaby,
                                QuantityChild = s.QuantityChild,
                                CarId = s.CarId,
                                Description = s.Description,
                                DepartureDate = s.DepartureDate,
                                ReturnDate = s.ReturnDate,
                                EndDate = s.EndDate,
                                Isdelete = s.Isdelete,
                                EmployeeId = s.EmployeeId,
                                IdSchedule = s.IdSchedule,
                                MaxCapacity = s.MaxCapacity,
                                MinCapacity = s.MinCapacity,
                                PromotionId = s.PromotionId,
                                DeparturePlace = s.DeparturePlace,
                                Status = s.Status,
                                TourId = s.TourId,
                                FinalPrice = s.FinalPrice,
                                FinalPriceHoliday = s.FinalPriceHoliday,
                                AdditionalPrice = s.AdditionalPrice,
                                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                IsHoliday = s.IsHoliday,
                                Profit = s.Profit,
                                QuantityCustomer = s.QuantityCustomer,
                                TimePromotion = s.TimePromotion,
                                Vat = s.Vat,
                                TotalCostTourNotService = s.TotalCostTourNotService,
                                CostTour = (from c in _db.CostTours.AsNoTracking() where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                Timelines = (from t in _db.Timelines.AsNoTracking() where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Promotions = (from p in _db.Promotions.AsNoTracking() where p.IdPromotion == s.PromotionId select p).FirstOrDefault(),
                                Tour = (from t in _db.Tour.AsNoTracking()
                                        where s.TourId == t.IdTour
                                        select new Tour
                                        {
                                            Thumbnail = t.Thumbnail,
                                            ToPlace = t.ToPlace,
                                            IdTour = t.IdTour,
                                            NameTour = t.NameTour,
                                            Alias = t.Alias,
                                            ApproveStatus = t.ApproveStatus,
                                            CreateDate = t.CreateDate,
                                            IsActive = t.IsActive,
                                            IsDelete = t.IsDelete,
                                            ModifyBy = t.ModifyBy,
                                            ModifyDate = t.ModifyDate,
                                            Rating = t.Rating,
                                            Status = t.Status
                                        }).First(),

                            }).OrderBy(x => x.DepartureDate).ToListAsync();


                var result = Mapper.MapSchedule(list);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);

            }

        }
        public async Task<Response> GetsRelatedSchedule(string idSchedule)
        {
            try
            {
                var schedule = await (from x in _db.Schedules.AsNoTracking()
                                      where x.IdSchedule == idSchedule
                                      select x).FirstOrDefaultAsync();
                var closetPrice1 = (schedule.FinalPrice - 200000);
                var closetPrice2 = (schedule.FinalPrice + 200000);
                var list1 = await (from x in _db.Schedules.AsNoTracking()
                                   where x.IdSchedule != idSchedule
                                   && x.EndDate > dateTimeNow
                                   && x.DeparturePlace == schedule.DeparturePlace
                                   && (x.FinalPrice >= closetPrice1 && x.FinalPrice <= closetPrice2)
                                   && x.Isdelete == false
                                   && x.Approve == (int)Enums.ApproveStatus.Approved
                                   && x.IsTempData == false
                                   select x).ToListAsync();
                var list2 = await (from x in _db.Schedules.AsNoTracking()
                                   where x.IdSchedule != idSchedule
                                   && !(from s in list1 select s.IdSchedule).Contains(x.IdSchedule)
                                   && x.EndDate > dateTimeNow
                                   && x.DeparturePlace == schedule.DeparturePlace
                                   && (x.Status == (int)StatusSchedule.Free && x.QuantityCustomer <= x.MinCapacity)
                                   && x.Isdelete == false
                                   && x.Approve == (int)Enums.ApproveStatus.Approved
                                   && x.IsTempData == false
                                   select x).OrderBy(x => x.BeginDate).ToListAsync();
                var rd = new Random();
                var lsFinal = list1.Concat(list2).ToList();
                lsFinal = lsFinal.Shuffle(rd);

                var list = (from s in lsFinal
                            select new Schedule
                            {
                                Alias = s.Alias,
                                Approve = s.Approve,
                                BeginDate = s.BeginDate,
                                QuantityAdult = s.QuantityAdult,
                                QuantityBaby = s.QuantityBaby,
                                QuantityChild = s.QuantityChild,
                                CarId = s.CarId,
                                Description = s.Description,
                                DepartureDate = s.DepartureDate,
                                ReturnDate = s.ReturnDate,
                                EndDate = s.EndDate,
                                Isdelete = s.Isdelete,
                                EmployeeId = s.EmployeeId,
                                IdSchedule = s.IdSchedule,
                                MaxCapacity = s.MaxCapacity,
                                MinCapacity = s.MinCapacity,
                                PromotionId = s.PromotionId,
                                DeparturePlace = s.DeparturePlace,
                                Status = s.Status,
                                TourId = s.TourId,
                                FinalPrice = s.FinalPrice,
                                FinalPriceHoliday = s.FinalPriceHoliday,
                                AdditionalPrice = s.AdditionalPrice,
                                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                IsHoliday = s.IsHoliday,
                                Profit = s.Profit,
                                QuantityCustomer = s.QuantityCustomer,
                                TimePromotion = s.TimePromotion,
                                Vat = s.Vat,
                                TotalCostTourNotService = s.TotalCostTourNotService,
                                CostTour = (from c in _db.CostTours.AsNoTracking() where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                Timelines = (from t in _db.Timelines.AsNoTracking() where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Promotions = (from p in _db.Promotions.AsNoTracking() where p.IdPromotion == s.PromotionId select p).FirstOrDefault(),
                                Tour = (from t in _db.Tour.AsNoTracking()
                                        where s.TourId == t.IdTour
                                        select new Tour
                                        {
                                            Thumbnail = t.Thumbnail,
                                            ToPlace = t.ToPlace,
                                            IdTour = t.IdTour,
                                            NameTour = t.NameTour,
                                            Alias = t.Alias,
                                            ApproveStatus = t.ApproveStatus,
                                            CreateDate = t.CreateDate,
                                            IsActive = t.IsActive,
                                            IsDelete = t.IsDelete,
                                            ModifyBy = t.ModifyBy,
                                            ModifyDate = t.ModifyDate,
                                            Rating = t.Rating,
                                            Status = t.Status
                                        }).First(),

                            }).OrderBy(x => x.DepartureDate).ToList();


                var result = Mapper.MapSchedule(list);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }


        #region dang chỉnh
        public Response Delete(string idSchedule, Guid idUser)
        {
            try
            {
                var schedule = (from x in _db.Schedules.AsNoTracking()
                                where x.IdSchedule == idSchedule
                                select x).FirstOrDefault();

                var userLogin = (from x in _db.Employees.AsNoTracking()
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                if (schedule.Approve == (int)ApproveStatus.Approved)
                {
                    schedule.IdUserModify = userLogin.IdEmployee;
                    schedule.Approve = (int)ApproveStatus.Waiting;
                    schedule.ModifyBy = userLogin.NameEmployee;
                    schedule.TypeAction = "delete";

                    UpdateDatabase(schedule);
                    SaveChange();

                    return Ultility.Responses("Đã gửi yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    if (schedule.IdUserModify == idUser)
                    {
                        if (schedule.TypeAction == "insert")
                        {
                            DeleteDatabase(schedule);
                            SaveChange();

                            return Ultility.Responses("Đã xóa!", Enums.TypeCRUD.Success.ToString());
                        }
                        else if (schedule.TypeAction == "update")
                        {
                            var idScheduleTemp = schedule.IdAction;
                            // old hotel
                            var scheduleTemp = (from x in _db.Schedules
                                                where x.IdSchedule == idScheduleTemp
                                                select x).FirstOrDefault();
                            schedule.Approve = (int)ApproveStatus.Approved;
                            schedule.IdAction = null;
                            schedule.TypeAction = null;
                            #region restore data

                            schedule.BeginDate = scheduleTemp.BeginDate;
                            schedule.CarId = scheduleTemp.CarId;
                            schedule.DepartureDate = scheduleTemp.DepartureDate;
                            schedule.DeparturePlace = scheduleTemp.DeparturePlace;
                            schedule.Description = scheduleTemp.Description;
                            schedule.EmployeeId = scheduleTemp.EmployeeId;
                            schedule.EndDate = scheduleTemp.EndDate;
                            schedule.IsHoliday = scheduleTemp.IsHoliday;
                            schedule.MaxCapacity = scheduleTemp.MaxCapacity;
                            schedule.MinCapacity = scheduleTemp.MinCapacity;
                            schedule.ModifyBy = userLogin.NameEmployee;
                            schedule.PromotionId = scheduleTemp.PromotionId;
                            schedule.ReturnDate = scheduleTemp.ReturnDate;
                            schedule.Vat = scheduleTemp.Vat;


                            schedule.TimePromotion = scheduleTemp.TimePromotion;
                            #endregion
                            DeleteDatabase(scheduleTemp);
                            UpdateDatabase(schedule);
                            SaveChange();

                            return Ultility.Responses("Đã hủy yêu cầu chỉnh sửa !", Enums.TypeCRUD.Success.ToString());
                        }
                        else if (schedule.TypeAction == "restore")
                        {
                            schedule.IdAction = null;
                            schedule.TypeAction = null;
                            schedule.Isdelete = true;
                            schedule.Approve = (int)ApproveStatus.Approved;
                            UpdateDatabase(schedule);

                            SaveChange();

                            return Ultility.Responses("Đã hủy yêu cầu khôi phục!", Enums.TypeCRUD.Success.ToString());

                        }
                        else // delete
                        {
                            schedule.IdAction = null;
                            schedule.TypeAction = null;
                            schedule.Isdelete = false;
                            schedule.Approve = (int)ApproveStatus.Approved;
                            UpdateDatabase(schedule);
                            SaveChange();
                            return Ultility.Responses("Đã hủy yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());
                        }
                    }

                    return Ultility.Responses("", Enums.TypeCRUD.Success.ToString());
                }


            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);

            }
        }
        public Response Approve(string idSchedule)
        {
            try
            {
                var schedule = (from x in _db.Schedules.AsNoTracking()
                                where x.IdSchedule == idSchedule
                                && x.Approve == (int)ApproveStatus.Waiting
                                select x).FirstOrDefault();
                if (schedule != null)
                {


                    if (schedule.TypeAction == "update")
                    {
                        var idScheduleTemp = schedule.IdAction;
                        schedule.Approve = (int)ApproveStatus.Approved;
                        schedule.IdAction = null;
                        schedule.TypeAction = null;


                        // delete tempdata
                        var scheduleTemp = (from x in _db.Schedules.AsNoTracking()
                                            where x.IdSchedule == idScheduleTemp
                                            select x).FirstOrDefault();
                        DeleteDatabase(scheduleTemp);
                    }
                    else if (schedule.TypeAction == "insert")
                    {
                        schedule.IdAction = null;
                        schedule.TypeAction = null;
                        schedule.Approve = (int)ApproveStatus.Approved;
                    }
                    else if (schedule.TypeAction == "restore")
                    {
                        schedule.IdAction = null;
                        schedule.TypeAction = null;
                        schedule.Approve = (int)ApproveStatus.Approved;
                        schedule.Isdelete = false;

                    }
                    else
                    {
                        schedule.IdAction = null;
                        schedule.TypeAction = null;
                        schedule.Approve = (int)ApproveStatus.Approved;
                        schedule.Isdelete = true;
                    }
                    UpdateDatabase(schedule);
                    SaveChange();
                    return Ultility.Responses($"Duyệt thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses("Không tìm thấy dữ liệu !", Enums.TypeCRUD.Warning.ToString());

                }
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
        public Response Refused(string idSchedule)
        {
            try
            {
                var schedule = (from x in _db.Schedules.AsNoTracking()
                                where x.IdSchedule == idSchedule
                                && x.Approve == (int)ApproveStatus.Waiting
                                select x).FirstOrDefault();
                if (schedule != null)
                {
                    if (schedule.TypeAction == "update")
                    {
                        var idScheduleTemp = schedule.IdAction;
                        // old hotel
                        var scheduleTemp = (from x in _db.Schedules.AsNoTracking()
                                            where x.IdSchedule == idScheduleTemp
                                            && x.IsTempData == true
                                            select x).FirstOrDefault();

                        schedule.Approve = (int)ApproveStatus.Approved;
                        schedule.IdAction = null;
                        schedule.TypeAction = null;

                        #region restore data

                        schedule.BeginDate = scheduleTemp.BeginDate;
                        schedule.CarId = scheduleTemp.CarId;
                        schedule.DepartureDate = scheduleTemp.DepartureDate;
                        schedule.DeparturePlace = scheduleTemp.DeparturePlace;
                        schedule.Description = scheduleTemp.Description;
                        schedule.EmployeeId = scheduleTemp.EmployeeId;
                        schedule.EndDate = scheduleTemp.EndDate;
                        schedule.IsHoliday = scheduleTemp.IsHoliday;
                        schedule.MaxCapacity = scheduleTemp.MaxCapacity;
                        schedule.MinCapacity = scheduleTemp.MinCapacity;

                        schedule.PromotionId = scheduleTemp.PromotionId;
                        schedule.ReturnDate = scheduleTemp.ReturnDate;
                        schedule.Vat = scheduleTemp.Vat;


                        schedule.TimePromotion = scheduleTemp.TimePromotion;
                        #endregion

                        DeleteDatabase(scheduleTemp);
                    }
                    else if (schedule.TypeAction == "insert")
                    {
                        schedule.IdAction = null;
                        schedule.TypeAction = null;
                        schedule.Approve = (int)ApproveStatus.Refused;
                    }
                    else if (schedule.TypeAction == "restore")
                    {
                        schedule.IdAction = null;
                        schedule.TypeAction = null;
                        schedule.Isdelete = true;
                        schedule.Approve = (int)ApproveStatus.Approved;
                    }
                    else // delete
                    {
                        schedule.IdAction = null;
                        schedule.TypeAction = null;
                        schedule.Isdelete = false;
                        schedule.Approve = (int)ApproveStatus.Approved;
                    }
                    UpdateDatabase(schedule);
                    SaveChange();
                    return Ultility.Responses($"Từ chối thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses("Không tìm thấy dữ liệu !", Enums.TypeCRUD.Warning.ToString());

                }
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
        public Response Update(UpdateScheduleViewModel input)
        {
            try
            {
                var userLogin = (from x in _db.Employees.AsNoTracking()
                                 where x.IdEmployee == input.IdUserModify
                                 select x).FirstOrDefault();

                var schedule = (from x in _db.Schedules.AsNoTracking()
                                where x.IdSchedule == input.IdSchedule
                                select x).FirstOrDefault();

                // clone new object
                var scheduleOld = new Schedule();
                scheduleOld = Ultility.DeepCopy<Schedule>(schedule);
                scheduleOld.IdAction = scheduleOld.IdSchedule.ToString();
                scheduleOld.IdSchedule = $"{Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now)}Temp";
                scheduleOld.IsTempData = true;

                CreateDatabase(scheduleOld);

                #region setdata
                schedule.IdAction = scheduleOld.IdSchedule.ToString();
                schedule.IdUserModify = input.IdUserModify;
                schedule.ModifyBy = userLogin.NameEmployee;

                schedule.Approve = (int)ApproveStatus.Waiting;
                schedule.TypeAction = "update";
                schedule.BeginDate = input.BeginDate;
                schedule.CarId = input.CarId;
                schedule.DepartureDate = input.DepartureDate;
                schedule.DeparturePlace = input.DeparturePlace;
                schedule.Description = input.Description;
                schedule.EmployeeId = input.EmployeeId;
                schedule.EndDate = input.EndDate;
                schedule.IsHoliday = input.IsHoliday;
                schedule.MaxCapacity = input.MaxCapacity;
                schedule.MinCapacity = input.MinCapacity;
                schedule.ReturnDate = input.ReturnDate;
                schedule.Vat = input.Vat;
                schedule.ModifyBy = userLogin.NameEmployee;
                #endregion

                UpdateDatabase(schedule);
                SaveChange();
                return Ultility.Responses("Đã gửi yêu cầu sửa !", Enums.TypeCRUD.Success.ToString());

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
        #endregion
        private bool CheckAnyBookingInSchedule(string idSchedule) // chỉ dùng khi thay đổi thông tin tour
        {
            // cách 1
            var scheduleInTour = (from x in _db.Schedules.AsNoTracking()
                                  where x.IdSchedule == idSchedule
                                  && x.QuantityCustomer == 0
                                  && x.Isdelete == false
                                  && x.Status == (int)Enums.StatusSchedule.Free
                                  && x.Approve == (int)Enums.ApproveStatus.Approved
                                  select x).FirstOrDefault();
            if (scheduleInTour != null) // có dữ liệu, tức là ko có tour
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public Response SearchSchedule(JObject frmData, string idTour)
        {
            try
            {
                Keywords keywords = new Keywords();

                if (!String.IsNullOrEmpty(idTour))
                {
                    keywords.KwIdTour = idTour;
                }

                var isDelete = PrCommon.GetString("isDelete", frmData);
                if (!String.IsNullOrEmpty(isDelete))
                {
                    keywords.IsDelete = Boolean.Parse(isDelete);
                }

                var kwIdSchedule = PrCommon.GetString("idSchedule", frmData);
                if (!String.IsNullOrEmpty(kwIdSchedule))
                {
                    keywords.KwId = kwIdSchedule.Trim().ToLower();
                }
                else
                {
                    keywords.KwId = "";
                }

                
                var kwBeginDate = PrCommon.GetString("beginDateFrom", frmData);
                if (!String.IsNullOrEmpty(kwBeginDate))
                {
                    keywords.KwBeginDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(kwBeginDate));
                }
                else
                {
                    keywords.KwBeginDate = 0;
                }

                var kwEndDate = PrCommon.GetString("beginDateTo", frmData);
                if (!String.IsNullOrEmpty(kwEndDate))
                {
                    keywords.KwEndDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(kwEndDate).AddDays(1).AddSeconds(-1));
                }
                else
                {
                    keywords.KwEndDate = 0;
                }

                var kwdepartureDate = PrCommon.GetString("departureDateFrom", frmData);
                if (!String.IsNullOrEmpty(kwdepartureDate))
                {
                    keywords.KwDepartureDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(kwdepartureDate));
                }
                else
                {
                    keywords.KwDepartureDate = 0;
                }

                var kwReturnDate = PrCommon.GetString("departureDateTo", frmData);
                if (!String.IsNullOrEmpty(kwReturnDate))
                {
                    keywords.KwReturnDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(kwReturnDate).AddDays(1).AddSeconds(-1));
                }
                else
                {
                    keywords.KwReturnDate = 0;
                }
                

                var kwTotalCostTourNotSvc = PrCommon.GetString("TotalCostTourNotService", frmData);
                if (!String.IsNullOrEmpty(kwTotalCostTourNotSvc))
                {
                    keywords.KwTotalCostTourNotService = float.Parse(kwTotalCostTourNotSvc);
                }
                else
                {
                    keywords.KwTotalCostTourNotService = 0;
                }

                var kwFinalPrice = PrCommon.GetString("finalPrice", frmData);
                if (!String.IsNullOrEmpty(kwFinalPrice))
                {
                    keywords.KwFinalPrice = float.Parse(kwFinalPrice);
                }
                else
                {
                    keywords.KwFinalPrice = 0;
                }

                var kwFinalPriceHoliday = PrCommon.GetString("finalPriceHoliday", frmData);
                if (!String.IsNullOrEmpty(kwFinalPriceHoliday))
                {
                    keywords.KwFinalPriceHoliday = float.Parse(kwFinalPriceHoliday);
                }
                else
                {
                    keywords.KwFinalPriceHoliday = 0;
                }

                var listSchedule = new List<Schedule>();
                if (!string.IsNullOrEmpty(isDelete))
                {
                    if (!string.IsNullOrEmpty(kwTotalCostTourNotSvc))
                    {
                        listSchedule = (from x in _db.Schedules
                                        where x.Isdelete == keywords.IsDelete &&
                                              x.TourId == idTour &&
                                              x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                              x.IsTempData == false &&
                                              x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                              x.TotalCostTourNotService.Equals(keywords.KwTotalCostTourNotService)

                                        select new Schedule
                                        {
                                            IdSchedule = x.IdSchedule,
                                            BeginDate = x.BeginDate,
                                            EndDate = x.EndDate,
                                            TotalCostTourNotService = x.TotalCostTourNotService,
                                            FinalPrice = x.FinalPrice,
                                            FinalPriceHoliday = x.FinalPriceHoliday,
                                            EmployeeId = x.EmployeeId,
                                            CarId = x.CarId,
                                            DepartureDate = x.DepartureDate,
                                            ReturnDate = x.ReturnDate,
                                            MaxCapacity = x.MaxCapacity,
                                            MinCapacity = x.MinCapacity,
                                            DeparturePlace = x.DeparturePlace,
                                            Description = x.Description,
                                            Vat = x.Vat,
                                            PromotionId = x.PromotionId,
                                            TimePromotion = x.TimePromotion
                                        }).ToList();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(kwFinalPrice))
                        {
                            listSchedule = (from x in _db.Schedules
                                            where x.Isdelete == keywords.IsDelete &&
                                                  x.TourId == idTour &&
                                                  x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                  x.IsTempData == false &&
                                                  x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                  x.FinalPrice.Equals(keywords.KwFinalPrice)

                                            select new Schedule
                                            {
                                                IdSchedule = x.IdSchedule,
                                                BeginDate = x.BeginDate,
                                                EndDate = x.EndDate,
                                                TotalCostTourNotService = x.TotalCostTourNotService,
                                                FinalPrice = x.FinalPrice,
                                                FinalPriceHoliday = x.FinalPriceHoliday,
                                                EmployeeId = x.EmployeeId,
                                                CarId = x.CarId,
                                                DepartureDate = x.DepartureDate,
                                                ReturnDate = x.ReturnDate,
                                                MaxCapacity = x.MaxCapacity,
                                                MinCapacity = x.MinCapacity,
                                                DeparturePlace = x.DeparturePlace,
                                                Description = x.Description,
                                                Vat = x.Vat,
                                                PromotionId = x.PromotionId,
                                                TimePromotion = x.TimePromotion
                                            }).ToList();
                        }
                        else
                        {

                            if (!string.IsNullOrEmpty(kwFinalPriceHoliday))
                            {
                                listSchedule = (from x in _db.Schedules
                                                where x.Isdelete == keywords.IsDelete &&
                                                      x.TourId == idTour &&
                                                      x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                      x.IsTempData == false &&
                                                      x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                      x.FinalPriceHoliday.Equals(keywords.KwFinalPriceHoliday)

                                                select new Schedule
                                                {
                                                    IdSchedule = x.IdSchedule,
                                                    BeginDate = x.BeginDate,
                                                    EndDate = x.EndDate,
                                                    TotalCostTourNotService = x.TotalCostTourNotService,
                                                    FinalPrice = x.FinalPrice,
                                                    FinalPriceHoliday = x.FinalPriceHoliday,
                                                    EmployeeId = x.EmployeeId,
                                                    CarId = x.CarId,
                                                    DepartureDate = x.DepartureDate,
                                                    ReturnDate = x.ReturnDate,
                                                    MaxCapacity = x.MaxCapacity,
                                                    MinCapacity = x.MinCapacity,
                                                    DeparturePlace = x.DeparturePlace,
                                                    Description = x.Description,
                                                    Vat = x.Vat,
                                                    PromotionId = x.PromotionId,
                                                    TimePromotion = x.TimePromotion
                                                }).ToList();
                            }
                            else
                            {
                                // ngày bán vé
                                if (keywords.KwBeginDate > 0 && keywords.KwEndDate > 0)
                                {
                                    listSchedule = (from x in _db.Schedules
                                                    where x.TourId == idTour &&
                                                          x.Isdelete == keywords.IsDelete &&
                                                          x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                          x.IsTempData == false &&
                                                          x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                          x.BeginDate >= keywords.KwBeginDate &&
                                                          x.EndDate <= keywords.KwEndDate
                                                    select x).ToList();
                                }
                                else
                                {
                                    if (keywords.KwBeginDate == 0 && keywords.KwEndDate > 0)
                                    {
                                        listSchedule = (from x in _db.Schedules
                                                        where x.TourId == idTour &&
                                                              x.Isdelete == keywords.IsDelete &&
                                                              x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                              x.IsTempData == false &&
                                                              x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                              x.EndDate <= keywords.KwEndDate
                                                        select x).ToList();
                                    }
                                    else
                                    {
                                        if (keywords.KwEndDate == 0 && keywords.KwBeginDate > 0)
                                        {
                                            listSchedule = (from x in _db.Schedules
                                                            where x.TourId == idTour &&
                                                                  x.Isdelete == keywords.IsDelete &&
                                                                  x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                                  x.IsTempData == false &&
                                                                  x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                                  x.BeginDate >= keywords.KwBeginDate
                                                            select x).ToList();
                                        }
                                        else
                                        {
                                            //Ngày khởi hành
                                            if (keywords.KwDepartureDate > 0 && keywords.KwReturnDate > 0)
                                            {
                                                listSchedule = (from x in _db.Schedules
                                                                where x.TourId == idTour &&
                                                                      x.Isdelete == keywords.IsDelete &&
                                                                      x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                                      x.IsTempData == false &&
                                                                      x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                                      x.DepartureDate >= keywords.KwDepartureDate &&
                                                                      x.ReturnDate <= keywords.KwReturnDate
                                                                select x).ToList();
                                            }
                                            else
                                            {
                                                if (keywords.KwDepartureDate == 0 && keywords.KwReturnDate > 0)
                                                {
                                                    listSchedule = (from x in _db.Schedules
                                                                    where x.TourId == idTour &&
                                                                          x.Isdelete == keywords.IsDelete &&
                                                                          x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                                          x.IsTempData == false &&
                                                                          x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                                          x.ReturnDate <= keywords.KwReturnDate
                                                                    select x).ToList();
                                                }
                                                else
                                                {
                                                    if (keywords.KwReturnDate == 0 && keywords.KwDepartureDate > 0)
                                                    {
                                                        listSchedule = (from x in _db.Schedules
                                                                        where x.TourId == idTour &&
                                                                              x.Isdelete == keywords.IsDelete &&
                                                                              x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                                              x.IsTempData == false &&
                                                                              x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                                              x.DepartureDate >= keywords.KwDepartureDate
                                                                        select x).ToList();
                                                    }
                                                    else
                                                    {
                                                        listSchedule = (from x in _db.Schedules
                                                                        where x.TourId == idTour &&
                                                                              x.Isdelete == keywords.IsDelete &&
                                                                              x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                                              x.IsTempData == false &&
                                                                              x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved)
                                                                        select x).ToList();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(kwTotalCostTourNotSvc))
                    {
                        listSchedule = (from x in _db.Schedules
                                        where x.Isdelete == keywords.IsDelete &&
                                              x.TourId == idTour &&
                                              x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                              x.IsTempData == false &&
                                              x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                              x.TotalCostTourNotService.Equals(keywords.KwTotalCostTourNotService) 

                                        select x).ToList();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(kwFinalPrice))
                        {
                            listSchedule = (from x in _db.Schedules
                                            where x.Isdelete == keywords.IsDelete &&
                                                  x.TourId == idTour &&
                                                  x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                  x.IsTempData == false &&
                                                  x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                  x.FinalPrice.Equals(keywords.KwFinalPrice)

                                            select x).ToList();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(kwFinalPriceHoliday))
                            {
                                listSchedule = (from x in _db.Schedules
                                                where x.Isdelete == keywords.IsDelete &&
                                                      x.TourId == idTour &&
                                                      x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                      x.IsTempData == false &&
                                                      x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                      x.FinalPriceHoliday.Equals(keywords.KwFinalPriceHoliday)

                                                select x).ToList();
                            }
                            else
                            {
                                // ngày bán vé
                                if (keywords.KwBeginDate > 0 && keywords.KwEndDate > 0)
                                {
                                    listSchedule = (from x in _db.Schedules
                                                    where x.TourId == idTour &&
                                                          x.Isdelete == keywords.IsDelete &&
                                                          x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                          x.IsTempData == false &&
                                                          x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                          x.BeginDate >= keywords.KwBeginDate &&
                                                          x.EndDate <= keywords.KwEndDate
                                                    select x).ToList();
                                }
                                else
                                {
                                    if (keywords.KwBeginDate == 0 && keywords.KwEndDate > 0)
                                    {
                                        listSchedule = (from x in _db.Schedules
                                                        where x.TourId == idTour &&
                                                              x.Isdelete == keywords.IsDelete &&
                                                              x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                              x.IsTempData == false &&
                                                              x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                              x.EndDate <= keywords.KwEndDate
                                                        select x).ToList();
                                    }
                                    else
                                    {
                                        if (keywords.KwEndDate == 0 && keywords.KwBeginDate > 0)
                                        {
                                            listSchedule = (from x in _db.Schedules
                                                            where x.TourId == idTour &&
                                                                  x.Isdelete == keywords.IsDelete &&
                                                                  x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                                  x.IsTempData == false &&
                                                                  x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                                  x.BeginDate >= keywords.KwBeginDate
                                                            select x).ToList();
                                        }
                                        else
                                        {
                                            //ngày khởi hành
                                            if (keywords.KwDepartureDate > 0 && keywords.KwReturnDate > 0)
                                            {
                                                listSchedule = (from x in _db.Schedules
                                                                where x.TourId == idTour &&
                                                                      x.Isdelete == keywords.IsDelete &&
                                                                      x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                                      x.IsTempData == false &&
                                                                      x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                                      x.DepartureDate >= keywords.KwDepartureDate &&
                                                                      x.ReturnDate <= keywords.KwReturnDate
                                                                select x).ToList();
                                            }
                                            else
                                            {
                                                if (keywords.KwDepartureDate == 0 && keywords.KwReturnDate > 0)
                                                {
                                                    listSchedule = (from x in _db.Schedules
                                                                    where x.TourId == idTour &&
                                                                          x.Isdelete == keywords.IsDelete &&
                                                                          x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                                          x.IsTempData == false &&
                                                                          x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                                          x.ReturnDate <= keywords.KwReturnDate
                                                                    select x).ToList();
                                                }
                                                else
                                                {
                                                    if (keywords.KwReturnDate == 0 && keywords.KwDepartureDate > 0)
                                                    {
                                                        listSchedule = (from x in _db.Schedules
                                                                        where x.TourId == idTour &&
                                                                              x.Isdelete == keywords.IsDelete &&
                                                                              x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                                              x.IsTempData == false &&
                                                                              x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved) &&
                                                                              x.DepartureDate >= keywords.KwDepartureDate
                                                                        select x).ToList();
                                                    }
                                                    else
                                                    {
                                                        listSchedule = (from x in _db.Schedules
                                                                        where x.TourId == idTour &&
                                                                              x.Isdelete == keywords.IsDelete &&
                                                                              x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                                              x.IsTempData == false &&
                                                                              x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved)
                                                                        select x).ToList();
                                                    }
                                                        
                                                }
    
                                            }   
                                        }
                                    }
                                }
                                
                            }
                                
                        }
                           
                    }
                }
                var result = Mapper.MapSchedule(listSchedule);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);

            }
            catch(Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response SearchScheduleWaiting(JObject frmData, string idTour)
        {
            try
            {
                Keywords keywords = new Keywords();

                if (!String.IsNullOrEmpty(idTour))
                {
                    keywords.KwIdTour = idTour;
                }

                var kwIdSchedule = PrCommon.GetString("idSchedule", frmData);
                if (!String.IsNullOrEmpty(kwIdSchedule))
                {
                    keywords.KwId = kwIdSchedule.Trim().ToLower();
                }
                else
                {
                    keywords.KwId = "";
                }

                var kwFinalPrice = PrCommon.GetString("finalPrice", frmData);
                if (!String.IsNullOrEmpty(kwFinalPrice))
                {
                    keywords.KwFinalPrice = float.Parse(kwFinalPrice);
                }
                else
                {
                    keywords.KwFinalPrice = 0;
                }

                var kwFinalPriceHoliday = PrCommon.GetString("finalPriceHoliday", frmData);
                if (!String.IsNullOrEmpty(kwFinalPriceHoliday))
                {
                    keywords.KwFinalPriceHoliday = float.Parse(kwFinalPriceHoliday);
                }
                else
                {
                    keywords.KwFinalPriceHoliday = 0;
                }

                var KwModifyBy = PrCommon.GetString("modifyBy", frmData);
                if (!String.IsNullOrEmpty(KwModifyBy))
                {
                    keywords.KwModifyBy = KwModifyBy.Trim().ToLower();
                }
                else
                {
                    keywords.KwModifyBy = "";
                }

                var fromDate = PrCommon.GetString("modifyDateFrom", frmData);
                if (!String.IsNullOrEmpty(fromDate))
                {
                    keywords.KwFromDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(fromDate));
                }
                else
                {
                    keywords.KwFromDate = 0;
                }

                var toDate = PrCommon.GetString("modifyDateTo", frmData);
                if (!String.IsNullOrEmpty(toDate))
                {
                    keywords.KwToDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(toDate).AddDays(1).AddSeconds(-1));
                }
                else
                {
                    keywords.KwToDate = 0;
                }

                var typeAction = PrCommon.GetString("typeAction", frmData);
                keywords.KwTypeActions = PrCommon.getListString(typeAction, ',', false);

                var listSchedule = new List<Schedule>();

                if (!string.IsNullOrEmpty(kwFinalPrice))
                {
                    listSchedule = (from x in _db.Schedules
                                    where
                                          x.TourId == idTour &&
                                          x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                          x.IsTempData == false &&
                                          x.Approve == Convert.ToInt16(Enums.ApproveStatus.Waiting) &&
                                          x.FinalPrice.Equals(keywords.KwFinalPrice)

                                    select x).ToList();
                }
                else
                {
                    if (!string.IsNullOrEmpty(kwFinalPriceHoliday))
                    {
                        listSchedule = (from x in _db.Schedules
                                        where
                                              x.TourId == idTour &&
                                              x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                              x.IsTempData == false &&
                                              x.Approve == Convert.ToInt16(Enums.ApproveStatus.Waiting) &&
                                              x.FinalPriceHoliday.Equals(keywords.KwFinalPriceHoliday)

                                        select x).ToList();
                    }
                    else
                    {
                        if (keywords.KwTypeActions.Count > 0)
                        {
                            listSchedule = (from x in _db.Schedules
                                            where
                                                  x.TourId == idTour &&
                                                  x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                  x.IsTempData == false &&
                                                  x.Approve == Convert.ToInt16(Enums.ApproveStatus.Waiting) &&
                                                  keywords.KwTypeActions.Contains(x.TypeAction)

                                            select x).ToList();
                        }
                        else
                        {
                            listSchedule = (from x in _db.Schedules
                                            where x.TourId == idTour &&
                                                  x.IdSchedule.ToLower().Contains(keywords.KwId) &&
                                                  x.IsTempData == false &&
                                                  x.Approve == Convert.ToInt16(Enums.ApproveStatus.Waiting)
                                            select x).ToList();
                        }
                            
                    }

                }

                var result = Mapper.MapSchedule(listSchedule);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
    }
}
