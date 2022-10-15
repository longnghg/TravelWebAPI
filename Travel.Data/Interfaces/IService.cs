﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.ContractVM;

namespace Travel.Data.Interfaces
{
   public  interface IService
    {
        string CheckBeforSave(JObject frmData, ref Notification _message, Shared.Ultilities.Enums.TypeService type, bool isUpdate = false);
        Response GetHotel();
        Response CreateHotel(CreateHotelViewModel input);
        Response GetRestaurant();
        Response CreateRestaurant(CreateRestaurantViewModel input);
        Response GetPlace();
        Response CreatePlace(CreatePlaceViewModel input);

    }
}
