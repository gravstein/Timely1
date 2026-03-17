using Abstraction.Interfaces.Strategies;
using BLL.Strategies;

namespace BLL.Factories
{
    // фабрика для создания стратегий скидок. в зависимости от типа скидки будет возвращаться соответствующая стратегия для её расчёта
    public class DiscountFactory
    {
        public IDiscountStrategy CreateDiscountStrategy(string discountType, decimal value)
        {
            return discountType switch
            {
                "percent" => new PercentDiscountStrategy(value),
                "fixed" => new FixedDiscountStrategy(value),
                _ => throw new ArgumentException("Invalid discount type")
            };
        }
    }
}
