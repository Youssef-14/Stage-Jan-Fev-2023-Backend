using AspWebApp.Data;
using Microsoft.EntityFrameworkCore;
using serverapp.Data;

namespace serverapp.Services
{
    internal static class DemandeRepository
    {
        //method that returns all the demandes
        internal async static Task<IEnumerable<Demande>> GetDemandesAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.ToListAsync();
            }
        }
        //same as above but only for the demandes of a specific user
        internal async static Task<IEnumerable<Demande>> GetDemandsByUserIdAsync(int userid)
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.Where(d => d.UserId == userid).ToListAsync();
            }
        }
        //method that returns accepted demands
        internal async static Task<IEnumerable<Demande>> GetAcceptedDemandesAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.Where(d => d.Type == TypeDemande.Accepte).ToListAsync();
            }
        }
        //method that returns refused demands
        internal async static Task<IEnumerable<Demande>> GetRefusedDemandesAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.Where(e => e.Type == TypeDemande.Refusé).ToListAsync();
            }
        }
        //method that returns demands in progress
        internal async static Task<IEnumerable<Demande>> GetPendingDemandesAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.Where(e => e.Type == TypeDemande.EnCours).ToListAsync();
            }
        }
        //method that returns demands to be corrected
        internal async static Task<IEnumerable<Demande>> GetToBeCorrectedDemandesAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.Where(e => e.Type == TypeDemande.Acorriger).ToListAsync();
            }
        }
        //method that returns a demand by its id

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
        internal async static Task<bool> SetDemandeToAcceptedAsync(Demande demande)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    demande.Status = "accepté";
                    db.Demandes.Update(demande);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }
        internal async static Task<bool> SetDemandeToRefusedAsync(Demande demande)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    demande.Status = "refusé";
                    db.Demandes.Update(demande);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> SetDemandeToPendingAsync(Demande demande)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    demande.Status = "encours";
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
