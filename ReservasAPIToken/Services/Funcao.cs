namespace ReservasAPIToken.Services
{
    public class Funcao
    {
       public static bool CampoEhNulo(object campo)
        {
            if (campo == null)
            {
                return true;
            }

            return false;
        }
    }
}
