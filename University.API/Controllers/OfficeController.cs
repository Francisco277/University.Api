using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Repositories.Implements;
using AutoMapper;

namespace University.API.Controllers
{
    [RoutePrefix("api/OfficeAssignments")]
    public class OfficeController : ApiController
    {
        private readonly IMapper mapper;
        private readonly OfficeAssignmentRepository officeRepository = new OfficeAssignmentRepository(new UniversityEntities());

        public OfficeController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var office = await officeRepository.GetAll();
            var officeDTO = office.Select(x => mapper.Map<OfficeDTO>(x));

            return Ok(officeDTO);
        }
        [HttpGet]

        public async Task<IHttpActionResult> GetById(int id)
        {
            var office = await officeRepository.GetById(id);
            var officeDTO = mapper.Map<OfficeDTO>(office);
            return Ok(officeDTO);
        }
        [HttpPost]
        public async Task<IHttpActionResult> Create(OfficeDTO officeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var office = mapper.Map<OfficeAssignment>(officeDTO);
                office = await officeRepository.Insert(office);


                officeDTO.InstructorID = office.InstructorID;
               
                return Ok(officeDTO);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        [HttpPut]

        public async Task<IHttpActionResult> Edit(int id, OfficeDTO officeDTO)
        {
            try
            {
                if (id != officeDTO.InstructorID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var office = await officeRepository.GetById(id);
                if (office == null)
                    return NotFound();


                office.InstructorID = officeDTO.InstructorID;
                office.Location = officeDTO.Location;

                await officeRepository.Update(office);


                return Ok(officeDTO);
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


                var office = await officeRepository.GetById(id);
                if (office == null)
                    return NotFound();

                await officeRepository.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}