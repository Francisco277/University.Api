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
    [RoutePrefix("api/Instructors")]
    public class InstructorController : ApiController
    {
        private readonly IMapper mapper;
        private readonly InstructorRepository instructorRepository = new InstructorRepository(new UniversityEntities());

        public InstructorController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {

            var instructors = await instructorRepository.GetAll();
            var instructorsDTO = instructors.Select(x => mapper.Map<InstructorDTO>(x));


            return Ok(instructorsDTO);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {

            var instructor = await instructorRepository.GetById(id);

            var instructorDTO = mapper.Map<InstructorDTO>(instructor);

            return Ok(instructorDTO);
        }
        [HttpPost]
        public async Task<IHttpActionResult> Create(InstructorDTO instructorDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var instructor = mapper.Map<Instructor>(instructorDTO);
                instructor = await instructorRepository.Insert(instructor);

                instructorDTO.ID = instructor.ID;
                return Ok(instructorDTO);
            }


            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        [HttpPut]

        public async Task<IHttpActionResult> Edit(int id, InstructorDTO instructorDTO)
        {
            try
            {
                if (id != instructorDTO.ID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);



                var instructor = await instructorRepository.GetById(id);
                if (instructor == null)
                    return NotFound();


                instructor.LastName = instructorDTO.LastName;
                instructor.FirstMidName = instructorDTO.FirstMidName;
                instructor.HireDate = instructorDTO.HireDate;




                await instructorRepository.Update(instructor);

                return Ok(instructorDTO);
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

                var instructor = await instructorRepository.GetById(id);
                if (instructor == null)
                    return NotFound();



                await instructorRepository.Delete(id);

                return Ok();
            }


            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}
