using Microsoft.EntityFrameworkCore;

namespace serverapp.Helpers
{
    public class DemandVerification
    {
        internal static bool TypeValidation(string type)
        {
            if (type == null)
                return false;
            if (type.Length < 2)
                return false;

            return true;
        }
        internal static bool CheckIfDemandeExist(int userId, string type)
        {
            using var db = new AppDBContext();
            return db.Demandes.Any(d => d.UserId == userId && d.type == type);
        }

    }
}
