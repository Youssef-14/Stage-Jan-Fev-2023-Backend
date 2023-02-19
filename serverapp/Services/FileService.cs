using Microsoft.EntityFrameworkCore;

namespace serverapp
{
    public static class FileService
    {
        internal async static Task<IEnumerable<File>> GetFilesAsync()
        {
            using var db = new AppDBContext();
            return (IEnumerable<File>)await db.Files.ToListAsync();
        }
        internal async static Task<IEnumerable<File>> GetFilesByDemandIdAsync(int demandId)
        {
            using var db = new AppDBContext();
            return await db.Files.Where(d => d.DemandeId == demandId).ToListAsync();
        }
        internal async static Task<File> GetFileByIdAsync(int id)
        {
            using var db = new AppDBContext();
            return await db.Files.FirstOrDefaultAsync(f => f.DemandeId == id);
        }
        internal async static Task<bool> CreateFileAsync(File file)
        {
            using var db = new AppDBContext();
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
        internal async static Task<bool> UpdateFileAsync(File file)
        {
            using var db = new AppDBContext();
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
        internal async static Task<bool> DeleteFileAsync(int id)
        {
            using var db = new AppDBContext();
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
        /*internal async static Task<IEnumerable<Data.File>> DeleteFilesByDemandIdAsync(int DemandId)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    var filesToRemove = db.Files.Where(f => f.DemandeId == DemandId);
                    db.Files.RemoveRange(filesToRemove);
                    await db.SaveChanges();
                }
                catch
                {
                    return null;
                }
            }
        }*/
    }
}
