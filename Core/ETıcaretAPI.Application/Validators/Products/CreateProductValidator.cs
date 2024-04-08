using ETıcaretAPI.Application.Features.Products.Commands.Create;

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen ürün adını boş geçmeyiniz");
            RuleFor(p => p.Name).MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Lütfen ürün adını 5 ile 150 karakter arasında giriniz");

           
            RuleFor(p => p.Stock)
                .NotNull()
                .WithMessage("Lütfen stok bilgisini boş geçmeyin");
            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("stok bilgisi minimum 0 olabilir");

            RuleFor(p => p.Price)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Fiyat bilgisi 0 dan büyük olmalıdır");
        }
    }
}
