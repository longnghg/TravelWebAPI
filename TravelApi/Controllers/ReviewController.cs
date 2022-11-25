﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.ReviewVM;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReview _review;
        private Notification message;
        private Response res;

        public ReviewController(IReview review)
        {
            _review = review;
            res = new Response();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("list-review")]
        public object GetReview()
        {
            res = _review.GetsReview();
            return Ok(res);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("create-review")]
        public object CreateReview([FromBody] JObject frmData)
        {

            message = null;
            var result = _review.CheckBeforSave(frmData, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateReviewModel>(result);
                res = _review.CreateReview(createObj);
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }
    }
}
