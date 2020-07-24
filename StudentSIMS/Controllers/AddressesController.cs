using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSIMS;
using StudentSIMS.Data;

namespace StudentSIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly StudentSimsContext _context;

        public AddressesController(StudentSimsContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            return await _context.Address.ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            var updateAddress = await _context.Address.FirstOrDefaultAsync(s => s.id == address.id);
            _context.Entry(updateAddress).State = EntityState.Modified;

            updateAddress.streetNumber = address.streetNumber;
            updateAddress.street = address.street;
            updateAddress.suburb = address.suburb;
            updateAddress.city = address.city;
            updateAddress.country = address.country;
            updateAddress.postCode = address.postCode;
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPut("{studentID}/ChangeStudentAddress")]
        public async Task<IActionResult> PutAddressByStudentID(int studentID, int id, Address address)
        {
            if (id != address.id)
            {
                return BadRequest();
            }

            if (!AddressExists(id))
            {
                return BadRequest();
            }

            if(studentID != address.studentID)
            {
                return BadRequest();
            }

            if(!_context.Student.Any(e => e.studentID == studentID))
            {
                return BadRequest();
            }

            var updateAddress = await _context.Address.FirstOrDefaultAsync(s => (s.id == address.id)&&(s.studentID==address.studentID));
            _context.Entry(updateAddress).State = EntityState.Modified;

            updateAddress.streetNumber = address.streetNumber;
            updateAddress.street = address.street;
            updateAddress.suburb = address.suburb;
            updateAddress.city = address.city;
            updateAddress.country = address.country;
            updateAddress.postCode = address.postCode;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.id }, address);
        }

        [HttpPost("{studentID}")]
        public async Task<ActionResult<Address>> PostAddressToStudent(int studentID, Address address)
        {
            address.studentID = studentID;

            _context.Address.Add(address);


            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.id, studentID = address.studentID }, address);
        }


        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return address;
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.id == id);
        }
    }
}
