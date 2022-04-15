using AutoMapper;
using Business.IRepostitory;
using Business.Repository;
using Entities.DAL;
using Entities.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Model.APIs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Website.Services;

namespace Website.Api
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IStudentRepository _studentRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly string FolderStored = "images";
        private readonly string rootPath = @"wwwroot\Templetes\images";
        protected readonly TNRContext _context;
        public StudentsController(ILogger<EventsController> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
           IStudentRepository studentRepository,
           TNRContext context,
           ITokenService tokenService)
        {
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _studentRepository = studentRepository;
            _tokenService = tokenService;
            _context = context;

        }

        [AllowAnonymous]
        [HttpPost()]
        public IActionResult AddStudent()
        {
           
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    int grade = i < 5 ? 1 : 2;
                    _studentRepository.Add(new Student { Name = $"Test-{i}", CurrentGradeId = grade });
                }
               
                return Ok("Insert thành công!!!!");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("AddStudentWithNew")]
        public IActionResult AddStudentWithNew()
        {
            var studentRespository = new StudentRepository(_context);
            try
            {
                for (int i = 11; i <= 20; i++)
                {
                    int grade = i < 15 ? 1 : 2;
                    studentRespository.Add(new Student { Name = $"Test-{i}", CurrentGradeId = grade });
                }

                return Ok("Insert thành công!!!!");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet()]
        public IActionResult GetStudents()
        {

            try
            {
                var data = _studentRepository.GetAllData().Include(x => x.Grade).ToList()
                    .Select(x=> new { x.Id, x.Name, GradeName = x.Grade.Name }); 

                return Ok(data);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }

}
