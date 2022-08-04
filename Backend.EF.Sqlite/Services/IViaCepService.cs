using Backend.EF.Sqlite.DTOs;
using Backend.EF.Sqlite.DTOs.Address;
using Refit;

namespace Backend.EF.Sqlite.Services
{
    public interface IViaCepService
    {
        [Get("/{zipcode}/json/")]
        Task<AddressResponse> GetByZipcode(int zipcode);
    }
}
