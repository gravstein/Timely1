using Abstraction.Interfaces.Strategies;

namespace BLL.Strategies
{
    public class FixedDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _fixedAmount;
        public FixedDiscountStrategy(decimal fixedAmount)
        {
            _fixedAmount = fixedAmount;
        }
        public decimal Apply(decimal price)
        {
            return price - _fixedAmount;
        }
    }
}
