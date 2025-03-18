using AutoMapper;
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
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            _logger.LogInformation("Getting all villas");

            IEnumerable<Villa> villas = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VillaDTO>>(villas));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            //var villa = VillaStore.VillasList.FirstOrDefault(v => v.Id == id);
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
            {
                _logger.LogError($"Villa with id {id} not found");
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        public async Task<ActionResult<VillaDTO>> CreateVilla(VillaCreateDTO createVilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _db.Villas.FirstOrDefaultAsync(v => v.Nombre.ToLower() == createVilla.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("Nombre", "Ya existe una villa con ese nombre");
                return BadRequest(ModelState);
            }
            if (createVilla == null)
            {
                return BadRequest(createVilla);
            }

            // villa.Id = VillaStore.VillasList.Max(v => v.Id) + 1;
            // VillaStore.VillasList.Add(villa);
            Villa modelo = _mapper.Map<Villa>(createVilla);

            await _db.Villas.AddAsync(modelo);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVilla), new { id = modelo.Id }, modelo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVilla(int id)
        {
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async  Task<ActionResult<VillaDTO>> UpdateVilla(int id, VillaUpdateDTO updateVilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (updateVilla == null)
            {
                return BadRequest(updateVilla);
            }
            if (updateVilla.Id != id)
            {
                return BadRequest("El id de la villa no coincide con el id de la URL");
            }
            var existingVilla = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
            if (existingVilla == null)
            {
                return NotFound();
            }

            existingVilla = _mapper.Map<Villa>(updateVilla);
            //existingVilla.FechaActualizacion = System.DateTime.Now;

            _db.Villas.Update(existingVilla);
            await _db.SaveChangesAsync();

            return Ok(existingVilla);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<VillaDTO>> UpdateVillaPartial(int id, JsonPatchDocument<VillaUpdateDTO> patch)
        {
            if (patch == null)
            {
                return BadRequest();
            }
            var existingVilla = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
            if (existingVilla == null)
            {
                return NotFound();
            }

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(existingVilla);

            patch.ApplyTo(villaDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelo = _mapper.Map<Villa>(villaDTO);
            //modelo.FechaActualizacion = System.DateTime.Now;


            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();

            return Ok(modelo);
        }
    }
}