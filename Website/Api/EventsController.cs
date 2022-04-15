using AutoMapper;
using Business.IRepostitory;
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
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IStoreRegisterRepository _storeRegisterRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly string FolderStored = "images";
        private readonly string rootPath = @"wwwroot\Templetes\images";
        public EventsController(ILogger<EventsController> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
           IStoreRegisterRepository storeRegisterRepository,
           ITokenService tokenService)
        {
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _storeRegisterRepository = storeRegisterRepository;
            _tokenService = tokenService;

        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(StoreRegisterRequest model)
        {
            var result = new ResponseModel<StoreResponse>()
            {
                Success = false,
                Message = "Sự kiện chưa xãy ra, hoặc đã kết thúc!"
            };
            try
            {
                if (model != null)
                {

                    var entity = _mapper.Map<StoreRegister>(model);
                    entity.Badges = string.Join(",", model.Badges);
                    entity.Level = model.Badges.Count();
                    result.Success = true;
                    result.Message = "Đăng ký thành công!";
                    var cate = new Catalog();
                    string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                    string schema = _httpContextAccessor.HttpContext.Request.Scheme;
                    var rs = new StoreResponse()
                    {
                        FbShareUrl = $"{schema}://{host}/" + DrawFBShare(model.StoreName, GetFileNameFBShareByBadges(model.Badges.Count()), model.Badges.Count()),
                        CertificateUrl = $"{schema}://{host}/" + DrawCertificate(model.StoreName, GetFileNameByBadges(model.Badges.Count()), model.Badges.Count()),


                    };

                     rs.DownloadCertificate = $"{schema}://{host}/api/Events/Download?url=" + rs.CertificateUrl;
                     entity.BadgeUrl = rs.CertificateUrl;
                     entity.BadgeFBShareUrl = rs.FbShareUrl;
                   
                   // var a = DrawFBShare(model.StoreName, name, model.Badges.Count());
                    _storeRegisterRepository.Add(entity);
                    result.Data = rs;
                    rs.StoreId = entity.Id;
                   // rs.Token = _tokenService.GenerateAccessTokenForRegister(8, entity.StoreCode, entity.Id, "");
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(result);
            }
        }

        /*[HttpGet("GetData/{top}")]
        public IActionResult GetRandomData(int top)
        {
            var result = new ResponseModel<int>()
            {
                Success = false,
            };
            try
            {
                var rs = _storeRegisterRepository.GetAllData().OrderBy(r => Guid.NewGuid()).Take(top);
                return Ok(rs.Select(x=> new { x.StoreName, x.StoreCode, x.PhoneNumber}));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(result);
            }
        }

        [HttpPost("LuckyDraw/{prizeType}")]
        public IActionResult LuckyDraw(int prizeType)
        {
            var result = new ResponseModel<int>()
            {
                Success = false,
            };
            try
            {
                var existed = _luckyDrawResultRepository.GetAllData().Include(x => x.Store)
                    .Select(x => x.Store.PhoneNumber).Distinct().ToList();
                var luckyStores = _storeRegisterRepository.GetAllData().
                    Where(x => !existed.Contains(x.PhoneNumber)).OrderBy(r => Guid.NewGuid()).Take(prizeType).ToList();
                if (luckyStores.Any())
                {
                    var luckers = luckyStores.Select(x => new LuckyDrawResult()
                    {
                        StoreId = x.Id,
                        Prize = prizeType,
                        Status = 0,
                        CreateDate = DateTime.Now
                    });
                    _luckyDrawResultRepository.InsertEntities(luckers);
                }
                return Ok(luckyStores.Select(x => new { x.StoreName, x.StoreCode, PhoneNumber = x.PhoneNumber.Remove(x.PhoneNumber.Length - 3, 3)+"xxx" 
            }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(result);
            }
        }*/

        [HttpGet("GetStoreInfo")]
        public IActionResult GetStoreInfo()
        {
            var result = new ResponseModel<StoreInfoResponse>()
            {
                Success = false,
            };
            try
            {
                var storeIdStr = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (!string.IsNullOrEmpty(storeIdStr))
                {
                    int storeId = int.Parse(storeIdStr);
                    var data = _storeRegisterRepository.GetAllData().Where(x => x.Id == storeId).FirstOrDefault();
                    if (data != null)
                    {
                        var info = new StoreInfoResponse()
                        {
                            StoreCode = data.StoreCode,
                            StoreName = data.StoreName,
                            FbShareUrl = data.BadgeFBShareUrl
                        };
                        result.Success = true;
                        result.Data = info;
                    }

                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(result);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetStoreInfoById/{storeId}")]
        public IActionResult GetStoreInfoById(int storeId)
        {
            var result = new ResponseModel<StoreInfoResponse>()
            {
                Success = false,
            };
            try
            {
                var data = _storeRegisterRepository.GetAllData().Where(x => x.Id == storeId).FirstOrDefault();
                if (data != null)
                {
                    var info = new StoreInfoResponse()
                    {
                        StoreCode = data.StoreCode,
                        StoreName = data.StoreName,
                        FbShareUrl = data.BadgeUrl
                    };
                    result.Success = true;
                    result.Data = info;
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(result);
            }
        }

        [HttpPost("UploadFile")]
        public IActionResult UploadFile(IFormFile file)
        {

            try
            {
                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                string schema = _httpContextAccessor.HttpContext.Request.Scheme;
                var image = ImageUtils.UploadFileImage(file, FolderStored);
                var url = $"{schema}://{host}/{image}";
                return Ok(url);
            }
            catch
            {
                string message = $"file / upload failed!";
                return Ok(message);
            }
        }

        private string DrawFBShare(string name, string fileName, int numOfBadges)
        {
            using (Bitmap bitmap = new Bitmap(Image.FromFile(@$"{rootPath}\{fileName}")))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    //  string fontsfolder = @"wwwroot\Templetes\Fonts\";
                    var conv = new ColorConverter();
                    var color = (Color)conv.ConvertFromString(GetColor(numOfBadges));
                    Brush brush = new SolidBrush(color);
                    Font font = new Font("ProximaNovaCond-Medium", 40, FontStyle.Bold, GraphicsUnit.Pixel);
                    SizeF textSize = new SizeF();
                    textSize = graphics.MeasureString(name, font);
                    Point position = new Point(500, 87);
                    RectangleF rect1 = new RectangleF(430, 85, 780, 100);
                    //rect1.
                    // Create a StringFormat object with the each line of text, and the block
                    // of text centered on the page.
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                   
                    // Draw the text and the surrounding rectangle.
                    //e.Graphics.DrawString(text1, font1, Brushes.Blue, rect1, stringFormat);
                    //  Point position = new Point(bitmap.Width - ((int)textSize.Width + 10), bitmap.Height - ((int)textSize.Height + 10));
                    graphics.DrawString(name, font, brush, rect1, stringFormat);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    FolderStored);
                    bool folderExists = Directory.Exists(path);
                    if (!folderExists)
                        Directory.CreateDirectory(path);
                    var extension = Path.GetExtension(fileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + extension;
                    string filePath = Path.Combine(path, uniqueFileName);
                    bitmap.Save(filePath, ImageFormat.Png);
                    return $"{FolderStored}/{uniqueFileName}";
                }
            }
        }

        private string DrawCertificate(string name, string fileName, int numOfBadges)
        {
            using (Bitmap bitmap = new Bitmap(Image.FromFile(@$"{rootPath}\{fileName}")))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    //  string fontsfolder = @"wwwroot\Templetes\Fonts\";
                    var conv = new ColorConverter();
                    var color = (Color)conv.ConvertFromString(GetColor(numOfBadges));
                    Brush brush = new SolidBrush(color);
                    Font font = new Font("ProximaNovaCond-Medium", 40, FontStyle.Bold, GraphicsUnit.Pixel);
                    SizeF textSize = new SizeF();
                    textSize = graphics.MeasureString(name, font);
                    Point position = new Point(600, 280);
                    Rectangle rect1 = new Rectangle(600, 280, 520, 130);
                    // Create a StringFormat object with the each line of text, and the block
                    // of text centered on the page.
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    // Draw the text and the surrounding rectangle.
                    //e.Graphics.DrawString(text1, font1, Brushes.Blue, rect1, stringFormat);
                    //  Point position = new Point(bitmap.Width - ((int)textSize.Width + 10), bitmap.Height - ((int)textSize.Height + 10));
                    graphics.DrawString(name, font, brush, rect1, stringFormat);
                    //  Point position = new Point(bitmap.Width - ((int)textSize.Width + 10), bitmap.Height - ((int)textSize.Height + 10));
                   // graphics.DrawString(name, font, brush, position);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    FolderStored);
                    bool folderExists = Directory.Exists(path);
                    if (!folderExists)
                        Directory.CreateDirectory(path);
                    var extension = Path.GetExtension(fileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + extension;
                    string filePath = Path.Combine(path, uniqueFileName);
                    bitmap.Save(filePath, ImageFormat.Png);
                    return $"{FolderStored}/{uniqueFileName}";
                }
            }
        }
        private string GetFileNameFBShareByBadges(int numOfBadges)
        {
            if (numOfBadges < 3)
            {
                return "ShareFB1.png";
            }
            else if (numOfBadges >= 3 && numOfBadges <=4)
            {
                return "ShareFB2.png";

            }
            else
            {
                return "ShareFB3.png";
            }
        }
        private string GetColor(int numOfBadges)
        {
            if (numOfBadges > 4)
            {
                return "#1dffe7";
            }
            else
            {
                return "#fbfe22";
            }
        }

        private string GetFileNameByBadges(int numOfBadges)
        {
            if (numOfBadges < 3)
            {
                return "chungnhan1.png";
            }
            else if (numOfBadges >= 3 && numOfBadges <= 4)
            {
                return "chungnhan2.png";

            }
            else
            {
                return "chungnhan3.png";
            }
        }

        [AllowAnonymous]
        [HttpGet("Download")]
        public async Task<FileStreamResult> Download(string url)
        {
            var stream = await new HttpClient().GetStreamAsync(url);
            return File(stream, "image/png", "certificate.png");
        }
    }

}
