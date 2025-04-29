using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.interfaces
{
    public interface IStockRepository
    {
        Task<List<StockModel>> GetAllAsync(QueryObject query);
        Task<StockModel?> GetByIdAsync(int id);
        Task<StockModel> CreateAsync(StockModel stockModel);
        Task<StockModel?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<StockModel?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}