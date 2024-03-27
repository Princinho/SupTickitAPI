using Microsoft.EntityFrameworkCore;
using Suptickit.Application;
using Suptickit.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Infrastructure
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly SuptickitContext _db;
        public QuoteRepository(SuptickitContext db)
        {
            _db = db;
        }
        public async Task<ServiceResponse<Quote>> AddAsync(Quote quote)
        {
            try
            {
                quote.DateCreated = DateTime.UtcNow;
                //Making sure no price has been modified for parts that don't allow for it
                quote.QuoteDetails = NormalizeQuoteDetailPrices(quote.QuoteDetails);
                quote.Total = quote.QuoteDetails.Sum(qd => qd.Quantity * qd.PricePerUnit);
                quote.Description ??= "";
                quote.Title ??= "";
                quote.SubTitle ??= "";
                //Calculalting all taxes and advantages
                double taxOrBonusesTotal = 0;
                foreach (var taxOrBonus in await _db.TaxOrBonuses.Where(t => t.IsEnabled && !t.IsArchived).ToListAsync())
                {
                    var appliedValue = Utils.CalculateTaxOrBonus(taxOrBonus, quote.QuoteDetails);
                    quote.TaxOrBonusesApplied.Append(new TaxOrBonusApplied { Amount = appliedValue, QuoteId = quote.Id, TaxOrBonusId = taxOrBonus.Id });
                    taxOrBonusesTotal += appliedValue;
                }
                
                quote.TotalWithTaxOrBonuses = quote.Total + taxOrBonusesTotal;
                _db.Quotes.Add(quote);
                await _db.SaveChangesAsync();
                return new ServiceResponse<Quote> { Data = quote, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Quote> { Success = false, Message = "Failed to create Quote, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<IEnumerable<Quote>>> GetAllAsync()
        {
            try
            {
                var taxOrBonuss = await _db.Quotes.Include(q => q.QuoteDetails).ToListAsync();
                return new ServiceResponse<IEnumerable<Quote>> { Data = taxOrBonuss, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<Quote>> { Success = true, Message = "Failed to retrieve TaxOrBonuss, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<Quote>> GetByIdAsync(int id)
        {
            try
            {
                var quote = await _db.Quotes.FindAsync(id);
                if (quote == null) return new ServiceResponse<Quote> { Success = false, Message = "No matching quote for id " + id };
                return new ServiceResponse<Quote> { Data = quote, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Quote> { Success = false, Message = "Failed to retrieve quote, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<Quote>> RemoveAsync(int id)
        {
            try
            {
                var quote = await _db.Quotes.FirstOrDefaultAsync(t => t.Id == id);

                _db.Quotes.Remove(quote);

                await _db.SaveChangesAsync();
                return new ServiceResponse<Quote> { Data = quote, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Quote> { Success = false, Message = "Failed to retrieve quote, " + ex.Message };
            }
        }
        IEnumerable<QuoteDetail> NormalizeQuoteDetailPrices(IEnumerable<QuoteDetail> details)
        {

            //Making sure no price has been modified for parts that don't allow for it
            foreach (var quoteDetail in details)
            {
                var part = _db.Parts.AsNoTracking().First(q=>q.Id==quoteDetail.PartId);
                if (part != null && !part.IsLineEditAllowed)
                {
                    quoteDetail.PricePerUnit = part.Price;
                }
            }
            return details;
        }
        public async Task<ServiceResponse<Quote>> UpdateAsync(Quote quote)
        {
            try
            {
                //Making sure no price has been modified for parts that don't allow for it
                quote.QuoteDetails = NormalizeQuoteDetailPrices(quote.QuoteDetails);
                var oldQuote = await _db.Quotes.Include(q => q.QuoteDetails).FirstOrDefaultAsync(t => t.Id == quote.Id);
                var details = _db.QuoteDetail.Where(qd => qd.QuoteId == quote.Id).ToList();
                await _db.SaveChangesAsync();
                foreach (var detail in details)
                {
                    _db.QuoteDetail.Remove(detail);
                }
                oldQuote.CustomerId = quote.CustomerId;
                oldQuote.VehicleId = quote.VehicleId;
                oldQuote.Date = quote.Date;
                oldQuote.QuoteDetails = quote.QuoteDetails;
                oldQuote.Total = oldQuote.QuoteDetails.Sum(qd => qd.Quantity * qd.PricePerUnit);
                //Calculalting all taxes and advantages
                double taxOrBonusesTotal = 0;
                foreach (var taxOrBonus in await _db.TaxOrBonuses.Where(t => t.IsEnabled && !t.IsArchived).ToListAsync())
                {
                    var appliedValue = Utils.CalculateTaxOrBonus(taxOrBonus, oldQuote.QuoteDetails);
                    _ = quote.TaxOrBonusesApplied.Append(new TaxOrBonusApplied { Amount = appliedValue, QuoteId = quote.Id, TaxOrBonusId = taxOrBonus.Id });
                    taxOrBonusesTotal += appliedValue;
                }
                oldQuote.TotalWithTaxOrBonuses = oldQuote.Total + taxOrBonusesTotal;
                await _db.SaveChangesAsync();
                return new ServiceResponse<Quote> { Data = quote, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Quote> { Success = false, Message = "Failed to update quote, " + ex.Message };
            }
        }
    }
}
