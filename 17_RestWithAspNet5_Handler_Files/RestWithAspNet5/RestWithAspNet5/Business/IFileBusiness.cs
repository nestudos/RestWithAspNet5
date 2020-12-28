using Microsoft.AspNetCore.Http;
using RestWithAspNet5.Data.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNet5.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string name);

        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);

        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
