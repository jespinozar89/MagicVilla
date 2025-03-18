using MagicVillaAPI.Datos;
using MagicVillaAPI.Modelos;
using MagicVillaAPI.Modelos.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [EnableCors("AllowAll")]
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Getting all villas");
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            //var villa = VillaStore.VillasList.FirstOrDefault(v => v.Id == id);
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                _logger.LogError($"Villa with id {id} not found");
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDTO> CreateVilla(VillaDTO villa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_db.Villas.FirstOrDefault(v => v.Nombre.ToLower() == villa.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("Nombre", "Ya existe una villa con ese nombre");
                return BadRequest(ModelState);
            }
            if (villa == null)
            {
                return BadRequest(villa);
            }
            if (villa.Id != 0)
            {
                return BadRequest("El id de la villa no debe ser especificado");
            }

            // villa.Id = VillaStore.VillasList.Max(v => v.Id) + 1;
            // VillaStore.VillasList.Add(villa);
            Villa modelo = new()
            {
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                Tarifa = villa.Tarifa,
                Ocupantes = villa.Ocupantes,
                MetrosCuadrados = villa.MetrosCuadrados,
                ImagenUrl = villa.ImagenUrl,
                Amenidad = villa.Amenidad,
                FechaCreacion = System.DateTime.Now,
                FechaActualizacion = System.DateTime.Now
            };

            _db.Villas.Add(modelo);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetVilla), new { id = villa.Id }, villa);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteVilla(int id)
        {
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<VillaDTO> UpdateVilla(int id, VillaDTO villa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (villa == null)
            {
                return BadRequest(villa);
            }
            if (villa.Id != id)
            {
                return BadRequest("El id de la villa no coincide con el id de la URL");
            }
            var existingVilla = _db.Villas.FirstOrDefault(v => v.Id == id);
            if (existingVilla == null)
            {
                return NotFound();
            }

            // existingVilla.Nombre = villa.Nombre;
            // existingVilla.Ocupantes = villa.Ocupantes;
            // existingVilla.MetrosCuadrados = villa.MetrosCuadrados;

            existingVilla.Nombre = villa.Nombre;
            existingVilla.Detalle = villa.Detalle;
            existingVilla.Tarifa = villa.Tarifa;
            existingVilla.Ocupantes = villa.Ocupantes;
            existingVilla.MetrosCuadrados = villa.MetrosCuadrados;
            existingVilla.ImagenUrl = villa.ImagenUrl;
            existingVilla.Amenidad = villa.Amenidad;
            existingVilla.FechaActualizacion = System.DateTime.Now;

            _db.Villas.Update(existingVilla);
            _db.SaveChanges();

            return Ok(existingVilla);
        }

        [HttpPatch("{id}")]
        public ActionResult<VillaDTO> UpdateVillaPartial(int id, JsonPatchDocument<VillaDTO> patch)
        {
            if (patch == null)
            {
                return BadRequest();
            }
            var existingVilla = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);
            if (existingVilla == null)
            {
                return NotFound();
            }

            VillaDTO villa = new()
            {
                Id = existingVilla.Id,
                Nombre = existingVilla.Nombre,
                Detalle = existingVilla.Detalle,
                Tarifa = existingVilla.Tarifa,
                Ocupantes = existingVilla.Ocupantes,
                MetrosCuadrados = existingVilla.MetrosCuadrados,
                ImagenUrl = existingVilla.ImagenUrl,
                Amenidad = existingVilla.Amenidad
            };

            patch.ApplyTo(villa, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = new()
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                Tarifa = villa.Tarifa,
                Ocupantes = villa.Ocupantes,
                MetrosCuadrados = villa.MetrosCuadrados,
                ImagenUrl = villa.ImagenUrl,
                Amenidad = villa.Amenidad,
                FechaCreacion = existingVilla.FechaCreacion,
                FechaActualizacion = System.DateTime.Now
            };

            _db.Villas.Update(modelo);
            _db.SaveChanges();

            return Ok(modelo);
        }
    }
}