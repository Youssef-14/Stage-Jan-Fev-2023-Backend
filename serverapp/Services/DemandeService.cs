using Microsoft.EntityFrameworkCore;
using serverapp.Helpers;

namespace serverapp
{
    public  class DemandeService
    {
        private static readonly AppDBContext db = new AppDBContext();
        //method that returns all the demandes
        internal static async Task<IEnumerable<Demande>> GetDemandesAsync()
        {
            return await db.Demandes.ToListAsync();
        }

        internal static async Task<int> GetDemandesFilteredNumber(string type, string status)
        {
            return await db.Demandes.Where(d => (status == "all" || d.Status == status) && (type == "all" || d.type == type)).CountAsync();
        }
        //same as above but only for the demandes of a specific user
        public static async Task<IEnumerable<Demande>> GetDemandsByUserIdAsync(int userid)
        {

            return await db.Demandes.Where(d => d.UserId == userid).ToListAsync();

        }
        //Filter
        internal static async Task<IEnumerable<Demande>> GetFilteredDemandesAsync(string type,string status,int begin,int end, string tri)
        {
            if(tri == "récente")
                return await db.Demandes.Where(d => (status == "all" || d.Status == status) && (type == "all" || d.type == type)).OrderByDescending(d => d.Date).Skip(begin).Take(end).ToListAsync();
            else
                return await db.Demandes.Where(d => (status == "all" || d.Status == status) && (type == "all" || d.type == type)).OrderBy(d => d.Date).Skip(begin).Take(end).ToListAsync();
        }
        //method that returns accepted demands
        internal static async Task<IEnumerable<Demande>> GetAcceptedDemandesAsync()
        {
            return await db.Demandes.Where(d => d.Status == StatusDemande.Accepte).ToListAsync();
        }
        //method that returns refused demands
        internal static async Task<IEnumerable<Demande>> GetRefusedDemandesAsync()
        {
            return await db.Demandes.Where(e => e.Status == StatusDemande.Refusé).ToListAsync();
        }
        //method that returns demands in progress
        internal static async Task<IEnumerable<Demande>> GetPendingDemandesAsync()
        {
            return await db.Demandes.Where(e => e.Status == StatusDemande.EnCours).ToListAsync();
        }
        //method that returns demands to be corrected
        internal static async Task<IEnumerable<Demande>> GetToBeCorrectedDemandesAsync()
        {
            return await db.Demandes.Where(e => e.Status == StatusDemande.Acorriger).ToListAsync();

        }
        //method that returns a demand by its id
        
        public static async Task<Demande> GetDemandeByIdAsync(int id)
        {
            return await db.Demandes.FirstOrDefaultAsync(d => d.Id == id);
        }

        internal static async Task<int> CreateDemandeAsync(Demande demande)
        {
            try
            {
                if (DemandVerification.TypeValidation(demande.type) == false)
                    return -1;
                if (DemandVerification.CheckIfDemandeExist(demande.UserId, demande.type) == true)
                    return -2;
                demande.Status = StatusDemande.EnCours;
                await db.Demandes.AddAsync(demande);
                await db.SaveChangesAsync();
                return demande.Id;
            }
            catch
            {
                return 0;
            }
        }

        internal static async Task<bool> UpdateDemandeAsync(Demande demande)
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
        internal static async Task<bool> SetDemandeToAcceptedAsync(int id,int AdminId)
        {
            try
            {
                var requests = db.Demandes.Where(r => r.Id == id);

                foreach (var request in requests)
                {
                    request.Status = "accepté";
                    request.AdminId = AdminId;

                }
                return await db.SaveChangesAsync() >= 1;
            }
            catch
            {
                return false;
            }
        }
        internal static async Task<bool> SetDemandeToRefusedAsync(int id, int AdminId)
        {
            try
            {
                var requests = db.Demandes.Where(r => r.Id == id);
                foreach (var request in requests)
                {
                    request.Status = "refusé";
                    request.AdminId = AdminId;
                }
                return await db.SaveChangesAsync() >= 1;
            }
            catch
            {
                return false;
            }
        }
        internal static async Task<bool> SetDemandeToPendingAsync(int id)
        {
            try
            {
                var requests = db.Demandes.Where(r => r.Id == id);
                foreach (var request in requests)
                {
                    request.Status = "encours";
                }
                return await db.SaveChangesAsync() >= 1;
            }
            catch
            {
                return false;
            }
        }
        internal static async Task<bool> SetDemandeToBeCorrectedAsync(int id, int AdminId)
        {
            try
            {
                var requests = db.Demandes.Where(r => r.Id == id);
                foreach (var request in requests)
                {
                    request.Status = "àcorriger";
                    request.AdminId = AdminId;
                }
                return await db.SaveChangesAsync() >= 1;
            }
            catch
            {
                return false;
            }
        }
        internal static async Task<bool> DeleteDemandeAsync(int id)
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
