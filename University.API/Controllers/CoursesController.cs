using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Repositories.Implements;
using AutoMapper;
namespace University.API.Controllers

{
    [RoutePrefix("api/Courses")]
    public class CoursesController : ApiController
    {
        private readonly IMapper mapper;
        private readonly CourseRepository courseRepository = new CourseRepository(new UniversityEntities());

        public CoursesController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {

            var courses = await courseRepository.GetAll();
            var coursesDTO = courses.Select(x => mapper.Map<CourseDTO>(x));

            return Ok(coursesDTO);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {

            var course = await courseRepository.GetById(id);

            var courseDTO = mapper.Map<CourseDTO>(course);

            return Ok(courseDTO);
        }
        [HttpPost]
        public async Task<IHttpActionResult> Create(CourseDTO courseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var course = mapper.Map<Course>(courseDTO);
                course = await courseRepository.Insert(course);

                courseDTO.CourseID = courseDTO.CourseID;
                return Ok(courseDTO);
            }


            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }
        [HttpPut]
        public async Task<IHttpActionResult> Edit(int id, CourseDTO courseDTO)
        {
            try
            {
                if (id != courseDTO.CourseID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);



                var course = await courseRepository.GetById(id);
                if (course == null)
                    return NotFound();

                
                course.Title = courseDTO.Title;
                course.Credits = courseDTO.Credits;


                await courseRepository.Update(course);

                return Ok(courseDTO);
            }


            catch (Exception ex)
            {

                return InternalServerError(ex);

            }

        }
        [HttpDelete]

        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {

                var course = await courseRepository.GetById(id);
                if (course == null)
                    return NotFound();


                   await courseRepository.Delete(id);

                return Ok();
            }


            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}
    
