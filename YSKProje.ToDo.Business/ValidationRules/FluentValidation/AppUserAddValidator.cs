using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
    public class AppUserAddValidator : AbstractValidator<AppUserAddDto>
    {
        public AppUserAddValidator()
        {
            RuleFor(I => I.UserName).NotNull().WithMessage("Kullanıcı Adı boş geçilemez");
            RuleFor(I => I.Password).NotNull().WithMessage("Parola boş geçilemez");
            RuleFor(I => I.ConfirmPassword).NotNull().WithMessage("Parola onay alanı boş geçilemez");
            RuleFor(I => I.ConfirmPassword).Equal(I => I.Password).WithMessage("Parolalar eşleşmiyor");
            RuleFor(I => I.Email).NotNull().WithMessage("Email boş geçilemez").EmailAddress().WithMessage("Geçersiz Email");
            RuleFor(I => I.Name).NotNull().WithMessage("Ad boş geçilemez");
            RuleFor(I => I.Surname).NotNull().WithMessage("Soyad boş geçilemez");
        }
    }
}
