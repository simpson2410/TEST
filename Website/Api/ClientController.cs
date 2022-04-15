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
using Model.TaskManagement;
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
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IMemberRepository _memberRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly string FolderStored = "images";
        private readonly string rootPath = @"wwwroot\Templetes\images";
        protected readonly TNRContext _context;
        public ClientsController(ILogger<EventsController> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
           IMemberRepository memberRepository,
           IClientRepository clientRepository,
           TNRContext context,
           ITokenService tokenService)
        {
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _memberRepository = memberRepository;
            _clientRepository = clientRepository;
            _tokenService = tokenService;
            _context = context;

        }
        // Add Data Client
        [AllowAnonymous]
        [HttpPost()]
        public IActionResult Add([FromBody] ClientModel model)
        {

            if(model != null)
            {
                try
                {
                    _clientRepository.Add(new Client { Name = model.Name, Phone = model.Phone, Email = model.Email, Address = model.Address, Create_At = model.Create_At });

                    return Ok(model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    return BadRequest(ex.Message);
                }
            }
            return Ok(model);
        }


        //GetDataAllClient
        [AllowAnonymous]
        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                var data = _clientRepository.GetAll().ToList().Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Email,
                    x.Phone,
                    x.Address,
                    x.Create_At,
                    x.Update_At,
                });
                return Ok(data);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        //GetDataAllClient
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            try
            {
                var err = $"Id = {id}, Not Found In Data";
                var dataId = _clientRepository.GetAllData().Include(x => x.Projects).ToList()
                        .Select(x => new
                        {
                            x.Id,
                            x.Name,
                            x.Email,
                            x.Phone,
                            x.Address,
                            x.Create_At,
                            x.Update_At,
                        }).FirstOrDefault(x => x.Id == id);
                /*if (dataId.Count() != 0 )
                {  
                    return Ok(dataId);
                }
                return Ok(err);*/
                return dataId != null ? Ok(dataId) : Ok(err);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        //UpdateClient
        [AllowAnonymous]
        [HttpPut()]
        public IActionResult Update([FromBody] ClientModel1 model)
        {
            try
            {
                var dataTest = _clientRepository.GetAllData().Include(x => x.Projects).ToList().FirstOrDefault(x => x.Id == Convert.ToInt32(model.Id));
                //var dataTest = _clientRepository.GetById(model.Id);
                if (dataTest != null)
                {
                    _clientRepository.Update(new Client
                    { Id = Convert.ToInt32(model.Id), 
                        Name = model.Name, 
                        Phone = model.Phone, 
                        Email = model.Email, 
                        Address = model.Address, 
                        Update_At = model.Update_At });
                    return Ok(model);
                }    
                return Ok($"Id = {model.Id}, Not Found Data, Error Try again !");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        //DeleteClient
        [AllowAnonymous]
        [HttpDelete()]
        public IActionResult Delete(int id)
        {
            try
            {
               if ( id != 0)
                {
                    _clientRepository.Delete(new Client { Id = id});
                    return Ok($"Thành công xoá client có {id}");
                }    
                return Ok("Error, Try Again !");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
