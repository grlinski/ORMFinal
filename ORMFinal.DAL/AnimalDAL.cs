namespace ORMFinal.DAL
{
    public class AnimalDAL
    {
        private readonly ORMFinalContext _context;

        public AnimalDAL(ORMFinalContext context)
        {
            _context = context;
        }
    }
}
