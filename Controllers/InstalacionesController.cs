using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smartphones.Models;

namespace Smartphones.Controllers
{
    [Route("api/1.0/[controller]")]
    [ApiController]
    public class InstalacionesController : ControllerBase
    {
        private readonly SmartphoneContext _context;

        public InstalacionesController(SmartphoneContext context)
        {
            _context = context;
        }

        // GET: api/Instalaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instalacion>>> GetInstalacion()
        {
            return await _context.Instalacion
                .Include(item => item.App)
                .Include(item => item.Operario)
                .Include(item => item.Telefono)
                .ToListAsync();
        }

        // GET: api/Instalaciones/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Instalacion>> GetInstalacion(int id)
        public async Task<ActionResult<Instalacion>> GetInstalacion(int id)
        {
            var instalacion = await _context.Instalacion.FindAsync(id);

            if (instalacion == null)
            {
                return NotFound();
            }

            return instalacion;
            
        }


        // GET: api/ofertas/buscar
        [HttpGet("buscar")]
        public dynamic Buscar(int instala)
        {
            return _context.Instalacion
                .Where(item =>
                    item.TelefonoId == instala
                )
                .Select(item => new {
                    item.TelefonoId,
                    item.InstalacionId,
                    item.Exitosa,
                    item.Fecha,
                    item.Operario.OperarioId,
                    item.Operario.Apellido,
                    item.Operario.Nombre,
                    item.App.AppId,
                    aplicacion = item.App.Nombre
                })
                .ToList();

        }

        // PUT: api/Instalaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstalacion(int id, Instalacion instalacion)
        {
            if (id != instalacion.InstalacionId)
            {
                return BadRequest();
            }

            _context.Entry(instalacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstalacionExists(id))
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

        // POST: api/Instalaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Instalacion>> PostInstalacion(Instalacion instalacion)
        {
            _context.Instalacion.Add(instalacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstalacion", new { id = instalacion.InstalacionId }, instalacion);
        }

        // DELETE: api/Instalaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstalacion(int id)
        {
            var instalacion = await _context.Instalacion.FindAsync(id);
            if (instalacion == null)
            {
                return NotFound();
            }

            _context.Instalacion.Remove(instalacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstalacionExists(int id)
        {
            return _context.Instalacion.Any(e => e.InstalacionId == id);
        }
    }
}
