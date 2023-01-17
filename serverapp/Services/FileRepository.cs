using AspWebApp.Data;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using serverapp.Data;

namespace serverapp.Services
{
    [ApiController]
    public static class FileRepository
    {
        internal async static Task<IEnumerable<Data.File>> GetFilesAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Files.ToListAsync();
            }
        }
        internal async static Task<Data.File> GetFileByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                return await db.Files.FirstOrDefaultAsync(f => f.DemandeId == id);
            }
        }

        [HttpPost]
        [Route("files")]
        [ProducesResponseType(typeof(Data.File), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        internal async static Task<bool> CreateFileAsync(Data.File file)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Files.AddAsync(file);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }
        internal async static Task<bool> UpdateFileAsync(Data.File file)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Files.Update(file);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }
        internal async static Task<bool> DeleteFileAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    var file = await db.Files.FirstOrDefaultAsync(f => f.Id == id);
                    db.Files.Remove(file);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
