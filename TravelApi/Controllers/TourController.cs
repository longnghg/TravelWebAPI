﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.TourVM;
using TravelApi.Hubs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITour _tourRes;
        private Notification message;
        private Response res;
        private IHubContext<TravelHub, ITravelHub> _messageHub;

        public TourController(ITour tourRes, IHubContext<TravelHub, ITravelHub> messageHub)
        {
            _tourRes = tourRes;
            res = new Response();
            _messageHub = messageHub;
        }

        [HttpPost]
        [Authorize]
        [Route("create-tour")]
        public object Create([FromBody] JObject frmData)
        {
            message = null;
            var result = _tourRes.CheckBeforSave(frmData, ref message,false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateTourViewModel>(result);
                res = _tourRes.Create(createObj);
            }
            return Ok(res);
        }
        // GET api/<TourController>/5
        [HttpGet]
        [AllowAnonymous]
        [Route("gets-tour")]
        public object Get()
        {
            res = _tourRes.Get();
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-tour-waiting")]
        public object GetWaiting()
        {
            res = _tourRes.GetWaiting();
            return Ok(res);
        }

        // POST api/<TourController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TourController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpGet("{id}")]
        [Authorize]
        [Route("delete-tour")]
        public object DeleteTour(string idTour)
        {
            res = _tourRes.Delete(idTour);
            _messageHub.Clients.All.Init();
            return Ok(res);
        }


        [HttpGet("{id}")]
        [Authorize]
        [Route("restore-tour")]
        public object RestoreTour(string idTour)
        {
            res = _tourRes.RestoreTour(idTour);
            _messageHub.Clients.All.Init();
            return Ok(res);
        }
    }
}
