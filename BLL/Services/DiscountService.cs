using Abstraction.Interfaces.DataSourse;
using Abstraction.Interfaces.Services;
using BLL.Factories;
using Common.Resources;
using Mapster;
using Microsoft.Extensions.Localization;
using Models.DTO;
using Models.Entities;

namespace BLL.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IGenericDataSourse<Discount> _DiscountDataSource;
        private readonly IGenericDataSourse<Guitar> _GuitarDataSource;
        private readonly DiscountFactory _discountFactory;
        private readonly IStringLocalizer<ErrorMessages> _localizer;

        public DiscountService(IGenericDataSourse<Discount> discountDataSource, IGenericDataSourse<Guitar> guitarDataSource, DiscountFactory discountFactory, IStringLocalizer<ErrorMessages> localizer)
        {
            _DiscountDataSource = discountDataSource;
            _GuitarDataSource = guitarDataSource;
            _discountFactory = discountFactory;
            _localizer = localizer;
        }

        public async Task<int> AddDiscount(DiscountDTO discountDTO)
        {
            var discount = _DiscountDataSource.GetElements().FirstOrDefault(d => d.GuitarId == discountDTO.GuitarId);
            if (discount is null)
            {
                discount = discountDTO.Adapt<Discount>();
                await _DiscountDataSource.AddAsync(discount);
            }
            else
            {
                throw new ArgumentException(_localizer["DiscountAlreadyExist"]);
            }

            await _DiscountDataSource.SaveChangesAsync();
            return discount.Id;
        }

        public List<DiscountDTO> GetAllDiscounts()
        {
            return _DiscountDataSource.GetElements()
                .Select(d => d.Adapt<DiscountDTO>())
                .ToList();
        }

        public decimal GetDiscountedPrice(int guitarId)
        {
            var guitar = _GuitarDataSource.GetElements().FirstOrDefault(g => g.Id == guitarId);
            if (guitar is null) throw new ArgumentException(_localizer["GuitarNotFound"]);

            var discount = _DiscountDataSource.GetElements().FirstOrDefault(d => d.GuitarId == guitarId);
            if (discount is null) return guitar.Price; // price by default if no discount

            var strategy = _discountFactory.CreateDiscountStrategy(discount.Type.ToString().ToLower(), discount.Value); // берём стратегию
            return strategy.Apply(guitar.Price); // и применяем её к цене гитары
        }
    }
}
