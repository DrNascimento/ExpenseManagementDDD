﻿using Domain.Commands.ExpenseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.ExpenseCommandValidations
{
    public class DeleteExpenseCommandValidator : ExpenseCommandValidations<DeleteExpenseCommand>
    {
        public DeleteExpenseCommandValidator() 
        {
            ValidateId();
        }
    }
}
