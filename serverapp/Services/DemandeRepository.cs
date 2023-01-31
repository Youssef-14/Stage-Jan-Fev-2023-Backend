using AspWebApp.Data;
using Microsoft.EntityFrameworkCore;
using serverapp.Data;
using System.Collections.ObjectModel;

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

        internal async static Task<int> GetDemandesFilteredNumber(string type, string status)
        {
            using (var db = new AppDBContext())
            {
                if (type == status)
                {
                    return await db.Demandes.CountAsync();
                }
                if (type == "all")
                {
                    return await db.Demandes.Where(d => d.Status == status).CountAsync();
                }
                if (status == "all")
                {
                    return await db.Demandes.Where(t => t.type == type).CountAsync();
                }
                return await db.Demandes.Where(d => d.Status == status).Where(t => t.type == type).CountAsync();
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
        //Filter
        internal async static Task<IEnumerable<Demande>> GetFilteredDemandesAsync(string type,string status,int begin,int end)
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.Where(d => (status=="all" || d.Status == status) && (type == "all" || d.type == type)).Skip(begin).Take(end).ToListAsync();
            }
        }
        //method that returns accepted demands
        internal async static Task<IEnumerable<Demande>> GetAcceptedDemandesAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.Where(d => d.Status == StatusDemande.Accepte).ToListAsync();
            }
        }
        //method that returns refused demands
        internal async static Task<IEnumerable<Demande>> GetRefusedDemandesAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.Where(e => e.Status == StatusDemande.Refusé).ToListAsync();
            }
        }
        //method that returns demands in progress
        internal async static Task<IEnumerable<Demande>> GetPendingDemandesAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.Where(e => e.Status == StatusDemande.EnCours).ToListAsync();
            }
        }
        //method that returns demands to be corrected
        internal async static Task<IEnumerable<Demande>> GetToBeCorrectedDemandesAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Demandes.Where(e => e.Status == StatusDemande.Acorriger).ToListAsync();
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
                    demande.Status = StatusDemande.EnCours;
                    await db.Demandes.AddAsync(demande);
                    var result = await db.SaveChangesAsync() >= 1;
                    return result;
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
        internal async static Task<bool> SetDemandeToBeCorrectedAsync(Demande demande)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    demande.Status = "àcorriger";
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
