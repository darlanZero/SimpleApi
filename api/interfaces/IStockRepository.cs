using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.interfaces
{
    public interface IStockRepository
    {
        Task<List<StockModel>> GetAllAsync();
        Task<StockModel?> GetByIdAsync(int id);
        Task<StockModel> CreateAsync(StockModel stockModel);
        Task<StockModel?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<StockModel?> DeleteAsync(int id);
    }
}