using MovieClub.Contracts.Interfaces;
using MovieClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Rents
{
    public class RentBuilder
    {
        private readonly Rent _rent;
        public RentBuilder(int FilmId, int UserId)
        {
            _rent = new Rent
            {
                UserId = UserId,
                FilmId = FilmId,
                RentAt = new DateTime(2024, 03, 1),
                FilmRate = 0,
                GiveBackAt = null,
                Cost = 0

            };
        }
        public RentBuilder WithFilmDailyPrice(decimal dailyPrice)
        {
            _rent.FilmDailyPrice = dailyPrice;
            return this;
        }
        public RentBuilder WithFilmPenaltyPrice(decimal penaltyPrice)
        {
            _rent.FilmPenaltyPrice = penaltyPrice;
            return this;
        }
        public RentBuilder WithRentAt(DateTime date)
        {
            _rent.RentAt = date;
            return this;
        }
        public Rent Build()
        {
            return _rent;
        }
    }
}
