using FluentValidation;
using GlobalExceptionHandler.Core.Enums;
using System;

namespace GlobalExceptionHandler.Core.Models
{
    public class RecordModel
    {
        public EnumResponseMessageType ErrorType { get; set; }
    }

    public class RecordModelValidator : AbstractValidator<RecordModel>
    {
        public RecordModelValidator()
        {
            RuleFor(d => d.ErrorType)
                .NotEmpty().WithMessage("ErrorType cannot be empty");
        }
    }
    
}
