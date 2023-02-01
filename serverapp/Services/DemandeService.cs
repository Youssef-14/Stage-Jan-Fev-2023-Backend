using AspWebApp.Data;
using Microsoft.EntityFrameworkCore;
using serverapp.Data;
using System.Collections.ObjectModel;

namespace serverapp.Services
{
    internal class DemandeService
    {
        private readonly AppDBContext db;
        public DemandeService(AppDBContext db)
        {
            this.db = db;
        }
        //method that returns all the demandes
        internal async Task<IEnumerable<Demande>> GetDemandesAsync()
        {
            return await db.Demandes.ToListAsync();
        }

        internal async Task<int> GetDemandesFilteredNumber(string type, string status)
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
        //same as above but only for the demandes of a specific user
        internal async Task<IEnumerable<Demande>> GetDemandsByUserIdAsync(int userid)
        {

            return await db.Demandes.Where(d => d.UserId == userid).ToListAsync();

        }
        //Filter
        internal async Task<IEnumerable<Demande>> GetFilteredDemandesAsync(string type,string status,int begin,int end)
        {
            return await db.Demandes.Where(d => (status == "all" || d.Status == status) && (type == "all" || d.type == type)).Skip(begin).Take(end).ToListAsync();
        }
        //method that returns accepted demands
        internal async Task<IEnumerable<Demande>> GetAcceptedDemandesAsync()
        {
            return await db.Demandes.Where(d => d.Status == StatusDemande.Accepte).ToListAsync();
        }
        //method that returns refused demands
        internal async Task<IEnumerable<Demande>> GetRefusedDemandesAsync()
        {
            return await db.Demandes.Where(e => e.Status == StatusDemande.Refusé).ToListAsync();
        }
        //method that returns demands in progress
        internal async Task<IEnumerable<Demande>> GetPendingDemandesAsync()
        {
            return await db.Demandes.Where(e => e.Status == StatusDemande.EnCours).ToListAsync();
        }
        //method that returns demands to be corrected
        internal async Task<IEnumerable<Demande>> GetToBeCorrectedDemandesAsync()
        {
            return await db.Demandes.Where(e => e.Status == StatusDemande.Acorriger).ToListAsync();

        }
        //method that returns a demand by its id

        internal async Task<Demande> GetDemandeByIdAsync(int id)
        {
            return await db.Demandes.FirstOrDefaultAsync(d => d.Id == id);
        }

        internal async Task<bool> CreateDemandeAsync(Demande demande)
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
        internal async Task<bool> UpdateDemandeAsync(Demande demande)
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
        internal async Task<bool> SetDemandeToAcceptedAsync(Demande demande)
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
        internal async Task<bool> SetDemandeToRefusedAsync(Demande demande)
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

        internal async Task<bool> SetDemandeToPendingAsync(Demande demande)
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
        internal async Task<bool> SetDemandeToBeCorrectedAsync(Demande demande)
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

        internal async Task<bool> DeleteDemandeAsync(int id)
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
