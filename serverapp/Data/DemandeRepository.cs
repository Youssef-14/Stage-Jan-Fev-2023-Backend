using AspWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace serverapp.Data
{
    internal class DemandeRepository
    {
        internal async static Task<IEnumerable<Demande>> GetDemandesAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.ToListAsync();
            }
        }
        internal async static Task<Demande> GetDemandeByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.FirstOrDefaultAsync(d => d.Id == id);
            }
        }
        internal async static Task<bool> CreateDemandeAsync(Demande demande)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Demandes.AddAsync(demande);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }
        internal async static Task<bool> UpdateDemandeAsync(Demande demande)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Demandes.Update(demande);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }
        internal async static Task<bool> DeleteDemandeAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    var demande = await db.Demandes.FirstOrDefaultAsync(d => d.Id == id);
                    db.Demandes.Remove(demande);
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
