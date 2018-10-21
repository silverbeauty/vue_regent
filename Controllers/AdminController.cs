using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson;
using Regents.EntityFramework;
using Regents.Models.Courses;
using Regents.ViewModels.Courses;
using Newtonsoft.Json;
namespace Regents.Controllers
{
    //[Authorize(Roles = "Admin")] //Comment this out to test without sso
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdminController : BaseController
    {
        private readonly RegentsMongoContext _regentsMongoContext;


        #region SETUP AND CONSTRUCTOR

        public AdminController(RegentsMongoContext regentsMongoContext)
        {
           this._regentsMongoContext = regentsMongoContext;
        }
        #endregion

        #region COURSE_MANAGER
        /******************************    COURSE_MANAGER    ************************************/

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(CourseManagerVm))]

        public IActionResult CourseManager()
        {
            //Get courses, units, and topics from database
            var courses = _regentsMongoContext.Courses.AsQueryable().ToList();
            var units = _regentsMongoContext.Units.AsQueryable().ToList();
            var topics = _regentsMongoContext.Topics.AsQueryable().ToList();

            //Map to view models
            var coursesVms = courses.Select(Mapper.Map<CourseVm>).ToList();
            var unitsVms = courses.Select(Mapper.Map<UnitVm>).ToList();
            var topicsVms = courses.Select(Mapper.Map<TopicVm>).ToList();


            //Create view model
            var vm = new CourseManagerVm();
            vm.Courses = coursesVms;
            vm.Units = unitsVms;
            vm.Topics = topicsVms;

            return Ok(vm);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CourseVm>))]

        public IActionResult Courses()
        {
            var courses = _regentsMongoContext.Courses.AsQueryable().ToList();
            var coursesVms = courses.Select(Mapper.Map<CourseVm>).ToList();
            return Ok(coursesVms);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public  async Task<IActionResult> CreateCourse([FromBody] CourseVm vm){
            var desc = vm.Description;
            var c = new CourseVm();
            c.Description = desc;
            var course = Mapper.Map<Course>(c);
            
            await _regentsMongoContext.Courses.InsertOneAsync(course);
            return Ok();
        }


       

        #endregion

    }
}