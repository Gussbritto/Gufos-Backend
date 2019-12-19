using System.Collections.Generic;
using System.Threading.Tasks;
using BACKEND.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Para adicionar a árvore de objetos adicionais uma nova biblioteca Json
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

namespace BACKEND.Controllers {
    // Definimos nossa rota do controller e dizemos que é um controller de API 
    [Route ("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase {
        GufosContext _contexto = new GufosContext ();

        //GET : api/TipoUsuario
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get () {
            var tipousuarios = await _contexto.TipoUsuario.Include("Categoria").Include("Localizacao").ToListAsync ();

            if (tipousuarios == null) {
                return NotFound ();
            }

            return tipousuarios;
        }

        //GET : api/TipoUsuario/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get (int id) {
            // FindAsync = procura algo especifico no banco 
            var tipousuario = await _contexto.TipoUsuario.FindAsync (id);

            if (tipousuario == null) {
                return NotFound ();
            }

            return tipousuario;
        }

        // POST api/TipoUsuario 
        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> Post (TipoUsuario tipousuario) {

            try {
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync (tipousuario);
                // Salvamos efetivamente o nosso objeto no banco 
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }

            return tipousuario;

        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, TipoUsuario tipousuario) {

            // Se o Id fo objeto não existir ele reteorna error 400
            if (id != tipousuario.TipoUsuarioId) {
                return BadRequest ();
            }
            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry (tipousuario).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no Banco
                var tipousuario_valido = await
                _contexto.TipoUsuario.FindAsync (id);

                if (tipousuario_valido == null) {
                    return NotFound ();
                } else {
                    throw; 
                }

            }
            // NoContent = Retorna 204, sem nada
            return NoContent ();

        }

        // DELETE api/tipousuario/id

        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoUsuario>> Delete (int id){
            var tipousuario = await _contexto.TipoUsuario.FindAsync(id);
            if(tipousuario == null){
                return NotFound();
            }

            _contexto.TipoUsuario.Remove(tipousuario);
            await _contexto.SaveChangesAsync();

            return tipousuario; 

        }

    }
}