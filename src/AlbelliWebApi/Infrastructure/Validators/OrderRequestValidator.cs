using AlbelliWebApi.Models.Requests;
using FluentValidation;
using AlbelliWebApi.Infrastructure.Constant;
using AlbelliWebApi.Infrastructure.Enums;

namespace AlbelliWebApi.Infrastructure.Validators
{

    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {
            RuleFor(p => p.OrderId).Must(q=>!string.IsNullOrEmpty(q)).WithMessage(ErrorCodeConstants.OrderIdRequired);
            RuleFor(p => p.Items).Must(p => p!= null && p.Any()).WithMessage(ErrorCodeConstants.ItemsRequired);
            RuleFor(p => p.Items).Must(p => p.All(q=>q.Quantity>0)).When(r=>r.Items != null).WithMessage(ErrorCodeConstants.ItemsQuantityRequired);
            RuleFor(p => p.Items).Must(p => p.All(q => q.ProductType > 0)).When(r => r.Items != null).WithMessage(ErrorCodeConstants.ItemsProductTypeRequired);
            RuleFor(p => p.Items).Must(p => p.All(q => Enum.IsDefined(typeof(ProductType), q.ProductType))).When(r => r.Items != null).WithMessage(ErrorCodeConstants.ItemsProductTypeNotValid);
        }
    }
}
