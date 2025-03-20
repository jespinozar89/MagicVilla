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
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        private readonly INumeroVillaRepositorio _numeroVillaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepo,INumeroVillaRepositorio numeroVillaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numeroVillaRepo = numeroVillaRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Obtener Numeros Villas");

                IEnumerable<NumeroVilla> numeroVillas = await _numeroVillaRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDTO>>(numeroVillas);
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
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                var numeroVilla = await _numeroVillaRepo.Obtener(v => v.VillaNO == id);
                if (numeroVilla == null)
                {
                    _logger.LogError($"Number Villa with id {id} not found");
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<NumeroVillaDTO>(numeroVilla);
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
        public async Task<ActionResult<APIResponse>> CreateNumeroVilla(NumeroVillaCreateDTO createNumeroVilla)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _numeroVillaRepo.Obtener(v => v.VillaNO == createNumeroVilla.VillaNO) != null)
                {
                    ModelState.AddModelError("Numero Villa", "Ya existe el numero villa");
                    return BadRequest(ModelState);
                }
                if(await _villaRepo.Obtener(v => v.Id == createNumeroVilla.VillaId) == null)
                {
                    ModelState.AddModelError("Id Villa", "El Id Villa no existe");
                    return BadRequest(ModelState);
                }
                if (createNumeroVilla == null)
                {
                    return BadRequest(createNumeroVilla);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createNumeroVilla);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _numeroVillaRepo.Crear(modelo);

                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtAction(nameof(GetNumeroVilla), new { id = modelo.VillaNO }, _response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMensajes = new List<string>() { ex.ToString() };
                return _response;
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> DeleteNumeroVilla(int id)
        {
            try
            {

                var numeroVilla = await _numeroVillaRepo.Obtener(v => v.VillaNO == id);
                if (numeroVilla == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _numeroVillaRepo.Remover(numeroVilla);

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
        public async Task<ActionResult<APIResponse>> UpdateNumeroVilla(int id, NumeroVillaUpdateDTO updateNumeroVilla)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (updateNumeroVilla == null)
                {
                    return BadRequest(updateNumeroVilla);
                }
                if (updateNumeroVilla.VillaNO != id)
                {
                    return BadRequest("El numero de la villa no coincide con el numero de la URL");
                }
                var existingNumeroVilla = await _numeroVillaRepo.Obtener(v => v.VillaNO == id, tracked: false);
                if (existingNumeroVilla == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                 if(await _villaRepo.Obtener(v => v.Id == updateNumeroVilla.VillaId) == null)
                {
                    ModelState.AddModelError("Id Villa", "El Id Villa no existe");
                    return BadRequest(ModelState);
                }

                existingNumeroVilla = _mapper.Map<NumeroVilla>(updateNumeroVilla);
                existingNumeroVilla.FechaActualizacion = DateTime.Now;

                await _numeroVillaRepo.Actualizar(existingNumeroVilla);
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