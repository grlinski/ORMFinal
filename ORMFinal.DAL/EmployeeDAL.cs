namespace ORMFinal.DAL
{
    public class EmployeeDAL
    {
        private readonly ORMFinalContext _context;

        public EmployeeDAL(ORMFinalContext context)
        {
            _context = context;
        }
    }
}
