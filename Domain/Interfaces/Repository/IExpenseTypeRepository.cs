using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IExpenseTypeRepository : IRepository<ExpenseType>
    {
        bool IsNameAvailable(string name, int exceptId = 0);
    }
}
