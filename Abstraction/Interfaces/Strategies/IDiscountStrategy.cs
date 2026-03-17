using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces.Strategies
{
    // стратегия для расчёта скидки. в зависимости от типа скидки будет использоваться разная формула для её расчёта
    public interface IDiscountStrategy
    {
        decimal Apply(decimal price);
    }
}
