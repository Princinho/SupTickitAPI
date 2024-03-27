using Microsoft.EntityFrameworkCore;
using Suptickit.Domain.Models;
using Suptickit.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public class VehicleRepository:IVehicleRepository
    {
        private readonly SuptickitContext _db;
        public VehicleRepository(SuptickitContext db)
        {
            _db = db;
        }
        public async Task<ServiceResponse<Vehicle>> AddAsync(Vehicle vehicle)
        {
            try
            {
                if (vehicle.Year < 1900) throw new Exception("Invalid vehicle model year");
                _db.Vehicles.Add(vehicle);
                await _db.SaveChangesAsync();
                return new ServiceResponse<Vehicle> { Data = vehicle, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Vehicle> { Success = false, Message = "Failed to create Vehicle, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<IEnumerable<Vehicle>>> GetAllAsync()
        {
            try
            {
                var vehicles = await _db.Vehicles.ToListAsync();
                return new ServiceResponse<IEnumerable<Vehicle>> { Data = vehicles, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<Vehicle>> { Success = true, Message = "Failed to retrieve Vehicles, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<Vehicle>> GetByIdAsync(int id)
        {
            try
            {
                var vehicle = await _db.Vehicles.FindAsync(id);
                if (vehicle == null) return new ServiceResponse<Vehicle> { Success = false, Message = "No matching Vehicle for id " + id };
                return new ServiceResponse<Vehicle> { Data = vehicle, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Vehicle> { Success = false, Message = "Failed to retrieve Vehicle, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<Vehicle>> RemoveAsync(int id)
        {
            try
            {
                var vehicle = await _db.Vehicles.FindAsync(id);
                _db.Vehicles.Remove(vehicle);
                await _db.SaveChangesAsync();
                return new ServiceResponse<Vehicle> { Data = vehicle, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Vehicle> { Success = false, Message = "Failed to retrieve Vehicle, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<Vehicle>> UpdateAsync(Vehicle vehicle)
        {
            try
            {
                var oldVehicle = await _db.Vehicles.FindAsync(vehicle.Id);
                oldVehicle.PlateNumber=vehicle.PlateNumber;
                oldVehicle.CustomerId=vehicle.CustomerId;
                oldVehicle.Color=vehicle.Color;
                oldVehicle.VIN=vehicle.VIN;
                oldVehicle.Make=vehicle.Make;
                oldVehicle.Year = vehicle.Year;
                await _db.SaveChangesAsync();
                return new ServiceResponse<Vehicle> { Data = oldVehicle, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Vehicle> { Success = false, Message = "Failed to update Vehicle, " + ex.Message };
            }
        }
    }
}
