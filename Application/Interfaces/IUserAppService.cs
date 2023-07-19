using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserAppService
    {

        Task<UserViewModel> GetById(int id);

        Task<IList<UserViewModel>> GetAll();
    }
}
