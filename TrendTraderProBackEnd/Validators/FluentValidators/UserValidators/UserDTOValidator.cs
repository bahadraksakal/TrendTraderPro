using Entities.Users;
using FluentValidation;

namespace Validators.FluentValidators.UserValidators
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleSet("Register", () =>
            {
                RuleFor(user => user.Name).NotNull().NotEmpty().WithMessage("Kullanıcı adı zorunlu alan.")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olmalıdır.")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("Kullanıcı adı sadece harf ve rakam içermelidir.");

                RuleFor(user => user.Password).NotNull().NotEmpty().WithMessage("Şifre zorunlu alan.");
                RuleFor(user => user.Password).MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.");
                RuleFor(user => user.Password).Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.");
                RuleFor(user => user.Password).Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.");
                RuleFor(user => user.Password).Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.");
                RuleFor(user => user.Password).Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");

                RuleFor(user => user.Email).EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
                RuleFor(user => user.Email).MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olmalıdır.");
                RuleFor(user => user.Email).NotNull().NotEmpty().WithMessage("E-posta zorunlu alan.");
            });

            RuleSet("Login", () =>
            {
                RuleFor(user => user.Name).NotNull().NotEmpty().WithMessage("Kullanıcı adı zorunlu alan.");

                RuleFor(user => user.Password).NotNull().NotEmpty().WithMessage("Şifre zorunlu alan.");
            });
        }
    }
}
