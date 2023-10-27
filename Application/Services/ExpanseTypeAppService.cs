using Application.Interfaces;
using Application.ViewModel.ExpanseType;
using AutoMapper;
using Domain.Commands.ExpanseTypeCommands;
using Domain.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExpanseTypeAppService : IExpanseTypeAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IExpanseTypeRepository _expanseTypeRepository;

        public ExpanseTypeAppService(IMapper mapper,
            IMediator mediator,
            IExpanseTypeRepository expanseTypeRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _expanseTypeRepository = expanseTypeRepository;
        }

        public async Task<ExpanseTypeViewModel> GetById(int id)
        {
            var expanse = await _expanseTypeRepository.GetById(id);
            return _mapper.Map<ExpanseTypeViewModel>(expanse);
        }

        public IEnumerable<ExpanseTypeViewModel> GetAll()
        {
            var expanses = _expanseTypeRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .AsEnumerable();

            return _mapper.Map<IEnumerable<ExpanseTypeViewModel>>(expanses);
        }

        public async Task<int> Create(ExpanseTypeViewModel expanseTypeViewModel)
        {
            var command = _mapper.Map<CreateExpanseTypeCommand>(expanseTypeViewModel);

            return await _mediator.Send(command);
        }
    }
}
