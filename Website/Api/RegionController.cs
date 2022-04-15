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
using Region = Entities.Entities.Region;

namespace Website.Api
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class RegionController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IRegionRepository _regionRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly string FolderStored = "images";
        private readonly string rootPath = @"wwwroot\Templetes\images";
        protected readonly TNRContext _context;
        public RegionController(ILogger<EventsController> logger, IMapper mapper,
           IHttpContextAccessor httpContextAccessor,
           IRegionRepository regionRepository,
           TNRContext context,
           ITokenService tokenService)
        {
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _regionRepository = regionRepository;
            _tokenService = tokenService;
            _context = context;

        }

        // Add Data Region
        [AllowAnonymous]
        [HttpPost()]
        public IActionResult AddRegion([FromBody] RegionModel model)
        {

            if (model != null)
            {
                try
                {
                    _regionRepository.Add(new Region { 
                        Name = model.Name,
                        Description = model.Description, 
                        Create_At = model.Create_At });

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



        // Get ALL Data Region
        [AllowAnonymous]
        [HttpGet()]
        public IActionResult GetAllRegion()
        {

            try
            {
                /* var data = _regionRepository.GetAll().ToList();
                 return Ok(data);*/
                var err = "No data";
                var data = _regionRepository.GetAllData().ToList()
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Description,
                        x.Create_At,
                        x.Update_At,
                    });
                if (data != null)
                {
                    return Ok(data);
                }
                return Ok(err);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        //GetDataAllRegion
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetRegionsId(int id)
        {
            try
            {
                var err = $"Id = {id}, Not Found In Data";
                var dataId = _regionRepository.GetAllData().ToList()
                        .Select(x => new
                        {
                            x.Id,
                            x.Name,
                            x.Description,
                            x.Create_At,
                            x.Update_At,
                        }).Where(x => x.Id == id);
               
                return dataId.Count() != 0 ? Ok(err) : Ok(err);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }


        //UpdateRegion
        [AllowAnonymous]
        [HttpPut()]
        public IActionResult UpdateRegion([FromBody] RegionModelUp model)
        {
            try
            {
                var dataTest = _regionRepository.GetAllData().ToList().Where(x => x.Id == Convert.ToInt32(model.Id));
                //var dataId = _regionRepository.GetAllData().ToList().Where(s => s.Id == model.Id);
                if (dataTest.Count() != 0)
                {
                    _regionRepository.Update(new Region
                    { Id = Convert.ToInt32(model.Id), 
                        Name = model.Name,
                        Description = model.Description,
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


        //DeleteRegion
        [AllowAnonymous]
        [HttpDelete()]
        public IActionResult DeleteRegion(int id)
        {
            try
            {
                if (id != 0)
                {
                    _regionRepository.Delete(new Region { Id = id });
                    return Ok($"Thành công xoá region có ID là: {id}");
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
