using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ripoffnigeria.DTO;
using ripoffnigeriaonline.Models;

namespace ripoffnigeriaonline.Photo
{
    public interface IPhotoManager
    {
        Task<IEnumerable<PhotoViewModel>> Get();
        Task<IEnumerable<PhotoViewModel>> Get(ReportImage report);
        Task<PhotoActionResult> Delete(string fileName);
        Task<IEnumerable<PhotoViewModel>> Add(HttpRequestMessage request, int reportId);
        bool FileExists(string fileName);
    }
}