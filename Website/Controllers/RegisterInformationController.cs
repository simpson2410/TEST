using AutoMapper;
using Business.IRepostitory;
using Common;
using Entities.Entities;
using Entities.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Website.Helpers;
using Website.Models;

namespace Website.Controllers
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class RegisterInformationController : BaseController
    {
        private readonly IStoreRegisterRepository _storeRegisterRepository;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<RegisterInformationController> _logger;
        private readonly IMapper _mapper;
        private readonly int _pageSize = 20;

        public RegisterInformationController(ILogger<RegisterInformationController> logger,
            IWebHostEnvironment hostingEnvironment,
           IMapper mapper, IStoreRegisterRepository storeRegisterRepository)
        {
            _storeRegisterRepository = storeRegisterRepository;
            _logger = logger;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index(int? page)
        {
            var filter = HttpContext.Request.Query["search"];
            var status = HttpContext.Request.Query["SelectedStatus"];
            var type = HttpContext.Request.Query["SelectedContactType"];
            var fromdate = HttpContext.Request.Query["fromdate"];
            var todate = HttpContext.Request.Query["todate"];

            DateTime dateTimeFromdate = DateTime.Now.Date;
            DateTime dateTimeTodate = DateTime.Now.Date;

            if (string.IsNullOrEmpty(fromdate))
            {
                fromdate = DateTime.Now.AddDays(-_rangeDayDefault).ToString(_formatDateTime);

            }
            dateTimeFromdate = DateTime.ParseExact(fromdate, _formatDateTime, CultureInfo.InvariantCulture);

            if (string.IsNullOrEmpty(todate))
            {
                todate = DateTime.Now.AddMonths(1).ToString(_formatDateTime);

            }

            int statusContact = 0;
            if (!string.IsNullOrEmpty(status))
            {
                statusContact = status.ToString().ToInt32();
            }
            dateTimeTodate = DateTime.ParseExact(todate, _formatDateTime, CultureInfo.InvariantCulture);

            var rs = _storeRegisterRepository.GetAllData()
                .Where(x => (string.IsNullOrEmpty(filter) ||
                x.FullName.Contains(filter.ToString()))
                && (statusContact == 0 || x.Status == statusContact)
                && x.CreateDate.Date >= dateTimeFromdate
                && x.CreateDate.Date <= dateTimeTodate).AsQueryable().OrderBy(x => x.CreateDate).AsNoTracking();
            var data = await PaginatedList<StoreRegister>.CreateAsync(rs, page ?? 1, _pageSize);
            var vm = new ContactViewModel<StoreRegister>
            {
                FromDate = fromdate,
                ToDate = todate,
                SelectedStatus = status.ToString(),
                SelectedContactType = type.ToString(),
                ListItems = StatusList.ListItemStatusContact.ToList(),
                ContactTypes = StatusList.ListRegister.ToList()
            };
            vm.Data = data;
            return View(vm);
        }


        public IActionResult ShowDeleteConfirm(int id)
        {
            return PartialView("_Delete", id);
        }

        [HttpDelete("registerinformation/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var rs = new ResponseModel<int>();
            var contact = _storeRegisterRepository.GetAllData().FirstOrDefault(x => x.Id == id);
            if (contact != null && contact.Status == ContactStatus.NotYet.GetHashCode())
            {
                _storeRegisterRepository.Delete(contact);
                rs.Success = true;
                rs.Message = $"Xóa thông tin {contact.FullName} Thành công!";
            }
            else
            {
                rs.Message = "Xóa thất bại, vui lòng kiểm tra lại thông tin";
            }
            return Json(rs);
        }

        public async Task<IActionResult> Export(string lockout)
        {

            var query = _storeRegisterRepository.GetAllData();

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = $@"DanhSach_NguoiDung.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Dangky");
                IRow row = excelSheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("STT");
                row.CreateCell(1).SetCellValue("Tên gian hàng");
                row.CreateCell(2).SetCellValue("Mã gian hàng");
                row.CreateCell(3).SetCellValue("Chứng nhận");
                row.CreateCell(4).SetCellValue("Huy hiệu đã nhập");
             
                row.CreateCell(5).SetCellValue("Thời gian đăng ký");
                var data = await query.ToListAsync();
                int i = 1;
                foreach (var item in data)
                {
          
                    row = excelSheet.CreateRow(i);
                    row.CreateCell(0).SetCellValue(i);
                    row.CreateCell(1).SetCellValue(item.StoreName);
                    row.CreateCell(2).SetCellValue(item.StoreCode);
                    row.CreateCell(3).SetCellValue(GetFileNameByBadges(item.Level));
                    row.CreateCell(4).SetCellValue(item.Badges);
             
                    row.CreateCell(5).SetCellValue(item.CreateDate.ToString("dd-MM-yyyy HH:mm:ss.fff"));

                    i++;
                }
                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }
        private string GetFileNameByBadges(int numOfBadges)
        {
            if (numOfBadges < 3)
            {
                return "Shop xịn Sương Sương";
            }
            else if (numOfBadges >= 3 && numOfBadges <= 4)
            {
                return "Shop xịn Bá đạo";

            }
            else
            {
                return "Shop xịn Quyền lực";
            }
        }
    }

}
