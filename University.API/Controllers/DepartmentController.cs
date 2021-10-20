using AutoMapper;
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

namespace University.API.Controllers
{
    [RoutePrefix("api/Departments")]
    public class DepartmentController : ApiController
    {
        private readonly IMapper mapper;
        private readonly DepartmentRepository departmentRepository = new DepartmentRepository(new UniversityEntities());
        public DepartmentController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        [HttpGet]

        public async Task<IHttpActionResult> GetAll()
        {
            var department = await departmentRepository.GetAll();
            var departmentDTO = department.Select(x => mapper.Map<DepartmentDTO>(x));

            return Ok(departmentDTO);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var department = await departmentRepository.GetById(id);
            var departmentDTO = mapper.Map<DepartmentDTO>(department);
            return Ok(departmentDTO);
        }
        [HttpPost]

        public async Task<IHttpActionResult> Create(DepartmentDTO departmentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var department = mapper.Map<Department>(departmentDTO);
                department = await departmentRepository.Insert(department);



                departmentDTO.DepartmentID = department.DepartmentID;
                return Ok(departmentDTO);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        [HttpPut]

        public async Task<IHttpActionResult> Edit(int id, DepartmentDTO departmentDTO)
        {
            try
            {
                if (id != departmentDTO.DepartmentID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var department = await departmentRepository.GetById(id);
                if (department == null)
                    return NotFound();


                department.Name = departmentDTO.Name;
                department.Budget = (decimal?)departmentDTO.Budget;
                department.StartDate = departmentDTO.StartDate;
                department.InstructorID = departmentDTO.InstructorID;



                await departmentRepository.Update(department);


                return Ok(departmentDTO);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {


                var department = await departmentRepository.GetById(id);
                if (department == null)
                    return NotFound();

           

                await departmentRepository.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}

