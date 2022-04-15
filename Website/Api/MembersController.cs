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
using Newtonsoft.Json;
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
    public class MembersController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IMemberRepository _memberRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly string FolderStored = "images";
        private readonly string rootPath = @"wwwroot\Templetes\images";
        protected readonly TNRContext _context;
        public MembersController(ILogger<EventsController> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
           IMemberRepository memberRepository,
           TNRContext context,
           ITokenService tokenService)
        {
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _memberRepository = memberRepository;
            _tokenService = tokenService;
            _context = context;

        }

        [AllowAnonymous]
        [HttpPost()]
        public IActionResult Add([FromBody] MemberModel model)
        {

            try
            {
                //Member empObj = JsonConvert.DeserializeObject<Member>(model);
                _memberRepository.Add(new Member
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    Specialist = model.Specialist,
                    Status = model.Status,
                    Create_At = model.Create_At,
                    Update_At =  model.Update_At,
                    IdRegion = Convert.ToInt32(model.IdRegion),
                });


                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet()]
        public IActionResult GetAll()
        {

            try
            {
                var err = "No data";
                var data = _memberRepository.GetAllData().Include(x => x.Region).ToList()
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Email,
                        x.Phone,
                        x.Specialist,
                        x.Status,
                        x.Create_At,
                        x.IdRegion,
                        RegionName = x.Region.Name
                    });
                //if (data != null)
                //{
                //    return Ok(data);
                //}
                //return Ok(err);
                return data != null ? Ok(data) : Ok(err);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {

            try
            {
                var data = _memberRepository.GetAllData().Include(x => x.Region).ToList()
                         .Select(x => new
                         {
                             x.Id,
                             x.Name,
                             x.Email,
                             x.Phone,
                             x.Specialist,
                             x.Status,
                             x.Create_At,
                             x.IdRegion,
                             RegionName = x.Region.Name
                         }).FirstOrDefault(x => x.Id == id);

                var err = $"No Data Not Find: {id}";

                return data != null ? Ok(data) : Ok(err);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut()]
        public IActionResult Update([FromBody] MemberModelUp model)
        {

            try
            {
                var dataId = _memberRepository.GetAllData().ToList().FirstOrDefault(s => s.Id == model.Id);

                if (dataId != null)
                {
                    _memberRepository.Update(new Member
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Email = model.Email,
                        Phone = model.Phone,
                        Specialist = model.Specialist,
                        Status = model.Status,
                        Update_At = model.Update_At,
                        IdRegion = Convert.ToInt32(model.IdRegion),
                    });
                    return Ok(model);
                }

                return Ok("Update Thất bại ");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

    }

}
