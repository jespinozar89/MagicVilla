using System.Net;
using AutoMapper;
using MagicVillaAPI.Modelos;
using MagicVillaAPI.Modelos.DTO;
using MagicVillaAPI.Repositorio.IRepositorio;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VillaController(ILogger<VillaController> logger, IVillaRepositorio villaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                _logger.LogInformation("Getting all villas");

                IEnumerable<Villa> villas = await _villaRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<VillaDTO>>(villas);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMensajes = new List<string>() { ex.ToString() };
                return _response;
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                //var villa = VillaStore.VillasList.FirstOrDefault(v => v.Id == id);
                var villa = await _villaRepo.Obtener(v => v.Id == id);
                if (villa == null)
                {
                    _logger.LogError($"Villa with id {id} not found");
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<VillaDTO>(villa);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMensajes = new List<string>() { ex.ToString() };
                return _response;
            }
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateVilla(VillaCreateDTO createVilla)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _villaRepo.Obtener(v => v.Nombre.ToLower() == createVilla.Nombre.ToLower()) != null)
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
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _villaRepo.Crear(modelo);

                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtAction(nameof(GetVilla), new { id = modelo.Id }, _response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMensajes = new List<string>() { ex.ToString() };
                return _response;
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {

                var villa = await _villaRepo.Obtener(v => v.Id == id);
                if (villa == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _villaRepo.Remover(villa);

                _response.statusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMensajes = new List<string>() { ex.ToString() };
                return _response;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, VillaUpdateDTO updateVilla)
        {
            try
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
                var existingVilla = await _villaRepo.Obtener(v => v.Id == id, tracked: false);
                if (existingVilla == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                existingVilla = _mapper.Map<Villa>(updateVilla);
                existingVilla.FechaActualizacion = DateTime.Now;

                await _villaRepo.Actualizar(existingVilla);
                _response.statusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMensajes = new List<string>() { ex.ToString() };
                return _response;
            }

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<APIResponse>> UpdateVillaPartial(int id, JsonPatchDocument<VillaUpdateDTO> patch)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (patch == null)
                {
                    return BadRequest();
                }

                var existingVilla = await _villaRepo.Obtener(v => v.Id == id, tracked: false);
                if (existingVilla == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(existingVilla);

                patch.ApplyTo(villaDTO, ModelState);

                var modelo = _mapper.Map<Villa>(villaDTO);
                modelo.FechaActualizacion = DateTime.Now;


                await _villaRepo.Actualizar(modelo);
                _response.statusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMensajes = new List<string>() { ex.ToString() };
                return _response;
            }
        }
    }
}