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
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IMemberRepository _memberRepository;
        private readonly ICommentRepository _CommentRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly string FolderStored = "images";
        private readonly string rootPath = @"wwwroot\Templetes\images";
        protected readonly TNRContext _context;
        public CommentsController(ILogger<EventsController> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
           IMemberRepository memberRepository,
           ICommentRepository CommentRepository,
           TNRContext context,
           ITokenService tokenService)
        {
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _memberRepository = memberRepository;
            _CommentRepository = CommentRepository;
            _tokenService = tokenService;
            _context = context;

        }
        // Add Data Comment
        [AllowAnonymous]
        [HttpPost()]
        public IActionResult Add([FromBody] CommentModel model)
        {

            if (model != null)
            {
                try
                {
                    _CommentRepository.Add(new Comment { ContentMember = model.ContentMember, Create_At = model.Create_At });

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


        //GetDataAllComment
        [AllowAnonymous]
        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                var data = _CommentRepository.GetAll().ToList().Select(x => new
                {
                    x.Id,
                    x.ContentMember,
                    x.Create_At,
                });
                return Ok(data);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        //GetDataAllComment
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            try
            {
                var err = $"Id = {id}, Not Found In Data";
                var dataId = _CommentRepository.GetAllData().Include(x => x.Member).ToList()
                        .Select(x => new
                        {
                            x.Id,
                            x.ContentMember,
                            x.Create_At,
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

        //UpdateComment
        [AllowAnonymous]
        [HttpPut()]
        public IActionResult Update([FromBody] CommentModel1 model)
        {
            try
            {
                var dataTest = _CommentRepository.GetAllData().Include(x => x.Member).ToList().FirstOrDefault(x => x.Id == Convert.ToInt32(model.Id));
                //var dataTest = _CommentRepository.GetById(model.Id);
                if (dataTest != null)
                {
                    _CommentRepository.Update(new Comment
                    {
                        Id = Convert.ToInt32(model.Id),
                        ContentMember = model.ContentMember,
                    });
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

        //DeleteComment
        [AllowAnonymous]
        [HttpDelete()]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    _CommentRepository.Delete(new Comment { Id = id });
                    return Ok($"Thành công xoá Comment có {id}");
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
