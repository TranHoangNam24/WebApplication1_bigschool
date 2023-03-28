using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication1.Controllers.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow (FollowingDto followingDto)
        {
            var userID = User.Identity.GetUserId();
            if (_dbContext.Followings.Any(f => f.FollowerId == userID && f.FolloweeId == followingDto.FolloweeId))
                return BadRequest("Following already exists!");

            var following = new Following
            {
                FollowerId = userID,
                FolloweeId = followingDto.FolloweeId
            };
            _dbContext.Followings.Add(following);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}