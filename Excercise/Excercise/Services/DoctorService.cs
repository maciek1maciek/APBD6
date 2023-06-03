
using Excercise.Data;
using Excercise.Models;
using Excercise.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Excercise.Services
{   

    public interface IDoctorService
    {
        Task<IEnumerable<DoctorPOST>> GetDoctor(int idDoctor);

        Task<IEnumerable<DoctorPOST>> AddDoctor(DoctorPOST doctorPOST);

        Task<bool> DoesDoctorExist(int Id);

    }
    public class DoctorService : IDoctorService
    {
        private readonly HospitalContext _context;

        public DoctorService(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorPOST>> AddDoctor(DoctorPOST doctorPOST)
        {
            var newDoctor = new Doctor
            { 
                 FirstName = doctorPOST.FirstName,
                 LastName = doctorPOST.LastName,
                 Email = doctorPOST.Email,
            };

            _context.Doctor.Add(newDoctor);
            await _context.SaveChangesAsync();

            return new List<DoctorPOST> { doctorPOST };
        }

        public async Task<bool> DoesDoctorExist(int IdDoctor)
        {
            var doctorCount = await _context.Doctor
                           .Where(d => d.IdDoctor == IdDoctor)
                           .CountAsync();

            if (doctorCount == 0)
                return false;
            return true;
        }

        public async Task<IEnumerable<DoctorPOST>> GetDoctor(int idDoctor)
        {
            var doctors = await _context.Doctor
                        .Where(d => d.IdDoctor == idDoctor)
                        .ToListAsync();

            return doctors.Select(e => new DoctorPOST
            {   
                IdDoctor = e.IdDoctor,
                FirstName = e.FirstName,    
                LastName = e.LastName,  
                Email = e.Email
            });
        }
    }
}
/*var trips = await _context.Trips
           .Include(e => e.IdCountries) //to sa klucze obce 
           .Include(e => e.IdClientTrips)
           .ThenInclude(equals => IdClientNvigation)
           .ToListAsync();

return trips.Select(e => new TripWithAdditionalData)
    {
    Description = equals.Description, //ostatnie pole tripa
    Countries = equals.IdCountries.Select(c => new CountryName { Name = c.Name }) //tutaj dodaje klase country 
        //tutaj bym dodwal kolejne 
});*/
             
