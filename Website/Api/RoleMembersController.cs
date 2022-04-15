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
    public class RoleMembersController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IRoleMemberRepository _roleMebmberRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly string FolderStored = "images";
        private readonly string rootPath = @"wwwroot\Templetes\images";
        protected readonly TNRContext _context;
        public RoleMembersController(ILogger<EventsController> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
           IRoleMemberRepository roleMebmberRepository,
           TNRContext context,
           ITokenService tokenService)
        {
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _roleMebmberRepository = roleMebmberRepository;
            _tokenService = tokenService;
            _context = context;

        }

        [AllowAnonymous]
        [HttpPost()]
        public IActionResult Add([FromBody] RoleMemberModel model)
        {

            try
            {
                //Member empObj = JsonConvert.DeserializeObject<Member>(model);
                _roleMebmberRepository.Add(new RoleMember
                {
                    Name = model.Name,
                    Description = model.Description,
                    Create_At = model.Create_At,
                    Update_At = model.Update_At,
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
                var data = _roleMebmberRepository.GetAllData().ToList()
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Description,
                        x.Create_At,
                        x.Update_At
                    });
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
                var data = _roleMebmberRepository.GetAllData().ToList()
                         .Select(x => new
                         {
                             x.Id,
                             x.Name,
                             x.Description,
                             x.Create_At,
                             x.Update_At
                         }).FirstOrDefault(x => x.Id == id);

                var err = $"Data Not Find: {id}";

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
        public IActionResult Update([FromBody] RoleMemberModelUp model)
        {

            try
            {
                var dataId = _roleMebmberRepository.GetAllData().ToList().FirstOrDefault(s => s.Id == model.Id);

                if (dataId != null)
                {
                    _roleMebmberRepository.Update(new RoleMember
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description,
                        Update_At = model.Update_At,
                        
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
