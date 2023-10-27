using MediatR;

namespace Domain.Commands.ExpanseTypeCommands
{
    public class CreateExpanseTypeCommand : ExpanseTypeCommand, IRequest<int>
    {
        public CreateExpanseTypeCommand(string name, bool isFixed)
        {
            Name = name;
            IsFixed = isFixed;
        }

        public string Name { get; set; }

        public bool IsFixed { get; set; }
    }
}