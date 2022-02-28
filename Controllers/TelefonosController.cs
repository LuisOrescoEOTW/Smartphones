using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smartphones.Models;
using System.Collections.ObjectModel;

namespace Smartphones.Controllers
{
    [Route("api/1.0/[controller]")]
    [ApiController]
    public class TelefonosController : ControllerBase
    {
        private readonly SmartphoneContext _context;

        public TelefonosController(SmartphoneContext context)
        {
            _context = context;
        }

        // GET: api/Telefonos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefono()
        {
            return await _context.Telefono
                .Include(item => item.Sensores)
                .ToListAsync();
        }

        // GET: api/Telefonos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Telefono>> GetTelefono(int id)
        {
            var telefono = await _context.Telefono.FindAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }

            return telefono;            
        }

        // GET: api/telefonos/xsensor. Filtrar los telefonos por sensores o apps instaladas
        [HttpGet("xsensorapp")]
        public dynamic xsensorapp(int appid, int sensid)
        {
            if (appid != 0 && sensid != 0)
            {
                return BadRequest();
            }
            if (sensid != 0)
            {
                return _context.Sensor
                 .Where(item => 
                     item.SensorId == sensid)
                 .Select(item => new
                 {
                     item.Nombre,
                     Lista_de_Telefonos = item.Telefonos.Select(telephone => new
                     {
                         marca = telephone.Marca,
                         modelo = telephone.Modelo,
                         precio = telephone.Precio
                     }).ToList()
                 }).ToList();
            }
                return _context.App
                 .Where(item => 
                     item.AppId == appid)
                 .Select(item => new
                 {
                     item.Nombre,
                     Lista_de_Telefonos = item.Instalaciones.Select(instalation => new
                     {
                         instalation.Telefono.Marca,
                         instalation.Telefono.Modelo,
                         instalation.Telefono.Precio
                     }).ToList()
                 }).ToList();

        }
        
        
        // PUT: api/Telefonos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelefono(int id, Telefono telefono)
        {
            // La variable telefono tendrá la información que recibimos por PUT
            // La variable viv tendrá la info original del telefono con el id recibido

            var viv = await _context.Telefono.FindAsync(id);

            if (viv == null)
            {
                return BadRequest();
            }
            if (id != telefono.TelefonoId)
            {
                return BadRequest();
            }
                       
            
            // Borraremos los sensores del telefono para reemplazarlos con los recibidos

            if (viv.Sensores != null)
            {
                viv.Sensores.Clear();
            }            

            await _context.SaveChangesAsync();

            // Esto es importante porque tenemos que avisarle a EF
            // que aquí termina una transacción y comienza otra
            _context.ChangeTracker.Clear();


            // Agregamos a la info del telefono los nuevos sensores
            if (telefono.SensoresList != null)
            {
                foreach (var sensId in telefono.SensoresList)
                {
                    var sens = await _context.Sensor.FindAsync(sensId);
                    if (sens != null)
                    {
                        telefono.Sensores.Add(sens);
                    }
                }
            }

            // Avisamos que hemos modificado la vivienda para que EF tome los cambios al guardar
            _context.Entry(telefono).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            // Si llegamos aquí es porque todo salió bien
            // devolvemos OK (http 200) y los datos del telefono
            return Ok(telefono);

        }

        // POST: api/Telefonos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Telefono>> PostTelefono(Telefono telefono)
        {

            // A cada uno de los sensores recibidos lo agregamos al telefono
            foreach (var item in telefono.SensoresList)
            {
                Sensor p = await _context.Sensor.FindAsync(item);
                telefono.Sensores.Add(p);
            }

            // Agregamos el telefono con toda su info a la base de datos
            _context.Telefono.Add(telefono);
            await _context.SaveChangesAsync();

            // Devolvemos CREATED con el telefono generado
            return CreatedAtAction("GetTelefono", new { id = telefono.TelefonoId }, telefono);
        }

        // DELETE: api/Telefonos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelefono(int id)
        {
            var telefono = await _context.Telefono.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            _context.Telefono.Remove(telefono);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TelefonoExists(int id)
        {
            return _context.Telefono.Any(e => e.TelefonoId == id);
        }
    }
}
