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
    public class PresencaController : ControllerBase {
        GufosContext _contexto = new GufosContext ();

        //GET : api/Presenca
        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get () {
            var presencas = await _contexto.Presenca.Include("Categoria").Include("Localizacao").ToListAsync();

            if (presencas == null) {
                return NotFound ();
            }

            return presencas;
        }

        //GET : api/Presenca/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Presenca>> Get (int id) {
            // FindAsync = procura algo especifico no banco 
            var presenca = await _contexto.Presenca.FindAsync (id);

            if (presenca == null) {
                return NotFound ();
            }

            return presenca;
        }

        // POST api/Presenca 
        [HttpPost]
        public async Task<ActionResult<Presenca>> Post (Presenca presenca) {

            try {
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync (presenca);
                // Salvamos efetivamente o nosso objeto no banco 
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }

            return presenca;

        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Presenca presenca) {

            // Se o Id fo objeto não existir ele reteorna error 400
            if (id != presenca.PresencaId) {
                return BadRequest ();
            }
            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry (presenca).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no Banco
                var presenca_valido = await
                _contexto.Presenca.FindAsync (id);

                if (presenca_valido == null) {
                    return NotFound ();
                } else {
                    throw; 
                }

            }
            // NoContent = Retorna 204, sem nada
            return NoContent ();

        }

        // DELETE api/presenca/id

        [HttpDelete("{id}")]
        public async Task<ActionResult<Presenca>> Delete (int id){
            var presenca = await _contexto.Presenca.FindAsync(id);
            if(presenca == null){
                return NotFound();
            }

            _contexto.Presenca.Remove(presenca);
            await _contexto.SaveChangesAsync();

            return presenca; 

        }

    }
}