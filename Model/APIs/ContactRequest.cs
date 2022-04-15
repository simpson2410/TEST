using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.APIs
{
    public class ContactRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string Slug { get; set; }

    }

    public class StoreRegisterRequest
    {
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public List<string> Badges { get; set; }

    }

    public class StoreResponse
    {
        public int StoreId { get; set; }
        public string Token { get; set; }
        public string CertificateUrl { get; set; }
        public string FbShareUrl { get; set; }
        public string DownloadCertificate { get; set; }
    }

    public class StoreInfoResponse
    {
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string FbShareUrl { get; set; }
    }

}
