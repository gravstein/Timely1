using Abstraction.Interfaces.Strategies;

namespace BLL.Strategies
{
    // стратегия для расчёта скидки в процентах
    public class PercentDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _percent;
        public PercentDiscountStrategy(decimal percent)
        {
            _percent = percent;
        }
        public decimal Apply(decimal price)
        {
            return price - (price * _percent / 100);
        }
    }
}
