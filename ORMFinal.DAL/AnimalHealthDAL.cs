namespace ORMFinal.DAL
{
    public class AnimalHealthDAL
    {
        private readonly ORMFinalContext _context;

        public AnimalHealthDAL(ORMFinalContext context)
        {
            _context = context;
        }
    }
}
